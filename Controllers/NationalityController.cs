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
[Route("Nationality")]
public class NationalityController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

        [HttpPost("get-nationalities")]
        public IActionResult GetNationality(bool isAll = false)
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
                var query = db.TblNationality.AsQueryable();

                if (isAll) return new ServerResponse().Success(query.ToList(), "Succeeded!");
                
                if (!string.IsNullOrEmpty(searchValue))
                    query = query.Where(d =>
                        d.NationalityName!.Contains(searchValue) ||
                        d.NationalityInKhmer!.Contains(searchValue));

                var recordsTotal = query.Count();
                //query = query.OrderByDescending(d => d.NationalityId);
                switch (sortColumn)
                {
                    case "name":
                        query = sortDirection == "asc" ? query.OrderBy(d => d.NationalityName) : 
                            query.OrderByDescending(d => d.NationalityName);
                        break;
                    case "nameKh":
                        query = sortDirection == "asc" ? query.OrderBy(d => d.NationalityInKhmer) :
                        query.OrderByDescending(d => d.NationalityInKhmer);
                        break;
                    default:
                        query = sortDirection == "asc" ? query.OrderBy(d => d.NationalityId) :
                        query.OrderByDescending(d => d.NationalityId);
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
         public async Task<IActionResult> SaveChange([FromForm] NationalityDto? nationality)
         {
             try
             {
                 if (nationality == null) return new ServerResponse().BadRequest("Bad Request!");
           
                 var db = campusDbContext.DbContext(_campus);
                 if (nationality.NationalityId == 0)
                 {
                     var data = mapper.Map<NationalityDto, Nationality>(nationality);
                     await  db.TblNationality.AddAsync(data);
                     await db.SaveChangesAsync();
                     
                     return new ServerResponse().Success(data, "Saved successfully!");    
                 }
                 var oldData = await db.TblNationality.Where(x =>x.NationalityId == nationality.NationalityId).FirstOrDefaultAsync();
                 if(oldData == null) return new ServerResponse().BadRequest("Nationality not found!");
        
                 mapper.Map(nationality, oldData);
                 await db.SaveChangesAsync();
                 return new ServerResponse().Success(oldData, "Updated successfully!");
             }
             catch (Exception ex)
             {
                 return Ok(new
                 {
                     Message = ex.Message,
                     Inner = ex.InnerException?.Message,
                     StackTrace = ex.StackTrace
                 });
             }
         }
}