using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Models.Req;
using BBU_SYSTEM.Repository;
using BBU_SYSTEM.ViewModel;
using BBU_SYSTEM.ViewModel.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LocalReport = Microsoft.Reporting.NETCore.LocalReport;
using Microsoft.Reporting.NETCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("registry")]
public class RegistryController(
    ICampusDbContext campusDbContext,
    IMapper mapper,
    IHttpContextAccessor context,
    IConfiguration configuration)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [Route("all-student-register")]
    public ActionResult Index()
    {
        return View();
    }

    [Route("create-new-student")]
    public ActionResult Create()
    {
        return View();
    }

    [HttpGet("Details/{studentId}")]
    public ActionResult Details(string? studentId)
    {
        var db = campusDbContext.DbContext(_campus);
        var listData = new ListData
        {
            Provinces = db.TblProvince.ToList(),
            Races = db.TblRace.ToList(),
            Nationalities = db.TblNationality.ToList(),
            HightSchools = db.TblHighSchool.ToList(),
            StudentJobs = db.TblStudentJob.ToList(),
            Disabilities = db.TblDisability.ToList(),
            Degrees = db.TblDegree.ToList(),
            Schools = db.TblSchool.ToList(),
            Fields = db.TblField.ToList(),
            Promotions = db.TblPromotion.ToList(),
            Stages = db.TblStage.ToList(),
            Terms = db.TblTerm.ToList(),
            Groups = db.TblGroup.ToList(),
            GroupRooms = db.TblGroupRoom.ToList(),
            StudyTimes = db.TblStudyTime.ToList(),
            Sponsors = db.TblSponsor.ToList(),
            Certificates = db.TblCertificate.ToList(),
            Universities = db.TblUsersity.ToList()
        };
        var registry = db.TblRegistry.FirstOrDefault(o => o.StudentId == studentId);
        var student = db.TblStudent.FirstOrDefault(s => s.StudentId == studentId)!;
        var group = new Group();
        var groupRoom = new GroupRoom();
        var stage = new Stage();
        var promotion = new Promotion();
        var term = new Term();
        var extend = new Extend();
        School school;
        Degree degree;
        var studentGroup = db.TblStudentGroup.OrderByDescending(t => t.StudentGroupId)
            .FirstOrDefault(g => g.StudentId == studentId);
        var field = db.TblField.FirstOrDefault(f => f.FieldId == student.FieldId)!;
        if (studentGroup != null)
        {
            group = db.TblGroup.FirstOrDefault(i => i.GroupId == studentGroup.GroupId)!;
            groupRoom = db.TblGroupRoom.FirstOrDefault(i =>
                i.GroupId == group.GroupId && i.TermNo == studentGroup.TermNo)!;
            stage = db.TblStage.FirstOrDefault(g => g.StageId == group.StageId)!;
            promotion = db.TblPromotion.FirstOrDefault(p => p.PromotionId == stage.PromotionId)!;
            term = db.TblTerm.FirstOrDefault(t => t.StageId == stage.StageId && t.TermNo == studentGroup.TermNo)!;
            extend = db.TblExtend.FirstOrDefault(x => x.StudentId == studentId)!;

            school = db.TblSchool.FirstOrDefault(s => s.SchoolId == promotion.SchoolId)!;
            degree = db.TblDegree.FirstOrDefault(d => d.DegreeId == promotion.DegreeId)!;
        }
        else
        {
            school = db.TblSchool.FirstOrDefault(s => s.SchoolId == registry!.SchoolId)!;
            degree = db.TblDegree.FirstOrDefault(d => d.DegreeId == registry!.DegreeId)!;
        }

        var studentView = new StudentViewModel
        {
            Student = student,
            Registry = registry,
            Degree = degree,
            School = school,
            Field = field,
            Promotion = promotion,
            Stage = stage,
            Term = term,
            Group = group,
            GroupRoom = groupRoom,
            Extend = extend,
            ContactPerson = db.TblContactPerson.FirstOrDefault(x => x.ContactPersonId == student.ContactPersonId),
            StudentCertificates = db.TblStudentCertificate.Where(x => x.StudentId == studentId).ToList(),
            Schoolarships = db.TblScholarship.Where(x => x.StudentId == studentId).ToList()
        };
        return View(new RegisryDetailViewModel
        {
            ListData = listData,
            StudentView = studentView
        });
    }

    [HttpPost]
    [Route("GetStudentRegistryList")]
    public IActionResult GetStudentRegistryList(DateTime from, DateTime to)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);


        var registry = db.TblRegistry.AsQueryable();
        if (from != DateTime.MinValue && to != DateTime.MinValue)
        {
            var fromdate = Convert.ToDateTime(from.ToString("yyyy-MM-dd"));
            var toDate = Convert.ToDateTime(to.ToString("yyyy-MM-dd"));
            registry = registry.Where(o => o.RegistrationDate >= fromdate && o.RegistrationDate <= toDate);
        }

        //if (!string.IsNullOrEmpty(searchValue))
        //{
        //    registry = registry.Where(d =>d.student_id!.Contains(searchValue));
        //}
        registry = registry.OrderByDescending(d => d.RegistrationId);
        var query = (from re in registry
            join s in db.TblStudent on re.StudentId equals s.StudentId
            join d in db.TblDegree on re.DegreeId equals d.DegreeId
            join sc in db.TblSchool on re.SchoolId equals sc.SchoolId
            select new
            {
                s.StudentId,
                s.StudentName,
                s.StudentNameInKhmer,
                s.Sex,
                s.DateOfBirth,
                s.Phone,
                s.Status,
                d.DegreeId,
                d.DegreeName,
                d.DegreeInKhmer,
                sc.SchoolId,
                sc.SchoolName,
                sc.SchoolNameInKhmer,
                re.PromotionNo,
                re.StageNo,
                re.TermNo,
                re.StudyTime,
                re.RegistrationDate,
                re.HighSchoolResult,
                re.HighSchoolTableNo
            }).AsQueryable();

        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(d =>
                d.StudentId!.Contains(searchValue) ||
                d.StudentName!.Contains(searchValue) ||
                d.SchoolName!.Contains(searchValue) ||
                d.DegreeName.Contains(searchValue)
            );
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


    [HttpPost("PostRegistry")]
    public async Task<ActionResult> PostRegistry(RegistryReq registryReq)
    {
        if (!ModelState.IsValid || registryReq.Student == null || registryReq.Registry == null ||
            registryReq.ContactPerson == null)
            return BadRequest(new
            {
                code = 400,
                message = "Invalid data"
            });
        var db = campusDbContext.DbContext(_campus);

        var newStudent = db.TblStudent
            .OrderByDescending(x => x.StudentId)
            .FirstOrDefault();

        var oldId = newStudent?.StudentId?
            .Replace(_campus, "", StringComparison.OrdinalIgnoreCase) ?? "0";
        var numericId = int.TryParse(oldId, out var n) ? n : 0;
        numericId++;
        var newId = $"{_campus}{numericId:D5}";

        registryReq.Student.Status = "REGISTER";
        registryReq.Student.StudentId = newId;
        registryReq.Student.IsContinuedStudent = registryReq.IsContinue;
        registryReq.Student.AssociateToBachelor = registryReq.AssToBach;
        var student = mapper.Map<StudentDto, Student>(registryReq.Student);
        var registry = mapper.Map<RegistryDto, Registry>(registryReq.Registry);
        var contactPerson = mapper.Map<ContactPersonDto, ContactPerson>(registryReq.ContactPerson);
        var scholarships =
            mapper.Map<List<StudentScholarshipDto>, List<StudentScholarship>>(registryReq.Scholarships!);
        var studentCertificates =
            mapper.Map<List<StudentCertificateDto>, List<StudentCertificate>>(registryReq.StudentCertificates!);
        var extend = mapper.Map<ExtendDto, Extend>(registryReq.Extend!);


        await using var tran = await db.Database.BeginTransactionAsync();

        try
        {
            // Contact Person
            await db.TblContactPerson.AddAsync(contactPerson);
            await db.SaveChangesAsync();
            // Student
            student.ContactPersonId = contactPerson.ContactPersonId;
            await db.TblStudent.AddAsync(student);
            await db.SaveChangesAsync();

            // Registry
            registry.StudentId = student.StudentId;
            await db.TblRegistry.AddAsync(registry);

            // Scholarships
            foreach (var s in scholarships)
                s.StudentId = student.StudentId;

            if (scholarships.Count != 0)
                await db.TblScholarship.AddRangeAsync(scholarships);

            // Certificates
            foreach (var c in studentCertificates)
                c.StudentId = student.StudentId;

            if (studentCertificates.Count != 0)
                await db.TblStudentCertificate.AddRangeAsync(studentCertificates);

            // Extend
            if (extend is { ExtendDate: not null } && extend.ExtendDate.Value != DateTime.MinValue)
            {
                extend.StudentId = student.StudentId;
                await db.TblExtend.AddAsync(extend);
            }

            if (registryReq.AssToBach || registryReq.BachToMas)
            {
                var resumeDto = new ResumeDto
                {
                    StudentId = student.StudentId,
                    FieldId = student?.FieldId ?? 0,
                    Other = "Continue Year",
                    CPromotion = registry.PromotionNo,
                    Stage = registry.StageNo.ToString(),
                    CYear = registry?.TermNo ?? 0,
                    CSemester = registry?.TermNo ?? 0,
                    Type = registryReq.AssToBach ? "Associate to Bachelor" :
                        registryReq.BachToMas ? "Bachelor to Master" : ""
                };
                var resume = mapper.Map<ResumeDto, Resume>(resumeDto);
                await db.TblResume.AddAsync(resume);
            }

            await db.SaveChangesAsync();
            await tran.CommitAsync();

            return Ok(new
            {
                data = student,
                status = new
                {
                    code = 200,
                    message = "Registry created successfully",
                }
            });
        }
        catch (Exception ex)
        {
            await tran.RollbackAsync();
            return StatusCode(500, new
            {
                data = new { },
                status = new
                {
                    code = 500,
                    message = ex.InnerException?.Message ?? ex.Message
                }
            });
        }
    }

    [HttpPost("update")]
    public ActionResult PutRegistry(RegistryViewModel req)
    {
        var student = mapper.Map<StudentDto, Student>(req.Student!);
        var registry = mapper.Map<RegistryDto, Registry>(req.Registry!);
        var contactPerson = mapper.Map<ContactPersonDto, ContactPerson>(req.ContactPerson!);
        var schoolarships = mapper.Map<List<StudentScholarshipDto>, List<StudentScholarship>>(req.Schoolarships!);
        var studentCertificates =
            mapper.Map<List<StudentCertificateDto>, List<StudentCertificate>>(req.StudentCertificates!);
        var extend = mapper.Map<ExtendDto, Extend>(req.Extend!);
        var resume = mapper.Map<ResumeDto, Resume>(req.Resume!);
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);
        using var tran = db.Database.BeginTransaction();
        try
        {
            db.TblStudent.Add(student);
            db.TblRegistry.Add(registry);
            db.TblContactPerson.Add(contactPerson);

            foreach (var dt in schoolarships) db.TblScholarship.Add(dt);
            foreach (var dt in studentCertificates) db.TblStudentCertificate.Add(dt);
            db.TblExtend.Add(extend);
            if (req.AssToBach || req.BachToMas) db.TblResume.Add(resume);
            db.SaveChanges();
            tran.Commit();
            TempData["resul"] = true;
        }
        catch
        {
            tran.Rollback();
            TempData["resul"] = false;
        }

        return RedirectToAction(nameof(Index));
    }


    [HttpGet("getstudytime/{degreeId}/{schoolId}/{promotionNo}/{stageNo}")]
    public IActionResult GetStudyTime(int degreeId = 0, int schoolId = 0, int promotionNo = 0, int stageNo = 0)
    {
        var db = campusDbContext.DbContext(_campus);
        var query = db.TblRegistry.AsQueryable();
        query = query.Where(x =>
            x.TermNo == 1 && x.DegreeId == degreeId && x.SchoolId == schoolId && x.PromotionNo == promotionNo &&
            x.StageNo == stageNo).AsQueryable();
        query = query.Where(x =>
            db.TblStudent.Where(s => s.Status == "REGISTER").Select(s => s.StudentId).Contains(x.StudentId));
        var data = query.Select(x => x.StudyTime).Distinct().ToList();
        return Json(new
        {
            data
        });
    }

    [HttpPost("registered-students")]
    public IActionResult RegisteredStudent(RegisteredViewModel registeredStudent)
    {
        var connectionString =
            configuration.GetConnectionString($"{_campus}_campus");

        using var con = new SqlConnection(connectionString);
        using var cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText = """
                                  SELECT *
                                  FROM V_ADMIN_REPORT_REGISTERED_STUDENT
                                  WHERE (@PromotionNo = 0 OR PROMOTION_NO = @PromotionNo)
                                    AND (@DegreeId = 0 OR DEGREE_ID = @DegreeId)
                                    AND (@StageNo = 0 OR STAGE_NO = @StageNo)
                                    AND (@FromDate IS NULL OR REGISTRATION_DATE >= @FromDate)
                                    AND (@ToDate IS NULL OR REGISTRATION_DATE <= @ToDate)
                              
                          """;
        cmd.Parameters.Add("@PromotionNo", SqlDbType.Int).Value = registeredStudent.PromotionNo;
        cmd.Parameters.Add("@DegreeId", SqlDbType.Int).Value = registeredStudent.DegreeId;
        cmd.Parameters.Add("@StageNo", SqlDbType.Int).Value = registeredStudent.StageNo;
        cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = (object?)registeredStudent.FromDate ?? DBNull.Value;
        cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = (object?)registeredStudent.ToDate ?? DBNull.Value;

        var da = new SqlDataAdapter(cmd);
        var dt = new DataTable();
        da.Fill(dt);

        var report = new LocalReport();
        report.ReportPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Reports", "REGISTER", "REGISTER_RPT.rdlc");
        report.DataSources.Clear();
        report.DataSources.Add(new ReportDataSource("DataSet1", dt));

        var reportParams = new ReportParameter[]
        {
            new("reporter", registeredStudent.Reporter),
            new("receiver", registeredStudent.Receiver),
        };
        report.SetParameters(reportParams);
        var pdf = report.Render("PDF");
        return File(pdf, "application/pdf");
    }
}