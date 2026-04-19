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

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;

            var db = campusDbContext.DbContext(_campus);
            var query = db.TblDegree.AsQueryable();
            if (isAll)
                return new ServerResponse().Success(query.ToList());

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.DegreeName!.Contains(searchValue) ||
                    d.DegreeInKhmer!.Contains(searchValue));

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.DegreeId);
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

    [HttpGet("get-degree/{degreeId:int}")]
    public async Task<IActionResult> GetDegree(int degreeId)
    {
        var db = campusDbContext.DbContext(_campus);
        var data = await db.TblDegree.Where(x => x.DegreeId == degreeId).FirstOrDefaultAsync();
        if (data == null)
        {
            return new ServerResponse().NotFound("Degree not found");
        };
        return new ServerResponse().Success(data);
    }

    [HttpPost("SaveChange")]
    public async Task<IActionResult> SaveChange([FromForm] DegreeDto? degree)
    {
        try
        {
            if (degree == null)
                return new ServerResponse().BadRequest();

            var db = campusDbContext.DbContext(_campus);
            if (degree.DegreeId == 0)
            {
                var data = mapper.Map<DegreeDto, Degree>(degree);
                await db.TblDegree.AddAsync(data);
                await db.SaveChangesAsync();
                return new ServerResponse().Success(degree);
            }
            var oldData = await db.TblDegree.Where(x => x.DegreeId == degree.DegreeId).FirstOrDefaultAsync();
            if (oldData == null)
                return new ServerResponse().BadRequest();
            mapper.Map(degree, oldData);
            db.TblDegree.Update(oldData);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(degree);
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
                return new ServerResponse().BadRequest();
            db.TblDegree.Remove(degree);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(null);
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
}