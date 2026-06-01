using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("disability")]
public class DisabilityController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-disabilities")]
    public IActionResult Gets(bool isAll = false)
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
            var query = db.TblDisability.AsQueryable();
            if (isAll)
                return Ok(new
                {
                    data = query.Select(x => new { x.Id, x.DisabilityName }).ToList(),
                    status = new
                    {
                        code = "200",
                        message = "Succeeded!"
                    }
                });
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.DisabilityName!.Contains(searchValue) ||
                    d.DisabilityNameKh!.Contains(searchValue));

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.Id);
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
public async Task<IActionResult> SaveChange([FromForm] DisabilityDto? disabilityDto)
{
    try
    {
        if (disabilityDto == null)
        {
            return BadRequest(new
            {
                code = "400",
                message = "Bad Request!"
            });
        }

        if (string.IsNullOrWhiteSpace(disabilityDto.DisabilityName))
        {
            return BadRequest(new
            {
                code = "400",
                message = "Disability name is required."
            });
        }

        if (string.IsNullOrWhiteSpace(disabilityDto.DisabilityNameKh))
        {
            return BadRequest(new
            {
                code = "400",
                message = "Disability name Khmer is required."
            });
        }

        var db = campusDbContext.DbContext(_campus);

        var disability = db.TblDisability
            .FirstOrDefault(x => x.Id == disabilityDto.Id);

        if (disability != null)
        {
            disability.DisabilityName = disabilityDto.DisabilityName.Trim();
            disability.DisabilityNameKh = disabilityDto.DisabilityNameKh.Trim();

            db.TblDisability.Update(disability);
            await db.SaveChangesAsync();

            return Ok(new
            {
                code = "200",
                message = "Updated successfully!"
            });
        }

        var newDisability = new Disability
        {
            DisabilityName = disabilityDto.DisabilityName.Trim(),
            DisabilityNameKh = disabilityDto.DisabilityNameKh.Trim()
        };

        await db.TblDisability.AddAsync(newDisability);
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
}