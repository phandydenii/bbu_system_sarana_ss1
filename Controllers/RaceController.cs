using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
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
            {
                return new ServerResponse().Success(query.ToList(), "Succeeded!");
            }

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
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpPost("SaveChange")]
    public async Task<IActionResult> SaveChange([FromForm] RaceDto? raceDto)
{
    try
    {
        if (raceDto == null)
        {
            return new ServerResponse().BadRequest("Bad Request!");
        }

        var db = campusDbContext.DbContext(_campus);

        var race = db.TblRace.FirstOrDefault(x => x.RaceId == raceDto.RaceId);

        if (race != null)
        {
            // Update
            race.RaceName = raceDto.RaceName.Trim();
            race.RaceInKhmer = raceDto.RaceInKhmer.Trim();

            await db.SaveChangesAsync();

            return new ServerResponse().Success(race, "Updated successfully!");
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

            return new ServerResponse().Success(newRace, "Saved successfully!");
        }
    }
    catch (Exception ex)
    {
        return new ServerResponse().ErrorInternal(ex);
    }
}
}