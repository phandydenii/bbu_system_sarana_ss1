using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models;
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
    public async Task<IActionResult> CreteGroup(GroupDto group, GroupRoomDto grouproom)
    {
        var db = campusDbContext.DbContext(_campus);
        if (group == null || grouproom == null) throw new Exception("Bad Request");
        var tran = await db.Database.BeginTransactionAsync();
        try
        {
            var data = mapper.Map<GroupDto, Group>(group);
            await db.TblGroup.AddAsync(data);
            await db.SaveChangesAsync();

            grouproom.GroupId = data.GroupId;
            var dataGroup = mapper.Map<GroupRoomDto, GroupRoom>(grouproom);
            await db.TblGroupRoom.AddAsync(dataGroup);
            await db.SaveChangesAsync();
            await tran.CommitAsync();
            return new ServerResponse().Success(dataGroup);
        }
        catch (Exception ex)
        {
            await tran.RollbackAsync();
            return new ServerResponse().ErrorInternal(ex);
        }
    }
}