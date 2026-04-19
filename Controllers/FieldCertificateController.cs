using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("field-certificate")]
public class FieldCertificateController(ICampusDbContext campusDbContext, IHttpContextAccessor context) : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("get-field-certificates")]
    public IActionResult GetFieldCertificates()
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
            var query = db.TblFieldCertificate.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.FieldName!.Contains(searchValue) ||
                    d.FieldNameKhmer!.Contains(searchValue));

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.FieldId);
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
            throw new Exception($"Error: {e.Message}");
        }
    }

    [HttpPost("Create")]
    public IActionResult Create(FieldCertificateDto fieldCertificate)
    {
        Console.WriteLine(fieldCertificate);
        return Json(new
        {
            data = fieldCertificate
        });
    }
}