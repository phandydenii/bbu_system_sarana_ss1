using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("school")]
public class SchoolController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller {
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
            var db = campusDbContext.DbContext(_campus);
            var query = db.TblSchool.AsQueryable();
            if (isAll)
            {
                return new ServerResponse().Success(query.ToList(), "Succeeded!");
            }

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
            return new ServerResponse().Success(query, "Succeeded!");
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
                return new ServerResponse().BadRequest("Bad Request!");
            }

            var db = campusDbContext.DbContext(_campus);

            if (schoolDto.SchoolId == 0)
            {
                var newSchool = mapper.Map<SchoolDto, School>(schoolDto);

                await db.TblSchool.AddAsync(newSchool);
                await db.SaveChangesAsync();

                return new ServerResponse().Success(newSchool, "Saved successfully!");
            }
            
            var oldData = await db.TblSchool
                .FirstOrDefaultAsync(x => x.SchoolId == schoolDto.SchoolId);

            if (oldData == null)
            {
                return new ServerResponse().BadRequest("School not found!");
            }

            mapper.Map(schoolDto, oldData);

            db.TblSchool.Update(oldData);
            await db.SaveChangesAsync();

            return new ServerResponse().Success(oldData, "Updated successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
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
                return new ServerResponse().BadRequest("School not found!");
            }

            db.TblSchool.Remove(school);
            await db.SaveChangesAsync();

            return new ServerResponse().Success(school, "Deleted successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
}