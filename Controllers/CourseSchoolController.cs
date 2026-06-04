using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models.Req;
using BBU_SYSTEM.Modelsss;  
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("course-school")]
public class CourseSchoolController(ICampusDbContext campusDbContext, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-course-school/{schoolId}")]
    public IActionResult GetCourseSchool(int schoolId)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        var db = campusDbContext.DbContext(_campus);
        var qury = (from cs in db.TblCourseSchools
            join s in db.TblSchool on cs.SchoolId equals s.SchoolId
            join c in db.TblCourses on cs.CourseId equals c.CourseId
            where cs.SchoolId == schoolId
            select new
            {
                c.CourseId,
                c.CourseFullName,
                c.CourseFullNameInKhmer
            }).AsQueryable();
        if (!string.IsNullOrEmpty(searchValue)) qury = qury.Where(c => c.CourseFullName!.Contains(searchValue));

        var recordsTotal = qury.Count();
        var data = qury.Skip(skip).Take(pageSize).ToList();

        return Json(new
        {
            draw,
            recordsFiltered = recordsTotal,
            recordsTotal,
            data
        });
    }

    [HttpPost("assign-course-to-school")]
    public async Task<IActionResult> AssignCourseToSchool([FromBody] AssignCourseToSchoolReq req)
    {
        try
        {
            if (req.CourseIds.Count < 0) 
                return new ServerResponse().BadRequest("Atlas one course to be  assigned!");
            var db = campusDbContext.DbContext(_campus);
            var courseIds = req.CourseIds.Where(x => x > 0).Distinct().ToList();
            var existingCourseIds = await db.TblCourseSchools
                .Where(x => x.SchoolId == req.SchoolId)
                .Select(x => x.CourseId)
                .ToListAsync();
            var newCourseIds = courseIds
                .Where(courseId => !existingCourseIds.Contains(courseId))
                .ToList();  
            if (!newCourseIds.Any())
                return new ServerResponse().Success(msg:"All selected courses already assigned to this school!"); 
            foreach (var courseId in newCourseIds)
            {
                db.TblCourseSchools.Add(new Models.CourseSchool()
                {
                    SchoolId = req.SchoolId,
                    CourseId = courseId
                });
            } 
            await db.SaveChangesAsync(); 
            return new ServerResponse().Success(msg:$"{newCourseIds.Count} Course was assigned to School Id {req.SchoolId}");
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }
}