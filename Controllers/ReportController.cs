using System.Data; 
using BBU_SYSTEM.Repository; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient; 
using BBU_SYSTEM.Data;
using BBU_SYSTEM.Helper;
using Microsoft.AspNetCore.Authorization;
using LocalReport = Microsoft.Reporting.NETCore.LocalReport;
using Microsoft.Reporting.NETCore;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Models.Req; 
using BBU_SYSTEM.ViewModel.Report.Academic;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("report")]
public class   ReportController(
    IConfiguration configuration,
    ICampusDbContext campusDbContext,
    IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";


    //======1-Administration Report

    #region ------------------------ registration ------------------------
    [Route("administration/registration")]
    public IActionResult Registration()
    {
        return View("Admin/Registration/RegisteredStudent");
    }
    [HttpPost("registration-student-generate")] 
    public async Task<IActionResult> RegistrationStudentGenerate([FromForm] RegistrationStudentGenerateReq req)
    {
        try
        {
            if (req.DegreeId <= 0) return new ServerResponse().BadRequest("Degree is required."); 
            if (req.PromotionNo <= 0) return new ServerResponse().BadRequest("Promotion is required."); 
            if (req.StageNo <= 0)  return new ServerResponse().BadRequest("Stage is required."); 
            if (req.FromDate.HasValue && req.ToDate.HasValue && req.ToDate.Value < req.FromDate.Value)
                return new ServerResponse().BadRequest("To Date must be greater than or equal to From Date.");

            var connectionString = configuration.GetConnectionString($"{_campus}_campus"); 
            const string query = @"
                SELECT *
                FROM V_ADMIN_REPORT_REGISTERED_STUDENT
                WHERE DEGREE_ID = @DegreeId
                  AND PROMOTION_NO = @PromotionNo
                  AND STAGE_NO = @StageNo
                  AND (
                        @FromDate IS NULL
                        OR REGISTRATION_DATE >= @FromDate
                      )
                  AND (
                        @ToDate IS NULL
                        OR REGISTRATION_DATE < DATEADD(
                            DAY,
                            1,
                            CAST(@ToDate AS DATE)
                        )
                      )
                ORDER BY STUDENT_NAME;
            ";

            var parameters = new[]
            {
                new SqlParameter("@DegreeId",req.DegreeId),
                new SqlParameter("@PromotionNo",req.PromotionNo),
                new SqlParameter("@StageNo",req.StageNo), 
                new SqlParameter("@FromDate",req.FromDate.HasValue ? req.FromDate.Value : DBNull.Value),  
                new SqlParameter("@ToDate",req.ToDate.HasValue ? req.ToDate.Value : DBNull.Value)
            };

            var dt = await DataManager.DataTableRawSqlAsync(connectionString!,query,parameters);
            if (dt.Rows.Count == 0) 
                return new ServerResponse().Success(msg:"No registered student data was found.");
            
            var reportPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Reports",
                "REGISTER",
                "REGISTER_RPT.rdlc"
            ); 

            using var localReport = new LocalReport();
            localReport.ReportPath = reportPath;
            localReport.DataSources.Add(
                new ReportDataSource("DataSet1",dt)
            );

            localReport.SetParameters(new[]
                {
                    new ReportParameter("reporter",req.Reporter ?? string.Empty),
                    new ReportParameter("receiver",req.Receiver ?? string.Empty)
                }
            );
            var pdf = localReport.Render("PDF");
            return File(
                pdf,
                "application/pdf",
                "registered-student.pdf"
            );
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    [Route("administration/registration-static")]
    public IActionResult RegistrationStatic()
    {
        return View("Admin/Registration/RegistrationStatic");
    }
    [Route("administration/registration-by-province")]
    public IActionResult RegistrationByProvince()
    {
        return View("Admin/Registration/RegistrationStaticProvince");
    }
    [Route("administration/registration-scholarship")]
    public IActionResult Scholarship()
    {
        return View("Admin/Registration/ScholarshipStudent");
    }
    #endregion
    
    #region ------------------------ Payment ------------------------
    [Route("administration/payment")]
    public IActionResult Payment()
    {
        return View("Admin/Payment/StudentPaymentSummary");
    }
    [Route("administration/income")]
    public IActionResult Income()
    {
        return View("Admin/Payment/Income");
    } 
    [Route("administration/payment-by-date")]
    public IActionResult PaymentByDate()
    {
        return View("Admin/Payment/StudentPaymentByDate");
    }
    [Route("administration/payment-by-group")]
    public IActionResult PaymentByGroup()
    {
        return View("Admin/Payment/StudentPaymentByGroup");
    }
    [Route("administration/not-payment")]
    public IActionResult NotPayment()
    {
        return View("Admin/Payment/StudentNotPaymentNew");
    }
    [HttpPost("administration/generate/not-payment")]
    public async Task<IActionResult> NotPayments(AdministrationNotPaymentReq req)
    {
        var connectionString = configuration.GetConnectionString($"{_campus}_campus");
        var parms = new[]
        {
            new SqlParameter("@DegreeId", req.DegreeId),
            new SqlParameter("@SchoolId", req.SchoolId),
            new SqlParameter("@FieldId", req.FieldId),
            new SqlParameter("@PromotionId", req.PromotionId)
        };
        var dt = await DataManager.DataTableRawSqlAsync(
            connectionString!,
            $"SELECT * FROM V_ADMIN_REPORT_LIST_OF_STUDENT_NOT_PAYMENT_NEW " +
            $"WHERE DEGREE_ID = @DegreeId " +
            $"AND SCHOOL_ID = @SchoolId " +
            $"AND FIELD_ID = @FieldId " +
            $"AND PROMOTION_NO = @PromotionId " +
            $"ORDER BY STUDENT_NAME",
            parms
        );
        var localReport = new LocalReport();
        localReport.ReportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports","STUDENT_NOT_PAYMENT_RPT.rdlc");
        localReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
        var pdf = localReport.Render("PDF");
        return File(pdf, "application/pdf", "not_payment.pdf");
    }
    [Route("administration/re-exam-payment")]
    public IActionResult ReExamPayment()
    {
        return View("Admin/Payment/ReExaminationPayment");
    }
    [Route("administration/owe")]
    public IActionResult Owe()
    {
        return View("Admin/Payment/StudentIncomplete");
    }
    [Route("administration/insurance")]
    public IActionResult Insurance()
    {
        return View("Admin/Payment/StudentInsurance");
    }
    #endregion

    #region ------------------------ Book Clothe ------------------------
    [Route("administration/booking-clothes")]
    public IActionResult BookClothes()
    {
        return View("Admin/BookClothe/BookingClothes");
    }
    [Route("administration/return-clothes")]
    public IActionResult ReturnClothes()
    {
        return View("Admin/BookClothe/ReturnClothes");
    }
    #endregion

    #region ------------------------ Others ------------------------
    [Route("administration/others")]
    public IActionResult Other()
    {
        return View("Admin/Other/GraduateStudent");
    }
    [Route("administration/student-statistic")]
    public IActionResult StudentStatistic()
    {
        return View("Admin/Other/StatisticStudent");
    }
    [Route("administration/student-letter-list")]
    public IActionResult StudentLetterList()
    {
        return View("Admin/Other/StudentLetterList");
    }
    [Route("administration/student-accept-certificate")]
    public IActionResult StudentAcceptCertificate()
    {
        return View("Admin/Other/StudentAcceptCertificate");
    }
    [HttpPost("student-list-accept-certificate-generate")] 
    public async Task<IActionResult> StudentListAcceptCertificateGenerate([FromForm] StudentListAcceptCertificateGenerateReq req)
    {
        try
        {
            if (req.FromPromotionNo <= 0 || req.ToPromotionNo <=0 || req.DegreeId <=0 || req.SchoolId <=0) 
                return new ServerResponse().BadRequest("Degree, School and Promotion is required.");  
            if (req.FromDate.HasValue && req.ToDate.HasValue && req.ToDate.Value < req.FromDate.Value)
                return new ServerResponse().BadRequest("To Date must be greater than or equal to From Date.");

            var connectionString = configuration.GetConnectionString($"{_campus}_campus"); 
            const string query = @"
                SELECT *
                FROM V_STUDENT
                WHERE DEGREE_ID = @DegreeId
                  AND SCHOOL_ID = @SchoolId
                  AND PROMOTION_NO BETWEEN @FromPromotionNo AND @ToPromotionNo
                  AND IS_ACCEPT_CERTIFICATE = @IsAcceptCertificate
                  AND (
                        @FromDate IS NULL
                        OR START_DATE >= @FromDate
                      )
                  AND (
                        @ToDate IS NULL
                        OR END_DATE < DATEADD(
                            DAY,
                            1,
                            CAST(@ToDate AS DATE)
                        )
                      )
                ORDER BY STUDENT_NAME;
            ";

            var parameters = new[]
            {
                new SqlParameter("@DegreeId",req.DegreeId),
                new SqlParameter("@SchoolId",req.SchoolId),
                new SqlParameter("@FromPromotionNo",req.FromPromotionNo),
                new SqlParameter("@ToPromotionNo",req.ToPromotionNo), 
                new SqlParameter("@Title",req.Title ?? string.Empty), 
                new SqlParameter("@IsAcceptCertificate",req.IsAcceptCertificate), 
                new SqlParameter("@FromDate",req.FromDate.HasValue ? req.FromDate.Value : DBNull.Value),  
                new SqlParameter("@ToDate",req.ToDate.HasValue ? req.ToDate.Value : DBNull.Value)
            };

            var dt = await DataManager.DataTableRawSqlAsync(connectionString!,query,parameters);
            if (dt.Rows.Count == 0) 
                return new ServerResponse().Success(msg:"No accept certificate student data was found.");
            
            var reportPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Reports",
                "STUDENT_CERTIFICATE",
                "STUDENT_LIST_ACCEPT_CERTIFICATE_RPT.rdlc"
            ); 

            using var localReport = new LocalReport();
            localReport.ReportPath = reportPath;
            localReport.DataSources.Add(
                new ReportDataSource("DataSet1",dt)
            );

            localReport.SetParameters(new[]
                {
                    new ReportParameter("title",req.Title ?? string.Empty), 
                    new ReportParameter("frompro",req.FromPromotionNo.ToString()), 
                    new ReportParameter("topro",req.ToPromotionNo.ToString()), 
                    new ReportParameter("fromdate",req.FromDate.ToString()), 
                    new ReportParameter("todate",req.ToDate.ToString()), 
                }
            );
            var pdf = localReport.Render("PDF");
            return File(
                pdf,
                "application/pdf",
                "student-list-accept-certificate.pdf"
            );
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    [Route("administration/student_fee_collection")]
    public IActionResult StudentFeeCollection()
    {
        return View("Admin/Other/StudentFeeCollection");
    }
    #endregion

    //======2-Academic Report===

    #region ------------------------ Student Report ------------------------ 
    [Route("academic/student")]
    public IActionResult Student()
    {
        return View("Academic/Student/StudentList");
    }
    [HttpGet]
    [Route("academic/student/skill")]
    public IActionResult StudentSkillList()
    {
        return View("Academic/Student/StudentSkillList");
    }
    [HttpGet]
    [Route("academic/student/score")]
    public IActionResult StudentScoreList()
    {
        return View("Academic/Student/StudentScoreList");
    }
    [HttpGet]
    [Route("academic/student/moeys")]
    public IActionResult StudentMinistryDoc()
    {
        return View("Academic/Student/StudentMinistryDoc");
    }
    #endregion

    [Route("academic/passing-candidate")]
    public IActionResult PassingCandidate()
    {
        return View("Academic/PassingCandidate");
    }

    [Route("academic/certification")]
    public IActionResult Certificate()
    {
        return View("academic/certificate");
    }

    #region ------------------------ Student Result ------------------------ 
    [Route("academic/student-result")]
    public IActionResult StudentResult()
    {
        return View("Academic/Result/GraduateStudent");
    }
    /*[Route("academic/student-statistic")]
    public IActionResult StudentStatic()
    {
        return View("Academic/Result/StudentStatic");
    }
    [Route("academic/student_letter_list")]
    public IActionResult StudentStatic()
    {
        return View("Academic/Result/StudentStatic");
    }
    [Route("academic/student-accept-certificate")]
    public IActionResult StudentStatic()
    {
        return View("Academic/Result/StudentAcceptCertificate");
    }
    [Route("academic/student-fee-collection")]
    public IActionResult StudentFeeCollection()
    {
        return View("Admin/Result/StudentFeeCollection");
    }*/
    #endregion

    //======3-Continue Education
    [Route("continue-edu/student")]
    public IActionResult ContinueStudent()
    {
        return View("ContinueEducation/ContinueStudent");
    }

    [Route("continue-edu/credit-completion")]
    public IActionResult CreditCompletion()
    {
        return View("ContinueEducation/CreditCompletion");
    }

    [Route("continue-edu/re-examination")]
    public IActionResult ReExamination()
    {
        return View("ContinueEducation/ReExamination");
    }

    //====4-Dean
    [Route("dean/examination-result")]
    public IActionResult DeanExaminationResult()
    {
        return View("Dean/ExamResult");
    }

    [Route("dean/information-revision")]
    public IActionResult DeanInformationRevision()
    {
        return View("Dean/InformationRevision");
    }

    [Route("dean/practicum-result")]
    public IActionResult DeanPracticumResult()
    {
        return View("Dean/PracticumResult");
    }

    [Route("dean/project-paper-result")]
    public IActionResult DeanProjectPaperResult()
    {
        return View("Dean/ProjectPaperResult");
    }

    [Route("dean/score-sheet")]
    public IActionResult DeanScoreSheet()
    {
        return View("Dean/ScoreSheet");
    }

    [Route("dean/skill-students")]
    public IActionResult DeanSkillStudents()
    {
        return View("Dean/SkillStudent");
    }

    [Route("dean/state-exam-result")]
    public IActionResult DeanStateExamResult()
    {
        return View("Dean/StateExamResult");
    }

    [Route("dean/students-list")]
    public IActionResult DeanStudentList()
    {
        return View("Dean/StudentList");
    }

    [Route("dean/students-status")]
    public IActionResult DeanStudentStatus()
    {
        return View("Dean/StudentStatus");
    }

    [Route("dean/summary-fail-list")]
    public IActionResult DeanSummaryFailList()
    {
        return View("Dean/SummaryFailList");
    }

    //5======Foundation Year
    [Route("foundation/certificate-fy-course")]
    public IActionResult CertificateFyCourse()
    {
        return View("FoundationYear/CertificateOfFYCourse");
    }

    [Route("foundation/examination-list")]
    public IActionResult ExaminationList()
    {
        return View("FoundationYear/ExaminationList");
    }

    [Route("foundation/fail-students-list")]
    public IActionResult FoundationYearFailStudentList()
    {
        return View("FoundationYear/FailStudentList");
    }

    [Route("foundation/foundation-year-students")]
    public IActionResult FoundationYearStudents()
    {
        return View("FoundationYear/FoundationYearStudent");
    }

    [Route("foundation/foundation-year-exam-result")]
    public IActionResult FoundationYearExamResult()
    {
        return View("FoundationYear/FYExamResult");
    }

    [Route("foundation/foundation-year-re-exam-result")]
    public IActionResult FoundationYearReExamResult()
    {
        return View("FoundationYear/FYReExamResult");
    }

    [Route("foundation/score-sheet")]
    public IActionResult FoundationYearScoreSheet()
    {
        return View("FoundationYear/ScoreSheet");
    }

    [Route("foundation/student-quit-suspend-suppress")]
    public IActionResult FyStudentQuitSuspendSuppress()
    {
        return View("FoundationYear/StudentQuitSuspendSuppress");
    }

    [Route("foundation/student-statistic-by-province")]
    public IActionResult FyStudentStatisticByProvince()
    {
        return View("FoundationYear/StudentStatisticByProvince");
    }


    //6=====SPO
    [Route("spo/graduate")]
    public IActionResult SpoGraduate()
    {
        return View("SPO/Graduate");
    }

    [Route("spo/graduate-master-doctor")]
    public IActionResult SpoGraduateMasterDoctor()
    {
        return View("SPO/GraduateMasterDoctor");
    }

    [Route("spo/state-exam-result")]
    public IActionResult SpoStateExamResult()
    {
        return View("SPO/StateExamResult");
    }

    [Route("spo/student-card-list")]
    public IActionResult SpoStudentCardList()
    {
        return View("SPO/StudentCardList");
    }

    [Route("spo/student-statistic")]
    public IActionResult SpoStudentStatistic()
    {
        return View("SPO/StudentStatistic");
    }

    [HttpPost("run-query")]
    public async Task<IActionResult> RunQuery([FromBody] QueryRequest model)
    {
        try
        {
            var sql = model.Query!.ToLower();

            // block dangerous commands
            string[] blocked = ["delete", "drop", "truncate", "update", "insert", "alter"];

            if (blocked.Any(b => sql.Contains(b)))
            {
                return BadRequest("Dangerous SQL commands are not allowed.");
            }

            await using var conn = new SqlConnection(configuration.GetConnectionString($"{_campus}_campus"));
            await conn.OpenAsync();

            var cmd = new SqlCommand(model.Query, conn);

            var reader = await cmd.ExecuteReaderAsync();

            var list = new List<dynamic>();

            while (await reader.ReadAsync())
            {
                list.Add(new
                {
                    label = reader.GetValue(0)?.ToString(),
                    value = Convert.ToDecimal(reader.GetValue(1))
                });
            }

            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("run-query-multiple-chart")]
    public async Task<IActionResult> MutiChartRunQuery([FromBody] QueryRequest model)
    {
        try
        {
            var sql = model.Query!.ToLower();
            // block dangerous commands
            string[] blocked = ["delete", "drop", "truncate", "update", "insert", "alter"];
            if (blocked.Any(b => sql.Contains(b)))
            {
                return BadRequest("Dangerous SQL commands are not allowed.");
            }

            await using var conn = new SqlConnection(configuration.GetConnectionString($"{_campus}_campus"));
            await conn.OpenAsync();
            var data = new List<ChartData>();

            await using (var cmd = new SqlCommand(model.Query, conn))
            {
                await using var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    data.Add(new ChartData()
                    {
                        Label = reader[0].ToString()!,
                        Key = reader[1].ToString(),
                        Value = Convert.ToInt32(reader[2])
                    });
                }

                reader.Close();
            }

            await conn.CloseAsync();

            // 🔹 labels = distinct school names
            var labels = data
                .Select(x => x.Label)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            // 🔹 datasets = promotion numbers
            var datasets = data
                .GroupBy(x => x.Key)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    key = g.Key?.ToString(),
                    value = labels.Select(label =>
                        g.FirstOrDefault(x => x.Label == label)?.Value ?? 0
                    ).ToList()
                })
                .ToList();
            var result = new
            {
                labels,
                datasets
            };
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("owe-by-date")]
    public async Task<IActionResult> GetOweByRankDate(int promotionNo = 0, DateTime? toDate = null)
    {
        try
        {
            const string sql = "";
            // block dangerous commands
            string[] blocked = ["delete", "drop", "truncate", "update", "insert", "alter"];
            if (blocked.Any(b => sql.ToLower().Contains(b)))
            {
                return BadRequest("Dangerous SQL commands are not allowed.");
            }

            await using var conn = new SqlConnection(configuration.GetConnectionString($"{_campus}_campus"));
            await conn.OpenAsync();
            var data = new List<ChartData>();

            await using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@promotionNo", promotionNo);
                cmd.Parameters.AddWithValue("@toDate", toDate);
                await using var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    data.Add(new ChartData()
                    {
                        Label = reader[0].ToString()!,
                        Key = reader[1].ToString(),
                        Value = Convert.ToInt32(reader[2])
                    });
                }

                reader.Close();
            }

            await conn.CloseAsync();
            // 🔹 labels = distinct school names
            var labels = data
                .Select(x => x.Label)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            // 🔹 datasets = promotion numbers
            var datasets = data
                .GroupBy(x => x.Key)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    key = g.Key?.ToString(),
                    value = labels.Select(label =>
                        g.FirstOrDefault(x => x.Label == label)?.Value ?? 0
                    ).ToList()
                })
                .ToList();
            var result = new
            {
                labels,
                datasets
            };
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("student-moeys")]
    public async Task<IActionResult> StudentMoeys(int degreeId, int schoolId, int fieldId, int promotionId, string filter)
    {
        var degree = $" DEGREE_ID={degreeId}";
        var school = $" AND SCHOOL_ID={schoolId}";
        var field = $" AND FIELD_ID={fieldId}";
        var promotion = $" AND PROMOTION_ID={promotionId}";
        var target = " And documentin !='' and documentout !=''";
        var report = "MoEYSOfficialList.rdlc";

        if (!string.IsNullOrEmpty(filter))
        {
            if (filter == "document_in")
            {
                target = " and documentin !=''";
                report = "EntranceExamList.rdlc";
            }
            else if (filter == "document_out")
            {
                target = " AND documentout !=''";
                report = "ComprehensiveExamList.rdlc";
            }
            else if (filter == "official")
            {
                target = " And documentin !='' and documentout !=''";
                report = "MoEYSOfficialList.rdlc";
            }
            else if (filter == "authenticated")
            {
                target = " And AUTHENTICATED_NO !=''";
                report = "HIGTH_SCHOOL_CERTIFICATE_RPT.rdlc";
            }
            else if (filter == "no_document_in")
            {
                target = " And documentin=''";
                report = "NoEntranceExamList.rdlc";
            }
            else if (filter == "no_document_out")
            {
                target = " And documentout =''";
                report = "NoComprehensiveExamList.rdlc";
            }
            else if (filter == "no_official")
            {
                target = " And documentin ='' and documentout =''";
                report = "NoMoEYSOfficialList.rdlc";
            }
            else if (filter == "no_authenticated")
            {
                target = " And AUTHENTICATED_NO =''";
                report = "NO_HIGTH_SCHOOL_CERTIFICATE_RPT.rdlc";
            }
        }

        var connectionString = configuration.GetConnectionString($"{_campus}_campus");
        var parms = new[]
        {
            new SqlParameter("@DegreeId", degreeId),
            new SqlParameter("@SchoolId", schoolId),
            new SqlParameter("@FieldId", fieldId),
            new SqlParameter("@PromotionId", promotionId)
        };
        var dt = await DataManager.DataTableRawSqlAsync(
            connectionString!,
            $"SELECT * " +
                $"FROM V_ACADEMIC_OFFICE_REPORT_MOYES_OFFICIAL_LIST " +
                $"WHERE {degree} {school} {field} {promotion} {target} " +
                $"ORDER BY STUDENT_NAME",
            parms
        );
        var localReport = new LocalReport();
        localReport.ReportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "MOEYS", $"{report}");

        localReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
        var pdf = localReport.Render("PDF");
        return File(pdf, "application/pdf", "invoice.pdf");
    }

    [HttpPost("transcripts")]
    public IActionResult Transcript(TranscriptViewModel req)
    {
        var report = new LocalReport();
        var connectionString = configuration.GetConnectionString($"{_campus}_campus");
        var con = new SqlConnection(connectionString);
        var db = campusDbContext.DbContext(_campus);
        var degreeId = 0;
        var schoolId = 0;
        var fieldId = 0;
        var promotionNo = 0;

        const string query = "SELECT * FROM [V_REPORT_STUDENT_TRANSCRIPT] WHERE STUDENT_ID = @id";
        using (var cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@id", req.StudentId);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    degreeId = Convert.ToInt32(reader["DEGREE_ID"]);
                    schoolId = Convert.ToInt32(reader["SCHOOL_ID"]);
                    fieldId = Convert.ToInt32(reader["FIELD_ID"]);
                    promotionNo = Convert.ToInt32(reader["PROMOTION_NO"]);
                }
                else
                {
                    return new ServerResponse().BadRequest("Invalid student information.");
                }
            }
        }

        var fieldCertificate = db.TblFieldCertificate.FirstOrDefault(x =>
            x.DegreeId == degreeId && x.SchoolId == schoolId && x.FieldId == fieldId && x.PromotionNo == promotionNo);
        if (fieldCertificate == null)
        {
            return new ServerResponse().BadRequest("Invalid field certificate.");
        }

        var degree = req.IsKhmer ? fieldCertificate.DegreeNameKhmer : fieldCertificate.DegreeName;
        var field = req.IsKhmer ? fieldCertificate.FieldNameKhmer : fieldCertificate.FieldName;
        var type = req.IsKhmer ? fieldCertificate.TypeKhmer : fieldCertificate.Type;

        var dt = new DataTable();
        dt.Columns.Add("YEAR");
        dt.Columns.Add("TERM");
        dt.Columns.Add("COURSE_ID");
        dt.Columns.Add("COURSE_NAME");
        dt.Columns.Add("TOTAL");
        dt.Columns.Add("GRADE");
        dt.Columns.Add("CREDIT");
        dt.Columns.Add("GPV");
        dt.Columns.Add("GPE");
        dt.Columns.Add("COURSE_NAME_KHMER");
        dt.Columns.Add("CODE");


        var scoreList = new List<StudentResultScoreReq>();

        foreach (var sl in new List<StudentResultScoreReq>())
        {
        }


        DataRow dr;
        var i = 1;
        float totalcredit = 0;
        float totalgradepoints = 0;

        report.DataSources.Add(new ReportDataSource("DataSet1", dt));
        report.SetParameters([
            new ReportParameter("title", ""),
            new ReportParameter("gpa", ""),
            new ReportParameter("id", ""),
            new ReportParameter("firstname", ""),
            new ReportParameter("familyname", ""),
            new ReportParameter("dob", ""),
            new ReportParameter("degree", ""),
            new ReportParameter("field", ""),
            new ReportParameter("type", ""),
            new ReportParameter("yearkm", ""),
            new ReportParameter("campus", ""),
            new ReportParameter("branchname", ""),
            new ReportParameter("signature", ""),
            new ReportParameter("shortname", ""),
            new ReportParameter("description", ""),
            new ReportParameter("total", "")
        ]);
        var reportPath = req.IsKhmer ? "TRANSCRIPT_KHMER_RPT" : "TRANSCRIPT_ENGLISH_RPT";
        report.ReportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "TRANSCRIPT", reportPath);
        var pdf = report.Render("PDF");
        return File(pdf, "application/pdf", "invoice.pdf");
    }

    [HttpPost("print")]
    public IActionResult Print()
    {
        var report = new LocalReport();
        report.ReportPath = Path.Combine(Directory.GetCurrentDirectory(), "Report", "Invoice.rdlc");
        var dt = new DataTable();
        report.DataSources.Add(new ReportDataSource("InvoiceDataSet", dt));
        var param = new ReportParameter("InvoiceNo", "INV-2025-001");
        report.SetParameters(param);
        var pdf = report.Render("PDF");
        return File(pdf, "application/pdf", "invoice.pdf");
    }
}