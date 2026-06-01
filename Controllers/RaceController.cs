using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("race")]
public class RaceController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-races")]
    public IActionResult Get(bool isAll = false)
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
            var query = db.TblRace.AsQueryable();

            if (isAll)
                return Ok(new
                {
                    data = query.Select(x => new { x.RaceId, x.RaceName }).ToList(),
                    status = new
                    {
                        code = "200",
                        message = "Succeeded!"
                    }
                });

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.RaceName!.Contains(searchValue) ||
                    d.RaceInKhmer!.Contains(searchValue));

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.RaceId);
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
    public async Task<IActionResult> SaveChange([FromForm] RaceDto? raceDto)
{
    try
    {
        if (raceDto == null)
        {
            return BadRequest(new
            {
                code = "400",
                message = "Bad Request!"
            });
        }

        if (string.IsNullOrWhiteSpace(raceDto.RaceName))
        {
            return BadRequest(new
            {
                code = "400",
                message = "Race name is required."
            });
        }

        if (string.IsNullOrWhiteSpace(raceDto.RaceInKhmer))
        {
            return BadRequest(new
            {
                code = "400",
                message = "Race name Khmer is required."
            });
        }

        var db = campusDbContext.DbContext(_campus);

        var race = db.TblRace.FirstOrDefault(x => x.RaceId == raceDto.RaceId);

        if (race != null)
        {
            // Update
            race.RaceName = raceDto.RaceName.Trim();
            race.RaceInKhmer = raceDto.RaceInKhmer.Trim();

            db.TblRace.Update(race);
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
            var newRace = new Race
            {
                RaceName = raceDto.RaceName.Trim(),
                RaceInKhmer = raceDto.RaceInKhmer.Trim()
            };

            await db.TblRace.AddAsync(newRace);
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