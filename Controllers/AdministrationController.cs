using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using BBU_SYSTEM.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("administration")]
public class AdministrationController(
    ICampusDbContext campusDbContext,
    // IMapper mapper,
    IHttpContextAccessor context)
    : Controller
{
    // private readonly IMapper _mapper = mapper;
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [Route("admin-letter")]
    public IActionResult AdminLetter()
    {
        return View();
    }

    [Route("booking-clothes")]
    public ActionResult BookingClothes()
    {
        return View();
    }

    [Route("category-letters")]
    public ActionResult CategoryLetter()
    {
        return View();
    }

    [Route("graduate-certificate")]
    public IActionResult GraduateCertificate()
    {
        return View();
    }

    [Route("student-payment")]
    public ActionResult Payment()
    {
        return View();
    }

    [Route("administration-user")]
    public ActionResult Users()
    {
        var db = campusDbContext.DbContext(_campus);
        var data = db.TblUser.Where(x => x.Status!.ToLower() == "enabled" && x.UserGroup!.ToLower() == "admin")
            .AsQueryable();
        var viewmodel = new UserViewModel
        {
            Users = data.ToList()
        };
        return View(viewmodel);
    }

    [Route("student-discount")]
    public ActionResult Discount()
    {
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);
        //var discountStudentIds = db.tbl_student_discount
        //                  .Select(d => d.student_id)
        //                  .Distinct()
        //                  .ToList();

        //// Filter students who are in the discount table
        //var studentsInDiscount = db.tbl_student
        //                           .Where(s => discountStudentIds.Contains(s.student_id))
        //                           .ToList();
        var data = (from s in db.TblStudent
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
            ).Distinct().ToList();
        return View(data);
    }
}