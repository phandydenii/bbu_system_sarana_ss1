using AutoMapper;
using BBU_SYSTEM.DTOs;
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
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                code = "500",
                message = $"Internal Server Error:{e.Message}"
            });
        }
    }

   [HttpPost("SaveChange")]
public async Task<IActionResult> SaveChange([FromForm] UniversityDto? universityDto)
{
    try
    {
        if (universityDto == null)
        {
            return BadRequest(new
            {
                code = "400",
                message = "Bad Request!"
            });
        }

        if (string.IsNullOrWhiteSpace(universityDto.UniversityName))
        {
            return BadRequest(new
            {
                code = "400",
                message = "University name is required."
            });
        }

        if (string.IsNullOrWhiteSpace(universityDto.UniversityNameInKhmer))
        {
            return BadRequest(new
            {
                code = "400",
                message = "University name Khmer is required."
            });
        }

        var db = campusDbContext.DbContext(_campus);

        var university = db.TblUsersity
            .FirstOrDefault(x => x.UniversityId == universityDto.UniversityId);

        if (university != null)
        {
            university.UniversityName = universityDto.UniversityName.Trim();
            university.UniversityNameInKhmer = universityDto.UniversityNameInKhmer.Trim();
            university.AbbreviationName = universityDto.AbbreviationName?.Trim();

            db.TblUsersity.Update(university);
            await db.SaveChangesAsync();

            return Ok(new
            {
                code = "200",
                message = "Updated successfully!"
            });
        }

        var newUniversity = new University
        {
            UniversityName = universityDto.UniversityName.Trim(),
            UniversityNameInKhmer = universityDto.UniversityNameInKhmer.Trim(),
            AbbreviationName = universityDto.AbbreviationName?.Trim()
        };

        await db.TblUsersity.AddAsync(newUniversity);
        await db.SaveChangesAsync();

        return Ok(new
        {
            code = "200",
            message = "Saved successfully!"
        });
    }
    catch (Exception ex)
    {
        var realError = ex.InnerException?.Message ?? ex.Message;

        return StatusCode(500, new
        {
            code = "500",
            message = $"Internal Server Error: {realError}"
        });
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
            return BadRequest(new
            {
                code = "400",
                message = "University not found!"
            });
        }

        db.TblUsersity.Remove(university);
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