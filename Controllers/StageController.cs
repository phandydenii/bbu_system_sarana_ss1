using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("stage")]
public class StageController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-stages")]
    public IActionResult GetStages(int promotionId = 0, bool isAll = false)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? int.Parse(length) != -1 ? Convert.ToInt32(length) : 10 : 10;
            var skip = start != null ? Convert.ToInt32(start) : 0;

            // var pageSize = length != null ? Convert.ToInt32(length) : 0;
            // var skip = start != null ? Convert.ToInt32(start) : 0;

            var db = campusDbContext.DbContext(_campus);
            var query = db.TblStage.AsQueryable();
            if (isAll)
                return new ServerResponse().Success(query.ToList());

            if (promotionId > 0) query = query.Where(x => x.PromotionId == promotionId).AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.StageNo == int.Parse(searchValue) ||
                    d.StageId == int.Parse(searchValue));
            var recordsTotal = query.Count();
            query = query.OrderBy(d => d.StageId);
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
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("save-change")]
    public async Task<IActionResult> CreatePromotion(StageDto stage)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus); 
            stage.Status = "ACTIVE";
            var data = mapper.Map<StageDto, Stage>(stage);
            await db.TblStage.AddAsync(data);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(stage);
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    
    [HttpDelete("delete/{stageId:int}")]
    public async Task<IActionResult> Delete(int stageId)
    {
        var db = campusDbContext.DbContext(_campus); 
        try
        {
            var data = db.TblStage.FirstOrDefault(x => x.StageId == stageId);
            if (data == null)
            {
                return new ServerResponse().NotFound("Stage not found");
            }
            db.TblStage.Remove(data);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(data);
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }
}