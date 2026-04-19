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
[Route("daily-report")]
public class DailyReportController(
    ICampusDbContext campusDbContext,
    ILogger<DailyReportController> logger,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper)
    : Controller
{
    private readonly string _campus = httpContextAccessor.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("gets")]
    public IActionResult GetDailyReports()
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
            var query = db.TblDailyReport
                .Select(dr => new
                {
                    dr.Id,
                    dr.Title,
                    TitleKhmer = dr.TitleKhmer ?? "",
                    dr.Campus,
                    Description = dr.Description ?? "",
                    dr.CreateDate,
                    dr.RequestDate,
                    dr.ReportDate,
                    Images = db.TblDailyReportImages
                        .Where(img => img.ReportId == dr.Id)
                        .Select(img => img.ImageId)
                        .ToList()
                })
                .AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.Title!.Contains(searchValue) ||
                    d.Description!.Contains(searchValue));

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
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("SaveChange")]
    public async Task<IActionResult> SaveChange([FromForm] DailyReportDto dailyReport)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            if (dailyReport.Id == 0)
            {
                // dailyReport.CreateDate = DateTime.Now;
                var data = mapper.Map<DailyReportDto, DailyReport>(dailyReport);
                await db.TblDailyReport.AddAsync(data);
            }
            else
            {
                var data = await db.TblDailyReport.Where(x => x.Id == dailyReport.Id).FirstOrDefaultAsync();
                if (data == null)
                    return BadRequest(new
                    {
                        code = "400",
                        message = "Bad Request!"
                    });
                mapper.Map(dailyReport, data);
                db.TblDailyReport.Update(data);
            }

            await db.SaveChangesAsync();

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

    [HttpPost("save-changes")]
    public async Task<IActionResult> Save(DailyReportDto dailyReportDto, List<IFormFile> images)
    {
        var db = campusDbContext.DbContext(_campus);
        try
        {
            await db.Database.BeginTransactionAsync();
            //1-Save report
            dailyReportDto.CreateDate = DateTime.Now;
            var dailyReport = mapper.Map<DailyReportDto, DailyReport>(dailyReportDto);
            db.TblDailyReport.Add(dailyReport);
            await db.SaveChangesAsync();
            // 2. Save images
            foreach (var file in images)
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","Files");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                // Generate unique file name
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadPath,fileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                db.TblDailyReportImages.Add(new DailyReportImages
                {
                    ReportId = dailyReport.Id,
                    ImageId = Path.Combine("Files", fileName)
                });
                
            }

            await db.SaveChangesAsync();
            await db.Database.CommitTransactionAsync();
            return new ServerResponse().Success();
        }
        catch (Exception e)
        {
            await db.Database.RollbackTransactionAsync();
            return new ServerResponse().ErrorInternal(e);
        }
    }
}