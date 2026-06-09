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
[Route("course-term")]
public class CourseTermController(ICampusDbContext campusDbContext,IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

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
    public async Task<IActionResult> SaveCoursesTerm(CoursetermDto courseTerm)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            if (courseTerm.CoursetermId > 0)
            {
                var oldData = await db.TblCourseTerms.FindAsync(courseTerm.CoursetermId);
                mapper.Map(courseTerm, oldData);
                db.TblCourseTerms.Update(oldData);
                await db.SaveChangesAsync();
                return new ServerResponse().Success("Update course term successfully!");
            }
            
            var newData = mapper.Map<CoursetermDto, CourseTerm>(courseTerm);
            await db.TblCourseTerms.AddAsync(newData);
            await db.SaveChangesAsync();
            return new ServerResponse().Success("Add Course term successfully!");
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }
    
    [HttpDelete("delete/{courseTermId:int}")]
    public async Task<IActionResult> DeleteCourseTerm(int courseTermId)
    {
        var db = campusDbContext.DbContext(_campus); 
        await using var transaction = await db.Database.BeginTransactionAsync(); 
        try
        {
            var courseTerm = await db.TblCourseTerms.FirstOrDefaultAsync(x => x.CourseTermId == courseTermId);

            if (courseTerm == null) return new ServerResponse().NotFound("Course term not found.");
            await db.Database.ExecuteSqlInterpolatedAsync($@"
                DECLARE @course_id INT;
                DECLARE @term_id INT;
                DECLARE @term_no INT;
                DECLARE @stage_id INT;

                SELECT 
                    @course_id = COURSE_ID, 
                    @term_id = TERM_ID
                FROM COURSE_TERM
                WHERE COURSE_TERM_ID = {courseTermId};

                SELECT 
                    @term_no = TERM_NO, 
                    @stage_id = STAGE_ID
                FROM TERM
                WHERE TERM_ID = @term_id;

                DELETE FROM SCORE
                WHERE COURSE_ID = @course_id
                  AND STUDENT_GROUP_ID IN (
                        SELECT STUDENT_GROUP_ID
                        FROM STUDENT_GROUP
                        WHERE GROUP_ID IN (
                            SELECT GROUP_ID
                            FROM [GROUP]
                            WHERE STAGE_ID = @stage_id
                        )
                        AND TERM_NO = @term_no
                  );

                DELETE FROM COURSE_TERM
                WHERE COURSE_TERM_ID = {courseTermId};
            "); 
            await transaction.CommitAsync(); 
            return new ServerResponse().Success("Delete course term successfully!");
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return new ServerResponse().ErrorInternal(e);
        }
    }
}