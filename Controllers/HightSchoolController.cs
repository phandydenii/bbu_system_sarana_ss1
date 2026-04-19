using AutoMapper;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("high-school")]
public class HightSchoolController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-high-school")]
    public IActionResult GetHighSchool(bool isAll = false)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;

            var db = campusDbContext.DbContext(_campus);
            var query = db.TblHighSchool.AsQueryable();

            if (isAll)
                return Ok(new
                {
                    data = query.ToList(),
                    status = new
                    {
                        code = "200",
                        message = "Succeeded!"
                    }
                });
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.HighSchoolId.ToString().Contains(searchValue) ||
                    d.HighSchoolName!.Contains(searchValue) ||
                    d.HighSchoolNameInKhmer!.Contains(searchValue));

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.HighSchoolId);
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
            return StatusCode(500, new
            {
                code = "500",
                message = $"Internal Server Error:{e.Message}"
            });
        }
    }
}