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
public class FieldCertificateController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";
    
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
    
    [HttpPost("save-change")]
    public async Task<IActionResult> SaveChange([FromForm] FieldCertificateDto? fieldCertificate)
    {
        try
        {
            if (fieldCertificate == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }

            var db = campusDbContext.DbContext(_campus);
            
            var school = db.TblSchool
                .FirstOrDefault(x => x.SchoolId == fieldCertificate.SchoolId);

            if (school == null)
            {
                return new ServerResponse().BadRequest("School not found.");
            }
            
            fieldCertificate.SchoolName = school.SchoolName;
            fieldCertificate.SchoolNameKhmer = school.SchoolNameInKhmer;
            
            fieldCertificate.Status = true;
            
            fieldCertificate.DegreeName = fieldCertificate.DegreeName?.Trim();
            fieldCertificate.DegreeNameKhmer = fieldCertificate.DegreeNameKhmer?.Trim();
            fieldCertificate.SchoolName = fieldCertificate.SchoolName?.Trim();
            fieldCertificate.SchoolNameKhmer = fieldCertificate.SchoolNameKhmer?.Trim();
            fieldCertificate.FieldName = fieldCertificate.FieldName?.Trim();
            fieldCertificate.FieldNameKhmer = fieldCertificate.FieldNameKhmer?.Trim();
            fieldCertificate.Type = fieldCertificate.Type?.Trim();
            fieldCertificate.TypeKhmer = fieldCertificate.TypeKhmer?.Trim();
            
            var isExist = db.TblFieldCertificate.Any(x =>
                x.Id != fieldCertificate.Id &&
                x.DegreeId == fieldCertificate.DegreeId &&
                x.SchoolId == fieldCertificate.SchoolId &&
                x.FieldId == fieldCertificate.FieldId &&
                x.PromotionNo == fieldCertificate.PromotionNo);

            if (isExist)
            {
                return new ServerResponse().BadRequest("Field certificate already exists!");
            }

            var data = db.TblFieldCertificate
                .FirstOrDefault(x => x.Id == fieldCertificate.Id);

            if (data != null)
            {
                data.DegreeId = fieldCertificate.DegreeId;
                data.DegreeName = fieldCertificate.DegreeName;
                data.DegreeNameKhmer = fieldCertificate.DegreeNameKhmer;

                data.SchoolId = fieldCertificate.SchoolId;
                data.SchoolName = fieldCertificate.SchoolName;
                data.SchoolNameKhmer = fieldCertificate.SchoolNameKhmer;

                data.FieldId = fieldCertificate.FieldId;
                data.FieldName = fieldCertificate.FieldName;
                data.FieldNameKhmer = fieldCertificate.FieldNameKhmer;

                data.PromotionNo = fieldCertificate.PromotionNo;
                
                data.Status = true;

                data.Type = fieldCertificate.Type;
                data.TypeKhmer = fieldCertificate.TypeKhmer;

                await db.SaveChangesAsync();

                return new ServerResponse().Success(data, "Updated successfully!");
            }
            
            var newData = new FieldCertificate
            {
                DegreeId = fieldCertificate.DegreeId,
                DegreeName = fieldCertificate.DegreeName,
                DegreeNameKhmer = fieldCertificate.DegreeNameKhmer,

                SchoolId = fieldCertificate.SchoolId,
                SchoolName = fieldCertificate.SchoolName,
                SchoolNameKhmer = fieldCertificate.SchoolNameKhmer,

                FieldId = fieldCertificate.FieldId,
                FieldName = fieldCertificate.FieldName,
                FieldNameKhmer = fieldCertificate.FieldNameKhmer,

                PromotionNo = fieldCertificate.PromotionNo,
                
                Status = true,

                Type = fieldCertificate.Type,
                TypeKhmer = fieldCertificate.TypeKhmer
            };

            await db.TblFieldCertificate.AddAsync(newData);
            await db.SaveChangesAsync();

            return new ServerResponse().Success(newData, "Saved successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
}