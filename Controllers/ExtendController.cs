using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("extend")]
public class ExtendController(ICampusDbContext campusDbContext, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-student-extend/{studentId}")]
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
            // var query = db.TblExtend.Where(x=>x.StudentId == studentId).AsQueryable();
            var query =
                from e in db.TblExtend

                // LEFT JOIN Branch
                join b in db.TblBranch
                    on e.FromId equals b.BranchId into bb
                from b in bb.DefaultIfEmpty()

                // LEFT JOIN University
                join u in db.TblUsersity
                    on e.FromId equals u.UniversityId into uu
                from u in uu.DefaultIfEmpty()

                where e.StudentId == studentId

                select new
                {
                    e.ExtendId,
                    e.ExtendDate,
                    e.ExtendFrom,
                    e.StudentId,
                    e.TermNo,
                    e.FromId,
                    e.IsCertificateReceived,
                    e.IsTranscriptReceived,
                    From = e.ExtendFrom == "OTHER_UNIVERSITY"
                        ? u.UniversityName
                        : b.BranchName
                };

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

    [HttpPost("students")]
    public IActionResult GetExtends()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        var db = campusDbContext.DbContext(_campus);

        var query = (from s in db.TblStudent
                join e in db.TblExtend on s.StudentId equals e.StudentId
                
                // LEFT JOIN Branch
                join b in db.TblBranch
                    on e.FromId equals b.BranchId into bb
                from b in bb.DefaultIfEmpty()

                // LEFT JOIN University
                join u in db.TblUsersity
                    on e.FromId equals u.UniversityId into uu
                from u in uu.DefaultIfEmpty()
                
                
                select new
                {
                    s.StudentId,
                    s.StudentName,
                    s.StudentNameInKhmer,
                    s.Sex,
                    s.DateOfBirth,
                    s.Phone,
                    s.Email,
                    s.Status,
                    e.ExtendId,
                    e.ExtendDate,
                    e.ExtendFrom, 
                    e.TermNo,
                    e.FromId,
                    e.IsCertificateReceived,
                    e.IsTranscriptReceived,
                    From = e.ExtendFrom == "OTHER_UNIVERSITY"
                        ? u.UniversityName
                        : b.BranchName
                }
            ).AsQueryable();
        
        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(x =>
                x.StudentName!.Contains(searchValue) 
                || x.StudentId!.Contains(searchValue) 
                || x.StudentNameInKhmer!.Contains(searchValue));
        var recordsTotal = query.Count();
        query = query.OrderByDescending(d => d.StudentId ?? "");
        var data = query.Skip(skip).Take(pageSize).ToList();

        return Json(new
        {
            draw,
            recordsFiltered = recordsTotal,
            recordsTotal,
            data
        });
    }
}