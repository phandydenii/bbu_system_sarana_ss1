using AutoMapper;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("QRCodeCertificate")]
public class QrCodeCertificateController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("GetStudentRequestQRCode")]
    public IActionResult GetStudentRequestQrCode(int degreeId = 0, int schoolId = 0, int fieldId = 0,
        int promotionId = 0, int stageNo = 0, string groupName = "All Group")
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        var db = campusDbContext.DbContext(_campus);
        var query = (from student in db.TblStudent
            join studentGroup in db.TblStudentGroup
                on student.StudentId equals studentGroup.StudentId
            join g in db.TblGroup
                on studentGroup.GroupId equals g.GroupId
            join stage in db.TblStage
                on g.StageId equals stage.StageId
            join promotion in db.TblPromotion
                on stage.PromotionId equals promotion.PromotionId
            join term in db.TblTerm
                on stage.StageId equals term.StageId
            join school in db.TblSchool
                on promotion.SchoolId equals school.SchoolId
            join field in db.TblField
                on student.FieldId equals field.FieldId
            join degree in db.TblDegree
                on promotion.DegreeId equals degree.DegreeId
            join faculty in db.TblFaculty
                on school.FacultyId equals faculty.FacultyId
            where term.Status == "ACTIVE"
                  && !db.TblStudent
                      .Where(s => s.Photo!.Length / 1024.0 / 1024.0 > 1)
                      .Select(s => s.StudentId)
                      .Contains(student.StudentId)
            select new
            {
                student.StudentId,
                student.StudentName,
                student.StudentNameInKhmer,
                student.Sex,
                student.DateOfBirth,
                student.Status,
                DocumentIn = string.IsNullOrEmpty(student.DocumentIn) ? null : student.DocumentIn,
                DocumentOut = string.IsNullOrEmpty(student.DocumentOut) ? null : student.DocumentOut,
                student.IsAuthenticated,
                degree.DegreeId,
                degree.DegreeName,
                degree.DegreeInKhmer,
                faculty.FacultyId,
                faculty.FacultyName,
                faculty.FacultyNameInKhmer,
                promotion.SchoolId,
                school.SchoolName,
                school.SchoolNameInKhmer,
                promotion.PromotionId,
                promotion.PromotionNo,
                PromotionYearStart = promotion.AcademicYearStart,
                PromotionYearEnd = promotion.AcademicYearEnd,
                stage.StageNo,
                g.GroupName,
                g.StudyTime,
                field.FieldId,
                field.FieldName,
                field.FieldNameInKhmer,
                DegreeNameInEnglish = field.DegreeName,
                field.DegreeNameInKhmer,
                term.TermNo,
                Type = field.Type,
                term.AcademicYearStart,
                GraduateDate = promotion.GraduateDate1,
                student.Url,
                student.DocumentKey,
                student.QrCodeData,
                CountPrint = student.CountPrint ?? 0,
                IsPrintCertificate = student.IsPrintCertificate,
                IsRequest = student.IsRequest,
                CertificateCode = student.CertificateCode ?? "",
                Ignor = student.Ignor,
                IgnorReason = student.IgnorReason ?? ""
            }).AsQueryable().Distinct();
        var students = db.TblStudent.Where(x => x.Photo != null).AsQueryable();
        var qrcodes = db.TblQrCodeCertificates.AsQueryable();
        query = query.Where(x =>
                x.DegreeId == degreeId && x.SchoolId == schoolId && x.FieldId == fieldId &&
                x.PromotionId == promotionId)
            .AsQueryable();

        if (stageNo != 0)
        {
            query = query.Where(x => x.StageNo == stageNo);
            if (groupName != "All Group") query = query.Where(x => x.GroupName == groupName);
        }

        query = query.Where(x =>
            x.IsRequest == false && new[] { "COMPLETED", "GRADUATED" }.Contains(x.Status) &&
            students.Any(s => s.StudentId == x.StudentId));
        query = query.Where(x => qrcodes.Any(qr => qr.StudentId != x.StudentId));

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

    [HttpPost("GetStudentRequestedQRCode")]
    public IActionResult GetStudentRequestedQrCode(int degreeId = 0, int schoolId = 0, int fieldId = 0,
        int promotionId = 0, int stageNo = 0, string groupName = "All Group")
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        var db = campusDbContext.DbContext(_campus);
        var query = db.TblQrCodeCertificates.AsQueryable().Distinct();
        query = query
            .Where(x => x.DegreeId == degreeId && x.SchoolId == schoolId && x.FieldId == fieldId &&
                        x.PromotionId == promotionId).OrderBy(x => x.StudentNameKhmer).AsQueryable();

        if (stageNo != 0)
        {
            query = query.Where(x => x.StageNo == stageNo);
            if (groupName != "All Group") query = query.Where(x => x.GroupName == groupName);
        }

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