using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("summary-report")]
public class SummaryReportController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("gets")]
    public IActionResult Gets(bool isAll = false)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query = db.TblSummaryReport.AsQueryable();
            query = query.OrderBy(x => x.Ordering);
            if (isAll)
            {
                query = query.Where(x => x.Show == true).AsQueryable();
                return Ok(new
                {
                    data = query.ToList(),
                    code = "200",
                    message = "Succeeded!"
                });
            }

            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.TitleEng!.Contains(searchValue) ||
                    d.TitleKm!.Contains(searchValue) ||
                    d.Id.ToString().Contains(searchValue));

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.Id);
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

    [HttpPost("save-changes")]
    public async Task<IActionResult> PostS(SummaryReportDto summaryReport)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var dataExist =await db.TblSummaryReport.FirstOrDefaultAsync(x => x.Id == summaryReport.Id);
            
            var dataOrders = await db.TblSummaryReport.Where(x => x.Ordering >= summaryReport.Ordering).OrderBy(x=>x.Ordering).ToListAsync();
            var orderNum = summaryReport.Ordering + 1;
            foreach (var o in dataOrders)
            {
                o.Ordering = orderNum;
                db.TblSummaryReport.Update(o);
                await db.SaveChangesAsync();
                orderNum += 1;
            }
            
            if (dataExist != null)
            {
                mapper.Map(summaryReport, dataExist);
                db.TblSummaryReport.Update(dataExist);
                await db.SaveChangesAsync();
                return Ok(new
                {
                    data = summaryReport,
                    status = new
                    {
                        code = "200",
                        message = "Succeeded!"
                    }
                });
            }
            
            var data = mapper.Map<SummaryReportDto, SummaryReport>(summaryReport);
            await db.TblSummaryReport.AddAsync(data);
            await db.SaveChangesAsync();
            return Ok(new
            {
                data = summaryReport,
                status = new
                {
                    code = "200",
                    message = "Succeeded!"
                }
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                data = new{},
                status = new
                {
                    code = "500",
                    message = $"Internal Server Error:{e.InnerException!.Message}"
                }
            });
        }
    }

    [HttpPost("save-change")]
    public async Task<IActionResult> Post(SummaryReportDto summaryReport)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            using var transaction = await db.Database.BeginTransactionAsync();

            // UPDATE
            var dataExist = await db.TblSummaryReport
                .FirstOrDefaultAsync(x => x.Id == summaryReport.Id);

            if (dataExist != null)
            {
                int oldOrder = dataExist.Ordering;
                int newOrder = summaryReport.Ordering;

                if (newOrder != oldOrder)
                {
                    if (newOrder < oldOrder)
                    {
                        // 🔼 move UP → shift others DOWN
                        var shiftUp = await db.TblSummaryReport
                            .Where(x => x.Ordering >= newOrder &&
                                        x.Ordering < oldOrder &&
                                        x.Id != dataExist.Id)
                            .ToListAsync();

                        foreach (var item in shiftUp)
                            item.Ordering += 1;
                    }
                    else
                    {
                        // 🔽 move DOWN → shift others UP
                        var shiftDown = await db.TblSummaryReport
                            .Where(x => x.Ordering <= newOrder &&
                                        x.Ordering > oldOrder &&
                                        x.Id != dataExist.Id)
                            .ToListAsync();

                        foreach (var item in shiftDown)
                            item.Ordering -= 1;
                    }
                }

                // update current record
                mapper.Map(summaryReport, dataExist);
                db.TblSummaryReport.Update(dataExist);

                await db.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new
                {
                    data = summaryReport,
                    status = new
                    {
                        code = "200",
                        message = "Updated successfully!"
                    }
                });
            }

            // INSERT
            int maxOrder = await db.TblSummaryReport
                .MaxAsync(x => (int?)x.Ordering) ?? 0;

            var data = mapper.Map<SummaryReport>(summaryReport);
            data.Ordering = maxOrder + 1; // add to bottom

            await db.TblSummaryReport.AddAsync(data);
            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new
            {
                data = summaryReport,
                status = new
                {
                    code = "200",
                    message = "Created successfully!"
                }
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                data = new { },
                status = new
                {
                    code = "500",
                    message = $"Internal Server Error: {e.InnerException?.Message ?? e.Message}"
                }
            });
        }
    }
}