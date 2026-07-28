using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Claims;
using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Models.Req;
using BBU_SYSTEM.Repository;
using BBU_SYSTEM.ViewModel;
using BBU_SYSTEM.ViewModel.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;
using LocalReport = Microsoft.Reporting.NETCore.LocalReport;

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
    private readonly ServerResponse _serverResponse = new();

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

    [HttpPost("GetStudentRegistryList")]
    public IActionResult GetStudentRegistryList(
        DateTime? from,
        DateTime? to)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue =
                Request.Form["search[value]"].FirstOrDefault();

            var pageSize = !string.IsNullOrEmpty(length)
                ? Convert.ToInt32(length)
                : 10;

            var skip = !string.IsNullOrEmpty(start)
                ? Convert.ToInt32(start)
                : 0;

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            var db = campusDbContext.DbContext(_campus);

            var registryQuery = db.TblRegistry.AsQueryable();

            if (from.HasValue)
            {
                var fromDate = from.Value.Date;

                registryQuery = registryQuery.Where(x =>
                    x.RegistrationDate >= fromDate);
            }

            if (to.HasValue)
            {
                var nextDate = to.Value.Date.AddDays(1);

                registryQuery = registryQuery.Where(x =>
                    x.RegistrationDate < nextDate);
            }

            var query =
                (
                    from re in registryQuery
                    join s in db.TblStudent
                        on re.StudentId equals s.StudentId
                    join d in db.TblDegree
                        on re.DegreeId equals d.DegreeId
                    join sc in db.TblSchool
                        on re.SchoolId equals sc.SchoolId
                    select new
                    {
                        studentId = s.StudentId,
                        studentName = s.StudentName,
                        studentNameInKhmer = s.StudentNameInKhmer,
                        sex = s.Sex,
                        dateOfBirth = s.DateOfBirth,
                        phone = s.Phone,
                        status = s.Status,

                        degreeId = d.DegreeId,
                        degreeName = d.DegreeName,
                        degreeInKhmer = d.DegreeInKhmer,

                        schoolId = sc.SchoolId,
                        schoolName = sc.SchoolName,
                        schoolNameInKhmer = sc.SchoolNameInKhmer,

                        promotionNo = re.PromotionNo,
                        stageNo = re.StageNo,
                        termNo = re.TermNo,
                        studyTime = re.StudyTime,
                        registrationDate = re.RegistrationDate,
                        highSchoolResult = re.HighSchoolResult,
                        highSchoolTableNo = re.HighSchoolTableNo
                    }
                )
                .AsQueryable();

            var recordsTotal = query.Count();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(x =>
                    (x.studentId ?? "").Contains(searchValue) ||
                    (x.studentName ?? "").Contains(searchValue) ||
                    (x.studentNameInKhmer ?? "").Contains(searchValue) ||
                    (x.schoolName ?? "").Contains(searchValue) ||
                    (x.degreeName ?? "").Contains(searchValue));
            }

            var recordsFiltered = query.Count();

            var data = query
                .OrderByDescending(x => x.registrationDate)
                .ThenByDescending(x => x.studentId)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            return Json(new
            {
                draw,
                recordsTotal,
                recordsFiltered,
                data
            });
        }
        catch (Exception e)
        {
            return _serverResponse.ErrorInternal(e);
        }
    }

    [HttpPost("PostRegistry")]
    public async Task<IActionResult> PostRegistry(
        [FromForm] RegistryReq registryReq)
    {
        // These checks prevent null-reference errors. They are not field validation.
        if (registryReq.Student == null)
        {
            return _serverResponse.BadRequest(
                "Student data was not submitted."
            );
        }

        if (registryReq.Registry == null)
        {
            return _serverResponse.BadRequest(
                "Registry data was not submitted."
            );
        }

        registryReq.Scholarships ??=
            new List<StudentScholarshipDto>();

        registryReq.StudentCertificates ??=
            new List<StudentCertificateDto>();

        registryReq.Extend ??= new ExtendDto();
        registryReq.Resume ??= new ResumeDto();

        var db = campusDbContext.DbContext(_campus);

        var username =
            User.Identity?.Name ??
            User.FindFirst(ClaimTypes.Name)?.Value ??
            User.FindFirst("UserName")?.Value ??
            User.FindFirst("Username")?.Value ??
            User.FindFirst("username")?.Value ??
            "Unknown";

        // The form may submit either PromotionId or PromotionNo.
        var submittedPromotionValue =
            registryReq.Registry.PromotionNo;

        var selectedPromotion =
            await db.TblPromotion.FirstOrDefaultAsync(x =>
                x.DegreeId == registryReq.Registry.DegreeId &&
                x.SchoolId == registryReq.Registry.SchoolId &&
                (
                    x.PromotionId == submittedPromotionValue ||
                    x.PromotionNo == submittedPromotionValue
                ));

        if (selectedPromotion == null)
        {
            return _serverResponse.BadRequest(
                "The selected promotion is invalid."
            );
        }

        // The form may submit either StageId or StageNo.
        var submittedStageValue =
            registryReq.Registry.StageNo;

        var selectedStage =
            await db.TblStage.FirstOrDefaultAsync(x =>
                x.PromotionId == selectedPromotion.PromotionId &&
                (
                    x.StageId == submittedStageValue ||
                    x.StageNo == submittedStageValue
                ));

        if (selectedStage == null)
        {
            return _serverResponse.BadRequest(
                "The selected stage is invalid."
            );
        }

        var campusPrefix = _campus
            .Trim()
            .ToUpperInvariant();

        var lastStudentId = await db.TblStudent
            .Where(x =>
                x.StudentId != null &&
                x.StudentId
                    .ToUpper()
                    .StartsWith(campusPrefix))
            .OrderByDescending(x => x.StudentId)
            .Select(x => x.StudentId)
            .FirstOrDefaultAsync();

        var numberText = "0";

        if (!string.IsNullOrWhiteSpace(lastStudentId) &&
            lastStudentId.Length > campusPrefix.Length)
        {
            numberText = lastStudentId.Substring(
                campusPrefix.Length
            );
        }

        var nextNumber = int.TryParse(
            numberText,
            out var currentNumber
        )
            ? currentNumber + 1
            : 1;

        var newStudentId =
            $"{campusPrefix}{nextNumber:D5}";

        registryReq.Student.StudentId = newStudentId;
        registryReq.Student.Status = "REGISTER";
        registryReq.Student.IsContinuedStudent =
            registryReq.IsContinue;
        registryReq.Student.AssociateToBachelor =
            registryReq.AssToBach;
        registryReq.Student.BachelorToMaster =
            registryReq.BachToMas ? 1 : 0;

        var student =
            mapper.Map<Student>(registryReq.Student);

        var registry =
            mapper.Map<Registry>(registryReq.Registry);

        var scholarships =
            mapper.Map<List<StudentScholarship>>(
                registryReq.Scholarships
            ) ?? new List<StudentScholarship>();

        var studentCertificates =
            mapper.Map<List<StudentCertificate>>(
                registryReq.StudentCertificates
            ) ?? new List<StudentCertificate>();

        var extend =
            mapper.Map<Extend>(registryReq.Extend);

        // Preserve the Personal-tab fields explicitly.
        student.PlaceOfBirthId =
            registryReq.Student.PlaceOfBirthId;
        student.NationalityId =
            registryReq.Student.NationalityId;
        student.FromProvinceId =
            registryReq.Student.FromProvinceId;
        student.RaceId =
            registryReq.Student.RaceId;
        student.JobId =
            registryReq.Student.JobId;
        student.DisabilityId =
            registryReq.Student.DisabilityId;
        student.FromHighSchoolNameInKhmer =
            registryReq.Student.FromHighSchoolNameInKhmer;

        // The form DTO uses bool, while STUDENT.IS_PHOTO_RECEIVED stores 1/0.
        // Map it explicitly because the source and destination properties have
        // different names and different types.
        student.IsPhotoReceived =
            registryReq.Student.IsReceivePhoto == true ? 1 : 0;

        student.UpdateBy = username;
        student.UpdateDate = DateTime.Now;

        registry.StudentId = newStudentId;
        registry.PromotionNo =
            selectedPromotion.PromotionNo;
        registry.StageNo =
            selectedStage.StageNo;

        foreach (var scholarship in scholarships)
        {
            scholarship.StudentScholarshipId = 0;
            scholarship.StudentId = newStudentId;
        }

        foreach (var certificate in studentCertificates)
        {
            certificate.StudentCertificateId = 0;
            certificate.StudentId = newStudentId;
        }

        await using var transaction =
            await db.Database.BeginTransactionAsync();

        try
        {
            // Create a contact-person row only when the user entered contact data.
            var hasContactPerson =
                registryReq.ContactPerson != null &&
                (
                    !string.IsNullOrWhiteSpace(
                        registryReq.ContactPerson.ContactPersonName) ||
                    !string.IsNullOrWhiteSpace(
                        registryReq.ContactPerson.Job) ||
                    !string.IsNullOrWhiteSpace(
                        registryReq.ContactPerson.Phone) ||
                    !string.IsNullOrWhiteSpace(
                        registryReq.ContactPerson.Address)
                );

            if (hasContactPerson)
            {
                var contactPerson =
                    mapper.Map<ContactPerson>(
                        registryReq.ContactPerson
                    );

                contactPerson.ContactPersonId = 0;

                await db.TblContactPerson.AddAsync(
                    contactPerson
                );

                await db.SaveChangesAsync();

                student.ContactPersonId =
                    contactPerson.ContactPersonId;
            }
            else
            {
                student.ContactPersonId = null;
            }

            await db.TblStudent.AddAsync(student);
            await db.TblRegistry.AddAsync(registry);

            if (scholarships.Count > 0)
            {
                await db.TblScholarship.AddRangeAsync(
                    scholarships
                );
            }

            if (studentCertificates.Count > 0)
            {
                await db.TblStudentCertificate
                    .AddRangeAsync(studentCertificates);
            }

            if (extend != null &&
                extend.ExtendDate.HasValue &&
                extend.ExtendDate.Value != DateTime.MinValue)
            {
                extend.StudentId = newStudentId;
                await db.TblExtend.AddAsync(extend);
            }

            if (registryReq.AssToBach ||
                registryReq.BachToMas)
            {
                var resumeDto = new ResumeDto
                {
                    StudentId = newStudentId,
                    FieldId = student.FieldId ?? 0,
                    DatePayment =
                        registryReq.Resume.DatePayment,
                    Other = "Continue Year",
                    CPromotion =
                        selectedPromotion.PromotionNo,
                    Stage =
                        selectedStage.StageNo.ToString(),
                    CYear = registryReq.Resume.CYear,
                    CSemester =
                        registryReq.Resume.CSemester,
                    Type = registryReq.AssToBach
                        ? "Associate to Bachelor"
                        : "Bachelor to Master"
                };

                var resumeEntity =
                    mapper.Map<Resume>(resumeDto);

                if (resumeEntity != null)
                {
                    await db.TblResume.AddAsync(
                        resumeEntity
                    );
                }
            }

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return _serverResponse.Success(
                new
                {
                    student,
                    redirectUrl = Url.Action(
                        nameof(Index),
                        "Registry"
                    )
                },
                "Registry created successfully"
            );
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return _serverResponse.ErrorInternal(ex);
        }
    }
    
    
    [HttpPost("update")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PutRegistry(
        [FromForm] RegistryReq registryReq,
        [FromForm] string oldStudentId,
        [FromForm] IFormFile? studentPhoto,
        [FromForm] bool replaceScholarships = false,
        [FromForm] bool replaceCertificates = false)
    {
        oldStudentId = oldStudentId?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(oldStudentId))
        {
            return _serverResponse.BadRequest(
                "Student ID is missing."
            );
        }

        if (registryReq.Student == null)
        {
            return _serverResponse.BadRequest(
                "Student data was not submitted."
            );
        }

        if (registryReq.Registry == null)
        {
            return _serverResponse.BadRequest(
                "Registry data was not submitted."
            );
        }

        registryReq.ContactPerson ??= new ContactPersonDto();
        registryReq.Scholarships ??= new List<StudentScholarshipDto>();
        registryReq.StudentCertificates ??=
            new List<StudentCertificateDto>();
        registryReq.Extend ??= new ExtendDto();

        var db = campusDbContext.DbContext(_campus);

        var username =
            User.Identity?.Name ??
            User.FindFirst(ClaimTypes.Name)?.Value ??
            User.FindFirst("UserName")?.Value ??
            User.FindFirst("Username")?.Value ??
            User.FindFirst("username")?.Value ??
            "Unknown";

        var student = await db.TblStudent
            .FirstOrDefaultAsync(x =>
                x.StudentId == oldStudentId);

        if (student == null)
        {
            return _serverResponse.NotFound(
                "Student was not found."
            );
        }

        var registry = await db.TblRegistry
            .FirstOrDefaultAsync(x =>
                x.StudentId == oldStudentId);

        if (registry == null)
        {
            return _serverResponse.NotFound(
                "Registry record was not found."
            );
        }

        var submittedPromotionValue =
            registryReq.Registry.PromotionNo;

        var selectedPromotion =
            await db.TblPromotion.FirstOrDefaultAsync(x =>
                x.DegreeId == registryReq.Registry.DegreeId &&
                x.SchoolId == registryReq.Registry.SchoolId &&
                (x.PromotionId == submittedPromotionValue ||
                 x.PromotionNo == submittedPromotionValue));

        if (selectedPromotion == null)
        {
            return _serverResponse.BadRequest(
                "The selected promotion is invalid."
            );
        }

        var submittedStageValue =
            registryReq.Registry.StageNo;

        var selectedStage =
            await db.TblStage.FirstOrDefaultAsync(x =>
                x.PromotionId == selectedPromotion.PromotionId &&
                (x.StageId == submittedStageValue ||
                 x.StageNo == submittedStageValue));

        if (selectedStage == null)
        {
            return _serverResponse.BadRequest(
                "The selected stage is invalid."
            );
        }

        await using var transaction =
            await db.Database.BeginTransactionAsync();

        try
        {
            // Keep existing keys before mapping
            var currentStudentId = student.StudentId;
            var currentContactPersonId =
                student.ContactPersonId;
            var currentStatus =
                student.Status;
            // Make a separate copy of the saved PHOTO bytes before AutoMapper.
            // StudentDto.Photo is normally null during a regular form update,
            // so mapping it directly can clear the tracked entity's photo.
            var currentPhoto =
                student.Photo?.ToArray();

            var hasNewPhoto =
                studentPhoto != null &&
                studentPhoto.Length > 0;

            // ==========================================
            // UPDATE STUDENT
            // ==========================================
            mapper.Map(
                registryReq.Student,
                student
            );

            // AutoMapper can map a null DTO Photo over the saved database
            // photo. Keep the current bytes unless a new file was selected.
            if (hasNewPhoto)
            {
                await using var photoStream =
                    new MemoryStream();

                await studentPhoto.CopyToAsync(
                    photoStream
                );

                student.Photo =
                    photoStream.ToArray();
            }
            else
            {
                // A normal Update must keep the bytes already stored in SQL.
                student.Photo = currentPhoto;
            }

            // When no replacement file was submitted, exclude PHOTO from the
            // generated UPDATE statement. This prevents AutoMapper's null DTO
            // value from clearing the database column.
            db.Entry(student)
                .Property(x => x.Photo)
                .IsModified = hasNewPhoto;

            // Do not allow AutoMapper to replace the key
            student.StudentId = currentStudentId;
            student.ContactPersonId =
                currentContactPersonId;

            student.Status = string.IsNullOrWhiteSpace(
                registryReq.Student.Status)
                    ? currentStatus ?? "REGISTER"
                    : registryReq.Student.Status.Trim();

            student.IsContinuedStudent =
                registryReq.IsContinue ? 1 : 0;

            student.AssociateToBachelor =
                registryReq.AssToBach ? 1 : 0;

            // The request DTO uses bool, but the STUDENT table stores 1/0.
            // Assign it explicitly because AutoMapper may not convert this
            // property when the source and destination names/types differ.
            student.IsPhotoReceived =
                registryReq.Student.IsReceivePhoto ? 1 : 0;

            // Explicitly preserve submitted province values
            student.PlaceOfBirthId =
                registryReq.Student.PlaceOfBirthId;

            student.FromProvinceId =
                registryReq.Student.FromProvinceId;

            student.UpdateBy = username;
            student.UpdateDate = DateTime.Now;

            // ==========================================
            // UPDATE OR CREATE CONTACT PERSON
            // ==========================================
            var hasContactPersonInput =
                Request.Form.Keys.Any(key =>
                    key.StartsWith(
                        "ContactPerson.",
                        StringComparison.OrdinalIgnoreCase) ||
                    key.StartsWith(
                        "registryReq.ContactPerson.",
                        StringComparison.OrdinalIgnoreCase));

            if (hasContactPersonInput)
            {
                ContactPerson? contactPerson = null;

                if (student.ContactPersonId > 0)
                {
                    contactPerson =
                        await db.TblContactPerson
                            .FirstOrDefaultAsync(x =>
                                x.ContactPersonId ==
                                student.ContactPersonId);
                }

                if (contactPerson == null)
                {
                    contactPerson =
                        mapper.Map<ContactPerson>(
                            registryReq.ContactPerson
                        );

                    contactPerson.ContactPersonId = 0;

                    await db.TblContactPerson.AddAsync(
                        contactPerson
                    );

                    // Save once so SQL Server generates the identity.
                    await db.SaveChangesAsync();

                    student.ContactPersonId =
                        contactPerson.ContactPersonId;
                }
                else
                {
                    var contactPersonId =
                        contactPerson.ContactPersonId;

                    mapper.Map(
                        registryReq.ContactPerson,
                        contactPerson
                    );

                    contactPerson.ContactPersonId =
                        contactPersonId;
                }
            }

            // ==========================================
            // UPDATE REGISTRY
            // ==========================================
            // Do not AutoMap a DTO onto this tracked entity.
            // RegistrationId is part of its EF key, and even assigning the
            // original value back after mapping can leave it marked Modified.
            // Update only fields that the user is allowed to edit.
            registry.DegreeId =
                registryReq.Registry.DegreeId;

            registry.SchoolId =
                registryReq.Registry.SchoolId;

            registry.PromotionNo =
                selectedPromotion.PromotionNo;

            registry.StageNo =
                selectedStage.StageNo;

            registry.TermNo =
                registryReq.Registry.TermNo;

            registry.StudyTime =
                registryReq.Registry.StudyTime;

            registry.UpdateBy = username;
            registry.UpdateDate = DateTime.Now;

            // ==========================================
            // REPLACE SCHOLARSHIPS
            // ==========================================
            if (replaceScholarships)
            {
                var oldScholarships =
                    await db.TblScholarship
                        .Where(x =>
                            x.StudentId == oldStudentId)
                        .ToListAsync();

                db.TblScholarship.RemoveRange(
                    oldScholarships
                );

                var scholarships =
                    mapper.Map<List<StudentScholarship>>(
                        registryReq.Scholarships
                    ) ?? new List<StudentScholarship>();

                foreach (var scholarship in scholarships)
                {
                    scholarship.StudentScholarshipId = 0;
                    scholarship.StudentId =
                        oldStudentId;
                }

                if (scholarships.Count > 0)
                {
                    await db.TblScholarship.AddRangeAsync(
                        scholarships
                    );
                }
            }

            // ==========================================
            // REPLACE CERTIFICATES
            // ==========================================
            if (replaceCertificates)
            {
                var oldCertificates =
                    await db.TblStudentCertificate
                        .Where(x =>
                            x.StudentId == oldStudentId)
                        .ToListAsync();

                db.TblStudentCertificate.RemoveRange(
                    oldCertificates
                );

                var certificates =
                    mapper.Map<List<StudentCertificate>>(
                        registryReq.StudentCertificates
                    ) ?? new List<StudentCertificate>();

                foreach (var certificate in certificates)
                {
                    certificate.StudentCertificateId = 0;
                    certificate.StudentId =
                        oldStudentId;
                }

                if (certificates.Count > 0)
                {
                    await db.TblStudentCertificate
                        .AddRangeAsync(certificates);
                }
            }

            // ==========================================
            // UPDATE OR CREATE EXTEND
            // ==========================================
            var extendEntity =
                await db.TblExtend.FirstOrDefaultAsync(
                    x => x.StudentId == oldStudentId
                );

            var hasExtendInput =
                Request.Form.Keys.Any(key =>
                    key.StartsWith(
                        "Extend.",
                        StringComparison.OrdinalIgnoreCase) ||
                    key.StartsWith(
                        "registryReq.Extend.",
                        StringComparison.OrdinalIgnoreCase));

            if (hasExtendInput &&
                registryReq.Extend.ExtendDate.HasValue &&
                registryReq.Extend.ExtendDate.Value !=
                DateTime.MinValue)
            {
                if (extendEntity == null)
                {
                    extendEntity =
                        mapper.Map<Extend>(
                            registryReq.Extend
                        );

                    extendEntity.StudentId =
                        oldStudentId;

                    await db.TblExtend.AddAsync(
                        extendEntity
                    );
                }
                else
                {
                    var extendStudentId =
                        extendEntity.StudentId;

                    mapper.Map(
                        registryReq.Extend,
                        extendEntity
                    );

                    extendEntity.StudentId =
                        extendStudentId;
                }
            }
            else if (hasExtendInput && extendEntity != null)
            {
                db.TblExtend.Remove(extendEntity);
            }

            // ==========================================
            // UPDATE OR CREATE RESUME
            // ==========================================
            var resumeEntity =
                await db.TblResume.FirstOrDefaultAsync(
                    x => x.StudentId == oldStudentId
                );

            if (registryReq.AssToBach ||
                registryReq.BachToMas)
            {
                var resumeDto =
                    registryReq.Resume ??
                    new ResumeDto();

                resumeDto.StudentId =
                    oldStudentId;

                resumeDto.FieldId =
                    student.FieldId ?? 0;

                resumeDto.CPromotion =
                    registry.PromotionNo;

                resumeDto.Stage =
                    registry.StageNo.ToString();

                resumeDto.Type =
                    registryReq.AssToBach
                        ? "Associate to Bachelor"
                        : "Bachelor to Master";

                if (resumeEntity == null)
                {
                    resumeEntity =
                        mapper.Map<Resume>(
                            resumeDto
                        );

                    await db.TblResume.AddAsync(
                        resumeEntity
                    );
                }
                else
                {
                    var resumeStudentId =
                        resumeEntity.StudentId;

                    mapper.Map(
                        resumeDto,
                        resumeEntity
                    );

                    resumeEntity.StudentId =
                        resumeStudentId;
                }
            }
            else if (resumeEntity != null)
            {
                db.TblResume.Remove(
                    resumeEntity
                );
            }

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return _serverResponse.Success(
                new
                {
                    studentId = student.StudentId,
                    photoBytes =
                        student.Photo?.Length ?? 0,
                    uploadedFileName =
                        hasNewPhoto
                            ? Path.GetFileName(
                                studentPhoto!.FileName
                            )
                            : null,
                    redirectUrl = Url.Action(
                        nameof(Index),
                        "Registry"
                    )
                },
                "Student updated successfully"
            );
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return _serverResponse.ErrorInternal(ex);
        }
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
        return _serverResponse.Success(data);
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
