using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Models.Req;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("invoice")]
public class InvoiceController(ICampusDbContext campusDbContext,IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("gets")]
    public IActionResult Index()
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
                join i in db.TblInvoice on s.StudentId equals i.StudentId
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
        if (!string.IsNullOrEmpty(searchValue)) query = query.Where(x => x.StudentId == searchValue).AsQueryable();
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

    [HttpPost(" get-invoice-by-student-id")]
    public IActionResult GetInvoiceByStudentId(string studentId)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        //var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var pageSize = length != null ? int.Parse(length) != -1 ? Convert.ToInt32(length) : 10 : 10;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        var db = campusDbContext.DbContext(_campus);

        //var students = db.tbl_student.AsQueryable();
        var query = (from i in db.TblInvoice
                join s in db.TblStudent on i.StudentId equals s.StudentId
                where i.StudentId == studentId
                select new
                {
                    s.StudentId,
                    s.StudentName,
                    s.StudentNameInKhmer,
                    s.Sex,
                    s.DateOfBirth,
                    s.Phone,
                    s.Address,
                    s.Email,
                    i.InvoiceId,
                    i.InvoiceDate,
                    i.InvoiceNo,
                    i.YearNumber,
                    i.DegreeId,
                    i.SchoolId,
                    i.FieldId,
                    i.PromotionId,
                    i.StageId,
                    i.GroupId,
                    i.StartDate,
                    i.EndDate,
                    i.TermNo,
                    i.GrandTotalUsd,
                    i.GrandTotalKhr,
                    i.TotalRiel,
                    i.TotalDollar,
                    i.TotalBath,
                    i.TotalDiscountUsd,
                    i.Vat,
                    i.OweUsd,
                    i.OweReason,
                    i.Description
                }
            ).AsQueryable();
        if (!string.IsNullOrEmpty(searchValue)) query = query.Where(x => x.StudentId == searchValue).AsQueryable();
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

    [HttpPost("get-invoice-detail/{invoiceId}")]
    public IActionResult GetInvoiceDetail(int invoiceId,bool isAll = false)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var pageSize = length != null ? int.Parse(length!) != -1 ? Convert.ToInt32(length) : 10 : 10;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            var db = campusDbContext.DbContext(_campus);

            //var students = db.tbl_student.AsQueryable();
            var query = (from i in db.TblInvoice
                    join id in db.TblInvoiceDetail on i.InvoiceId equals id.InvoiceId
                    join p in db.TblProduct on id.ProductId equals p.ProductId
                    where i.InvoiceId == invoiceId
                    select new InvoiceDetailRes
                    {
                        InvoiceDetailId = id.InvoiceDetailId,
                        InvoiceId = id.InvoiceId,
                        ProductId = id.ProductId,
                        ProductName = p.ProductName,
                        ProductNameKhmer = p.ProductNameInKhmer,
                        Qty = id.Qty,
                        Type = id.QtyNote,
                        PriceKhr = id.PriceKhr ?? 0,
                        PriceUsd = id.Price,
                        TotalKhr = id.PriceKhr * id.Qty ?? 0,
                        TotalUsd = id.Price * id.Qty,
                        Vat = id.Vat,
                        DiscountPercent = id.DiscountPercent ?? 0,
                        DiscountKhr = id.Discount ?? 0,
                        DiscountUsd = id.DiscountKhr,
                        OweKhr = id.OweKhr ?? 0,
                        GrandTotalKhr = id.Qty * id.PriceKhr - id.DiscountKhr + id.OweKhr ?? 0,
                        GrandTotalUsd = id.Qty * id.Price - id.Discount + id.Owe,
                        PayKhr = id.PRiel ?? 0,
                        PayUsd = id.PDollar ?? 0,
                        PayBath = id.PBath ?? 0,
                        Tuitionfees = p.TuitionFees,
                        CardCertificate = p.CardCertificate,
                        CategoryId = p.CategoryId,
                        OtherUsd = id.Other,
                        OtherKhr = id.OtherKhr
                    }
                ).AsQueryable();
            if (isAll)
            {
                return Ok(new
                {
                    data = query,
                    status = new
                    {
                        code = "200",
                        message = "Success",
                    }
                });
            }

            var recordsTotal = query.Count();
            var data = query.Skip(skip).Take(10).ToList();
            return Json(new
            {
                draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            });
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching invoice details: {ex.Message}");
        }
    }

    [HttpPost("create-payment")]
    public async Task<IActionResult> CreatePayment(InvoiceDto invoice, List<InvoiceDetailReq> invoiceDetails)
    {
        var db = campusDbContext.DbContext(_campus);

        var year = DateTime.Now.Year.ToString();
        invoice.InvoiceNo = 1;
        invoice.YearNumber = year;
        var lastInv = db.TblInvoice.Where(x => x.YearNumber == year).OrderByDescending(x => x.InvoiceId)
            .FirstOrDefault();
        if (lastInv != null) invoice.InvoiceNo = lastInv.InvoiceNo + 1;
        var tran = await db.Database.BeginTransactionAsync();
        try
        {
            var data = mapper.Map<InvoiceDto, Invoice>(invoice);
            await db.TblInvoice.AddAsync(data);
            await db.SaveChangesAsync();

            foreach (var invDetail in invoiceDetails)
            {
                var invoiceDetail = new InvoiceDetail
                {
                    InvoiceId = invoice.InvoiceId,
                    ProductId = invDetail.ProductId,
                    Qty = invDetail.Qty,
                    QtyNote = invDetail.Type,
                    PriceKhr = invDetail.PriceKhr,
                    Price = invDetail.PriceUsd,
                    Note = "",
                    Vat = invDetail.Vat,
                    PRiel = invDetail.PayKhr,
                    PDollar = invDetail.PayUsd,
                    PBath = invDetail.PayBath,
                    DiscountPercent = invDetail.DiscountPercent,
                    DiscountKhr = invDetail.DiscountKhr,
                    Discount = invDetail.DiscountUsd,
                    OweKhr = invDetail.OweKhr,
                    Owe = invDetail.OweUsd,
                    CategoryId = invDetail.CategoryId,
                    Other = invDetail.OtherUsd,
                    OtherKhr = invDetail.OtherKhr,
                };
                await db.TblInvoiceDetail.AddAsync(invoiceDetail);
                await db.SaveChangesAsync();
            }

            await tran.CommitAsync();
            return Ok(new
            {
                data = invoice,
                status = new
                {
                    code = "200",
                    message = "Succeeded!"
                }
                
            });
        }
        catch (Exception ex)
        {
            await tran.RollbackAsync();
            return StatusCode(500, new
            {
                code = "500",
                message = $"Internal Server Error:{ex.Message}"
            });
        }

        
    }
}