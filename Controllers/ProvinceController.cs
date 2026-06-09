using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            query = query.OrderByDescending(d => d.ProvinceId);
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
    public async Task<IActionResult> SaveChange([FromForm] ProvinceDto? province)
    {
        try
        {
            if (province == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }
            
            var db = campusDbContext.DbContext(_campus);

            var provinceEntity = db.TblProvince
                .FirstOrDefault(x => x.ProvinceId == province.ProvinceId);

            if (provinceEntity != null)
            {
                provinceEntity.ProvinceName = province.ProvinceName?.Trim();
                provinceEntity.ProvinceInKhmer = province.ProvinceInKhmer?.Trim();
                provinceEntity.IsCity = province.IsCity;

                await db.SaveChangesAsync();

                return new ServerResponse().Success(provinceEntity, "Updated successfully!");
            }
            else
            {
                var newProvince = new Province
                {
                    ProvinceName = province.ProvinceName?.Trim(),
                    ProvinceInKhmer = province.ProvinceInKhmer?.Trim(),
                    IsCity = province.IsCity
                };

                await db.TblProvince.AddAsync(newProvince);
                await db.SaveChangesAsync();

                return new ServerResponse().Success(newProvince, "Saved successfully!");
            }
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
}