using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Repository;
using BBU_SYSTEM.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("assign-student-group")]
public class AssignStudentGroupController(
    ICampusDbContext campusDbContext,
    IHttpContextAccessor context) : Controller
{
    private readonly string _campus =
        context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("get-students-registry-foundation")]
    public async Task<IActionResult> GetStudentsRegistryFoundation(
        [FromForm] AssignGroupViewModel req)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            var degreeId = Convert.ToInt32(req.DegreeId);
            var schoolId = Convert.ToInt32(req.SchoolId);
            var fieldId = Convert.ToInt32(req.FieldId);
            var promotionNo = Convert.ToInt32(req.PromotionNo);
            var stageNo = Convert.ToInt32(req.StageNo);
            var studyTime = (req.StudyTime ?? string.Empty).Trim();

            if (degreeId <= 0 ||
                schoolId <= 0 ||
                fieldId <= 0 ||
                promotionNo <= 0 ||
                stageNo <= 0 ||
                string.IsNullOrWhiteSpace(studyTime))
            {
                return new ServerResponse().BadRequest(
                    "Degree, school, field, promotion, stage and study time are required."
                );
            }

            var students = await (
                from registry in db.TblRegistry.AsNoTracking()
                join student in db.TblStudent.AsNoTracking()
                    on registry.StudentId equals student.StudentId
                where registry.DegreeId == degreeId
                      && registry.SchoolId == schoolId
                      && student.FieldId == fieldId
                      && registry.PromotionNo == promotionNo
                      && registry.StageNo == stageNo
                      && registry.StudyTime != null
                      && registry.StudyTime.Trim() == studyTime
                      && student.Status != null
                      && student.Status.Trim() == "REGISTER"
                select new
                {
                    student.StudentId,
                    student.StudentName,
                    student.StudentNameInKhmer,
                    student.Sex,
                    student.DateOfBirth
                })
                .Distinct()
                .OrderBy(x => x.StudentId)
                .ToListAsync();

            return new ServerResponse().Success(
                students,
                students.Count > 0
                    ? $"Found {students.Count} student(s)."
                    : "No students matched the selected filters."
            );
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpPost("get-students-registry-other")]
    public Task<IActionResult> GetStudentsRegistryOther(
        [FromForm] AssignGroupViewModel req)
    {
        var degreeId = Convert.ToInt32(req.DegreeId);

        return degreeId switch
        {
            1 => GetRegularStudents(req, expectedDegreeId: 1),
            4 => GetUnpromotedStudents(req),
            _ => Task.FromResult<IActionResult>(
                new ServerResponse().BadRequest(
                    "This filter only supports Associate or Unpromoted students."))
        };
    }

    [HttpPost("get-students-registry-diploma")]
    public Task<IActionResult> GetStudentsRegistryDiploma(
        [FromForm] AssignGroupViewModel req)
    {
        return GetRegularStudents(req, expectedDegreeId: 3);
    }

    [HttpPost("get-students-registry-master")]
    public Task<IActionResult> GetStudentsRegistryMaster(
        [FromForm] AssignGroupViewModel req)
    {
        return GetRegularStudents(
            req,
            expectedDegreeId: 4,
            filterTermNo: false);
    }

    [HttpPost("get-students-registry-doctor")]
    public Task<IActionResult> GetStudentsRegistryDoctor(
        [FromForm] AssignGroupViewModel req)
    {
        return GetRegularStudents(
            req,
            expectedDegreeId: 5,
            filterTermNo: false);
    }

    [HttpPost("get-students-registry-specialize")]
    public async Task<IActionResult> GetStudentsRegistrySpecialize(
        [FromForm] AssignGroupViewModel req)
    {
        try
        {
            var schoolId = Convert.ToInt32(req.SchoolId);
            var promotionId = Convert.ToInt32(req.PromotionId);
            var stageId = Convert.ToInt32(req.StageId);
            var academicYear = Convert.ToInt32(req.AcademicYear);
            var studyTime = (req.StudyTime ?? string.Empty).Trim();

            if (schoolId <= 0 ||
                promotionId <= 0 ||
                stageId <= 0 ||
                academicYear <= 0 ||
                string.IsNullOrWhiteSpace(studyTime))
            {
                return new ServerResponse().BadRequest(
                    "Academic year, school, promotion, stage and study time are required.");
            }

            var db = campusDbContext.DbContext(_campus);

            var students = await (
                from student in db.TblStudent.AsNoTracking()
                join registry in db.TblRegistry.AsNoTracking()
                    on student.StudentId equals registry.StudentId
                join studentGroup in db.TblStudentGroup.AsNoTracking()
                    on student.StudentId equals studentGroup.StudentId
                join groupRow in db.TblGroup.AsNoTracking()
                    on studentGroup.GroupId equals groupRow.GroupId
                join stage in db.TblStage.AsNoTracking()
                    on groupRow.StageId equals stage.StageId
                join promotion in db.TblPromotion.AsNoTracking()
                    on stage.PromotionId equals promotion.PromotionId
                join school in db.TblSchool.AsNoTracking()
                    on promotion.SchoolId equals school.SchoolId
                where promotion.DegreeId == 2
                      && registry.SchoolId == schoolId
                      && studentGroup.TermNo == 2
                      && groupRow.StudyTime != null
                      && groupRow.StudyTime.Trim() == studyTime
                      && stage.StageId == stageId
                      && promotion.PromotionId == promotionId
                      && promotion.AcademicYearStart == academicYear
                      && school.IsFoundationSchool == 1
                      && student.Status != null
                      && student.Status.Trim() == "REGISTER"
                select new
                {
                    student.StudentId,
                    student.StudentName,
                    student.StudentNameInKhmer,
                    student.Sex,
                    student.DateOfBirth
                })
                .Distinct()
                .OrderBy(x => x.StudentId)
                .ToListAsync();

            return StudentResult(students);
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpPost("assign/{groupId:int}")]
    public async Task<IActionResult> AssignStudents(
        [FromBody] List<string>? idList,
        int groupId)
    {
        try
        {
            var studentIds = idList?
                .Where(id => !string.IsNullOrWhiteSpace(id))
                .Select(id => id.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList() ?? new List<string>();

            if (studentIds.Count == 0)
            {
                return new ServerResponse().BadRequest(
                    "No students selected.");
            }

            if (groupId <= 0)
            {
                return new ServerResponse().BadRequest(
                    "Please select a valid group.");
            }

            var db = campusDbContext.DbContext(_campus);

            var groupInfo = await (
                from groupRow in db.TblGroup.AsNoTracking()
                join stage in db.TblStage.AsNoTracking()
                    on groupRow.StageId equals stage.StageId
                join promotion in db.TblPromotion.AsNoTracking()
                    on stage.PromotionId equals promotion.PromotionId
                where groupRow.GroupId == groupId
                select new
                {
                    groupRow.GroupId,
                    groupRow.StudyTime,
                    stage.StageNo,
                    promotion.PromotionNo,
                    promotion.DegreeId,
                    promotion.SchoolId
                })
                .FirstOrDefaultAsync();

            if (groupInfo == null)
            {
                return new ServerResponse().BadRequest(
                    $"GroupId {groupId} was not found.");
            }

            // Do not filter this query with an in-memory ID collection.
            // Newer EF Core versions translate that pattern into
            // OPENJSON(... '$'), which fails on older SQL Server
            // compatibility levels.
            var invalidStudentIds = new List<string>();

            foreach (var studentId in studentIds)
            {
                var student = await db.TblStudent
                    .FirstOrDefaultAsync(row =>
                        row.StudentId == studentId &&
                        row.Status != null &&
                        row.Status.Trim() == "REGISTER");

                if (student == null)
                {
                    invalidStudentIds.Add(studentId);
                    continue;
                }

                // Assigned students should no longer appear in the
                // REGISTER student-filter results.
                student.Status = "ACTIVE";
            }

            if (invalidStudentIds.Count > 0)
            {
                return new ServerResponse().BadRequest(
                    "These students were not found or are not REGISTER: " +
                    string.Join(", ", invalidStudentIds));
            }

            var targetStudyTime =
                (groupInfo.StudyTime ?? string.Empty).Trim();

            await using var transaction =
                await db.Database.BeginTransactionAsync();

            foreach (var studentId in studentIds)
            {
                var registryTermNoValue = await db.TblRegistry
                    .AsNoTracking()
                    .Where(registry =>
                        registry.StudentId == studentId &&
                        registry.DegreeId == groupInfo.DegreeId &&
                        registry.SchoolId == groupInfo.SchoolId &&
                        registry.PromotionNo == groupInfo.PromotionNo &&
                        registry.StageNo == groupInfo.StageNo &&
                        registry.StudyTime != null &&
                        registry.StudyTime.Trim() == targetStudyTime)
                    .OrderByDescending(registry =>
                        registry.RegistrationId)
                    .Select(registry => registry.TermNo)
                    .FirstOrDefaultAsync();

                var termNo = Convert.ToInt32(
                    registryTermNoValue);

                if (termNo <= 0)
                {
                    var previousTermNoValue =
                        await db.TblStudentGroup
                            .AsNoTracking()
                            .Where(studentGroup =>
                                studentGroup.StudentId == studentId)
                            .OrderByDescending(studentGroup =>
                                studentGroup.StudentGroupId)
                            .Select(studentGroup =>
                                studentGroup.TermNo)
                            .FirstOrDefaultAsync();

                    termNo = Convert.ToInt32(
                        previousTermNoValue);
                }

                if (termNo <= 0)
                {
                    termNo = 1;
                }

                var assignmentExists = await db.TblStudentGroup
                    .AsNoTracking()
                    .AnyAsync(studentGroup =>
                        studentGroup.StudentId == studentId &&
                        studentGroup.TermNo == termNo);

                if (assignmentExists)
                {
                    await db.Database.ExecuteSqlRawAsync(
                        @"UPDATE dbo.STUDENT_GROUP
                          SET GROUP_ID = @groupId
                          WHERE STUDENT_ID = @studentId
                            AND TERM_NO = @termNo",
                        new SqlParameter("@groupId", groupId),
                        new SqlParameter("@studentId", studentId),
                        new SqlParameter("@termNo", termNo));
                }
                else
                {
                    await db.Database.ExecuteSqlRawAsync(
                        @"INSERT INTO dbo.STUDENT_GROUP
                          (
                              STUDENT_ID,
                              GROUP_ID,
                              TERM_NO
                          )
                          VALUES
                          (
                              @studentId,
                              @groupId,
                              @termNo
                          )",
                        new SqlParameter("@studentId", studentId),
                        new SqlParameter("@groupId", groupId),
                        new SqlParameter("@termNo", termNo));
                }
            }

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return new ServerResponse().Success(
                new
                {
                    groupId,
                    assignedCount = studentIds.Count
                },
                $"Successfully assigned {studentIds.Count} student(s).");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    private async Task<IActionResult> GetRegularStudents(
        AssignGroupViewModel req,
        int expectedDegreeId,
        bool filterTermNo = true)
    {
        try
        {
            var submittedDegreeId = Convert.ToInt32(req.DegreeId);
            var schoolId = Convert.ToInt32(req.SchoolId);
            var fieldId = Convert.ToInt32(req.FieldId);
            var promotionNo = Convert.ToInt32(req.PromotionNo);
            var stageNo = Convert.ToInt32(req.StageNo);
            var studyTime = (req.StudyTime ?? string.Empty).Trim();

            if (submittedDegreeId != expectedDegreeId)
            {
                return new ServerResponse().BadRequest(
                    $"This form requires DegreeId {expectedDegreeId}.");
            }

            if (schoolId <= 0 ||
                fieldId <= 0 ||
                promotionNo <= 0 ||
                stageNo <= 0 ||
                string.IsNullOrWhiteSpace(studyTime))
            {
                return new ServerResponse().BadRequest(
                    "Degree, school, field, promotion, stage and study time are required.");
            }

            var db = campusDbContext.DbContext(_campus);

            var students = await (
                from registry in db.TblRegistry.AsNoTracking()
                join student in db.TblStudent.AsNoTracking()
                    on registry.StudentId equals student.StudentId
                where registry.DegreeId == expectedDegreeId
                      && registry.SchoolId == schoolId
                      && student.FieldId == fieldId
                      && registry.PromotionNo == promotionNo
                      && registry.StageNo == stageNo
                      && (!filterTermNo || registry.TermNo == 1)
                      && registry.StudyTime != null
                      && registry.StudyTime.Trim() == studyTime
                      && student.Status != null
                      && student.Status.Trim() == "REGISTER"
                select new
                {
                    student.StudentId,
                    student.StudentName,
                    student.StudentNameInKhmer,
                    student.Sex,
                    student.DateOfBirth
                })
                .Distinct()
                .OrderBy(x => x.StudentId)
                .ToListAsync();

            return StudentResult(students);
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    private async Task<IActionResult> GetUnpromotedStudents(
        AssignGroupViewModel req)
    {
        try
        {
            var degreeId = Convert.ToInt32(req.DegreeId);
            var schoolId = Convert.ToInt32(req.SchoolId);
            var promotionNo = Convert.ToInt32(req.PromotionNo);
            var stageNo = Convert.ToInt32(req.StageNo);
            var studyTime = (req.StudyTime ?? string.Empty).Trim();

            if (degreeId <= 0 ||
                schoolId <= 0 ||
                promotionNo <= 0 ||
                stageNo <= 0 ||
                string.IsNullOrWhiteSpace(studyTime))
            {
                return new ServerResponse().BadRequest(
                    "Degree, school, promotion, stage and study time are required.");
            }

            var db = campusDbContext.DbContext(_campus);

            var students = await (
                from registry in db.TblRegistry.AsNoTracking()
                join student in db.TblStudent.AsNoTracking()
                    on registry.StudentId equals student.StudentId
                where registry.DegreeId == degreeId
                      && registry.SchoolId == schoolId
                      && registry.PromotionNo == promotionNo
                      && registry.StageNo == stageNo
                      && registry.TermNo > 1
                      && registry.StudyTime != null
                      && registry.StudyTime.Trim() == studyTime
                      && student.Status != null
                      && student.Status.Trim() == "REGISTER"
                select new
                {
                    student.StudentId,
                    student.StudentName,
                    student.StudentNameInKhmer,
                    student.Sex,
                    student.DateOfBirth
                })
                .Distinct()
                .OrderBy(x => x.StudentId)
                .ToListAsync();

            return StudentResult(students);
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    private static IActionResult StudentResult<T>(List<T> students)
    {
        return new ServerResponse().Success(
            students,
            students.Count > 0
                ? $"Found {students.Count} student(s)."
                : "No students matched the selected filters.");
    }
}
