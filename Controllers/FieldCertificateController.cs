using AutoMapper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("field-certificate")]
public class FieldCertificateController(ICampusDbContext campusDbContext,IMapper mapper, IHttpContextAccessor context) : Controller
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
    [HttpPost("save-change")]
public async Task<IActionResult> SaveChange([FromForm] FieldCertificateDto? fieldCertificate)
{
    try
    {
        if (fieldCertificate == null)
        {
            return new ServerResponse().BadRequest("Bad Request!");
        }

        fieldCertificate.DegreeName = fieldCertificate.DegreeName?.Trim();
        fieldCertificate.DegreeNameKhmer = fieldCertificate.DegreeNameKhmer?.Trim();
        fieldCertificate.FieldName = fieldCertificate.FieldName?.Trim();
        fieldCertificate.FieldNameKhmer = fieldCertificate.FieldNameKhmer?.Trim();
        fieldCertificate.Type = fieldCertificate.Type?.Trim();
        fieldCertificate.TypeKhmer = fieldCertificate.TypeKhmer?.Trim();

        if (string.IsNullOrWhiteSpace(fieldCertificate.DegreeName))
        {
            return new ServerResponse().BadRequest("Degree name is required.");
        }

        if (string.IsNullOrWhiteSpace(fieldCertificate.DegreeNameKhmer))
        {
            return new ServerResponse().BadRequest("Degree name Khmer is required.");
        }

        var db = campusDbContext.DbContext(_campus);

        var data = db.TblFieldCertificate
            .FirstOrDefault(x => x.Id == fieldCertificate.Id);

        if (data != null)
        {
            mapper.Map(fieldCertificate, data);
            db.TblFieldCertificate.Update(data);
            await db.SaveChangesAsync();

            return new ServerResponse().Success(fieldCertificate, "Update success");
        }

        var isExist = db.TblFieldCertificate.Any(x =>
            x.DegreeName == fieldCertificate.DegreeName &&
            x.PromotionNo == fieldCertificate.PromotionNo);

        if (isExist)
        {
            return new ServerResponse().BadRequest("Field certificate already exists!");
        }

        await db.TblFieldCertificate.AddAsync(
            mapper.Map<FieldCertificateDto, FieldCertificate>(fieldCertificate)
        );

        await db.SaveChangesAsync();

        return new ServerResponse().Success(fieldCertificate, "Save success");
    }
    catch (Exception ex)
    {
        return new ServerResponse().ErrorInternal(ex);
    }
}
    
}