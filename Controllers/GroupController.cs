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
public class GroupController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";


    [HttpPost("get-groups-filter")]
    public async Task<IActionResult> Filter(int promotionId = 0, int stageNo = 0, string studyTime = "",
        bool allStage = false, bool allTime = false)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            int year = db.TblPromotion.Where(x => x.PromotionId == promotionId).Select(x => x.AcademicYearStart)
                .FirstOrDefault();
            var query = 
                from g in db.TblGroup
                join st in db.TblStage on g.StageId equals st.StageId
                join pr in db.TblPromotion on st.PromotionId equals pr.PromotionId
                join sc in db.TblSchool on pr.SchoolId equals sc.SchoolId
                select new
                {
                    Group = g,
                    Stage = st,
                    Promotion = pr,
                    School = sc
                };


            if (allStage && allTime)
            {
                query = query.Where(x =>
                    x.Promotion.AcademicYearStart == year && x.School.IsFoundationSchool==1);
            }
            else if (allStage)
            {
                query = query.Where(x =>
                    x.Promotion.AcademicYearStart == year &&
                    x.Group.StudyTime == studyTime &&
                    x.School.IsFoundationSchool==1);
            }
            else if (allTime)
            {
                query = query.Where(x =>
                    x.Promotion.AcademicYearStart == year &&
                    x.Stage.StageNo == stageNo &&
                    x.School.IsFoundationSchool==1);
            }
            else
            {
                query = query.Where(x =>
                    x.Promotion.AcademicYearStart == year &&
                    x.Stage.StageNo == stageNo &&
                    x.Group.StudyTime == studyTime);
            }

            var groups = await query.Select(x => x.Group).ToListAsync();
            return new ServerResponse().Success(groups);
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }


    [HttpPost("get-groups")]
    public IActionResult GetPromotionByStage(int stageId = 0, int fieldId = 0, bool isAll = false)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? int.Parse(length) != -1 ? Convert.ToInt32(length) : 10 : 10;
            var skip = start != null ? Convert.ToInt32(start) : 0;

            // var pageSize = length != null ? Convert.ToInt32(length) : 0;
            // var skip = start != null ? Convert.ToInt32(start) : 0;


            var db = campusDbContext.DbContext(_campus);
            var query = db.TblGroup.AsQueryable();

            if (stageId > 0) query = query.Where(x => x.StageId == stageId).AsQueryable();
            if (fieldId > 0) query = query.Where(x => x.FieldId == fieldId).AsQueryable();
            if (isAll)
                return new ServerResponse().Success(query.ToList());

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d => d.GroupName == searchValue || d.GroupId.ToString().Contains(searchValue))
                    .AsQueryable();

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.GroupId);
            var data = query.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            });
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("get-group-rooms")]
    public IActionResult GetGroupRoom(int groupId = 0, int termNo = 0, bool isAll = false)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? int.Parse(length) != -1 ? Convert.ToInt32(length) : 10 : 10;
            var skip = start != null ? Convert.ToInt32(start) : 0;

            // var pageSize = length != null ? Convert.ToInt32(length) : 0;
            // var skip = start != null ? Convert.ToInt32(start) : 0;


            var db = campusDbContext.DbContext(_campus);
            var query = db.TblGroupRoom.AsQueryable();

            if (groupId > 0) query = query.Where(x => x.GroupId == groupId).AsQueryable();
            if (termNo > 0) query = query.Where(x => x.TermNo == termNo).AsQueryable();
            if (isAll)
                return new ServerResponse().Success(query.FirstOrDefault());

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.GroupId);
            var data = query.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            });
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("getgroups/promotion")]
    public IActionResult GetPromotions(int promotionId)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? int.Parse(length) != -1 ? Convert.ToInt32(length) : 5 : 5;
            var skip = start != null ? Convert.ToInt32(start) : 0;

            var db = campusDbContext.DbContext(_campus);

            var stageIds = db.TblStage
                .Where(x => x.PromotionId == promotionId)
                .Select(x => x.StageId);
            var query = (from g in db.TblGroup
                join f in db.TblField on g.FieldId equals f.FieldId
                join st in db.TblStage on g.StageId equals st.StageId
                where stageIds.Contains(g.StageId)
                select new
                {
                    g.GroupId,
                    g.GroupName,
                    g.StageId,
                    st.StageNo,
                    f.FieldId, f.FieldName
                }).AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d => d.GroupId == Convert.ToInt16(searchValue)).AsQueryable();

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.GroupId);
            var data = query.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            });
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpGet("get-study-time")]
    public async Task<IActionResult> GetStudyTime()
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var data = await db.TblStudyTime.ToListAsync();
            return new ServerResponse().Success(data);
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("save-change")]
    public async Task<IActionResult> CreteGroup(GroupDto group, GroupRoomDto groupRoom)
    {
        var db = campusDbContext.DbContext(_campus);
        if (group == null || groupRoom == null) return new ServerResponse().BadRequest("Data is requried to input!");
        var tran = await db.Database.BeginTransactionAsync(); 
        try
        {
            // 1.group
            Models.Group? groupEntity; 
            if (group.GroupId > 0)
            { 
                groupEntity = await db.TblGroup.FirstOrDefaultAsync(x => x.GroupId == group.GroupId); 
                if (groupEntity == null) return new ServerResponse().NotFound("Group not found");
                mapper.Map(group, groupEntity);
                await db.SaveChangesAsync();
            }
            else
            { 
                groupEntity = mapper.Map<GroupDto, Models.Group>(group);  
                groupEntity.GroupId = 0; 
                await db.TblGroup.AddAsync(groupEntity);
                await db.SaveChangesAsync();
            } 
            // 2.group room 
            groupRoom.GroupId = groupEntity.GroupId; 
            Models.GroupRoom? groupRoomEntity; 
            if (groupRoom.GroupRoomId > 0)
            { 
                groupRoomEntity = await db.TblGroupRoom.FirstOrDefaultAsync(x => x.GroupRoomId == groupRoom.GroupRoomId); 
                if (groupRoomEntity == null)  return new ServerResponse().NotFound("Group room not found");
                mapper.Map(groupRoom,groupRoomEntity); 
                await db.SaveChangesAsync();
            }
            else
            { 
                groupRoomEntity = mapper.Map<GroupRoomDto, Models.GroupRoom>(groupRoom); 
                groupRoomEntity.GroupRoomId = 0;
                groupRoomEntity.GroupId = groupEntity.GroupId; 
                await db.TblGroupRoom.AddAsync(groupRoomEntity);
                await db.SaveChangesAsync();
            } 
            await tran.CommitAsync(); 
            return new ServerResponse().Success(new
            {
                group = groupEntity,
                groupRoom = groupRoomEntity
            });
        }
        catch (Exception ex)
        {
            await tran.RollbackAsync();
            var realError = ex.InnerException?.Message ?? ex.Message; 
            await Helper.Telegram.SendDebugToMyTelegramDirect(
                $"Group save error:\n{realError}"
            );
            return new ServerResponse().ErrorInternal(ex);
        }
    }
}