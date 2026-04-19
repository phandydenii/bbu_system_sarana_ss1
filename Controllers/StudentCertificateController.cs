using AutoMapper;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;
[Authorize]
[Route("student-certificate")]
public class StudentCertificateController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-student-certificate/{studentId}")]
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
            // var query = db.TblStudentCertificate.Where(x=>x.StudentId == studentId).AsQueryable();
            var query = (from sc in db.TblStudentCertificate
                join c in db.TblCertificate on sc.CertificateId equals c.CertificateId
                where sc.StudentId == studentId
                select new
                {
                    sc.StudentCertificateId,
                    sc.StudentId,
                    sc.CertificateId,
                    sc.CertificateIssueNo,
                    sc.Grade,
                    sc.IsReceived,
                    c.CertificateName,
                    c.CertificateCode
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