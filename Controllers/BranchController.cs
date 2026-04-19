using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("branch")]
public class BranchController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("get-branch")]
    public IActionResult GetBranch(bool isAll = false)
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
            var query = db.TblBranch.AsQueryable();
            if (isAll)
                return Ok(new
                {
                    data = query.ToList(),
                    code = "200",
                    message = "Succeeded!"
                });

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.BranchName!.Contains(searchValue) ||
                    d.BranchNameInKhmer!.Contains(searchValue));

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.BranchId);
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

    [HttpPost("save-branch")]
    public async Task<IActionResult> Save([FromForm] BranchDto? branch)
    {
        try
        {
            if (branch == null)
                return BadRequest(new
                {
                    code = "400",
                    message = "Bad Request!"
                });
            var db = campusDbContext.DbContext(_campus);
            var branchEntity = db.TblBranch.FirstOrDefault(x => x.BranchId == branch.BranchId);
            if (branchEntity != null)
            {
                mapper.Map(branch, branchEntity);
                db.TblBranch.Update(branchEntity);
                await db.SaveChangesAsync();
            }
            else
            {
                var newBranch = mapper.Map<BranchDto, Branch>(branch);
                await db.TblBranch.AddAsync(newBranch);
                await db.SaveChangesAsync();
            }

            return Ok(new
            {
                code = "200",
                message = "Succeeded!"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                code = "500",
                message = $"Internal Server Error:{ex.Message}"
            });
        }
    }
}