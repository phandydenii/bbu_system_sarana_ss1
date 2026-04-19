using AutoMapper;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;
 
[Authorize]
[Route("student-suspend")]
public class SuspendController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";


    [HttpPost("get-student-suspend/{studentId}")]
    public IActionResult Gets(string studentId)
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
            // var query = db.TblSuspend.Where(x=>x.StudentId == studentId).AsQueryable();
            var query = (from s in db.TblSuspend
                join g in db.TblGroup on s.GroupId equals g.GroupId
                join p in db.TblPromotion on s.PromotionId equals p.PromotionId
                where s.StudentId == studentId
                select new
                {
                    s.SuspendId,
                    s.StudentId,
                    s.GroupId,
                    s.TermNo,
                    s.PromotionId,
                    s.FromDate,
                    s.ToDate,
                    s.ReasonOfSuspend,
                    g.GroupName,
                    p.PromotionNo
                }).AsQueryable();
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
                code = "500",
                message = $"Internal Server Error:{e.Message}"
            });
        }
    }
}