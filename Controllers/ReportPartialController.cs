using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;
[Authorize]
[Route("report-partial")]
public class ReportPartialController : Controller
{
    [Route("academic/student-certificate/{formType}")]
    public IActionResult StudentCertificate(string formType = "")
    {
        return formType switch
        {
            "suspend" => PartialView("Academic/TabStudentCertificate/_TabSuspendLetter"),
            "change-branch" => PartialView("Academic/TabStudentCertificate/_TabChangeBranchLetter"),
            "certificate-of-edu" => PartialView("Academic/TabStudentCertificate/_TabCertificateOfEducation"),
            "certificate-qr" => PartialView("Academic/TabStudentCertificate/_TabCertificateQRCode"),
            "student-rank" => PartialView("Academic/TabStudentCertificate/_TabRankStudent"),
            "student-rank-province" => PartialView("Academic/TabStudentCertificate/_TabRankByPromotion"),
            "transcript" => PartialView("Academic/TabStudentCertificate/_TabTranscript"),
            "provisional" => PartialView("Academic/TabStudentCertificate/_TabProvissionalCertificate"),
            _           => Content("Select form type")
        };
    }
    
    
    [Route("academic/student/{formType}")]
    public IActionResult Student(string formType = "")
    {
        return formType switch
        {
            "student_list" => PartialView("Academic/TabStudent/_TabStudentList"),
            "student_skill_list" => PartialView("Academic/TabStudent/_TabStudentSkillList"),
            "student_all_status" => PartialView("Academic/TabStudent/_TabAllStatus"),
            "student_score_list" => PartialView("Academic/TabStudent/_TabScoreList"),
            "student_ministry_doc" => PartialView("Academic/TabStudent/_TabMinistryDoc"),
            _           => Content("Select form type")
        };
    }
    [Route("academic/student-result/{formType}")]
    public IActionResult StudentResult(string formType = "")
    {
        return formType switch
        {
            "graduate_student" => PartialView("Academic/TabStudentResult/_TabGraduateStudent"), 
            "student_statistic" => PartialView("Academic/TabStudentResult/_TabStudentStatic"), 
            "student_letter_list" => PartialView("Academic/TabStudentResult/_TabStudentLetterList"), 
            "student_accept_certificate" => PartialView("Academic/TabStudentResult/_TabStuentAcceptCertificate"), 
            "student_fee_collection" => PartialView("Academic/TabStudentResult/_TabStudentFeeConllection"), 
            _           => Content("Select form type")
        };
    }
    [Route("academic/student-passing-candidate/{formType}")]
    public IActionResult StudentPassingCandidate(string formType = "")
    {
        return formType switch
        {
            "report1" => PartialView("Academic/TabPassingCandidate/_TabReport1"),
            "report2" => PartialView("Academic/TabPassingCandidate/_TabReport2"),
            "report3" => PartialView("Academic/TabPassingCandidate/_TabReport3"),
            "report4" => PartialView("Academic/TabPassingCandidate/_TabReport4"),
            "report5" => PartialView("Academic/TabPassingCandidate/_TabReport5"),
            "pass_to_dhe" => PartialView("Academic/TabPassingCandidate/_TabReportPassToDHE"),
            "entrance_pass" => PartialView("Academic/TabPassingCandidate/_TabReportEntrancePass"),
            _           => Content("Select form type")
        };
    }
    
    [Route("admin/registered/{formType}")]
    public IActionResult Registered(string formType = "")
    {
        return formType switch
        {
            "registered_students" => PartialView("Admin/TabRegistrations/_RegisteredStudent"),
            "registered_static" => PartialView("Admin/TabRegistrations/_RegistrationStatic"),
            "registered_static_province" => PartialView("Admin/TabRegistrations/_RegistrationStaticProvince"),
            "scholarship" => PartialView("Admin/TabRegistrations/_ScholarshipStudent"),
            _           => Content("Select form type")
        };
    }
    
     
    [Route("admin/payment/{formType}")]
    public IActionResult Payment(string formType = "")
    {
        return formType switch
        {
            "income" => PartialView("Admin/TabPayments/_Income"),
            "payment_summary" => PartialView("Admin/TabPayments/_StudentPaymentSummary"),
            "payment_by_date" => PartialView("Admin/TabPayments/_StudentPaymentByDate"),
            "payment_by_group" => PartialView("Admin/TabPayments/_StudentPaymentByGroup"),
            "not_payment" => PartialView("Admin/TabPayments/_StudentNotPaymentNew"),
            "re_exam_payment" => PartialView("Admin/TabPayments/_ReExaminationPayment"),
            "owe" => PartialView("Admin/TabPayments/_StudentIncomplete"),
            "insurance" => PartialView("Admin/TabPayments/_StudentInsurance"),
            _           => Content("Select form type")
        };
    }
    
    [Route("admin/book-clothes/{formType}")]
    public IActionResult BookClothes(string formType = "")
    {
        return formType switch
        {
            "booking" => PartialView("Admin/TabBooking/_BookingClothes"),
            "return_booking" => PartialView("Admin/TabBooking/_ReturnClothes"),
            _           => Content("Select form type")
        };
    }
    
    [Route("admin/others/{formType}")]
    public IActionResult Other(string formType = "")
    {
        return formType switch
        {
            "graduate_student" => PartialView("Admin/TabOthers/_GraduateStudent"),
            "student_statistic" => PartialView("Admin/TabOthers/_StatisticStudent"),
            "student_letter_list" => PartialView("Admin/TabOthers/_StudentLetterList"),
            "student_accept_certificate" => PartialView("Admin/TabOthers/_StudentAcceptCertificate"),
            "student_fee_collection" => PartialView("Admin/TabOthers/_StudentFeeCollection"),
            _           => Content("Select form type")
        };
    }
}