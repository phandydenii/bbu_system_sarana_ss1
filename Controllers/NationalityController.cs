using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("Nationality")]
public class NationalityController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-nationalities")]
    public IActionResult GetBranchs(bool isAll = false)
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
            var query = db.TblNationality.AsQueryable();

            if (isAll)
                return Ok(new
                {
                    data = query.ToList(),
                    status = new
                    {
                        code = "200",
                        message = "Succeded!"
                    }
                });

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.NationalityName!.Contains(searchValue) ||
                    d.NationalityInKhmer!.Contains(searchValue));

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.NationalityId);
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
    public async Task<IActionResult> SaveChange([FromForm] NationalityDto? nationalityDto)
{
    try
    {
        if (nationalityDto == null)
        {
            return BadRequest(new
            {
                code = "400",
                message = "Bad Request!"
            });
        }

        if (string.IsNullOrWhiteSpace(nationalityDto.NationalityName))
        {
            return BadRequest(new
            {
                code = "400",
                message = "Nationality name is required."
            });
        }

        if (string.IsNullOrWhiteSpace(nationalityDto.NationalityInKhmer))
        {
            return BadRequest(new
            {
                code = "400",
                message = "Nationality name Khmer is required."
            });
        }

        var db = campusDbContext.DbContext(_campus);

        var nationality = db.TblNationality
            .FirstOrDefault(x => x.NationalityId == nationalityDto.NationalityId);

        if (nationality != null)
        {
            nationality.NationalityName = nationalityDto.NationalityName.Trim();
            nationality.NationalityInKhmer = nationalityDto.NationalityInKhmer.Trim();

            db.TblNationality.Update(nationality);
            await db.SaveChangesAsync();

            return Ok(new
            {
                code = "200",
                message = "Updated successfully!"
            });
        }

        var newNationality = new Nationality
        {
            NationalityName = nationalityDto.NationalityName.Trim(),
            NationalityInKhmer = nationalityDto.NationalityInKhmer.Trim()
        };

        await db.TblNationality.AddAsync(newNationality);
        await db.SaveChangesAsync();

        return Ok(new
        {
            code = "200",
            message = "Saved successfully!"
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