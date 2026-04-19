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
[Route("promotion")]
public class PromotionController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-promotions")]
    public IActionResult GetPromotions(int degreeId = 0, int schoolId = 0, bool isAll = false)
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
            var query = db.TblPromotion.AsQueryable();

            if (degreeId > 0) query = query.Where(x => x.DegreeId == degreeId).AsQueryable();

            if (schoolId > 0) query = query.Where(x => x.SchoolId == schoolId).AsQueryable();
            if (isAll)
                return new ServerResponse().Success(query.ToList());
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.PromotionNo == int.Parse(searchValue) ||
                    d.PromotionId == int.Parse(searchValue));

            var recordsTotal = query.Count();
            query = query.Distinct().OrderByDescending(d => d.PromotionId);
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

    [HttpPost("get-last-promotion")]
    public async Task<IActionResult> GetLastPromotion()
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var data = await db.TblPromotion.OrderByDescending(x => x.PromotionId).FirstOrDefaultAsync();
            return new ServerResponse().Success(data);
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("get-academic-year-start")]
    public async Task<IActionResult> GetAcademicYearStarts()
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var terms = db.TblTerm.Where(x => x.TermNo == 2).AsQueryable();
            var stages = db.TblStage.Where(x => terms.Any(t => t.StageId == x.StageId)).AsQueryable();
            var school = db.TblSchool.FirstOrDefault(x => x.IsFoundationSchool == 1);
            var query = db.TblPromotion.Where(p => stages.Any(s => s.PromotionId == p.PromotionId) && p.SchoolId==school!.SchoolId)
                .OrderByDescending(x => x.PromotionId)
                .Select(x => x.AcademicYearStart)
                .Distinct()
                .AsQueryable();
            var data = await query.ToListAsync();
            return new ServerResponse().Success(data);
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("save-change")]
    public async Task<IActionResult> SaveChange(PromotionDto promotion)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            promotion.Status = "ACTIVE";
            if (promotion.PromotionId > 0)
            {
                var dataUpdate = await db.TblPromotion.Where(x => x.PromotionId == promotion.PromotionId)
                    .FirstOrDefaultAsync();
                if (dataUpdate == null)
                {
                    return new ServerResponse().BadRequest("Promotion not found");
                }

                mapper.Map(promotion, dataUpdate);
                db.TblPromotion.Update(dataUpdate);
                await db.SaveChangesAsync();
                return new ServerResponse().Success(dataUpdate);
            }

            var data = mapper.Map<PromotionDto, Promotion>(promotion);
            await db.TblPromotion.AddAsync(data);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(promotion);
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }
}