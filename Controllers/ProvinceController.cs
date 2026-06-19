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
[Route("Province")]
public class ProvinceController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-provinces")]
    public IActionResult GetProvince(bool isAll = false)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            
            var sortColumnIndex = Request.Form["order[0][column]"].FirstOrDefault();
            var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}[data]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;

            var db = campusDbContext.DbContext(_campus);
            var query = db.TblProvince.AsQueryable();
            if (isAll)
            {
                return new ServerResponse().Success(query.ToList(), "Succeeded!");
            }

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.ProvinceName!.Contains(searchValue) ||
                    d.ProvinceInKhmer!.Contains(searchValue));

            var recordsTotal = query.Count();
            //query = query.OrderByDescending(d => d.ProvinceId);
            switch (sortColumn)
            {
                case "ProvinceName":
                    query = sortDirection == "asc" ? query.OrderBy(x => x.ProvinceName) : 
                        query.OrderByDescending(x => x.ProvinceInKhmer);
                    break;
                case "ProvinceInKhmer":
                    query = sortDirection == "asc" ? query.OrderBy(x => x.ProvinceInKhmer):
                    query.OrderByDescending(x => x.ProvinceInKhmer);
                    break;
                default:
                    query = sortDirection == "asc" ? query.OrderBy(x => x.ProvinceId):
                    query.OrderByDescending(x => x.ProvinceId);
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

    [HttpGet("get-province/{provinceId:int}")]
    public async Task<IActionResult> GetProvince(int provinceId)
    {
        var db = campusDbContext.DbContext(_campus);
        var data = await db.TblProvince.FirstOrDefaultAsync(x => x.ProvinceId == provinceId);
        if(data == null) return new ServerResponse().BadRequest("Province not found!");
        
        return new ServerResponse().Success(data, "Get successfully!");
    }

    [HttpPost("SaveChange")]
    public async Task<IActionResult> SaveChange([FromForm] ProvinceDto? province)
    {
        try
        {
            if (province == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }
            var db = campusDbContext.DbContext(_campus);
            if (province.ProvinceId == 0)
            {
                var data = mapper.Map<ProvinceDto, Province>(province);
                await db.TblProvince.AddAsync(data);
                await db.SaveChangesAsync();
                return new ServerResponse().Success(data, "Save successfully!");
            }
            var oldData = await db.TblProvince.FirstOrDefaultAsync(x => x.ProvinceId == province.ProvinceId);
            if(oldData == null) return new ServerResponse().BadRequest("Province not found!");
            
            mapper.Map(province, oldData);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(oldData, "Save successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpDelete("Delete/{provinceId:int}")]
    public async Task<IActionResult> Delete(int provinceId)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var province = await db.TblProvince.FirstOrDefaultAsync(x => x.ProvinceId == provinceId);
            if (province == null) return new ServerResponse().BadRequest("Province not found!");
            db.TblProvince.Remove(province);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(province, "Delete successfully!");

        }
        catch (Exception e)
        {
           return new ServerResponse().ErrorInternal(e);
        }
    }
}