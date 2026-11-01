using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Modelsss;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("group")]
public class GroupController(
    ICampusDbContext campusDbContext,
    IMapper mapper,
    IHttpContextAccessor context) : Controller
{
    private readonly string _campus =
        context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-groups-filter")]
    public async Task<IActionResult> Filter(
        [FromForm] int promotionId = 0,
        [FromForm] int stageNo = 0,
        [FromForm] string studyTime = "",
        [FromForm] bool allStage = false,
        [FromForm] bool allTime = false)
    {
        try
        {
            if (promotionId <= 0)
            {
                return new ServerResponse().BadRequest(
                    "PromotionId was not submitted.");
            }

            if (!allStage && stageNo <= 0)
            {
                return new ServerResponse().BadRequest(
                    "StageNo was not submitted.");
            }

            var normalizedStudyTime = (studyTime ?? string.Empty).Trim();

            if (!allTime && string.IsNullOrWhiteSpace(normalizedStudyTime))
            {
                return new ServerResponse().BadRequest(
                    "StudyTime was not submitted.");
            }

            var db = campusDbContext.DbContext(_campus);

            var academicYear = await db.TblPromotion
                .AsNoTracking()
                .Where(x => x.PromotionId == promotionId)
                .Select(x => (int?)x.AcademicYearStart)
                .FirstOrDefaultAsync();

            if (!academicYear.HasValue)
            {
                return new ServerResponse().BadRequest(
                    $"PromotionId {promotionId} was not found.");
            }

            var query =
                from groupRow in db.TblGroup.AsNoTracking()
                join stage in db.TblStage.AsNoTracking()
                    on groupRow.StageId equals stage.StageId
                join promotion in db.TblPromotion.AsNoTracking()
                    on stage.PromotionId equals promotion.PromotionId
                join school in db.TblSchool.AsNoTracking()
                    on promotion.SchoolId equals school.SchoolId
                where promotion.AcademicYearStart == academicYear.Value
                select new
                {
                    Group = groupRow,
                    Stage = stage,
                    School = school
                };

            // When grouping all stages or all study times, the destination
            // groups must belong to a foundation school, matching the old logic.
            if (allStage || allTime)
            {
                query = query.Where(x => x.School.IsFoundationSchool == 1);
            }

            if (!allStage)
            {
                query = query.Where(x => x.Stage.StageNo == stageNo);
            }

            if (!allTime)
            {
                query = query.Where(x =>
                    x.Group.StudyTime != null &&
                    x.Group.StudyTime.Trim() == normalizedStudyTime);
            }

            var groups = await query
                .Select(x => x.Group)
                .Distinct()
                .OrderBy(x => x.GroupName)
                .ToListAsync();

            var message = groups.Count > 0
                ? "Success"
                : $"No groups found for PromotionId={promotionId}, " +
                  $"AcademicYear={academicYear.Value}, StageNo={stageNo}, " +
                  $"StudyTime={normalizedStudyTime}.";

            return new ServerResponse().Success(groups, message);
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpPost("get-groups")]
    public async Task<IActionResult> GetGroups(
        [FromForm] int stageId = 0,
        [FromForm] int fieldId = 0,
        [FromForm] bool isAll = false)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query = db.TblGroup.AsNoTracking().AsQueryable();

            if (stageId > 0)
            {
                query = query.Where(x => x.StageId == stageId);
            }

            if (fieldId > 0)
            {
                query = query.Where(x => x.FieldId == fieldId);
            }

            if (isAll)
            {
                var allGroups = await query
                    .OrderByDescending(x => x.GroupId)
                    .ToListAsync();

                return new ServerResponse().Success(allGroups);
            }

            var draw = Request.Form["draw"].FirstOrDefault();
            var startValue = Request.Form["start"].FirstOrDefault();
            var lengthValue = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"]
                .FirstOrDefault()?.Trim();

            var skip = int.TryParse(startValue, out var start) ? start : 0;
            var pageSize = int.TryParse(lengthValue, out var length) && length > 0
                ? length
                : 10;

            var recordsTotal = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(x =>
                    (x.GroupName != null && x.GroupName.Contains(searchValue)) ||
                    x.GroupId.ToString().Contains(searchValue));
            }

            var recordsFiltered = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.GroupId)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return Json(new
            {
                draw,
                recordsTotal,
                recordsFiltered,
                data
            });
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpPost("get-group-rooms")]
    public async Task<IActionResult> GetGroupRoom(
        [FromForm] int groupId = 0,
        [FromForm] int termNo = 0,
        [FromForm] bool isAll = false)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query = db.TblGroupRoom.AsNoTracking().AsQueryable();

            if (groupId > 0)
            {
                query = query.Where(x => x.GroupId == groupId);
            }

            if (termNo > 0)
            {
                query = query.Where(x => x.TermNo == termNo);
            }

            if (isAll)
            {
                var groupRoom = await query.FirstOrDefaultAsync();
                return new ServerResponse().Success(groupRoom);
            }

            var draw = Request.Form["draw"].FirstOrDefault();
            var startValue = Request.Form["start"].FirstOrDefault();
            var lengthValue = Request.Form["length"].FirstOrDefault();

            var skip = int.TryParse(startValue, out var start) ? start : 0;
            var pageSize = int.TryParse(lengthValue, out var length) && length > 0
                ? length
                : 10;

            var recordsTotal = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.GroupId)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return Json(new
            {
                draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            });
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpPost("getgroups/promotion")]
    public async Task<IActionResult> GetPromotions(
        [FromForm] int promotionId)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var startValue = Request.Form["start"].FirstOrDefault();
            var lengthValue = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"]
                .FirstOrDefault()?.Trim();

            var skip = int.TryParse(startValue, out var start) ? start : 0;
            var pageSize = int.TryParse(lengthValue, out var length) && length > 0
                ? length
                : 5;

            var db = campusDbContext.DbContext(_campus);

            var stageIds = db.TblStage
                .AsNoTracking()
                .Where(x => x.PromotionId == promotionId)
                .Select(x => x.StageId);

            var query =
                from groupRow in db.TblGroup.AsNoTracking()
                join field in db.TblField.AsNoTracking()
                    on groupRow.FieldId equals field.FieldId
                join stage in db.TblStage.AsNoTracking()
                    on groupRow.StageId equals stage.StageId
                where stageIds.Contains(groupRow.StageId)
                select new
                {
                    groupRow.GroupId,
                    groupRow.GroupName,
                    groupRow.StageId,
                    stage.StageNo,
                    field.FieldId,
                    field.FieldName
                };

            var recordsTotal = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(x =>
                    (x.GroupName != null && x.GroupName.Contains(searchValue)) ||
                    x.GroupId.ToString().Contains(searchValue));
            }

            var recordsFiltered = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.GroupId)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return Json(new
            {
                draw,
                recordsTotal,
                recordsFiltered,
                data
            });
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpGet("get-study-time")]
    public async Task<IActionResult> GetStudyTime()
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var data = await db.TblStudyTime
                .AsNoTracking()
                .ToListAsync();

            return new ServerResponse().Success(data);
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpPost("save-change")]
    public async Task<IActionResult> SaveChange(
        [FromForm] GroupDto group,
        [FromForm] GroupRoomDto groupRoom)
    {
        if (group == null || groupRoom == null)
        {
            return new ServerResponse().BadRequest(
                "Group and group-room data are required.");
        }

        var db = campusDbContext.DbContext(_campus);
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            Models.Group groupEntity;

            if (group.GroupId > 0)
            {
                var existingGroup = await db.TblGroup
                    .FirstOrDefaultAsync(x => x.GroupId == group.GroupId);

                if (existingGroup == null)
                {
                    await transaction.RollbackAsync();
                    return new ServerResponse().NotFound("Group not found.");
                }

                mapper.Map(group, existingGroup);
                groupEntity = existingGroup;
            }
            else
            {
                groupEntity = mapper.Map<GroupDto, Models.Group>(group);
                groupEntity.GroupId = 0;
                await db.TblGroup.AddAsync(groupEntity);
            }

            await db.SaveChangesAsync();

            groupRoom.GroupId = groupEntity.GroupId;
            Models.GroupRoom groupRoomEntity;

            if (groupRoom.GroupRoomId > 0)
            {
                var existingGroupRoom = await db.TblGroupRoom
                    .FirstOrDefaultAsync(x =>
                        x.GroupRoomId == groupRoom.GroupRoomId);

                if (existingGroupRoom == null)
                {
                    await transaction.RollbackAsync();
                    return new ServerResponse().NotFound(
                        "Group room not found.");
                }

                mapper.Map(groupRoom, existingGroupRoom);
                existingGroupRoom.GroupId = groupEntity.GroupId;
                groupRoomEntity = existingGroupRoom;
            }
            else
            {
                groupRoomEntity = mapper.Map<GroupRoomDto, Models.GroupRoom>(
                    groupRoom);
                groupRoomEntity.GroupRoomId = 0;
                groupRoomEntity.GroupId = groupEntity.GroupId;
                await db.TblGroupRoom.AddAsync(groupRoomEntity);
            }

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return new ServerResponse().Success(new
            {
                group = groupEntity,
                groupRoom = groupRoomEntity
            });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            var realError = ex.InnerException?.Message ?? ex.Message;
            await Helper.Telegram.SendDebugToMyTelegramDirect(
                $"Group save error:\n{realError}");

            return new ServerResponse().ErrorInternal(ex);
        }
    }
}
