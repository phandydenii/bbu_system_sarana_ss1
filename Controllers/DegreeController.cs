using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("degree")]
public class DegreeController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-degrees")]
    public IActionResult GetDegrees(bool isAll = false)
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
            var query = db.TblDegree.AsQueryable();
            if (isAll)
            {
                return new ServerResponse().Success(query.ToList());
            }
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.DegreeName!.Contains(searchValue) ||
                    d.DegreeInKhmer!.Contains(searchValue));

            var recordsTotal = query.Count();
            //query = query.OrderByDescending(d => d.DegreeId);
            switch(sortColumn)
            {
                case "degreeName":
                    query = sortDirection == "asc" ? query.OrderBy(x => x.DegreeName) : query.OrderByDescending(x => x.DegreeName);
                    break;
                case "degreeInKhmer":
                    query = sortDirection == "asc" ? query.OrderBy(x => x.DegreeInKhmer) : query.OrderByDescending(x => x.DegreeInKhmer);
                    break;
                default:
                    query = sortDirection == "asc"? query.OrderBy(x => x.DegreeId): query.OrderByDescending(x => x.DegreeId);
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
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("SaveChange")]
    public async Task<IActionResult> SaveChange([FromForm] DegreeDto? degree)
    {
        try
        {
            if (degree == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }

            var db = campusDbContext.DbContext(_campus);

            if (degree.DegreeId == 0)
            {
                var newDegree = mapper.Map<DegreeDto, Degree>(degree);

                await db.TblDegree.AddAsync(newDegree);
                await db.SaveChangesAsync();

                return new ServerResponse().Success(newDegree, "Saved successfully!");
            }

            var oldData = await db.TblDegree
                .FirstOrDefaultAsync(x => x.DegreeId == degree.DegreeId);

            if (oldData == null)
            {
                return new ServerResponse().BadRequest("Degree not found!");
            }

            mapper.Map(degree, oldData);

            db.TblDegree.Update(oldData);
            await db.SaveChangesAsync();

            return new ServerResponse().Success(oldData, "Updated successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpDelete("delete/{degreeId:int}")]
    public async Task<IActionResult> PutDegree(int degreeId)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var degree = await db.TblDegree.Where(x => x.DegreeId == degreeId).FirstOrDefaultAsync();
            if (degree == null)
                return new ServerResponse().BadRequest("Bad Request!");
            db.TblDegree.Remove(degree);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(null, "Deleted successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
}