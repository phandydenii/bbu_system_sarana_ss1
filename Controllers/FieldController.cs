using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("Field")]

public class FieldController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-fields")]
    public IActionResult GetFields(int degreeId = 0, int schoolId = 0, bool isAll = false)
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
            var query = db.TblField.AsQueryable();
            if (degreeId != 0) query = query.Where(x => x.DegreeId == degreeId).AsQueryable();

            if (schoolId != 0) query = query.Where(x => x.SchoolId == schoolId).AsQueryable();

            if (isAll)
            {
                return new ServerResponse().Success(query.ToList(), "Succeeded!");
            }

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.FieldName!.Contains(searchValue) ||
                    d.FieldNameInKhmer!.Contains(searchValue));

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
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    [HttpPost("get-field-page-data")]
    public IActionResult GetFieldPageData()
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            var schools = db.TblSchool
                .OrderBy(x => x.SchoolName)
                .Select(x => new
                {
                    x.SchoolId,
                    x.SchoolName
                })
                .ToList();

            var degrees = db.TblDegree
                .OrderBy(x => x.DegreeId)
                .Select(x => new
                {
                    x.DegreeId,
                    x.DegreeName
                })
                .ToList();

            return new ServerResponse().Success(new
            {
                schools,
                degrees
            }, "Succeeded!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpPost("save-change")]
    public async Task<IActionResult> SaveChange([FromForm] FieldDto? field)
    {
        try
        {
            if (field == null)
                return new ServerResponse().BadRequest();

            var db = campusDbContext.DbContext(_campus);

            var data = db.TblField.FirstOrDefault(x => x.FieldId == field.FieldId);
            
            if (data != null)
            {
                data.FieldName = field.FieldName;
                data.FieldNameInKhmer = field.FieldNameInKhmer;
                data.SchoolId = field.SchoolId;
                data.DegreeId = field.DegreeId;
                data.DegreeName = field.DegreeName;
                data.DegreeNameInKhmer = field.DegreeNameInKhmer;
                data.Type = field.Type;

                await db.SaveChangesAsync();

                return new ServerResponse().Success(field, "Update success");
            }
            else
            {
                var isExist = db.TblField.Any(x => x.FieldName == field.FieldName);

                if (isExist)
                {
                    return new ServerResponse().BadRequest("Field with that name already exists!");
                }

                var newField = new Field
                {
                    FieldName = field.FieldName,
                    FieldNameInKhmer = field.FieldNameInKhmer,
                    SchoolId = field.SchoolId,
                    DegreeId = field.DegreeId,
                    DegreeName = field.DegreeName,
                    DegreeNameInKhmer = field.DegreeNameInKhmer,
                    Type = field.Type
                };

                await db.TblField.AddAsync(newField);
                await db.SaveChangesAsync();

                return new ServerResponse().Success(field, "Save success");
            }
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    
    [HttpPost("field-certificate/save-change")]
    public async Task<IActionResult> FieldCertificateSaveChange([FromForm] FieldCertificateDto? fieldCertificate)
    {
        try
        {
            fieldCertificate!.DegreeName = fieldCertificate.DegreeName!.Trim();
            fieldCertificate.DegreeNameKhmer = fieldCertificate.DegreeNameKhmer!.Trim();
            fieldCertificate.FieldName = fieldCertificate.FieldName!.Trim();
            fieldCertificate.FieldNameKhmer = fieldCertificate.FieldNameKhmer!.Trim();
           
            var db = campusDbContext.DbContext(_campus);
            var data = db.TblFieldCertificate.FirstOrDefault(x => x.Id == fieldCertificate.Id);
            if (data != null)
            {
                mapper.Map(fieldCertificate, data);
                db.TblFieldCertificate.Update(data);
                await db.SaveChangesAsync();
                return new ServerResponse().Success(fieldCertificate,"Update success");
            }
            var isExist = db.TblFieldCertificate.Any(x => x.FieldName == fieldCertificate.FieldName && x.PromotionNo == fieldCertificate.PromotionNo);
            if (isExist)
            {
                return new ServerResponse().BadRequest("Field certificate with that name already exists!");
            }
            await db.TblFieldCertificate.AddAsync(mapper.Map<FieldCertificateDto, FieldCertificate>(fieldCertificate));
            await db.SaveChangesAsync();
            return new ServerResponse().Success(fieldCertificate,"Save success");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
}