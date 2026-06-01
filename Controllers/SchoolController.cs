using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("school")]
public class SchoolController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-schools")]
    public IActionResult GetSchools(bool isAll = false)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            //var campus = HttpContext.Session.GetString("campus");
            var db = campusDbContext.DbContext(_campus);
            var query = db.TblSchool.AsQueryable();
            if (isAll)
                return new ServerResponse().Success(query.ToList());

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.SchoolName!.Contains(searchValue) ||
                    d.SchoolNameInKhmer!.Contains(searchValue));

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.SchoolId);
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

    [HttpPost("get-schools-by-academic-year")]
    public IActionResult GetSchoolByAcademicYear(int degreeId, int year)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query = (from p in db.TblPromotion
                join s in db.TblSchool on p.SchoolId equals s.SchoolId
                where p.DegreeId == degreeId && p.AcademicYearStart == year && s.IsFoundationSchool == 0
                select new
                {
                    s.SchoolId,
                    s.SchoolName,
                    s.SchoolNameInKhmer,
                    s.SchoolCode,
                    s.IsFoundationSchool
                }).AsQueryable();
            return new ServerResponse().Success(query.ToList());
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpPost("SaveChange")]
    public async Task<IActionResult> SaveChange([FromForm] SchoolDto? schoolDto)
    {
    try
    {
        if (schoolDto == null)
        {
            return BadRequest(new
            {
                code = "400",
                message = "Bad Request!"
            });
        }

        if (string.IsNullOrWhiteSpace(schoolDto.SchoolName))
        {
            return BadRequest(new
            {
                code = "400",
                message = "School name is required."
            });
        }

        if (string.IsNullOrWhiteSpace(schoolDto.SchoolNameInKhmer))
        {
            return BadRequest(new
            {
                code = "400",
                message = "School name in Khmer is required."
            });
        }

        if (string.IsNullOrWhiteSpace(schoolDto.SchoolCode))
        {
            return BadRequest(new
            {
                code = "400",
                message = "School code is required."
            });
        }

        if (schoolDto.FacultyId <= 0)
        {
            return BadRequest(new
            {
                code = "400",
                message = "Faculty is required."
            });
        }

        var db = campusDbContext.DbContext(_campus);

        var school = db.TblSchool.FirstOrDefault(x => x.SchoolId == schoolDto.SchoolId);

        if (school != null)
        {
            school.SchoolName = schoolDto.SchoolName.Trim();
            school.SchoolNameInKhmer = schoolDto.SchoolNameInKhmer.Trim();
            school.SchoolCode = schoolDto.SchoolCode.Trim();
            school.FacultyId = schoolDto.FacultyId;
            school.IsFoundationSchool = schoolDto.IsFoundationSchool;

            await db.SaveChangesAsync();

            return Ok(new
            {
                code = "200",
                message = "Updated successfully!"
            });
        }

        var newSchool = new School
        {
            SchoolName = schoolDto.SchoolName.Trim(),
            SchoolNameInKhmer = schoolDto.SchoolNameInKhmer.Trim(),
            SchoolCode = schoolDto.SchoolCode.Trim(),
            FacultyId = schoolDto.FacultyId,
            IsFoundationSchool = schoolDto.IsFoundationSchool
        };

        await db.TblSchool.AddAsync(newSchool);
        await db.SaveChangesAsync();

        return Ok(new
        {
            code = "200",
            message = "Saved successfully!"
        });
    }
    catch (Exception ex)
    {
        var realError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

        return StatusCode(500, new
        {
            code = "500",
            message = $"Internal Server Error: {realError}"
        });
    }
    }
    [HttpDelete("delete/{schoolId:int}")]
    public async Task<IActionResult> Delete(int schoolId)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            var school = db.TblSchool.FirstOrDefault(x => x.SchoolId == schoolId);

            if (school == null)
            {
                return BadRequest(new
                {
                    code = "400",
                    message = "School not found!"
                });
            }

            db.TblSchool.Remove(school);
            await db.SaveChangesAsync();

            return Ok(new
            {
                code = "200",
                message = "Deleted successfully!"
            });
        }
        catch (Exception ex)
        {
            var realError = ex.InnerException != null
                ? ex.InnerException.Message
                : ex.Message;

            return StatusCode(500, new
            {
                code = "500",
                message = $"Internal Server Error: {realError}"
            });
        }
    }
}