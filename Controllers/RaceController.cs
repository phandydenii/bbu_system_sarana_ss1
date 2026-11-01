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

            var sortColumnIndex = Request.Form["order[0][column]"].FirstOrDefault();
            var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"].FirstOrDefault();

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
            //query = query.OrderByDescending(d => d.RaceId);
            switch (sortColumn)
            {
                case "RaceName":
                    query = sortDirection == "asc"
                        ? query.OrderBy(d => d.RaceName)
                        : query.OrderByDescending(d => d.RaceName);
                    break;
                case "RaceInKhmer":
                    query = sortDirection == "asc"
                        ? query.OrderBy(d => d.RaceInKhmer)
                        : query.OrderByDescending(d => d.RaceInKhmer);
                    break;
                default:
                    query = sortDirection == "asc"
                        ? query.OrderBy(d => d.RaceId)
                        : query.OrderByDescending(d => d.RaceId);
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

    [HttpPost("SaveChange")]
    public async Task<IActionResult> SaveChange([FromForm] RaceDto? race)
    {
        try
        {
            if (race == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }
            var db = campusDbContext.DbContext(_campus);
            if (race.RaceId == 0)
            {
                var data = mapper.Map<RaceDto, Race>(race);
           
                await db.TblRace.AddAsync(data);
                await db.SaveChangesAsync();
                return new ServerResponse().Success(data,"Save success!");
            }

            var oldData = await db.TblRace.Where(x => x.RaceId == race.RaceId).FirstOrDefaultAsync();
            if (oldData == null) return new ServerResponse().BadRequest("Race not found!");
            
            mapper.Map(race, oldData);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(oldData,"Update success!");
        }
        catch (Exception ex)
        {
            return Json(new
            {
                Message = ex.Message,
                InnerMessage = ex.InnerException?.Message,
                StackTrace = ex.StackTrace
            });
        }
    }
}