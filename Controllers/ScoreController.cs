using System.Security.Claims;
using AutoMapper;
using BBU_SYSTEM.Data;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Repository;
using BBU_SYSTEM.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("scores")]
public class ScoreController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-scores/{studentGroupId:int}")]
    public IActionResult GetScores(int studentGroupId = 0, string type = "")
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

            var query =
                (from sc in db.TblScore
                    join c in db.TblCourses on sc.CourseId equals c.CourseId
                    join sg in db.TblStudentGroup on sc.StudentGroupId equals sg.StudentGroupId
                    join s in db.TblStudent on sg.StudentId equals s.StudentId
                    join g in db.TblGroup on sg.GroupId equals g.GroupId
                    join st in db.TblStage on g.StageId equals st.StageId
                    join pr in db.TblPromotion on st.PromotionId equals pr.PromotionId
                    join t in db.TblTerm on new { st.StageId, sg.TermNo } equals new { t.StageId, t.TermNo }
                    join f in db.TblField on s.FieldId equals f.FieldId
                    join ct in db.TblCourseTerms on new { f.FieldId, t.TermId, c.CourseId } equals new
                        { ct.FieldId, ct.TermId, ct.CourseId }
                    where sc.StudentGroupId == studentGroupId
                    select new ScoreViewModel()
                    {
                        ScoreId = sc.ScoreId ?? 0,
                        StudentGroupId = sc.StudentGroupId ?? 0,
                        CourseId = sc.CourseId ?? 0,
                        CourseName = c.CourseFullName,
                        CourseNameKhmer = c.CourseFullNameInKhmer,
                        MidTermScore = Convert.ToDecimal(sc.MidTermScore),
                        FinalScore = Convert.ToDecimal(sc.FinalScore ?? 0),
                        Type = ct.Type!.Trim(),
                        IsAllow = sc.IsAllow ?? false,
                    }).AsQueryable();
            if (string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case ScoreTypeConstant.Final:
                        query = query.Where(sc => sc.Type == ScoreTypeConstant.Final);
                        break;
                    case ScoreTypeConstant.FinalAndState:
                        query = query.Where(sc => sc.Type == ScoreTypeConstant.FinalAndState);
                        break; 
                    case ScoreTypeConstant.StateExam:
                        query = query.Where(sc => sc.Type == ScoreTypeConstant.StateExam);
                        break;
                    case ScoreTypeConstant.ProjectPaper:
                        query = query.Where(sc => sc.Type == ScoreTypeConstant.ProjectPaper);
                        break;
                    case ScoreTypeConstant.Practicum:
                        query = query.Where(sc => sc.Type == ScoreTypeConstant.Practicum);
                        break;
                } 
            }
               
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(x =>  x.CourseId!.ToString()!.Contains(searchValue)  
                                          || x.CourseName!.Contains(searchValue));
            }

            var recordsTotal = query.ToList().Count;
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


    public string GetCourseTermType(int courseId, int promotionId, int fieldId, int termId)
    {
        var db = campusDbContext.DbContext(_campus);

        // Get valid terms for the promotion
        var validTermIds = db.TblTerm
            .Where(t => t.TermId == termId &&
                        db.TblStage.Any(s => s.StageId == t.StageId &&
                                             s.PromotionId == promotionId))
            .Select(t => t.TermId);

        // Check if any CourseTerm matches the criteria
        var result = db.TblCourseTerms
            .Where(ct =>
                ct.CourseId == courseId &&
                ct.FieldId == fieldId &&
                validTermIds.Contains(ct.TermId))
            .Select(ct => ct.Type)
            .FirstOrDefault();

        return result ?? "";
    }


    [HttpPost("get-other-brand-score/{studentId}")]
    public IActionResult GetOtherBrandScore(string studentId)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        var db = campusDbContext.DbContext(_campus);

        var query = db.TblOtherBranchScores
            .Where(x => x.StudentId == studentId).AsQueryable();
        if (string.IsNullOrEmpty(searchValue))
        {
            query =
                query.Where(x => x.CourseName!.Contains(searchValue!) || x.CourseNameInKhmer!.Contains(searchValue!));
        }

        var recordsTotal = query.Count();
        var data = query.Skip(skip).Take(pageSize).ToList().OrderBy(x => x.TermNo);

        return Json(new
        {
            draw,
            recordsFiltered = recordsTotal,
            recordsTotal,
            data
        });
    }

    [HttpPost("GetExternalScore/{studentId}")]
    public IActionResult GetExternalScore(string studentId)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = !string.IsNullOrEmpty(length) ? Convert.ToInt32(length) : 10;
            var skip = !string.IsNullOrEmpty(start) ? Convert.ToInt32(start) : 0;

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            var db = campusDbContext.DbContext(_campus);

            var query =
                from es in db.TblExternalScores
                join s in db.TblStudent on es.StudentId equals s.StudentId
                where es.StudentId == studentId
                select new
                {
                    externalScoreId = es.ExternalScoreId,
                    studentId = es.StudentId,

                    studentName = s.StudentName,
                    studentNameInKhmer = s.StudentNameInKhmer,
                    sex = s.Sex,
                    dateOfBirth = s.DateOfBirth,

                    termNo = es.TermNo,
                    courseCode = es.CourseCode,
                    courseName = es.CourseName,
                    courseNameInKhmer = es.CourseNameInKhmer,
                    credit = es.Credit,

                    grade = es.Grade,
                    total = es.Total,

                    yearStart = es.YearStart,
                    yearEnd = es.YearEnd,
                    username = es.Username,
                    dateEdit = es.DateEdit
                };

            var recordsTotal = query.Count();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(x =>
                    (x.courseCode ?? "").Contains(searchValue) ||
                    (x.courseName ?? "").Contains(searchValue) ||
                    (x.courseNameInKhmer ?? "").Contains(searchValue) ||
                    (x.grade ?? "").Contains(searchValue));
            }

            var recordsFiltered = query.Count();

            var data = query
                .OrderBy(x => x.termNo)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            return Json(new
            {
                draw = draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = data
            });
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    
    [HttpPost("/scores/save-external-score")]
    public async Task<IActionResult> SaveExternalScore([FromForm] ExternalScoreDto? externalScore)
    {
        try
        {
            if (externalScore == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }

            var db = campusDbContext.DbContext(_campus);

            var existingScore = await db.TblExternalScores
                .FirstOrDefaultAsync(x => x.ExternalScoreId == externalScore.ExternalScoreId);

            if (existingScore == null)
            {
                return new ServerResponse().NotFound("External score not found!");
            }

            var username =
                User.Identity?.Name ??
                User.FindFirst("UserName")?.Value ??
                User.FindFirst("username")?.Value;

            existingScore.TermNo = externalScore.TermNo;
            existingScore.CourseCode = externalScore.CourseCode?.Trim();
            existingScore.CourseName = externalScore.CourseName?.Trim();
            existingScore.CourseNameInKhmer = externalScore.CourseNameInKhmer?.Trim();
            existingScore.Credit = externalScore.Credit;
            existingScore.Total = externalScore.Total;
            existingScore.Grade = externalScore.Grade?.Trim();
            existingScore.YearStart = externalScore.YearStart;
            existingScore.YearEnd = externalScore.YearEnd;

            // save edit user and edit date
            existingScore.Username = username;
            existingScore.DateEdit = DateTime.Now;

            await db.SaveChangesAsync();

            return new ServerResponse().Success(existingScore, "Updated successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }


    [HttpPost("GetComplementSemesterScores/{studentId}")]
    public IActionResult GetComplementSemesterScores(string studentId)
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

            var query = (from css in db.TblComplementSemesterScores
                join c in db.TblCourses on css.CourseId equals c.CourseId
                where css.StudentId == studentId
                select new
                {
                    css.ComplementSemesterScoreId,
                    css.StudentId,
                    css.TermNo,
                    css.MidTermScore,
                    css.FinalScore,
                    c.CourseId,
                    c.CourseFullName,
                    c.CourseFullNameInKhmer
                }).AsQueryable();
            var recordsTotal = query.Count();
            var data = query.Skip(skip).Take(pageSize).ToList().OrderBy(x => x.TermNo);

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

    [HttpPost("GetComplementOrientedCourseScores/{studentId}")]
    public IActionResult GetComplementOrientedCourseScores(string studentId)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        var db = campusDbContext.DbContext(_campus);

        var query = (from css in db.TblComplementOrientedCourseScores
            join c in db.TblCourses on css.CourseId equals c.CourseId
            where css.StudentId == studentId
            select new
            {
                css.ComplementOrientedCourseScoreId,
                css.StudentId,
                css.TermNo,
                css.MidTermScore,
                css.FinalScore,
                c.CourseId,
                c.CourseFullName,
                c.CourseFullNameInKhmer
            }).AsQueryable();
        var recordsTotal = query.Count();
        var data = query.Skip(skip).Take(pageSize).ToList().OrderBy(x => x.TermNo);

        return Json(new
        {
            draw,
            recordsFiltered = recordsTotal,
            recordsTotal,
            data
        });
    }

    [HttpPost("GetComplementFailedCourseScores/{studentId}")]
    public IActionResult GetComplementFailedCourseScores(string studentId)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = !string.IsNullOrEmpty(length) ? Convert.ToInt32(length) : 10;
            var skip = !string.IsNullOrEmpty(start) ? Convert.ToInt32(start) : 0;

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            var db = campusDbContext.DbContext(_campus);

            var query = (
                from css in db.TblComplementFailedCourseScores
                join c in db.TblCourses on css.CourseId equals c.CourseId
                where css.StudentId == studentId
                select new
                {
                    complementFailedCourseScoreId = css.ComplementFailedCourseScoreId,
                    studentId = css.StudentId,
                    termNo = css.TermNo,
                    midTermScore = css.MidTermScore,
                    finalScore = css.FinalScore,
                    courseId = c.CourseId,
                    courseFullName = c.CourseFullName,
                    courseFullNameInKhmer = c.CourseFullNameInKhmer
                }
            ).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(x =>
                    x.courseFullName.Contains(searchValue) ||
                    x.courseFullNameInKhmer.Contains(searchValue));
            }

            var recordsTotal = query.Count();

            var data = query
                .OrderBy(x => x.termNo)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

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
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    [HttpPost("/scores/save-complement-failed-course-score")]
    public async Task<IActionResult> SaveComplementFailedCourseScore([FromForm] ComplementFailedCourseScoreDto? dto)
    {
        try
        {
            if (dto == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }

            if (string.IsNullOrWhiteSpace(dto.StudentId))
            {
                return new ServerResponse().BadRequest("Student ID is required.");
            }

            if (dto.CourseId <= 0)
            {
                return new ServerResponse().BadRequest("Course ID is required.");
            }

            var db = campusDbContext.DbContext(_campus);

            var username =
                User.Identity?.Name ??
                User.FindFirst("UserName")?.Value ??
                User.FindFirst("username")?.Value ??
                User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value ??
                User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ??
                "System";

            var score = await db.TblComplementFailedCourseScores
                .FirstOrDefaultAsync(x =>
                    x.StudentId == dto.StudentId &&
                    x.CourseId == dto.CourseId &&
                    x.TermNo == dto.TermNo);

            if (score == null)
            {
                db.TblComplementFailedCourseScores.Add(new()
                {
                    StudentId = dto.StudentId,
                    TermNo = dto.TermNo,
                    CourseId = dto.CourseId,
                    MidTermScore = dto.MidTermScore,
                    FinalScore = dto.FinalScore,
                    UserName = username,
                    DateEdit = DateTime.Now
                });
            }
            else
            {
                score.MidTermScore = dto.MidTermScore;
                score.FinalScore = dto.FinalScore;
                score.UserName = username;
                score.DateEdit = DateTime.Now;
            }

            await db.SaveChangesAsync();

            return new ServerResponse().Success(dto, "Updated successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    

    [HttpPost("get-score-history/{studentId}/{courseId}")]
    public IActionResult GetScoreHistory(string studentId = "", int courseId = 0)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = !string.IsNullOrEmpty(length) ? Convert.ToInt32(length) : 1000;
            var skip = !string.IsNullOrEmpty(start) ? Convert.ToInt32(start) : 0;

            if (pageSize <= 0)
            {
                pageSize = 1000;
            }

            var db = campusDbContext.DbContext(_campus);

            var query = (
                from sh in db.TblScoreHistory
                join c in db.TblCourses on sh.CourseId equals c.CourseId
                where sh.StudentId == studentId && sh.CourseId == courseId
                select new
                {
                    scoreHistoryId = sh.ScoreHistoryId,
                    studentId = sh.StudentId,
                    courseId = sh.CourseId,
                    termNo = sh.TermNo,
                    midTermScore = sh.MidTermScore,
                    finalScore = sh.FinalScore,
                    time = sh.Time,
                    username = sh.Username,
                    dateEdit = sh.DateEdit,

                    courseFullName = c.CourseFullName,
                    courseFullNameInKhmer = c.CourseFullNameInKhmer
                }
            ).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(x =>
                    x.courseFullName.Contains(searchValue) ||
                    x.courseFullNameInKhmer.Contains(searchValue) ||
                    x.username.Contains(searchValue));
            }

            var recordsTotal = query.Count();

            var data = query
                .OrderByDescending(x => x.dateEdit)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

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
    [HttpPost("/scores/save/score-history")]
    public async Task<IActionResult> SaveScoreHistory([FromForm] ScoreHistoryDto? dto)
    {
        try
        {
            if (dto == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }

            if (string.IsNullOrWhiteSpace(dto.StudentId))
            {
                return new ServerResponse().BadRequest("Student ID is required.");
            }

            if (dto.CourseId <= 0)
            {
                return new ServerResponse().BadRequest("Course ID is required.");
            }

            var db = campusDbContext.DbContext(_campus);

            var username =
                User.Identity?.Name ??
                User.FindFirst("UserName")?.Value ??
                User.FindFirst("username")?.Value;

            var time = dto.Time > 0 ? dto.Time : 1;

            var scoreHistory = await db.TblScoreHistory
                .FirstOrDefaultAsync(x =>
                    x.StudentId == dto.StudentId &&
                    x.CourseId == dto.CourseId &&
                    x.TermNo == dto.TermNo &&
                    x.Time == time);

            if (scoreHistory == null)
            {
                return new ServerResponse().NotFound("Score history not found.");
            }

            scoreHistory.MidTermScore = dto.MidTermScore;
            scoreHistory.FinalScore = dto.FinalScore;
            scoreHistory.Username = username;
            scoreHistory.DateEdit = DateTime.Now;

            await db.SaveChangesAsync();

            return new ServerResponse().Success(scoreHistory, "Score history updated successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpPost("get-restudy-history/{studentId}/{courseId}")]
    public async Task<IActionResult> GetReStudyHistory(bool isAll = false, string studentId = "", int courseId = 0)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query =
                from sh in db.TblScoreHistoryUpdate // SCORE_HISTORY_UPDATE table
                join c in db.TblCourses // COURSE table
                    on sh.CourseId equals c.CourseId
                where sh.StudentId == studentId
                      && sh.CourseId == courseId
                select new
                {
                    sh.StudentId,
                    c.CourseId,
                    c.CourseFullName,
                    sh.MidTermScore,
                    sh.FinalScore,
                    sh.Username,
                    sh.DateEdit
                };
            if (isAll) return new ServerResponse().Success(await query.ToListAsync());  
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            var recordsTotal = query.Count();
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

    [HttpPost("get-student-score/{studentId}")]
    public IActionResult GetStudentAResult(string studentId)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        var db = campusDbContext.DbContext(_campus);

        var studyRecord1 =
            (from obs in db.TblOtherBranchScores
                join course in db.TblCourses on obs.CourseId equals course.CourseId
                select new
                {
                    obs.StudentId,
                    obs.TermNo,
                    COURSE_NAME = obs.CourseName,
                    obs.MidTermScore,
                    obs.FinalScore,
                    TOTAL = (obs.MidTermScore ?? 0) + (obs.FinalScore ?? 0),
                    course.CourseId,
                    course.CourseFullName,
                    course.CourseFullNameInKhmer,
                    course.CourseShortName,
                    course.CourseShortNameInKhmer,
                    course.Credit,
                    course.NumberOfHours
                }).AsQueryable();

        var studyRecord2 =
            (from sg in db.TblStudentGroup
                join score in db.TblScore on sg.StudentGroupId equals score.StudentGroupId
                join course in db.TblCourses on score.CourseId equals course.CourseId
                select new
                {
                    sg.StudentId,
                    sg.TermNo,
                    COURSE_NAME = course.CourseShortName,
                    score.MidTermScore,
                    score.FinalScore,
                    TOTAL = (score.MidTermScore ?? 0) + (score.FinalScore ?? 0),
                    course.CourseId,
                    course.CourseFullName,
                    course.CourseFullNameInKhmer,
                    course.CourseShortName,
                    course.CourseShortNameInKhmer,
                    course.Credit,
                    course.NumberOfHours
                }).AsQueryable();

        var studyRecord = studyRecord1.Union(studyRecord2);

        var query =
            from sr in studyRecord
            join student in db.TblStudent on sr.StudentId equals student.StudentId
            join sg in db.TblStudentGroup on student.StudentId equals sg.StudentId
            join grp in db.TblGroup on sg.GroupId equals grp.GroupId
            join stage in db.TblStage on grp.StageId equals stage.StageId
            join promo in db.TblPromotion on stage.PromotionId equals promo.PromotionId
            join term in db.TblTerm
                on new { stage.StageId, sg.TermNo } equals new { term.StageId, term.TermNo }
            join degree in db.TblDegree on promo.DegreeId equals degree.DegreeId
            where sr.StudentId == studentId
            orderby sr.StudentId, sr.TermNo
            select new
            {
                student.StudentName,
                student.StudentNameInKhmer,
                student.Sex,
                student.Phone,
                student.DateOfBirth,
                sr.TermNo,
                sr.CourseId,
                sr.CourseFullName,
                sr.CourseFullNameInKhmer,
                sr.CourseShortName,
                sr.CourseShortNameInKhmer,
                sr.MidTermScore,
                sr.FinalScore,
                total = (sr.MidTermScore ?? 0) + (sr.FinalScore ?? 0),
                sr.Credit,
                sr.NumberOfHours
            };
        var recordsTotal = query.Distinct().Count();
        var data = query.Distinct().OrderBy(x => x.TermNo).Skip(skip).Take(pageSize).ToList();

        return Json(new
        {
            draw,
            recordsFiltered = recordsTotal,
            recordsTotal,
            data
        });
    }


    [HttpPost("get-fail-score/{studentId}")]
    public IActionResult GetFailScore(string studentId)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        var db = campusDbContext.DbContext(_campus);
        var registry = db.TblRegistry.FirstOrDefault(x => x.StudentId == studentId);
        var totalScore = 60;
        if (registry!.DegreeId == 5)
        {
            totalScore = 70;
        }
        else if (registry.DegreeId == 4)
        {
            totalScore = 56;
        }

        var query = (from sc in db.TblScore
            join sg in db.TblStudentGroup on sc.StudentGroupId equals sg.StudentGroupId
            join c in db.TblCourses on sc.CourseId equals c.CourseId
            where (sc.MidTermScore + sc.FinalScore) < totalScore &&
                  db.TblStudentGroup.Any(sg =>
                      sg.StudentGroupId == sc.StudentGroupId &&
                      sg.StudentId == studentId
                  )
            select new
            {
                sc.ScoreId,
                sg.StudentGroupId,
                sg.StudentId,
                sg.TermNo,
                c.CourseId, c.CourseFullName, c.CourseFullNameInKhmer,
                sc.MidTermScore,
                sc.FinalScore
            }).AsQueryable();

        var recordsTotal = query.Distinct().Count();
        var data = query.Distinct().Skip(skip).Take(pageSize).ToList();

        return Json(new
        {
            draw,
            recordsFiltered = recordsTotal,
            recordsTotal,
            data
        });
    }


    [HttpPut("update-other-brand-score")]
    public async Task<IActionResult> UpdateOtherBrandScore(int id, float midterm, float final)
    {
        try
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (id == 0) return new ServerResponse().BadRequest();  
            var db = campusDbContext.DbContext(_campus);
            var dataExist = await db.TblOtherBranchScores.Where(x => x.OtherBranchScoreId == id)!.FirstOrDefaultAsync();
            if (dataExist == null) return new ServerResponse().BadRequest("Other branch store not found!");  
            dataExist.MidTermScore = midterm;
            dataExist.FinalScore = final;
            dataExist.Username = username;
            dataExist.DateEdit =  DateTime.Now;
            db.TblOtherBranchScores.Update(dataExist);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(msg:"Update successfully!");
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);  
        }
    }

    [HttpPut("update-score")]
    public async Task<IActionResult> UpdateScore(int id, float midterm, float final)
    {
        try
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (id == 0) return new ServerResponse().BadRequest(); 
            var db = campusDbContext.DbContext(_campus);
            var dataExist = await db.TblScore.Where(x => x.ScoreId == id)!.FirstOrDefaultAsync();
            if (dataExist == null) return new ServerResponse().BadRequest("Other branch store not found!");  
            dataExist.MidTermScore = midterm;
            dataExist.Username = username;
            dataExist.DateEdit = DateTime.Now;
            dataExist.FinalScore = final;
            db.TblScore.Update(dataExist);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(msg : "Update successfully!");
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e); 
        }
    }
}