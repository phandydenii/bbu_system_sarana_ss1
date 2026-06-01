using AutoMapper;
using BBU_SYSTEM.DTOs;
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
                return Ok(new
                {
                    data = query.Select(x => new { x.ProvinceId, x.ProvinceName }).ToList(),
                    status = new
                    {
                        code = "200",
                        message = "Succeeded!"
                    }
                });

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
public async Task<IActionResult> SaveChange([FromForm] ProvinceDto? province)
{
    try
    {
        if (province == null)
        {
            return BadRequest(new
            {
                code = "400",
                message = "Bad Request!"
            });
        }

        if (string.IsNullOrWhiteSpace(province.ProvinceName))
        {
            return BadRequest(new
            {
                code = "400",
                message = "Province name is required."
            });
        }

        if (string.IsNullOrWhiteSpace(province.ProvinceInKhmer))
        {
            return BadRequest(new
            {
                code = "400",
                message = "Province name Khmer is required."
            });
        }

        var db = campusDbContext.DbContext(_campus);

        var provinceEntity = db.TblProvince
            .FirstOrDefault(x => x.ProvinceId == province.ProvinceId);

        if (provinceEntity != null)
        {
            // Update
            provinceEntity.ProvinceName = province.ProvinceName.Trim();
            provinceEntity.ProvinceInKhmer = province.ProvinceInKhmer.Trim();
            provinceEntity.IsCity = province.IsCity;

            await db.SaveChangesAsync();

            return Ok(new
            {
                code = "200",
                message = "Updated successfully!"
            });
        }
        else
        {
            // Insert
            var newProvince = new Province
            {
                ProvinceName = province.ProvinceName.Trim(),
                ProvinceInKhmer = province.ProvinceInKhmer.Trim(),
                IsCity = province.IsCity
            };

            await db.TblProvince.AddAsync(newProvince);
            await db.SaveChangesAsync();

            return Ok(new
            {
                code = "200",
                message = "Saved successfully!"
            });
        }
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