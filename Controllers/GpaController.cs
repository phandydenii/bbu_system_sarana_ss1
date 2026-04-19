using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
public class GpaController(ICampusDbContext campusDbContext, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost]
    public IActionResult GetGpAs()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);

        var query = db.TblDoctoralContract.AsQueryable();
        if (!string.IsNullOrEmpty(searchValue)) query = query.Where(x => x.StudentId == searchValue);
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
}