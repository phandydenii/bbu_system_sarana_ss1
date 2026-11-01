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
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var sortColumnIndex = Request.Form["order[0][column]"].FirstOrDefault();
            var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();
            
            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            
            var db = campusDbContext.DbContext(_campus);
            var query = db.TblFaculty.AsQueryable();
            if (isAll)
            {
                return new ServerResponse().Success(query.ToList(), "Succeeded!");
            }
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(x =>
                    x.FacultyName!.Contains(searchValue) ||
                    x.FacultyNameInKhmer!.Contains(searchValue));
            
            var recordsTotal = query.Count();

            switch (sortColumn)
            {
                case "facultyName":
                    query = sortDirection == "asc" ? query.OrderBy(x => x.FacultyName):
                        query.OrderByDescending(x => x.FacultyName);
                    break;
                case "facultyNameInKhmer":
                    query = sortDirection == "asc" ? query.OrderBy(x => x.FacultyNameInKhmer):
                        query.OrderByDescending(x => x.FacultyNameInKhmer);
                    break;
                default:
                    query = sortDirection == "asc" ? query.OrderBy(x => x.FacultyId):
                        query.OrderByDescending(x => x.FacultyId);
                    break;
            }
            var data = query
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