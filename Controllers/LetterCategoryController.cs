using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("letter-category")]
public class LetterCategoryController(
    ICampusDbContext campusDbContext,
    IMapper mapper,
    IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-letter-category")]
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
            var query = db.TblLetterCategory.AsQueryable();

            if (isAll)
            {
                return Ok(new
                {
                    data = query.ToList(),
                    status = new
                    {
                        code = "200",
                        message = "Success"
                    }
                });
            }

            if (!string.IsNullOrEmpty(searchValue))
                query = query
                    .Where(x => x.CategoryId == int.Parse(searchValue) || x.CategoryName!.Contains(searchValue))
                    .AsQueryable();
            var recordsTotal = query.Count();
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
                data = new {},
                status = new
                {
                    code = "500",
                    message = e.Message
                }
            });
        }
    }

    [HttpPost("post-letter-category")]
    public async Task<IActionResult> Post(LetterCategoryDto? letterCategory)
    {
        if (letterCategory == null)
            return BadRequest(new
            {
                data = new{},
                status = new
                {
                    code = "400",
                    message = "Bad Request!"
                }
            });
        try
        {
            var db = campusDbContext.DbContext(_campus);
            if (letterCategory.CategoryId == 0)
            {
                var data = mapper.Map<LetterCategoryDto, LetterCategory>(letterCategory);
                await db.TblLetterCategory.AddAsync(data);
                await db.SaveChangesAsync();
                return Ok(new
                {   
                    data = new{},
                    status = new
                    {
                        code = "200",
                        message = "Insert Succeeded!"
                    }
                });
            }
            var dataExist = await db.TblLetterCategory.Where(x => x.CategoryId == letterCategory.CategoryId)
                .FirstOrDefaultAsync();
            if (dataExist == null)
                return BadRequest(new
                {
                    data = new{},
                    status = new
                    {
                        code = "400",
                        message = "Bad Request!"
                    }
                });
            mapper.Map(letterCategory, dataExist);
            db.TblLetterCategory.Update(dataExist);
            await db.SaveChangesAsync();
            return Ok(new
            {
                data = new{},
                status = new
                {
                    code = "200",
                    message = "Update Succeeded!"
                }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                data = new{},
                status = new
                {
                    code = "500",
                    message = $"Internal Server Error:{ex.Message}"
                }
            });
        }
    }
}