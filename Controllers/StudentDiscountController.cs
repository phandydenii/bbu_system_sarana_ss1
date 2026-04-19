using AutoMapper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BBU_SYSTEM.Controllers;

[Authorize]
public class StudentDiscountController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("student-discount/gets")]
    public IActionResult GetStudentDiscounts()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);

        var students = db.TblStudent.AsQueryable();
        var query = (from s in students
                join sd in db.TblStudentDiscount on s.StudentId equals sd.StudentId
                select new Student
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentName,
                    StudentNameInKhmer = s.StudentNameInKhmer,
                    Sex = s.Sex,
                    DateOfBirth = s.DateOfBirth,
                    Phone = s.Phone,
                    Address = s.Address,
                    Email = s.Email
                }
            ).Distinct().AsQueryable();
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
}