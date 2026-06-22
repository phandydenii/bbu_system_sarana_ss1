using AutoMapper;
using BBU_SYSTEM.Data;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Models.Req;
using BBU_SYSTEM.Repository;
using BBU_SYSTEM.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("student")]
public class StudentController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";


    [Route("all-students")]
    public ActionResult Index()
    {
        return View();
    }

    [Route("status/{status}")]
    public ActionResult Status(string status)
    {
        ViewData["status"] = status;
        ViewData["StudentStatusBadgeClasses"] = StudentStatusConstant.BadgeClasses;
        ViewData["StudentStatusDefaultBadgeClass"] = StudentStatusConstant.DefaultBadgeClass;
        return View();
    }

    [Route("details/{studentId}")]
    public ActionResult Details(string? studentId)
    {
        if (studentId == null) return NotFound(new { message = "Student not found" });
        var db = campusDbContext.DbContext(_campus);
        var registry = db.TblRegistry.FirstOrDefault(o => o.StudentId == studentId);
        var student = db.TblStudent.FirstOrDefault(s => s.StudentId == studentId)!;
        var group = new Group();
        var groupRoom = new GroupRoom();
        var stage = new Stage();
        var promotion = new Promotion();
        var term = new Term();
        var field = new Field();
        var extend = new Extend();
        School school;
        Degree degree;
        var studentGroup = db.TblStudentGroup.OrderByDescending(t => t.StudentGroupId)
            .FirstOrDefault(g => g.StudentId == studentId);
        if (studentGroup != null)
        {
            group = db.TblGroup.FirstOrDefault(i => i.GroupId == studentGroup.GroupId)!;
            groupRoom = db.TblGroupRoom.FirstOrDefault(i =>
                i.GroupId == group.GroupId && i.TermNo == studentGroup.TermNo)!;
            stage = db.TblStage.FirstOrDefault(g => g.StageId == group.StageId)!;
            promotion = db.TblPromotion.FirstOrDefault(p => p.PromotionId == stage.PromotionId)!;
            term = db.TblTerm.FirstOrDefault(t => t.StageId == stage.StageId && t.TermNo == studentGroup.TermNo)!;
            extend = db.TblExtend.FirstOrDefault(x => x.StudentId == studentId)!;
            field = db.TblField.FirstOrDefault(f => f.FieldId == student.FieldId)!;
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
            Extend = extend
        };
        var listDatas = new ListData
        {
            Degrees = db.TblDegree.ToList(),
            Schools = db.TblSchool.ToList(),
            Fields = db.TblField.ToList(),
            Promotions = db.TblPromotion.ToList(),
            Stages = db.TblStage.ToList(),
            Terms = db.TblTerm.ToList(),
            Groups = db.TblGroup.ToList(),
            GroupRooms = db.TblGroupRoom.ToList(),
            StudyTimes = db.TblStudyTime.ToList(),
            Provinces = db.TblProvince.ToList(),
            Races = db.TblRace.ToList(),
            Disabilities = db.TblDisability.ToList(),
            HightSchools = db.TblHighSchool.ToList(),
            Nationalities = db.TblNationality.ToList(),
            StudentJobs = db.TblStudentJob.ToList()
        };
        return View();
    }

    [HttpPost]
    public IActionResult GetStudents()
    {
        return View();
    }

    [HttpPost("transfer/{groupId:int}/{fromGroupId:int}/{termNo:int}")]
    public async Task<IActionResult> TransferStudents([FromBody] List<string> idList, int groupId, int fromGroupId,int termNo)
    {
        try
        {
            if (idList.Count == 0) return new ServerResponse().BadRequest("No students selected.");
            var db = campusDbContext.DbContext(_campus); 
            foreach (var id in idList)
            {
                if (string.IsNullOrWhiteSpace(id)) continue;   
                var studentId = id.Trim(); 
                await db.Database.ExecuteSqlInterpolatedAsync($@"
                    UPDATE STUDENT_GROUP
                    SET GROUP_ID = {groupId}
                    WHERE STUDENT_ID = {studentId}
                      AND GROUP_ID = {fromGroupId}
                      AND TERM_NO = {termNo}
                ");
            } 
            await db.SaveChangesAsync(); 
            return new ServerResponse().Success();
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("GetStudents")]
    public IActionResult GetStudents(StudentFilterReq req)
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
            where t.Status == TermStatusConstant.Active
            select new
            {
                sg.StudentGroupId,
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
                gr.StartPayment
            }).AsQueryable();
        if (req.DegreeId > 0) query = query.Where(x => x.DegreeId == req.DegreeId).AsQueryable();
        if (req.SchoolId != 0) query = query.Where(x => x.SchoolId == req.SchoolId).AsQueryable();
        if (req.FieldId != 0) query = query.Where(x => x.FieldId == req.FieldId).AsQueryable();
        if (req.PromotionId != 0) query = query.Where(x => x.PromotionId == req.PromotionId).AsQueryable();
        if (req.StageId != 0) query = query.Where(x => x.StageId == req.StageId).AsQueryable(); 
        if (req.GroupId != 0) query = query.Where(x => x.GroupId == req.GroupId).AsQueryable();
        if (req.TermId != 0) query = query.Where(x => x.TermId == req.TermId).AsQueryable(); 
        if (!string.IsNullOrEmpty(req.Filter))
        {
            query = req.Filter switch
            {
                "document_in" => query.Where(x => x.documentin != "").AsQueryable(),
                "document_out" => query.Where(x => x.documentout != "").AsQueryable(),
                "official" or "authenticated" => query.Where(x => x.authenticated_no != "").AsQueryable(),
                "no_document_in" => query.Where(x => x.documentin == "").AsQueryable(),
                "no_document_out" => query.Where(x => x.documentout == "").AsQueryable(),
                "no_official" => query.Where(x => x.documentin == "" && x.documentout == "").AsQueryable(),
                "no_authenticated" => query.Where(x => x.authenticated_no == "").AsQueryable(),
                _ => query
            };
        }

        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(d =>
                d.StudentName!.Contains(searchValue) ||
                d.StudentId.Contains(searchValue) ||
                d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();

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

    [HttpPost("get-students-registry-associate")]
    public async Task<IActionResult> GetStudentRegistryAssociateGroup(AssignGroupViewModel req,bool isAll = false)
    {
        try
        { 
            var db = campusDbContext.DbContext(_campus);  
            var query =
                from s in db.TblStudent
                join r in db.TblRegistry on s.StudentId equals r.StudentId
                join p in db.TblPromotion on r.PromotionNo equals p.PromotionNo
                join st in db.TblStage on r.StageNo equals st.StageNo
                where s.FieldId == req.FieldId
                      && s.Status == "REGISTER"
                      && r.TermNo == 1
                      && r.DegreeId == 1
                      && r.SchoolId == req.SchoolId
                      && r.StudyTime == req.StudyTime
                      && p.PromotionId == req.PromotionId
                      && st.StageId == req.StageId
                select new
                {
                    s.StudentId,
                    s.StudentName,
                    s.StudentNameInKhmer,
                    s.Sex,
                    s.DateOfBirth,
                    s.Phone,
                    s.Email,
                    s.Status
                };
            if (isAll) return new ServerResponse().Success(await query.ToListAsync());
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.StudentName!.Contains(searchValue) ||
                    d.StudentId.Contains(searchValue) ||
                    d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.StudentId ?? "");
            var data = await query.Skip(skip).Take(pageSize).ToListAsync();

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
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("get-students-registry-foundation")]
    public async Task<IActionResult> GetStudentRegistryFoundationGroup(AssignGroupViewModel req)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query = (from s in db.TblStudent
                where s.FieldId == req.FieldId && s.Status == "REGISTER"
                                               && (from r in db.TblRegistry
                                                   where r.TermNo == 1
                                                         && r.DegreeId == 2
                                                         && r.SchoolId == req.SchoolId
                                                         && r.PromotionNo == req.PromotionNo
                                                         && r.StageNo == req.StageNo
                                                         && r.StudyTime == req.StudyTime
                                                   select r.StudentId
                                               ).Contains(s.StudentId)
                select new
                {
                    s.StudentId,
                    s.StudentName,
                    s.StudentNameInKhmer,
                    s.Sex,
                    s.DateOfBirth,
                    s.Phone,
                    s.Email,
                    s.Status
                }).AsQueryable();
            if (req.IsAll) return new ServerResponse().Success(await query.ToListAsync());
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.StudentName!.Contains(searchValue) ||
                    d.StudentId.Contains(searchValue) ||
                    d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.StudentId ?? "");
            var data = await query.Skip(skip).Take(pageSize).ToListAsync();

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
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("get-students-registry-specialize")]
    public async Task<IActionResult> GetStudentSpecialGroup(AssignGroupViewModel req)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query = (
                from s in db.TblStudent
                join r in db.TblRegistry on s.StudentId equals r.StudentId
                join sg in db.TblStudentGroup on s.StudentId equals sg.StudentId
                join g in db.TblGroup on sg.GroupId equals g.GroupId
                join st in db.TblStage on g.StageId equals st.StageId
                join p in db.TblPromotion on st.PromotionId equals p.PromotionId
                join sc in db.TblSchool on p.SchoolId equals sc.SchoolId
                where p.DegreeId == 2
                      && r.SchoolId == req.SchoolId
                      && sg.TermNo == 2
                      && g.StudyTime == req.StudyTime
                      && st.StageId == req.StageId
                      && p.PromotionId == req.PromotionId
                      && p.AcademicYearStart == req.AcademicYear
                      && sc.IsFoundationSchool == 1
                select new
                {
                    s.StudentId,
                    s.StudentName,
                    s.StudentNameInKhmer,
                    s.Sex,
                    s.DateOfBirth,
                    s.PlaceOfBirthId,
                    s.RaceId,
                    s.NationalityId,
                    s.MaritalStatus,
                    s.HighSchoolGraduatedYear,
                    s.FromProvinceId,
                    s.JobId,
                    s.Phone,
                    s.Email,
                    s.Address,
                    s.AddressInKhmer,
                    s.ContactPersonId,
                    s.IsPhotoReceived,
                    s.Note,
                    s.FieldId,
                    s.Status
                }).AsQueryable();
            if (req.IsAll) new ServerResponse().Success(await query.ToListAsync());
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.StudentName!.Contains(searchValue) ||
                    d.StudentId.Contains(searchValue) ||
                    d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();

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
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("get-students-registry-diploma")]
    public async Task<IActionResult> GetStudentDiploma(AssignGroupViewModel req)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query = (from s in db.TblStudent
                where s.FieldId == req.FieldId && s.Status == "REGISTER"
                                               && (from r in db.TblRegistry
                                                   where r.TermNo == 1
                                                         && r.DegreeId == 3
                                                         && r.SchoolId == req.SchoolId
                                                         && r.PromotionNo == req.PromotionNo
                                                         && r.StageNo == req.StageNo
                                                         && r.StudyTime == req.StudyTime
                                                   select r.StudentId
                                               ).Contains(s.StudentId)
                select new
                {
                    s.StudentId,
                    s.StudentName,
                    s.StudentNameInKhmer,
                    s.Sex,
                    s.DateOfBirth,
                    s.Phone,
                    s.Email,
                    s.Status
                }).AsQueryable();
            if (req.IsAll)
            {
                return Ok(new
                {
                    data = await query.ToListAsync(),
                    status = new
                    {
                        code = "200",
                        message = "Succeeded"
                    }
                });
            }

            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.StudentName!.Contains(searchValue) ||
                    d.StudentId.Contains(searchValue) ||
                    d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();

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
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }


    [HttpPost("get-students-registry-master")]
    public async Task<IActionResult> GetStudentMasterGroup(AssignGroupViewModel req)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query = (from s in db.TblStudent
                where s.FieldId == req.FieldId && s.Status == "REGISTER"
                                               && (from r in db.TblRegistry
                                                   where r.TermNo == 1
                                                         && r.DegreeId == 4
                                                         && r.SchoolId == req.SchoolId
                                                         && r.PromotionNo == req.PromotionNo
                                                         && r.StageNo == req.StageNo
                                                         && r.StudyTime == req.StudyTime
                                                   select r.StudentId
                                               ).Contains(s.StudentId)
                select new
                {
                    s.StudentId,
                    s.StudentName,
                    s.StudentNameInKhmer,
                    s.Sex,
                    s.DateOfBirth,
                    s.Phone,
                    s.Email,
                    s.Status
                }).AsQueryable();

            if (req.IsAll) new ServerResponse().Success(await query.ToListAsync());
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.StudentName!.Contains(searchValue) ||
                    d.StudentId.Contains(searchValue) ||
                    d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();

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
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("get-students-registry-doctor")]
    public async Task<IActionResult> GetStudentDoctor(AssignGroupViewModel req)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query = (from s in db.TblStudent
                where s.FieldId == req.FieldId && s.Status == "REGISTER"
                                               && (from r in db.TblRegistry
                                                   where r.TermNo == 1
                                                         && r.DegreeId == req.DegreeId
                                                         && r.SchoolId == req.SchoolId
                                                         && r.PromotionNo == req.PromotionNo
                                                         && r.StageNo == req.StageNo
                                                         && r.StudyTime == req.StudyTime
                                                   select r.StudentId
                                               ).Contains(s.StudentId)
                select new
                {
                    s.StudentId,
                    s.StudentName,
                    s.StudentNameInKhmer,
                    s.Sex,
                    s.DateOfBirth,
                    s.Phone,
                    s.Email,
                    s.Status
                }).AsQueryable();

            if (req.IsAll) return new ServerResponse().Success(await query.ToListAsync());

            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.StudentName!.Contains(searchValue) ||
                    d.StudentId.Contains(searchValue) ||
                    d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();

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
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }


    [HttpPost("get-students-registry-other")]
    public IActionResult GetStudentOther()
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query =
                from s in db.TblStudent
                join r in db.TblRegistry on s.StudentId equals r.StudentId
                where s.Status == "REGISTER" && r.TermNo > 1
                orderby s.StudentName
                select s;


            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.StudentName!.Contains(searchValue) ||
                    d.StudentId!.Contains(searchValue) ||
                    d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();

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
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }


    [HttpPost("get-students-other-university")]
    public IActionResult GetStudentOtherUniversity()
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
            var fromUniversity = db.TblExtend.Where(e => e.ExtendFrom == "OTHER_UNIVERSITY").Select(x => x.StudentId)
                .AsQueryable();
            var query = db.TblStudent.Where(x => fromUniversity.Contains(x.StudentId)).AsQueryable();


            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.StudentName!.Contains(searchValue) ||
                    d.StudentId!.Contains(searchValue) ||
                    d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();

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
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }


    [HttpPost("get-all-student-re-exam-payment")]
    public IActionResult GetAllStudentReExamPayment()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);
        var fromUniversity = db.TblReExamPayment.Select(x => x.StudentId).AsQueryable();
        var query = db.TblStudent.Where(x => fromUniversity.Contains(x.StudentId)).AsQueryable();


        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(d =>
                d.StudentName!.Contains(searchValue) ||
                d.StudentId!.Contains(searchValue) ||
                d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();

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


    [HttpPost("get-all-student-re-exam-history")]
    public IActionResult GetAllStudentReExamHistory()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        var db = campusDbContext.DbContext(_campus);

        var query =
            (
                from s in db.TblStudent
                join sg in db.TblStudentGroup on s.StudentId equals sg.StudentId
                join g in db.TblGroup on sg.GroupId equals g.GroupId
                join st in db.TblStage on g.StageId equals st.StageId
                join p in db.TblPromotion on st.PromotionId equals p.PromotionId
                join d in db.TblDegree on p.DegreeId equals d.DegreeId
                join t in db.TblTerm
                    on new { st.StageId, sg.TermNo }
                    equals new { t.StageId, t.TermNo }
                where db.TblScore.Any(sc =>
                    sc.StudentGroupId == sg.StudentGroupId &&
                    (sc.MidTermScore + sc.FinalScore) <
                    (
                        d.DegreeName == "Doctor" ? 70 :
                        d.DegreeName == "Master" ? 65 : 60
                    )
                )
                select new
                {
                    s.StudentId,
                    s.StudentName,
                    s.StudentNameInKhmer,
                    s.Sex,
                    s.DateOfBirth
                }
            )
            .Distinct()
            .AsQueryable();


        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(d =>
                d.StudentName!.Contains(searchValue) ||
                d.StudentId!.Contains(searchValue) ||
                d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();

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

    [HttpPost("GetAllComplementSemesterStudents")]
    public IActionResult GetAllComplementSemesterStudents()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);
        var query = (from s in db.TblStudent
                where (from c in db.TblComplementSemesterScores
                        select c.StudentId).Distinct()
                    .Contains(s.StudentId)
                select s)
            .AsQueryable();


        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(d =>
                d.StudentName!.Contains(searchValue) ||
                d.StudentId!.Contains(searchValue) ||
                d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();

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

    [HttpPost("GetAllComplementOrientedCourseStudents")]
    public IActionResult GetAllComplementOrientedCourseStudents()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);
        var query = (from s in db.TblStudent
                where (from c in db.TblComplementOrientedCourseScores
                        select c.StudentId)
                    .Distinct()
                    .Contains(s.StudentId)
                select s)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(d =>
                d.StudentName!.Contains(searchValue) ||
                d.StudentId!.Contains(searchValue) ||
                d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();

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


    [HttpPost("GetAllComplementFailedCourseStudents")]
    public IActionResult GetAllComplementFailedCourseStudents()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);
        var query = (from s in db.TblStudent
                where (from c in db.TblComplementOrientedCourseScores
                        select c.StudentId)
                    .Distinct()
                    .Contains(s.StudentId)
                select s)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(d =>
                d.StudentName!.Contains(searchValue) ||
                d.StudentId!.Contains(searchValue) ||
                d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();

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


    [HttpGet("getsearchstudent")]
    public IActionResult GetStudentByFilter(string searchValue)
    {
        var db = campusDbContext.DbContext(_campus);
        var query = (from s in db.TblStudent
            select new
            {
                s.StudentId,
                s.StudentName,
                s.StudentNameInKhmer,
                s.Sex,
                s.DateOfBirth,
                s.Phone
            }).AsQueryable();
        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(d =>
                d.StudentName!.Contains(searchValue) ||
                d.StudentId.Contains(searchValue) ||
                d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();
        return Json(query.Take(50).ToList());
    }

    [Route("student_id/{studentId}")]
    [HttpGet]
    public ActionResult StudentById(string? studentId)
    {
        if (studentId == null) return NotFound();
        var db = campusDbContext.DbContext(_campus);
        var student = db.TblStudent.FirstOrDefault(s => s.StudentId == studentId)!;
        return Json(student);
    }

    [Route("academic/student-id/{studentId}")]
    [HttpGet]
    public IActionResult GetStudentDetail(string? studentId)
    {
        if (studentId == null) return new ServerResponse().NotFound("Student not found");
        var db = campusDbContext.DbContext(_campus);
        var registry = (from r in db.TblRegistry
            join d in db.TblDegree on r.DegreeId equals d.DegreeId
            join s in db.TblSchool on r.SchoolId equals s.SchoolId
            where r.StudentId == studentId
            select new
            {
                r.RegistrationId,
                r.RegistrationDate,
                r.DoneDate, r.HighSchoolResult, r.HighSchoolTableNo,
                d.DegreeId, d.DegreeName, d.DegreeInKhmer,
                s.SchoolId, s.SchoolName, s.SchoolNameInKhmer,
                r.PromotionNo, r.TermNo, r.StageNo, r.StudyTime, r.StudentId
            }).FirstOrDefault();
        var student = db.TblStudent.FirstOrDefault(s => s.StudentId == studentId)!;
        var contactPerson = db.TblContactPerson.FirstOrDefault(c => c.ContactPersonId == student.ContactPersonId);
        var group = new Group();
        var groupRoom = new GroupRoom();
        var stage = new Stage();
        var promotion = new Promotion();
        var term = new Term();
        var field = new Field();
        var fieldGroup = new Field();
        var extend = new Extend();
        School school;
        Degree degree;
        var studentGroup = db.TblStudentGroup.OrderByDescending(t => t.StudentGroupId)
            .FirstOrDefault(g => g.StudentId == studentId);
        if (studentGroup != null)
        {
            group = db.TblGroup.FirstOrDefault(i => i.GroupId == studentGroup.GroupId)!;
            groupRoom = db.TblGroupRoom.FirstOrDefault(i =>
                i.GroupId == group.GroupId && i.TermNo == studentGroup.TermNo)!;
            stage = db.TblStage.FirstOrDefault(g => g.StageId == group.StageId)!;
            promotion = db.TblPromotion.FirstOrDefault(p => p.PromotionId == stage.PromotionId)!;
            term = db.TblTerm.FirstOrDefault(t => t.StageId == stage.StageId && t.TermNo == studentGroup.TermNo)!;
            extend = db.TblExtend.FirstOrDefault(x => x.StudentId == studentId)!;
            field = db.TblField.FirstOrDefault(f => f.FieldId == student.FieldId)!;
            school = db.TblSchool.FirstOrDefault(s => s.SchoolId == promotion.SchoolId)!;
            degree = db.TblDegree.FirstOrDefault(d => d.DegreeId == promotion.DegreeId)!;
            fieldGroup = db.TblField.FirstOrDefault(x => x.FieldId == group.FieldId);
        }
        else
        {
            school = db.TblSchool.FirstOrDefault(s => s.SchoolId == registry!.SchoolId)!;
            degree = db.TblDegree.FirstOrDefault(d => d.DegreeId == registry!.DegreeId)!;
        }

        var studentView = new
        {
            Student = student,
            ContactPerson = contactPerson,
            Registry = registry,
            Degree = degree,
            School = school,
            Field = field,
            FieldGroup = fieldGroup,
            Promotion = promotion,
            Stage = stage,
            Term = term,
            Group = group,
            GroupRoom = groupRoom,
            Extend = extend
        };
        return new ServerResponse().Success(studentView);
    }

    [HttpPost("GetAllFailedStudents")]
    public IActionResult GetAllFailedStudents()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        var db = campusDbContext.DbContext(_campus);

        var query = (from student in db.TblStudent
                join sg in db.TblStudentGroup on student.StudentId equals sg.StudentId
                join g in db.TblGroup on sg.GroupId equals g.GroupId
                join stg in db.TblStage on g.StageId equals stg.StageId
                join pr in db.TblPromotion on stg.PromotionId equals pr.PromotionId
                join d in db.TblDegree on pr.DegreeId equals d.DegreeId
                join t in db.TblTerm on new { stg.StageId, sg.TermNo }
                    equals new { t.StageId, t.TermNo }
                where db.TblStudentGroup
                    .Where(x => db.TblScore.Any(s =>
                        s.StudentGroupId == x.StudentGroupId &&
                        s.MidTermScore + s.FinalScore <
                        (d.DegreeName == "Doctor" ? 70 :
                            d.DegreeName == "Master" ? 65 : 60)))
                    .Select(x => x.StudentId)
                    .Distinct()
                    .Contains(student.StudentId)
                select new
                {
                    student.StudentId,
                    student.StudentName,
                    student.StudentNameInKhmer,
                    student.Sex,
                    student.DateOfBirth
                })
            .Distinct()
            .AsQueryable();
        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(d =>
                d.StudentName!.Contains(searchValue) ||
                d.StudentId!.Contains(searchValue) ||
                d.StudentNameInKhmer!.Contains(searchValue)).AsQueryable();
        query = query.OrderBy(x => x.StudentName).AsQueryable();
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

    [HttpPost("academic/student-list/{status}")]
    public IActionResult GetStudentList(string status = "all")
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
            var query = db.TblStudent.AsQueryable();
            if (status != "all")
            {
                query = query.Where(s => s.Status!.ToLower() == status);
            }

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.StudentName!.Contains(searchValue) ||
                    d.StudentId!.Contains(searchValue) ||
                    d.StudentNameInKhmer!.Contains(searchValue));

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
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }


    [HttpPatch("academic/update-accept-certificate/{studentId}")]
    public async Task<IActionResult> UpdateStudentAcceptedCertificate(string studentId, StudentDto student)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var data = await db.TblStudent.FindAsync(studentId);
            if (data == null) return new ServerResponse().NotFound(); 
            data.IsAcceptCertificate = student.IsAcceptCertificate;
            data.AcceptDate = student.AcceptDate;
            data.CertificateNo = student.CertificateNo;
            data.CertificateOut = student.CertificateOut;
            data.NoteTicket = student.NoteTicket;
            await db.SaveChangesAsync();
            return new ServerResponse().Success(msg: "Student accepted");
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPatch("update-student")]
    public async Task<IActionResult> UpdateStudentAcceptedCertificate(StudentDto student, RegistryDto registry,
        ContactPersonDto contactPerson)
    {
        var db = campusDbContext.DbContext(_campus);
        await db.Database.BeginTransactionAsync();
        try
        {
            var studentCheck = await db.TblStudent.FirstOrDefaultAsync(s => s.StudentId == student.StudentId);
            if (studentCheck == null)
            {
                await db.Database.RollbackTransactionAsync();
                return new ServerResponse().NotFound("Student not found");
            }

            contactPerson.ContactPersonId = (int)studentCheck.ContactPersonId!;
            //===student 
            var studentUpdate = new StudentUpdateViewModel();
            mapper.Map(student, studentUpdate);
            mapper.Map(studentUpdate, studentCheck);
            // db.TblStudent.Update(studentCheck);
            var studentChanged = db.Entry(studentCheck).Properties.Any(p => p.IsModified);

            //===registry
            var registryCheck = await db.TblRegistry.FirstOrDefaultAsync(x => x.StudentId == student.StudentId);
            if (registryCheck == null)
            {
                await db.Database.RollbackTransactionAsync();
                return new ServerResponse().NotFound("Registry not found");
            }

            registryCheck.HighSchoolResult = registry.HighSchoolResult;
            registryCheck.HighSchoolTableNo = registry.HighSchoolTableNo;
            // db.TblRegistry.Update(registryCheck);
            var registryChanged = db.Entry(registryCheck).Properties.Any(p => p.IsModified);

            //===Contact Person
            var contactCheck =
                await db.TblContactPerson.FirstOrDefaultAsync(x => x.ContactPersonId == contactPerson.ContactPersonId);
            if (contactCheck == null)
            {
                await db.Database.RollbackTransactionAsync();
                return new ServerResponse().NotFound("Registry not found");
            }

            mapper.Map(contactPerson, contactCheck);
            // db.TblContactPerson.Update(contactCheck);
            var contactPersonChanged = db.Entry(contactCheck).Properties.Any(p => p.IsModified);

            // ==== SAVE ONLY IF CHANGED ====
            if (studentChanged || registryChanged || contactPersonChanged)
            {
                await db.SaveChangesAsync();
            }
            await db.Database.CommitTransactionAsync();
            return new ServerResponse().Success("Student and related data updated successfully");
        }
        catch (Exception e)
        {
            await db.Database.RollbackTransactionAsync();
            return new ServerResponse().ErrorInternal(e);
        }
        finally
        {
            await db.DisposeAsync();
        }
    }

    [HttpPost("change-branch")]
    public async Task<IActionResult> ChangeBranch(ChangeBranchDto dto)
    {
        var db = campusDbContext.DbContext(_campus);
        await using var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            var student = await db.TblStudent.FirstOrDefaultAsync(x => x.StudentId == dto.StudentId);
            if (student == null) return new ServerResponse().NotFound("Student not found.");
            student.Status = StudentStatusConstant.ChangeBranch;
            if (dto.ChangeBranchId == 0)
            {
                var data = mapper.Map<ChangeBranch>(dto);
                await db.TblChangeBranch.AddAsync(data);
                await db.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServerResponse().Success(data);
            }
            var oldData = await db.TblChangeBranch.FirstOrDefaultAsync(x => x.ChangeBranchId == dto.ChangeBranchId);
            if (oldData == null) return new ServerResponse().NotFound("Change branch record not found.");
            mapper.Map(dto, oldData);
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            return new ServerResponse().Success(oldData);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return new ServerResponse().ErrorInternal(e);
        }
    }
    [HttpPost("quit")]
    public async Task<IActionResult> Quit(QuitDto dto)
    {
        var db = campusDbContext.DbContext(_campus);
        try
        { 
            if (dto.QuitId == 0)
            {
                var data = mapper.Map<Quit>(dto);
                await db.TblQuit.AddAsync(data);
                await db.SaveChangesAsync(); 
                return new ServerResponse().Success(data);
            }
            var oldData = await db.TblQuit.FirstOrDefaultAsync(x => x.QuitId == dto.QuitId);
            if (oldData == null) return new ServerResponse().NotFound("Record found.");
            mapper.Map(dto, oldData);
            await db.SaveChangesAsync(); 
            return new ServerResponse().Success(oldData);
        }
        catch (Exception e)
        { 
            return new ServerResponse().ErrorInternal(e);
        }
    }
    [HttpPost("suppress")]
    public async Task<IActionResult> Suppress(SuppressDto dto)
    {
        var db = campusDbContext.DbContext(_campus);
        try
        { 
            if (dto.SuppressId == 0)
            {
                var data = mapper.Map<Suppress>(dto); 
                await db.TblSuppress.AddAsync(data);
                await db.SaveChangesAsync(); 
                return new ServerResponse().Success(data);
            }
            var oldData = await db.TblSuppress.FirstOrDefaultAsync(x => x.SuppressId == dto.SuppressId);
            if (oldData == null) return new ServerResponse().NotFound("Record found.");
            mapper.Map(dto, oldData);
            await db.SaveChangesAsync(); 
            return new ServerResponse().Success(oldData);
        }
        catch (Exception e)
        { 
            return new ServerResponse().ErrorInternal(e);
        }
    } [HttpPost("suspend")]
    public async Task<IActionResult> Suspend(SuspendDto dto)
    {
        var db = campusDbContext.DbContext(_campus); 
        await using var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            var student = await db.TblStudent.FirstOrDefaultAsync(x => x.StudentId == dto.StudentId);
            if (student == null) return new ServerResponse().NotFound("Student not found.");
            student.Status = StudentStatusConstant.Suspend;
            if (dto.SuspendId == 0)
            {
                var data = mapper.Map<Suspend>(dto); 
                await db.TblSuspend.AddAsync(data);
                await db.SaveChangesAsync(); 
                return new ServerResponse().Success(data);
            }
            var oldData = await db.TblSuspend.FirstOrDefaultAsync(x => x.SuspendId == dto.SuspendId);
            if (oldData == null) return new ServerResponse().NotFound("Record found.");
            mapper.Map(dto, oldData);
            await db.SaveChangesAsync();  
            await transaction.CommitAsync();
            return new ServerResponse().Success(oldData);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return new ServerResponse().ErrorInternal(e);
        }
    } 
}