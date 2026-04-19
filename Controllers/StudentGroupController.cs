using AutoMapper;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;
 
[Authorize]
[Route("student-group")]
public class StudentGroupController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";


    [HttpPost("get-student-group/{studentId}")]
    public IActionResult Gets(bool isAll = false,string studentId="")
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            // var query = db.TblStudentGroup.Where(x=>x.StudentId == studentId).AsQueryable();
            var query = (from sg in db.TblStudentGroup
                join g in db.TblGroup on sg.GroupId equals g.GroupId
                where sg.StudentId == studentId
                select new
                {
                    sg.StudentGroupId,
                    sg.StudentId,
                    sg.TermNo,
                    g.GroupId,
                    g.GroupName,
                }).AsQueryable();
            if (isAll)
            {
                return new ServerResponse().Success(query.ToList());
            }
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;

            
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