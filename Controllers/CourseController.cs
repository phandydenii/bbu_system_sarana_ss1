using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("course")]
public class CourseController(ICampusDbContext campusDbContext,IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpGet("get-course-list")]
    public IActionResult GetCourseList()
    {
        var db = campusDbContext.DbContext(_campus);
        var courses = db.TblCourses.AsQueryable();

        return Json(new
        {
            data = courses.ToList()
        });
    }

    [HttpPost("get-courses")]
    public IActionResult GetCourses()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        var db = campusDbContext.DbContext(_campus);
        var courses = db.TblCourses.AsQueryable();
        if (!string.IsNullOrEmpty(searchValue)) courses = courses.Where(c => c.CourseFullName!.Contains(searchValue));

        var recordsTotal = courses.Count();
        var data = courses.Skip(skip).Take(pageSize).ToList();

        return Json(new
        {
            draw,
            recordsFiltered = recordsTotal,
            recordsTotal,
            data
        });
    }

    [HttpPost("get-course-terms/{termId:int}/{stageId:int}/{promotionId:int}/{fieldId:int}")]
    public IActionResult GetCourseTerms(int termId, int stageId, int promotionId, int fieldId)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        var db = campusDbContext.DbContext(_campus);
        var query =
            (from term in db.TblTerm
                join stage in db.TblStage
                    on term.StageId equals stage.StageId
                join promotion in db.TblPromotion
                    on stage.PromotionId equals promotion.PromotionId
                join courseTerm in db.TblCourseTerms
                    on term.TermId equals courseTerm.TermId
                join course in db.TblCourses
                    on courseTerm.CourseId equals course.CourseId
                join courseCode in db.TblCourseCodes
                    on new { course.CourseId, promotion.SchoolId, courseTerm.FieldId, promotion.DegreeId }
                    equals new { courseCode.CourseId, courseCode.SchoolId, courseCode.FieldId, courseCode.DegreeId }
                    into courseCodeGroup
                from courseCode in courseCodeGroup.DefaultIfEmpty()
                where term.TermId == termId && stage.StageId == stageId && promotion.PromotionId == promotionId &&
                      courseTerm.FieldId == fieldId
                select new
                {
                    courseTerm.CourseTermId,
                    course.CourseId,
                    course.CourseFullName,
                    course.CourseShortName,
                    stage.StageId,
                    term.TermId,
                    promotion.PromotionId,
                    courseTerm.Credit,
                    courseTerm.FieldId,
                    term.TermNo,
                    code = courseCode.Code,
                    promotion.SchoolId,
                    courseTerm.Type,
                    courseTerm.Hours
                }).Distinct().AsQueryable();
        if (!string.IsNullOrEmpty(searchValue)) query = query.Where(c => c.CourseFullName!.Contains(searchValue));
        var recordsTotal = query.Count();
        var data = query.Skip(skip).Take(pageSize).ToList();

        return Json(new
        {
            draw,
            recordsFiltered = recordsTotal,
            recordsTotal,
            data
        });
    }

    [HttpPost("save-changes")]
    public async Task<IActionResult> SaveCourses(CourseDto courseDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return new ServerResponse().BadRequest(errors);
            }
            var db = campusDbContext.DbContext(_campus);
            var checkCourse = await db.TblCourses.FindAsync(courseDto.CourseId);
            if (checkCourse != null)
            {
                mapper.Map(courseDto, checkCourse);
                db.TblCourses.Update(checkCourse);
                await db.SaveChangesAsync();
                return new ServerResponse().Success("Update course success");
            }
            
            var newData = mapper.Map<CourseDto, Course>(courseDto);
            await db.TblCourses.AddAsync(newData);
            await db.SaveChangesAsync();
            return new ServerResponse().Success("Add Course success");
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }
}