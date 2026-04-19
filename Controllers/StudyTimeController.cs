using AutoMapper;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("study-time")]
public class StudyTimeController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpGet("get-study-time")]
    public IActionResult GetStudyTime()
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query = db.TblStudyTime.AsQueryable();
            return Ok(new
            {
                data = query.ToList(),
                code = "200",
                message = "Succeeded!"
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                code = "500",
                message = $"Internal Server Error:{e.Message}"
            });
        }
    }
}