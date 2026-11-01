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
[Route("branch")]
public class BranchController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("get-branches")]
    public IActionResult GetBranch(bool isAll = false)
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
            var query = db.TblBranch.AsQueryable();
            if (isAll)
            {
                return new ServerResponse().Success(query.ToList(), "Succeeded!");
            }

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.BranchName!.Contains(searchValue) ||
                    d.BranchNameInKhmer!.Contains(searchValue)||
                    d.ShortName!.Contains(searchValue));

            var recordsTotal = query.Count();
            //query = query.OrderByDescending(d => d.BranchId);
            switch (sortColumn)
            {
                case "branchName":
                    query = sortDirection == "asc" ? query.OrderBy(x => x.BranchName):
                    query.OrderByDescending(x => x.BranchName);
                    break;
                case "branchNameInKhmer":
                    query = sortDirection == "asc" ? query.OrderBy(x => x.BranchNameInKhmer):
                    query.OrderByDescending(x => x.BranchNameInKhmer);
                    break;
                case "ShortName":
                    query = sortDirection == "asc" ? query.OrderBy(x => x.ShortName):
                    query.OrderByDescending(x => x.ShortName);
                    break;
                default:
                    query = sortDirection == "asc" ? query.OrderBy(x => x.BranchId):
                    query.OrderByDescending(x => x.BranchId);
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
    
    [HttpGet("get-branch/{branchId:int}")]
    public async Task<IActionResult> GetBranch(int branchId)
    {
        var db = campusDbContext.DbContext(_campus);
        var data = await db.TblBranch.Where(x => x.BranchId == branchId).FirstOrDefaultAsync();
        if (data == null)
        {
            return new ServerResponse().NotFound("Branch not found");
        }
        return new ServerResponse().Success(data);
    }

    [HttpPost("save-branch")]
    public async Task<IActionResult> Save([FromForm] BranchDto? branch)
    {
        try
        {
            if (branch == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }
            var db = campusDbContext.DbContext(_campus);
            if (branch.BranchId == 0)
            {
                var data = mapper.Map<BranchDto, Branch>(branch);
                await db.TblBranch.AddAsync(data);
                await db.SaveChangesAsync();
                return new ServerResponse().Success(data, "Saved successfully!");
            }
            var oldData = await db.TblBranch.Where(x => x.BranchId == branch.BranchId).FirstOrDefaultAsync();
            if(oldData == null) return new ServerResponse().NotFound("Branch not found");
            
            mapper.Map(branch, oldData);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(oldData, "Updated successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    
    [HttpDelete("delete/{branchId:int}")]
    public async Task<IActionResult> Delete(int branchId)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            var branch = db.TblBranch.FirstOrDefault(x => x.BranchId == branchId);

            if (branch == null)
            {
                return new ServerResponse().BadRequest("Branch not found!");
            }

            db.TblBranch.Remove(branch);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(branch, "Deleted successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
}