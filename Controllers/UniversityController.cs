using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("university")]
public class UniversityController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-universities")]
    public IActionResult GetUniversities(bool isAll = false)
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
            var query = db.TblUsersity.AsQueryable();

            if (isAll)
            {
                return new ServerResponse().Success(query.ToList(), "Succeeded!");
            }
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.UniversityName!.Contains(searchValue) ||
                    d.UniversityNameInKhmer!.Contains(searchValue));

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.UniversityId);
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

   [HttpPost("SaveChange")]
public async Task<IActionResult> SaveChange([FromForm] UniversityDto? universityDto)
{
    try
    {
        if (universityDto == null)
        {
            return new ServerResponse().BadRequest("Bad Request!");
        }

        var db = campusDbContext.DbContext(_campus);

        var university = db.TblUsersity
            .FirstOrDefault(x => x.UniversityId == universityDto.UniversityId);

        if (university != null)
        {
            university.UniversityName = universityDto.UniversityName?.Trim();
            university.UniversityNameInKhmer = universityDto.UniversityNameInKhmer?.Trim();
            university.AbbreviationName = universityDto.AbbreviationName?.Trim();

            db.TblUsersity.Update(university);
            await db.SaveChangesAsync();

            return new ServerResponse().Success(university, "Updated successfully!");
        }

        var newUniversity = new University
        {
            UniversityName = universityDto.UniversityName?.Trim(),
            UniversityNameInKhmer = universityDto.UniversityNameInKhmer?.Trim(),
            AbbreviationName = universityDto.AbbreviationName?.Trim()
        };

        await db.TblUsersity.AddAsync(newUniversity);
        await db.SaveChangesAsync();

        return new ServerResponse().Success(newUniversity, "Saved successfully!");
    }
    catch (Exception ex)
    {
        return new ServerResponse().ErrorInternal(ex);
    }
}
[HttpDelete("delete/{universityId:int}")]
public async Task<IActionResult> Delete(int universityId)
{
    try
    {
        var db = campusDbContext.DbContext(_campus);

        var university = db.TblUsersity
            .FirstOrDefault(x => x.UniversityId == universityId);

        if (university == null)
        {
            return new ServerResponse().BadRequest("University not found!");
        }

        db.TblUsersity.Remove(university);
        await db.SaveChangesAsync();
        
        return new ServerResponse().Success(university, "Deleted successfully!");
    }
    catch (Exception ex)
    {
        return new ServerResponse().ErrorInternal(ex);
    }
}
}