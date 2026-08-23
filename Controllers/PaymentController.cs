using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
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
            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
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
            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
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
            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
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
    
    [HttpPost("save-re-exam-payment-detail")]
    public async Task<IActionResult> SaveReExamPaymentDetail([FromForm] StudentReexamPaymentDetailDto? detail)
    {
        try
        {
            if (detail == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }

            var db = campusDbContext.DbContext(_campus);

            var existingDetail = await db.TblReExamPaymentDetail
                .FirstOrDefaultAsync(x => x.StudentReexamPaymentDetailId == detail.StudentReexamPaymentDetailId);

            if (existingDetail != null)
            {
                existingDetail.CourseId = detail.CourseId;
                existingDetail.TermNo = detail.TermNo;
                existingDetail.Time = detail.Time;

                await db.SaveChangesAsync();

                return new ServerResponse().Success(existingDetail, "Updated successfully!");
            }

            db.TblReExamPaymentDetail.Add(new()
            {
                StudentReexamPaymentId = detail.StudentReexamPaymentId,
                CourseId = detail.CourseId,
                TermNo = detail.TermNo,
                Time = detail.Time
            });

            await db.SaveChangesAsync();

            return new ServerResponse().Success(detail, "Saved successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    
    [HttpPost("/payment/get-courses")]
    public IActionResult GetCourses()
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            var courses = db.TblCourses
                .OrderBy(x => x.CourseFullName)
                .Select(x => new
                {
                    courseId = x.CourseId,
                    courseFullName = x.CourseFullName,
                    courseFullNameInKhmer = x.CourseFullNameInKhmer
                })
                .ToList();

            return new ServerResponse().Success(courses, "Succeeded!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    
    [HttpDelete("/payment/delete-re-exam-payment-detail/{id:int}")]
    public async Task<IActionResult> DeleteReExamPaymentDetail(int id)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            var detail = await db.TblReExamPaymentDetail
                .FirstOrDefaultAsync(x => x.StudentReexamPaymentDetailId == id);

            if (detail == null)
            {
                return new ServerResponse().NotFound("Re-exam subject not found.");
            }

            db.TblReExamPaymentDetail.Remove(detail);
            await db.SaveChangesAsync();

            return new ServerResponse().Success(detail, "Deleted successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    
    [HttpPost("payment/getinvoicelist")]
    public IActionResult GetInvoiceList()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue =
            Request.Form["search[value]"].FirstOrDefault();

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
        var searchValue =
            Request.Form["search[value]"].FirstOrDefault();

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
            join i in db.TblInvoice on pa.StudentId equals i.StudentId
            join id in db.TblInvoiceDetail on i.InvoiceId equals id.InvoiceId
            join ca in db.TblCategory on id.CategoryId equals ca.CategoryId
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

    [HttpPost("save-invoice")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveInvoice(
        [FromForm] InvoiceDto? invoice,
        [FromForm] List<InvoiceDetailDto>? invoiceDetails)
    {
        if (invoice == null)
            return new ServerResponse().BadRequest("Invoice data is required.");

        if (string.IsNullOrWhiteSpace(invoice.StudentId))
            return new ServerResponse().BadRequest("Please select a student.");

        if (invoiceDetails == null || invoiceDetails.Count == 0)
            return new ServerResponse().BadRequest("Please add at least one payment item.");

        var db = campusDbContext.DbContext(_campus);
        await using var transaction = await db.Database.BeginTransactionAsync(
            System.Data.IsolationLevel.Serializable);

        try
        {
            var studentExists = await db.TblStudent
                .AnyAsync(x => x.StudentId == invoice.StudentId);

            if (!studentExists)
                return new ServerResponse().NotFound("Student not found.");
            
            var academic = await (
                from studentGroup in db.TblStudentGroup
                join student in db.TblStudent
                    on studentGroup.StudentId equals student.StudentId
                join groupRow in db.TblGroup
                    on studentGroup.GroupId equals groupRow.GroupId
                join stage in db.TblStage
                    on groupRow.StageId equals stage.StageId
                join promotion in db.TblPromotion
                    on stage.PromotionId equals promotion.PromotionId
                join term in db.TblTerm
                    on new
                    {
                        stage.StageId,
                        studentGroup.TermNo
                    }
                    equals new
                    {
                        term.StageId,
                        term.TermNo
                    }
                where studentGroup.StudentId == invoice.StudentId
                orderby term.EndDate descending,
                    term.StartDate descending,
                    studentGroup.TermNo descending,
                    studentGroup.GroupId descending
                select new
                {
                    DegreeId = promotion.DegreeId,
                    SchoolId = promotion.SchoolId,
                    FieldId = student.FieldId,
                    PromotionId = promotion.PromotionId,
                    StageId = stage.StageId,
                    TermNo = studentGroup.TermNo,
                    GroupId = groupRow.GroupId,
                    StartDate = term.StartDate,
                    EndDate = term.EndDate
                }
            ).FirstOrDefaultAsync();

            if (academic == null)
            {
                return new ServerResponse().BadRequest(
                    "The selected student has no academic group/term information.");
            }
            invoice.DegreeId = academic.DegreeId.ToString();
            invoice.SchoolId = academic.SchoolId.ToString();
            invoice.FieldId = academic.FieldId?.ToString();
            invoice.PromotionId = academic.PromotionId.ToString();
            invoice.StageId = academic.StageId.ToString();
            invoice.TermNo = academic.TermNo.ToString();
            invoice.GroupId = academic.GroupId.ToString();
            invoice.StartDate = academic.StartDate;
            invoice.EndDate = academic.EndDate;
            
            var invoiceVat = invoiceDetails
                .Select(detail => GetPropertyValue(
                    detail,
                    nameof(InvoiceDetailDto.Vat)))
                .Where(value => value != null)
                .Select(value => Convert.ToDecimal(value))
                .DefaultIfEmpty(0m)
                .Max();

            SetPropertyValue(
                invoice,
                nameof(InvoiceDto.Vat),
                invoiceVat);

            var isNew = invoice.InvoiceId <= 0;
            var invoiceEntity = isNew
                ? db.TblInvoice.Add(new()).Entity
                : await db.TblInvoice.FirstOrDefaultAsync(
                    x => x.InvoiceId == invoice.InvoiceId);

            if (invoiceEntity == null)
                return new ServerResponse().NotFound("Invoice not found.");

            if (isNew)
            {
                var invoiceDate = invoice.InvoiceDate ?? DateTime.Now;
                var yearNumber = string.IsNullOrWhiteSpace(invoice.YearNumber)
                    ? invoiceDate.Year.ToString()
                    : invoice.YearNumber.Trim();

                if (!invoice.InvoiceNo.HasValue ||
                    invoice.InvoiceNo.Value <= 0)
                {
                    var lastInvoiceNo = await db.TblInvoice
                        .Where(x => x.YearNumber == yearNumber)
                        .Select(x => (int?)x.InvoiceNo)
                        .MaxAsync() ?? 0;

                    invoice.InvoiceNo = lastInvoiceNo + 1;
                }

                invoice.InvoiceDate = invoiceDate;
                invoice.YearNumber = yearNumber;
            }
            else
            {
                var invoiceDate =
                    invoice.InvoiceDate ??
                    invoiceEntity.InvoiceDate ??
                    DateTime.Now;

                var yearNumber = !string.IsNullOrWhiteSpace(
                    invoice.YearNumber)
                    ? invoice.YearNumber.Trim()
                    : !string.IsNullOrWhiteSpace(invoiceEntity.YearNumber)
                        ? invoiceEntity.YearNumber.Trim()
                        : invoiceDate.Year.ToString();

                var invoiceNo =
                    invoice.InvoiceNo ??
                    invoiceEntity.InvoiceNo;

                if (!invoiceNo.HasValue || invoiceNo.Value <= 0)
                {
                    var lastInvoiceNo = await db.TblInvoice
                        .Where(x =>
                            x.YearNumber == yearNumber &&
                            x.InvoiceId != invoice.InvoiceId)
                        .Select(x => (int?)x.InvoiceNo)
                        .MaxAsync() ?? 0;

                    invoiceNo = lastInvoiceNo + 1;
                }

                invoice.InvoiceNo = invoiceNo;
                invoice.InvoiceDate = invoiceDate;
                invoice.YearNumber = yearNumber;
            }

            CopyMatchingProperties(
                invoice,
                invoiceEntity,
                nameof(InvoiceDto.InvoiceId));

            if (isNew)
                await db.SaveChangesAsync();

            var invoiceId = (int)(
                GetPropertyValue(invoiceEntity, "InvoiceId") ?? 0);

            if (!isNew)
            {
                var oldDetails = await db.TblInvoiceDetail
                    .Where(x => x.InvoiceId == invoiceId)
                    .ToListAsync();

                db.TblInvoiceDetail.RemoveRange(oldDetails);
            }

            foreach (var detail in invoiceDetails)
            {
                var detailEntity =
                    db.TblInvoiceDetail.Add(new()).Entity;

                CopyMatchingProperties(
                    detail,
                    detailEntity,
                    nameof(InvoiceDetailDto.InvoiceDetailId),
                    nameof(InvoiceDetailDto.InvoiceId));
                
                CopyAliasedProperty(
                    detail, "Type",
                    detailEntity, "QtyNote");
                CopyAliasedProperty(
                    detail, "PriceUsd",
                    detailEntity, "Price");
                CopyAliasedProperty(
                    detail, "DiscountUsd",
                    detailEntity, "Discount");
                CopyAliasedProperty(
                    detail, "OweUsd",
                    detailEntity, "Owe");
                CopyAliasedProperty(
                    detail, "OtherUsd",
                    detailEntity, "Other");
                CopyAliasedProperty(
                    detail, "PayKhr",
                    detailEntity, "PRiel");
                CopyAliasedProperty(
                    detail, "PayUsd",
                    detailEntity, "PDollar");
                CopyAliasedProperty(
                    detail, "PayBath",
                    detailEntity, "PBath");

                SetPropertyValue(
                    detailEntity,
                    "InvoiceId",
                    invoiceId);
            }

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return new ServerResponse().Success(new
            {
                invoiceId,
                isNew
            }, isNew
                ? "Payment created successfully!"
                : "Payment updated successfully!");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpGet("invoice/{invoiceId:int}")]
    public async Task<IActionResult> GetInvoice(int invoiceId)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            var invoice = await db.TblInvoice
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.InvoiceId == invoiceId);

            if (invoice == null)
                return new ServerResponse().NotFound("Invoice not found.");

            var detailRows = await (
                from detail in db.TblInvoiceDetail.AsNoTracking()
                join product in db.TblProduct.AsNoTracking()
                    on detail.ProductId equals product.ProductId
                where detail.InvoiceId == invoiceId
                select new
                {
                    Detail = detail,
                    Product = product
                }
            ).ToListAsync();
            
            var details = detailRows.Select(row =>
            {
                var qty = GetDecimalPropertyValue(
                    row.Detail, "Qty");

                var priceKhr = GetDecimalPropertyValue(
                    row.Detail, "PriceKhr");
                if (priceKhr == 0)
                {
                    priceKhr = GetDecimalPropertyValue(
                        row.Product, "PriceKhr");
                }

                var priceUsd = GetDecimalPropertyValue(
                    row.Detail, "Price", "PriceUsd");
                if (priceUsd == 0)
                {
                    priceUsd = GetDecimalPropertyValue(
                        row.Product, "Price", "PriceUsd");
                }

                var discountKhr = GetDecimalPropertyValue(
                    row.Detail, "DiscountKhr");
                var discountUsd = GetDecimalPropertyValue(
                    row.Detail, "Discount", "DiscountUsd");
                var oweKhr = GetDecimalPropertyValue(
                    row.Detail, "OweKhr");
                var oweUsd = GetDecimalPropertyValue(
                    row.Detail, "Owe", "OweUsd");
                var otherKhr = GetDecimalPropertyValue(
                    row.Detail, "OtherKhr");
                var otherUsd = GetDecimalPropertyValue(
                    row.Detail, "Other", "OtherUsd");

                var grandTotalKhr = qty * priceKhr;
                var grandTotalUsd = qty * priceUsd;
                var totalKhr =
                    grandTotalKhr -
                    discountKhr -
                    oweKhr -
                    otherKhr;
                var totalUsd =
                    grandTotalUsd -
                    discountUsd -
                    oweUsd -
                    otherUsd;

                return new Dictionary<string, object?>
                {
                    ["invoiceDetailId"] = GetPropertyValue(
                        row.Detail, "InvoiceDetailId"),
                    ["invoiceId"] = GetPropertyValue(
                        row.Detail, "InvoiceId"),
                    ["productId"] = GetPropertyValue(
                        row.Detail, "ProductId"),
                    ["productName"] = GetPropertyValue(
                        row.Product, "ProductName"),
                    ["productNameInKhmer"] = GetPropertyValue(
                        row.Product, "ProductNameInKhmer"),
                    ["qty"] = GetPropertyValue(
                        row.Detail, "Qty"),
                    ["priceKhr"] = priceKhr,
                    ["priceUsd"] = priceUsd,
                    ["totalKhr"] = totalKhr,
                    ["totalUsd"] = totalUsd,
                    ["type"] =
                        GetPropertyValue(row.Detail, "QtyNote") ??
                        GetPropertyValue(row.Detail, "Type") ??
                        0,
                    ["vat"] =
                        GetPropertyValue(row.Detail, "Vat") ??
                        0,
                    ["discountPercent"] =
                        GetPropertyValue(
                            row.Detail, "DiscountPercent") ??
                        0,
                    ["discountKhr"] = discountKhr,
                    ["discountUsd"] = discountUsd,
                    ["oweKhr"] = oweKhr,
                    ["oweUsd"] = oweUsd,
                    ["otherKhr"] = otherKhr,
                    ["otherUsd"] = otherUsd,
                    ["grandTotalKhr"] = grandTotalKhr,
                    ["grandTotalUsd"] = grandTotalUsd,
                    ["payKhr"] = GetDecimalPropertyValue(
                        row.Detail, "PRiel", "PayKhr"),
                    ["payUsd"] = GetDecimalPropertyValue(
                        row.Detail, "PDollar", "PayUsd"),
                    ["payBath"] = GetDecimalPropertyValue(
                        row.Detail, "PBath", "PayBath"),
                    ["tuitionFees"] =
                        GetPropertyValue(
                            row.Product, "TuitionFees") ??
                        0,
                    ["cardCertificate"] =
                        GetPropertyValue(
                            row.Product, "CardCertificate") ??
                        0,
                    ["categoryId"] =
                        GetPropertyValue(
                            row.Product, "CategoryId") ??
                        GetPropertyValue(
                            row.Detail, "CategoryId") ??
                        0,
                    ["paymentType"] =
                        GetPropertyValue(
                            row.Product, "PaymentType") ??
                        false
                };
            }).ToList();

            return new ServerResponse().Success(new
            {
                invoice,
                details
            }, "Invoice loaded successfully.");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpDelete("delete-invoice/{invoiceId:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteInvoice(int invoiceId)
    {
        var db = campusDbContext.DbContext(_campus);
        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            var invoice = await db.TblInvoice
                .FirstOrDefaultAsync(x =>
                    x.InvoiceId == invoiceId);

            if (invoice == null)
                return new ServerResponse().NotFound("Invoice not found.");

            var details = await db.TblInvoiceDetail
                .Where(x => x.InvoiceId == invoiceId)
                .ToListAsync();

            db.TblInvoiceDetail.RemoveRange(details);
            db.TblInvoice.Remove(invoice);

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return new ServerResponse().Success(
                new { invoiceId },
                "Payment deleted successfully!");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    private static void CopyMatchingProperties(
        object source,
        object target,
        params string[] excludedProperties)
    {
        var excluded = excludedProperties.ToHashSet(
            StringComparer.OrdinalIgnoreCase);

        var sourceProperties = source.GetType()
            .GetProperties()
            .Where(x => x.CanRead && !excluded.Contains(x.Name));

        var targetProperties = target.GetType()
            .GetProperties()
            .Where(x => x.CanWrite && !excluded.Contains(x.Name))
            .ToDictionary(
                x => x.Name,
                StringComparer.OrdinalIgnoreCase);

        foreach (var sourceProperty in sourceProperties)
        {
            if (!targetProperties.TryGetValue(
                    sourceProperty.Name,
                    out var targetProperty))
                continue;

            var value = sourceProperty.GetValue(source);

            if (value == null)
            {
                if (!targetProperty.PropertyType.IsValueType ||
                    Nullable.GetUnderlyingType(
                        targetProperty.PropertyType) != null)
                    targetProperty.SetValue(target, null);

                continue;
            }

            var targetType =
                Nullable.GetUnderlyingType(
                    targetProperty.PropertyType) ??
                targetProperty.PropertyType;

            if (targetType.IsInstanceOfType(value))
            {
                targetProperty.SetValue(target, value);
                continue;
            }

            targetProperty.SetValue(
                target,
                Convert.ChangeType(value, targetType));
        }
    }

    private static object? GetPropertyValue(
        object target,
        string propertyName)
    {
        return target.GetType()
            .GetProperty(
                propertyName,
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.IgnoreCase)?
            .GetValue(target);
    }

    private static decimal GetDecimalPropertyValue(
        object target,
        params string[] propertyNames)
    {
        foreach (var propertyName in propertyNames)
        {
            var value = GetPropertyValue(target, propertyName);
            if (value == null)
                continue;

            try
            {
                return Convert.ToDecimal(value);
            }
            catch (FormatException)
            {
            }
            catch (InvalidCastException)
            {
            }
            catch (OverflowException)
            {
            }
        }

        return 0m;
    }

    private static void CopyAliasedProperty(
        object source,
        string sourcePropertyName,
        object target,
        string targetPropertyName)
    {
        var value = GetPropertyValue(
            source,
            sourcePropertyName);

        if (value == null)
            return;

        SetPropertyValue(
            target,
            targetPropertyName,
            value);
    }

    private static void SetPropertyValue(
        object target,
        string propertyName,
        object value)
    {
        var property = target.GetType()
            .GetProperty(
                propertyName,
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.IgnoreCase);

        if (property == null || !property.CanWrite)
            return;

        var targetType =
            Nullable.GetUnderlyingType(property.PropertyType) ??
            property.PropertyType;

        property.SetValue(
            target,
            Convert.ChangeType(value, targetType));
    }
}
