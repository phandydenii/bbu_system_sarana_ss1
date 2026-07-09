using BBU_SYSTEM.Data;
using BBU_SYSTEM.Repository;
using BBU_SYSTEM.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("payment")]
public class PaymentController(ICampusDbContext campusDbContext, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [Route("all-student-payment")]
    // GET: /<controller>/
    public async Task<IActionResult> Index()
    {
        var db = campusDbContext.DbContext(_campus);

        //var degrees = db.tbl_degree.ToList();
        //var schools = db.tbl_school.ToList();
        //var fields = db.tbl_field.ToList();
        //var promotions = db.tbl_promotion.ToList();
        //var stages = db.tbl_stage.ToList();
        //var terms = db.tbl_term.ToList();
        //var groups = db.tbl_group.ToList();
        //var groupRooms = db.tbl_group_room.ToList();
        //var rooms = db.tbl_room.ToList();
        //var study_Times = db.tbl_study_time.ToList();
        var students = await (from s in db.TblStudent
            select new StudentSearch
            {
                StudentId = s.StudentId,
                StudentName = s.StudentName,
                StudentNameInKhmer = s.StudentNameInKhmer
            }).OrderByDescending(x => x.StudentId).Take(500).ToListAsync();

        var viewmodel = new ListData
        {
            StudentSearches = students
            //degrees = degrees,
            //schools = schools,
            //fields = fields,
            //promotions = promotions,
            //stages = stages,
            //groups = groups,
            //groupRooms = groupRooms,
            //terms = terms,
            //rooms = rooms,
            //study_Times = study_Times
        };
        return View(viewmodel);
    }

    [Route("create-student-payment")]
    public IActionResult Create()
    {
        var db = campusDbContext.DbContext(_campus);
        ViewData["StudentStatusBadgeClasses"] = StudentStatusConstant.BadgeClasses;
        ViewData["StudentStatusDefaultBadgeClass"] = StudentStatusConstant.DefaultBadgeClass;
        var viewmodel = new PaymentViewModel
        {
            Products = db.TblProduct.ToList()
        };
        return View(viewmodel);
    }

    [HttpGet("student/{studentId}")]
    public IActionResult Student(string studentId)
    {
        var db = campusDbContext.DbContext(_campus);
        var student = db.TblStudent.FirstOrDefault(s => s.StudentId == studentId)!;
        var studentViewModel = new StudentViewModel
        {
            Student = student
        };
        return View(studentViewModel);
    }

    [HttpPost("get-student-payments/{studentId}")]
    public IActionResult GetStudentPayment(string studentId)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            //var campus = HttpContext.Session.GetString("campus");
            var db = campusDbContext.DbContext(_campus);
            var query = db.TblPayment.Where(x=>x.StudentId == studentId).AsQueryable();  
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
                data = new { },
                status = new
                {
                    code = "500",
                    message = e.Message
                }
            });
        }
    }
    
    [HttpPost("get-student-re-exam-payments/{studentId}")]
    public IActionResult GetStudentReExamPayment(string studentId)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            //var campus = HttpContext.Session.GetString("campus");
            var db = campusDbContext.DbContext(_campus);
            var query = db.TblReExamPayment.Where(x=>x.StudentId == studentId).AsQueryable();  
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
                data = new { },
                status = new
                {
                    code = "500",
                    message = e.Message
                }
            });
        }
    }
    
    [HttpPost("get-student-re-exam-payments-detail/{reExamPaymentId:int}")]
    public IActionResult GetStudentReExamPaymentDetail(int reExamPaymentId)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            //var campus = HttpContext.Session.GetString("campus");
            var db = campusDbContext.DbContext(_campus);
            var query = (from pd in db.TblReExamPaymentDetail
                join c in db.TblCourses on pd.CourseId equals c.CourseId
                where pd.StudentReexamPaymentId == reExamPaymentId
                select new
                {
                    pd.StudentReexamPaymentDetailId,
                    pd.StudentReexamPaymentId,
                    pd.TermNo,pd.Time,
                    c.CourseId,c.CourseFullName,c.CourseFullNameInKhmer
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
                data = new { },
                status = new
                {
                    code = "500",
                    message = e.Message
                }
            });
        }
    }
    
    [HttpPost("payment/getinvoicelist")]
    public IActionResult GetInvoiceList()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);
        var query = (from i in db.TblInvoice
            join s in db.TblStudent on i.StudentId equals s.StudentId
            select new
            {
                s.StudentId,
                s.StudentName,
                s.StudentNameInKhmer,
                s.Sex,
                s.DateOfBirth,
                s.Status,
                i.InvoiceId,
                i.InvoiceNo,
                i.InvoiceDate,
                i.DegreeId,
                i.FieldId,
                i.PromotionId,
                i.TermNo,
                i.GroupId
            }).AsQueryable();
        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(d =>
                d.StudentId!.Contains(searchValue) ||
                d.StudentName!.Contains(searchValue) ||
                d.StudentNameInKhmer!.Contains(searchValue)
            );
        query = query.OrderByDescending(x => x.InvoiceId).AsQueryable();
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

    [HttpPost("payment/getpaymentlist")]
    public IActionResult GetPaymentList(int termno, int groupid)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);
        var query = (from sg in db.TblStudentGroup
            join s in db.TblStudent on sg.StudentId equals s.StudentId
            join g in db.TblGroup on sg.GroupId equals g.GroupId
            join gr in db.TblGroupRoom on new { g.GroupId, sg.TermNo } equals new { gr.GroupId, gr.TermNo }
            join st in db.TblStage on g.StageId equals st.StageId
            join pr in db.TblPromotion on st.PromotionId equals pr.PromotionId
            join t in db.TblTerm on new { st.StageId, sg.TermNo } equals new { t.StageId, t.TermNo }
            join sc in db.TblSchool on pr.SchoolId equals sc.SchoolId
            join de in db.TblDegree on pr.DegreeId equals de.DegreeId
            join f in db.TblField on s.FieldId equals f.FieldId
            join pa in db.TblPayment on new { s.StudentId, sg.TermNo } equals new { pa.StudentId, pa.TermNo }
            where sg.TermNo == termno && sg.GroupId == groupid
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
                documentin = s.DocumentIn ?? "",
                documentout = s.DocumentOut ?? "",
                authenticated_no = s.AuthenticatedNo ?? "",
                s.IsAcceptCertificate,
                de.DegreeId,
                de.DegreeName,
                de.DegreeInKhmer,
                sc.SchoolId,
                sc.SchoolName,
                sc.SchoolNameInKhmer,
                f.FieldId,
                f.FieldName,
                f.FieldNameInKhmer,
                pr.PromotionId,
                pr.PromotionNo,
                st.StageId,
                st.StageNo,
                t.TermId,
                t.TermNo,
                start_term = t.StartDate,
                end_term = t.EndDate,
                g.GroupId,
                g.GroupName,
                gr.RoomName,
                gr.StartPayment,
                pa.PaymentId, pa.InvoiceDate, pa.InvoiceNo,
                pay_term = pa.TermNo, pa.Paid, pa.Deposit
            }).AsQueryable();

        var recordsTotal = query.Count();
        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(s => s.StudentName.Contains(searchValue)).AsQueryable();
        query = query.OrderByDescending(d => d.PromotionId);
        var data = query.Skip(skip).Take(pageSize).ToList();

        return Json(new
        {
            draw,
            recordsFiltered = recordsTotal,
            recordsTotal,
            data
        });
    }


    [HttpPost("payment/getpaymentnewlist")]
    public IActionResult GetPaymentNewList(int termno, int groupid)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);
        var query = (from sg in db.TblStudentGroup
            join s in db.TblStudent on sg.StudentId equals s.StudentId
            join g in db.TblGroup on sg.GroupId equals g.GroupId
            join gr in db.TblGroupRoom on new { g.GroupId, sg.TermNo } equals new { gr.GroupId, gr.TermNo }
            join st in db.TblStage on g.StageId equals st.StageId
            join pr in db.TblPromotion on st.PromotionId equals pr.PromotionId
            join t in db.TblTerm on new { st.StageId, sg.TermNo } equals new { t.StageId, t.TermNo }
            join sc in db.TblSchool on pr.SchoolId equals sc.SchoolId
            join de in db.TblDegree on pr.DegreeId equals de.DegreeId
            join f in db.TblField on s.FieldId equals f.FieldId
            join pa in db.TblPayment on new { s.StudentId, sg.TermNo } equals new { pa.StudentId, pa.TermNo }
            join i in db.TblInvoice on pa.StudentId equals i.StudentId
            join id in db.TblInvoiceDetail on i.InvoiceId equals id.InvoiceId
            join ca in db.TblCategory on id.CategoryId equals ca.CategoryId
            //where sg.term_no == termno && sg.group_id == groupid
            where ca.CategoryName == "Tution Fee"
                  && Convert.ToInt16(i.TermNo) == termno
                  && sg.TermNo == termno
                  && sg.GroupId == groupid
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
                documentin = s.DocumentIn ?? "",
                documentout = s.DocumentOut ?? "",
                authenticated_no = s.AuthenticatedNo ?? "",
                s.IsAcceptCertificate,
                de.DegreeId,
                de.DegreeName,
                de.DegreeInKhmer,
                sc.SchoolId,
                sc.SchoolName,
                sc.SchoolNameInKhmer,
                f.FieldId,
                f.FieldName,
                f.FieldNameInKhmer,
                pr.PromotionId,
                pr.PromotionNo,
                st.StageId,
                st.StageNo,
                t.TermId,
                t.TermNo,
                start_term = t.StartDate,
                end_term = t.EndDate,
                g.GroupId,
                g.GroupName,
                gr.RoomName,
                gr.StartPayment,
                pa.PaymentId, pa.InvoiceDate, pa.InvoiceNo,
                pay_term = pa.TermNo, pa.Paid, pa.Deposit
            }).AsQueryable();

        var recordsTotal = query.Count();
        //query = query.OrderByDescending(d => d.is_authenticated.);
        var data = query.Skip(skip).Take(pageSize).ToList();

        return Json(new
        {
            draw,
            recordsFiltered = recordsTotal,
            recordsTotal,
            data
        });
    }

    [HttpPost("payment/getnotpaymentnewlist")]
    public IActionResult GetNotPaymentNewList(int degreeid, int schoolid, int fieldid, int proid, int stageid,
        int termno, int groupid, string filter)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        var db = campusDbContext.DbContext(_campus);
        var query = (from sg in db.TblStudentGroup
            join s in db.TblStudent on sg.StudentId equals s.StudentId
            join g in db.TblGroup on sg.GroupId equals g.GroupId
            join gr in db.TblGroupRoom on new { g.GroupId, sg.TermNo } equals new { gr.GroupId, gr.TermNo }
            join st in db.TblStage on g.StageId equals st.StageId
            join pr in db.TblPromotion on st.PromotionId equals pr.PromotionId
            join t in db.TblTerm on new { st.StageId, sg.TermNo } equals new { t.StageId, t.TermNo }
            join sc in db.TblSchool on pr.SchoolId equals sc.SchoolId
            join de in db.TblDegree on pr.DegreeId equals de.DegreeId
            join f in db.TblField on s.FieldId equals f.FieldId
            join pa in db.TblPayment on new { s.StudentId, sg.TermNo } equals new { pa.StudentId, pa.TermNo }
                into studentPay
            from pa in studentPay.DefaultIfEmpty()
            where pa == null
                  && !db.TblScholarship.Any(x => x.StudentId == s.StudentId && x.TermNo == sg.TermNo)
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
                documentin = s.DocumentIn ?? "",
                documentout = s.DocumentOut ?? "",
                authenticated_no = s.AuthenticatedNo ?? "",
                s.IsAcceptCertificate,
                de.DegreeId,
                de.DegreeName,
                de.DegreeInKhmer,
                sc.SchoolId,
                sc.SchoolName,
                sc.SchoolNameInKhmer,
                f.FieldId,
                f.FieldName,
                f.FieldNameInKhmer,
                pr.PromotionId,
                pr.PromotionNo,
                st.StageId,
                st.StageNo,
                t.TermId,
                t.TermNo,
                start_term = t.StartDate,
                end_term = t.EndDate,
                g.GroupId,
                g.GroupName,
                gr.RoomName,
                gr.StartPayment,
                pa.PaymentId, pa.InvoiceDate, pa.InvoiceNo,
                pay_term = pa.TermNo, pa.Paid, pa.Deposit,
                duration = EF.Functions.DateDiffDay(t.StartDate, DateTime.Now)
            }).AsQueryable();
        query = query.Where(x => x.DegreeId == degreeid && x.SchoolId == schoolid && x.PromotionId == proid)
            .AsQueryable();
        query = query.Where(x => x.Status == "ACTIVE").AsQueryable();
        if (fieldid != 0) query = query.Where(x => x.FieldId == fieldid).AsQueryable();
        if (stageid != 0) query = query.Where(x => x.StageId == stageid).AsQueryable();
        if (groupid != 0 && termno != 0)
            query = query.Where(x => x.GroupId == groupid && x.TermNo == termno).AsQueryable();
        if (!string.IsNullOrEmpty(filter))
        {
            var from = Convert.ToInt32(filter.Split("-")[0]);
            var to = Convert.ToInt32(filter.Split("-")[1]);
            query = to == 0
                ? query.Where(x => x.duration >= from).AsQueryable()
                : query.Where(x => x.duration >= from && x.duration <= to).AsQueryable();
        }

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