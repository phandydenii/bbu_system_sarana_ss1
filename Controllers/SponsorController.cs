using AutoMapper;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("sponsor")]
public class SponsorController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost]
    [Route("get-sponsor")]
    public IActionResult GetSponsor(bool isAll = false)
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
            var query = db.TblSponsor.AsQueryable();

            if (isAll)
            {
                return Ok(new
                {
                    data = query.ToList(),
                    status = new
                    {
                        code = "200",
                        message = "Successfully"
                    }
                });
            }

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.SponsorId!.ToString()!.Contains(searchValue) ||
                    d.SponsorName!.Contains(searchValue)
                );
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
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                data = new { },
                status = new
                {
                    code = "200",
                    message = e.Message
                }
            });
        }
    }
}