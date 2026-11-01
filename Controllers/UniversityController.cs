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
[Route("university")]
public class UniversityController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";
    [HttpPost("get-universities")]
    public IActionResult GetUniversities(bool isAll = false)
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
            // query = query.OrderByDescending(d => d.UniversityId);
            switch (sortColumn)
            {
                case "universityName":
                    query = sortDirection == "asc" ? query.OrderBy(x => x.UniversityName):
                        query.OrderByDescending(x => x.UniversityName);
                    break;
                case "universityNameInKhmer":
                    query = sortDirection == "asc" ? query.OrderBy(x => x.UniversityNameInKhmer):
                        query.OrderByDescending(x => x.UniversityNameInKhmer);
                    break;
                case "abbreviationName":
                    query = sortDirection == "asc" ? query.OrderBy(x => x.AbbreviationName):
                        query.OrderByDescending(x => x.AbbreviationName);
                    break;
                default:
                    query = sortDirection == "asc" ? query.OrderBy(x => x.UniversityId):
                        query.OrderByDescending(x => x.UniversityId);
                    break;
            }
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
    
    [HttpGet("get-university/{universityId:int}")]
    public async Task<IActionResult> GetUniversity(int universityId)
    {
        var db = campusDbContext.DbContext(_campus);
        var data = await db.TblUsersity.Where(x => x.UniversityId == universityId).FirstOrDefaultAsync();
        if (data == null)
        {
            return new ServerResponse().NotFound("University not found");
        }
        return new ServerResponse().Success(data);
    }
    
   [HttpPost("SaveChange")]
public async Task<IActionResult> SaveChange([FromForm] UniversityDto? university)
{
    try
    {
        if (university == null)
        {
            return new ServerResponse().BadRequest("Bad Request!");
        }

        var db = campusDbContext.DbContext(_campus);
        //university.UniversityId = 0;
        if (university.UniversityId == 0)
        {
            var data = mapper.Map<UniversityDto, University>(university);
            await db.TblUsersity.AddAsync(data);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(data, "Saved successfully!");
        }
        var oldData = await db.TblUsersity.Where(x => x.UniversityId == university.UniversityId).FirstOrDefaultAsync();
        if(oldData == null) return new ServerResponse().NotFound("UniversityId not found");
            
        mapper.Map(university, oldData);
        await db.SaveChangesAsync();
        return new ServerResponse().Success(oldData, "Updated successfully!");
    }
    catch (Exception ex)
    {
        return StatusCode(500, new
        {
            ex.Message,
            InnerException = ex.InnerException?.Message,
            ex.StackTrace
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