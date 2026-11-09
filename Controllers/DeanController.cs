using BBU_SYSTEM.Repository;
using BBU_SYSTEM.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("dean")]
public class DeanController(ICampusDbContext campusDbContext, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [Route("course")]
    public async Task<IActionResult> CourseAsync()
    {
        var db = campusDbContext.DbContext(_campus); 
        ViewBag.TotalSchools = await db.TblSchool.CountAsync(); 
        return View();
    }

    [Route("learner")]
    public ActionResult Learner()
    {
        return View();
    }

    [Route("mark")]
    public ActionResult Mark()
    {
        var db = campusDbContext.DbContext(_campus);
        var degrees = db.TblDegree.ToList();
        var schools = db.TblSchool.ToList();
        var fields = db.TblField.ToList();
        var promotions = db.TblPromotion.ToList();
        var stages = db.TblStage.ToList();
        var terms = db.TblTerm.ToList();
        var groups = db.TblGroup.ToList();
        var groupRooms = db.TblGroupRoom.ToList();

        var viewmodel = new ListData
        {
            Degrees = degrees,
            Schools = schools,
            Fields = fields,
            Promotions = promotions,
            Stages = stages,
            Groups = groups,
            GroupRooms = groupRooms,
            Terms = terms
        };
        return View(viewmodel);
    }

    [Route("practicum-mark")]
    public ActionResult PracticumMark()
    {
        var db = campusDbContext.DbContext(_campus);
        var degrees = db.TblDegree.ToList();
        var schools = db.TblSchool.ToList();
        var fields = db.TblField.ToList();
        var promotions = db.TblPromotion.ToList();
        var stages = db.TblStage.ToList();
        var terms = db.TblTerm.ToList();
        var groups = db.TblGroup.ToList();
        var groupRooms = db.TblGroupRoom.ToList();

        var viewmodel = new ListData
        {
            Degrees = degrees,
            Schools = schools,
            Fields = fields,
            Promotions = promotions,
            Stages = stages,
            Groups = groups,
            GroupRooms = groupRooms,
            Terms = terms
        };
        return View(viewmodel);
    }

    [Route("state-exam-mark")]
    public ActionResult StateExamMark()
    {
        var db = campusDbContext.DbContext(_campus);
        var degrees = db.TblDegree.ToList();
        var schools = db.TblSchool.ToList();
        var fields = db.TblField.ToList();
        var promotions = db.TblPromotion.ToList();
        var stages = db.TblStage.ToList();
        var terms = db.TblTerm.ToList();
        var groups = db.TblGroup.ToList();
        var groupRooms = db.TblGroupRoom.ToList();

        var viewmodel = new ListData
        {
            Degrees = degrees,
            Schools = schools,
            Fields = fields,
            Promotions = promotions,
            Stages = stages,
            Groups = groups,
            GroupRooms = groupRooms,
            Terms = terms
        };
        return View(viewmodel);
    }

    [Route("re-study-mark")]
    public ActionResult ReStudyMark()
    {
        var db = campusDbContext.DbContext(_campus);
        var students = (from s in db.TblStudent
            select new StudentSearch
            {
                StudentId = s.StudentId,
                StudentName = s.StudentName,
                StudentNameInKhmer = s.StudentNameInKhmer
            }).OrderByDescending(x => x.StudentId).Take(500).ToList();

        var viewmodel = new ListData
        {
            StudentSearches = students
        };
        return View(viewmodel);
    }

    [Route("thesis-mark")]
    public ActionResult ThesisMark()
    {
        var db = campusDbContext.DbContext(_campus);
        var degrees = db.TblDegree.ToList();
        var schools = db.TblSchool.ToList();
        var fields = db.TblField.ToList();
        var promotions = db.TblPromotion.ToList();
        var stages = db.TblStage.ToList();
        var terms = db.TblTerm.ToList();
        var groups = db.TblGroup.ToList();
        var groupRooms = db.TblGroupRoom.ToList();

        var viewmodel = new ListData
        {
            Degrees = degrees,
            Schools = schools,
            Fields = fields,
            Promotions = promotions,
            Stages = stages,
            Groups = groups,
            GroupRooms = groupRooms,
            Terms = terms
        };
        return View(viewmodel);
    }
}