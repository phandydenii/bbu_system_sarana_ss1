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
[Route("Faculty")]
public class FacultyController(ICampusDbContext campusDbContext,IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";
    
   [HttpPost("get-faculties")]
    public IActionResult GetFacultyList(bool isAll = false)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query = db.TblFaculty.AsQueryable();
            if (isAll)
            {
                var faculties = query
                    .OrderBy(x => x.FacultyName)
                    .Select(x => new
                    {
                        facultyId = x.FacultyId,
                        facultyName = x.FacultyName,
                        facultyNameInKhmer = x.FacultyNameInKhmer
                    })
                    .ToList();

                return new ServerResponse().Success(faculties, "Succeeded!");
            }

            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 10;
            var skip = start != null ? Convert.ToInt32(start) : 0;

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(x =>
                    x.FacultyName!.Contains(searchValue) ||
                    x.FacultyNameInKhmer!.Contains(searchValue));
            }

            var recordsTotal = query.Count();

            var data = query
                .OrderByDescending(x => x.FacultyId)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

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
    public async Task<IActionResult> SaveChange([FromForm] FacultyDto? faculty)
    {
        try
        {
            if (faculty == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }

            var db = campusDbContext.DbContext(_campus);

            if (faculty.FacultyId == 0)
            {
                var newFaculty = mapper.Map<FacultyDto, Faculty>(faculty);

                await db.TblFaculty.AddAsync(newFaculty);
                await db.SaveChangesAsync();

                return new ServerResponse().Success(newFaculty, "Saved successfully!");
            }

            var oldData = await db.TblFaculty
                .FirstOrDefaultAsync(x => x.FacultyId == faculty.FacultyId);

            if (oldData == null)
            {
                return new ServerResponse().BadRequest("Faculty not found!");
            }

            mapper.Map(faculty, oldData);

            db.TblFaculty.Update(oldData);
            await db.SaveChangesAsync();

            return new ServerResponse().Success(oldData, "Updated successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    [HttpDelete("delete/{facultyId}")]
    public async Task<IActionResult> Delete(decimal facultyId)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            var faculty = db.TblFaculty
                .FirstOrDefault(x => x.FacultyId == facultyId);

            if (faculty == null)
            {
                return new ServerResponse().BadRequest("Faculty not found!");
            }

            db.TblFaculty.Remove(faculty);
            await db.SaveChangesAsync();

            return new ServerResponse().Success(faculty, "Deleted successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
}