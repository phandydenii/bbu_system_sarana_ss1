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
[Route("disability")]
public class DisabilityController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-disabilities")]
    public IActionResult Gets([FromQuery] bool isAll = false)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            var query = db.TblDisability
                .AsNoTracking()
                .Select(x => new DisabilityDto
                {
                    Id = x.Id,
                    DisabilityName = x.DisabilityName,
                    DisabilityNameKh = x.DisabilityNameKh
                });

            // Registry Create dropdown request.
            if (isAll)
            {
                var disabilities = query
                    .OrderBy(x => x.DisabilityName)
                    .ToList();

                return new ServerResponse().Success(
                    disabilities,
                    "Succeeded!"
                );
            }

            // Disability DataTable request.
            var draw = 0;
            var start = 0;
            var length = 10;
            var searchValue = string.Empty;
            var sortColumn = "id";
            var sortDirection = "desc";

            if (Request.HasFormContentType)
            {
                var form = Request.Form;

                int.TryParse(
                    form["draw"].FirstOrDefault(),
                    out draw
                );

                int.TryParse(
                    form["start"].FirstOrDefault(),
                    out start
                );

                if (!int.TryParse(
                        form["length"].FirstOrDefault(),
                        out length) ||
                    length <= 0)
                {
                    length = 10;
                }

                searchValue =
                    form["search[value]"].FirstOrDefault()?.Trim()
                    ?? string.Empty;

                var sortColumnIndex =
                    form["order[0][column]"].FirstOrDefault();

                sortDirection =
                    form["order[0][dir]"].FirstOrDefault()
                    ?? "desc";

                if (!string.IsNullOrWhiteSpace(sortColumnIndex))
                {
                    sortColumn =
                        form[$"columns[{sortColumnIndex}][data]"]
                            .FirstOrDefault()
                        ?? "id";
                }
            }

            var recordsTotal = query.Count();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(x =>
                    (x.DisabilityName != null &&
                     x.DisabilityName.Contains(searchValue)) ||
                    (x.DisabilityNameKh != null &&
                     x.DisabilityNameKh.Contains(searchValue))
                );
            }

            var recordsFiltered = query.Count();

            query = sortColumn switch
            {
                "disabilityName" =>
                    sortDirection == "asc"
                        ? query.OrderBy(x => x.DisabilityName)
                        : query.OrderByDescending(
                            x => x.DisabilityName
                        ),

                "disabilityNameKh" =>
                    sortDirection == "asc"
                        ? query.OrderBy(x => x.DisabilityNameKh)
                        : query.OrderByDescending(
                            x => x.DisabilityNameKh
                        ),

                _ =>
                    sortDirection == "asc"
                        ? query.OrderBy(x => x.Id)
                        : query.OrderByDescending(x => x.Id)
            };

            var data = query
                .Skip(start)
                .Take(length)
                .ToList();

            return Json(new
            {
                draw,
                recordsTotal,
                recordsFiltered,
                data
            });
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }


    [HttpPost("SaveChange")]
    public async Task<IActionResult> SaveChange([FromForm] DisabilityDto? disabilityDto)
    {
        try
        {
            if (disabilityDto == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }

            var db = campusDbContext.DbContext(_campus);

            if (disabilityDto.Id == 0)
            {
                var newDisability = mapper.Map<DisabilityDto, Disability>(disabilityDto);

                await db.TblDisability.AddAsync(newDisability);
                await db.SaveChangesAsync();

                return new ServerResponse().Success(newDisability, "Saved successfully!");
            }

            var oldData = await db.TblDisability
                .FirstOrDefaultAsync(x => x.Id == disabilityDto.Id);

            if (oldData == null)
            {
                return new ServerResponse().BadRequest("Disability not found!");
            }

            mapper.Map(disabilityDto, oldData);

            db.TblDisability.Update(oldData);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(oldData, "Updated successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
}