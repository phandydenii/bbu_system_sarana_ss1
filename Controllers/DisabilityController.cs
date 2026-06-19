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
    public IActionResult Gets(bool isAll = false)
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
            var query = db.TblDisability.AsQueryable();
            if (isAll)
            {
                return new ServerResponse().Success(query.ToList(), "Succeeded!");
            }
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.DisabilityName!.Contains(searchValue) ||
                    d.DisabilityNameKh!.Contains(searchValue));

            var recordsTotal = query.Count();
            //query = query.OrderByDescending(d => d.Id);
            switch (sortColumn)
            {
                case "disabilityName":
                    query = sortDirection == "asc" ? query.OrderBy(x => x.DisabilityName):
                        query.OrderByDescending(x => x.DisabilityName);
                    break;
                case "disabilityNameKh":
                    query = sortDirection == "asc" ? query.OrderBy(x => x.DisabilityNameKh):
                        query.OrderByDescending(x => x.DisabilityNameKh);
                    break;
                default:
                    query = sortDirection == "asc" ? query.OrderBy(x => x.Id):
                        query.OrderByDescending(x => x.Id);
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