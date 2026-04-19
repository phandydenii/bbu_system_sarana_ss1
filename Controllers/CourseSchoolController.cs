using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
}