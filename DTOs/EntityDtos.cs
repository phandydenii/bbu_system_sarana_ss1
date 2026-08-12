using System.ComponentModel.DataAnnotations;

namespace BBU_SYSTEM.DTOs;

public class DailyReportDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? TitleKhmer { get; set; }
    public string? Description { get; set; }
    public string? Campus { get; set; }
    public DateTime ReportDate { get; set; } = DateTime.Now;
    public DateTime RequestDate { get; set; } = DateTime.Now;
    public DateTime CreateDate { get; set; }= DateTime.Now;
    
}
public class DailyReportImagesDto
{
    public int Id { get; set; } 
    public int ReportId { get; set; } 
    public string? ImageId { get; set; }
}
public class AbsenceDto
{
    public int AbsenceId { get; set; }
    public int InstructorId { get; set; }
    public DateTime AbsenceDate { get; set; }
    public string? AbsenceTime { get; set; }
    public string? Reason { get; set; }
}

public class AbsentDto
{
    public int AbsentId { get; set; }
    public DateTime? AbsentDate { get; set; }
    public int? DegreeId { get; set; }
    public int? SchoolId { get; set; }
    public int? PromotionId { get; set; }
    public int? StageId { get; set; }
    public int? TermId { get; set; }
    public int? FieldId { get; set; }
    public int? GroupId { get; set; }
}

public class AbsentCourseDto
{
    public int AbsentCourseId { get; set; }
    public int? AbsentDetailId { get; set; }
    public int? AbsentLetterId { get; set; }
    public int? CourseId { get; set; }
    public DateTime? AbsentCourseDate { get; set; }
}

public class AbsentDetailDto
{
    public int AbsentDetailId { get; set; }
    public int? AbsentId { get; set; }
    public string? StudentId { get; set; }
}

public class AbsentLetterDto
{
    public int AbsentLetterId { get; set; }
    public string? Letter { get; set; }
    public decimal? LetterValue { get; set; }
}

public class AcademicReportConEduAssociateToBachelorDto
{
    public string? StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? StudentNameInKhmer { get; set; }
    public string? Sex { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? SchoolName { get; set; }
    public int? FieldId { get; set; }
    public string? FieldNameInKhmer { get; set; }
    public int? SchoolId { get; set; }
    public string? SchoolNameInKhmer { get; set; }
    public int? PromotionNo { get; set; }
    public string? FieldName { get; set; }
    public int? TermNo { get; set; }
    public int? CreateInTermNo { get; set; }
}

public class AcademicReportConEduAssociateToBachelorTempDto
{
    public string? StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? StudentNameInKhmer { get; set; }
    public string? Sex { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? SchoolName { get; set; }
    public int? FieldId { get; set; }
    public string? FieldNameInKhmer { get; set; }
    public int? SchoolId { get; set; }
    public string? SchoolNameInKhmer { get; set; }
    public int? PromotionNo { get; set; }
    public string? FieldName { get; set; }
    public int? TermNo { get; set; }
    public int? CreatedInTermNo { get; set; }
}

public class AcademicReportExaminationResultDto
{
    public string? StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? StudentNameInKhmer { get; set; }
    public string? Sex { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public double? Mid1 { get; set; }
    public double? Final1 { get; set; }
    public double? Total1 { get; set; }
    public double? Mid2 { get; set; }
    public double? Final2 { get; set; }
    public double? Total2 { get; set; }
    public double? Mid3 { get; set; }
    public double? Final3 { get; set; }
    public double? Total3 { get; set; }
    public double? Mid4 { get; set; }
    public double? Final4 { get; set; }
    public double? Total4 { get; set; }
    public double? Mid5 { get; set; }
    public double? Final5 { get; set; }
    public double? Total5 { get; set; }
    public double? Mid6 { get; set; }
    public double? Final6 { get; set; }
    public double? Total6 { get; set; }
    public double? Mid7 { get; set; }
    public double? Final7 { get; set; }
    public double? Total7 { get; set; }
}

public class AcademicReportReStudyStudentDto
{
    public string? StudentId { get; set; }
    public int CourseId { get; set; }
}

public class AcademicReportReexamStudentDoDto
{
    public string? StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? StudentNameInKhmer { get; set; }
    public string? Sex { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? GroupName { get; set; }
    public double? Total1 { get; set; }
    public double? Total2 { get; set; }
    public double? Total3 { get; set; }
    public double? Total4 { get; set; }
    public double? Total5 { get; set; }
    public double? Total6 { get; set; }
}

public class AcademicReportStateExaminationResultDto
{
    public string? StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? StudentNameInKhmer { get; set; }
    public string? Sex { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public double? Score1 { get; set; }
    public double? Score2 { get; set; }
    public double? Score3 { get; set; }
    public double? Score4 { get; set; }
    public double? Score5 { get; set; }
    public double? Score6 { get; set; }
}

public class AdminReportStatisticByProvinceDto
{
    public int? FromProvinceId { get; set; }
    public string? Province { get; set; }
    public int? StudyTime1 { get; set; }
    public int? StudyTime2 { get; set; }
    public int? StudyTime3 { get; set; }
    public int? StudyTime4 { get; set; }
    public int? TotalFemale { get; set; }
}

public class AdminScoreSheetDto
{
    public int PromotionNo { get; set; }
    public int StageNo { get; set; }
    public int SchoolId { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolNameInKhmer { get; set; }
    public int FieldId { get; set; }
    public string? FieldName { get; set; }
    public string? FieldNameInKhmer { get; set; }
    public int TermId { get; set; }
    public int TermNo { get; set; }
    public int GroupId { get; set; }
    public string? GroupName { get; set; }
    public string? StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? StudentNameInKhmer { get; set; }
    public string? Sex { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Phone { get; set; }
    public string? RoomName { get; set; }
    public int? IsPhotoReceived { get; set; }
}

public class AppcBankStudentIdDto
{
    public string? StudentId { get; set; }
}

public class AvailableTimeDto
{
    public int AvailableTimeId { get; set; }
    public int InstructorId { get; set; }
    public string? DayOfWeek { get; set; }
    public string? Time { get; set; }
}

public class BookClothesDto
{
    public int Id { get; set; }
    public string? StudentId { get; set; }
    public bool? IsDeposit { get; set; }
    public bool? IsReturn { get; set; }
    public string? InvoiceNo { get; set; }
    public string? ContactNumber { get; set; }
    public string? Note { get; set; }
}

public class BookingTblDto
{
    public int? BookingId { get; set; }
    public DateTime? BookingDate { get; set; }
    public int? UserId { get; set; }
    public string? StudentId { get; set; }
    public decimal? ExchangeId { get; set; }
    public decimal? Total { get; set; }
    public int? Vat { get; set; }
    public decimal? Discount { get; set; }
    public decimal? PayDollar { get; set; }
    public decimal? PayRieal { get; set; }
    public string? Note { get; set; }
    public bool? Active { get; set; }
    public string? Degree { get; set; }
    public int? SchoolId { get; set; }
    public int? FieldId { get; set; }
    public int? PromotionNo { get; set; }
    public int? StageNo { get; set; }
    public int? GroupId { get; set; }
    public int? TermNo { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? StudyTime { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? ReturnAlready { get; set; }
    public decimal? ReturnRateIn { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal? ReturnAmount { get; set; }
    public decimal? ReturnDollar { get; set; }
    public decimal? ReturnRiel { get; set; }
    public int? BookingNo { get; set; }
    public string? YearNumber { get; set; }
}

public class BookingDetailDto
{
    public int BookingDetailId { get; set; }
    public int? BookingId { get; set; }
    public int? ClothId { get; set; }
    public decimal? Qty { get; set; }
    public decimal? Price { get; set; }
}

public class BookingItemDto
{
    public int BookingItemId { get; set; }
    public string? ItemName { get; set; }
    public string? ItemNameKhmer { get; set; }
    public decimal? Price { get; set; }
}

public class BookingReturnDto
{
    public int BookingReturnId { get; set; }
    public int? BookingId { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int? BookingReturnNo { get; set; }
    public string? YearNumber { get; set; }
    public int? UserId { get; set; }
    public decimal? ExchangeId { get; set; }
    public string? StudentId { get; set; }
    public string? Degree { get; set; }
    public int? SchoolId { get; set; }
    public int? FieldId { get; set; }
    public int? PromotionNo { get; set; }
    public int? StageNo { get; set; }
    public int? GroupNo { get; set; }
    public int? TermNo { get; set; }
    public string? StudyTime { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public decimal? ReturnRateIn { get; set; }
    public decimal? ReturnAmount { get; set; }
    public int? Vat { get; set; }
    public decimal? Discount { get; set; }
    public decimal? ReturnDollar { get; set; }
    public decimal? ReturnRiel { get; set; }
    public string? Note { get; set; }
    public bool? Active { get; set; }
}

public class BookingReturnDetailDto
{
    public int BookingReturnDetailId { get; set; }
    public int? BookingReturnId { get; set; }
    public int? BookingId { get; set; }
    public int? ClothId { get; set; }
    public decimal? Qty { get; set; }
    public decimal? Price { get; set; }
}

public class BranchDto
{
    public int BranchId { get; set; }
    public string? BranchName { get; set; }
    public string? BranchNameInKhmer { get; set; }
    public string? ShortName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}

public class CategoryDto
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
}

public class CertificateDto
{
    public int CertificateId { get; set; }
    public string? CertificateCode { get; set; }
    public string? CertificateName { get; set; }
}

public class ChangeBranchDto
{
    public int ChangeBranchId { get; set; }
    public string? StudentId { get; set; }
    public int ToBranchId { get; set; }
    public int TermNo { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string? DegreeId { get; set; }
    public string? SchoolId { get; set; }
    public string? FieldId { get; set; }
    public string? PromotionId { get; set; }
    public string? StageId { get; set; }
    public string? GroupId { get; set; }
}

public class ChangeFieldDto
{
    public int ChangeId { get; set; }
    public DateTime? ChangeDate { get; set; }
    public string? StudentId { get; set; }
    public int? OldFieldId { get; set; }
    public int? NewFieldId { get; set; }
    public string? UserName { get; set; }
    public string? DegreeId { get; set; }
    public string? SchoolId { get; set; }
    public string? SchoolIdNew { get; set; }
    public string? PromotionId { get; set; }
    public string? StageId { get; set; }
    public string? TermNo { get; set; }
    public string? GroupId { get; set; }
}

public class ComplementFailedCourseScoreDto
{
    public int ComplementFailedCourseScoreId { get; set; }
    public string? StudentId { get; set; }
    public int TermNo { get; set; }
    public int CourseId { get; set; }
    public double MidTermScore { get; set; }
    public double FinalScore { get; set; }
    public string? Username { get; set; }
    public DateTime? DateEdit { get; set; }
}

public class ComplementOrientedCourseScoreDto
{
    public int ComplementOrientedCourseScoreId { get; set; }
    public string? StudentId { get; set; }
    public int TermNo { get; set; }
    public int CourseId { get; set; }
    public double MidTermScore { get; set; }
    public double FinalScore { get; set; }
    public string? Note { get; set; }
    public string? Username { get; set; }
    public DateTime? DateEdit { get; set; }
}

public class ComplementSemesterScoreDto
{
    public int ComplementSemesterScoreId { get; set; }
    public string? StudentId { get; set; }
    public int TermNo { get; set; }
    public int CourseId { get; set; }
    public double MidTermScore { get; set; }
    public double FinalScore { get; set; }
    public string? Username { get; set; }
    public DateTime? DateEdit { get; set; }
}

public class ContactPersonDto
{
    public int ContactPersonId { get; set; }
    public string? ContactPersonName { get; set; }
    public string? Job { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
}

public class CourseDto
{
    public int CourseId { get; set; }
    [Required(ErrorMessage = "Course Name is required")]
    public string? CourseFullName { get; set; }
    [Required(ErrorMessage = "Course Name Khmer is required")]
    public string? CourseFullNameInKhmer { get; set; }
    [Required(ErrorMessage = "Course Short Name is required")]
    public string? CourseShortName { get; set; }
    [Required(ErrorMessage = "Course Short Name Khmer is required")]
    public string? CourseShortNameInKhmer { get; set; }
    public double? Credit { get; set; }
    public double? NumberOfHours { get; set; }
}

public class CourseCodeDto
{
    public int CourseCodeId { get; set; }
    public int CourseId { get; set; }
    public int SchoolId { get; set; }
    public int FieldId { get; set; }
    public int DegreeId { get; set; }
    public int TermNo { get; set; }
    public string? Code { get; set; }
}

public class CourseSchoolDto
{
    public int SchoolId { get; set; }
    public int CourseId { get; set; }
}

public class CoursetermDto
{
    public int CoursetermId { get; set; }
    public int CourseId { get; set; } = 0;
    public int FieldId { get; set; } = 0;
    public int TermId { get; set; } = 0;
    public double Credit { get; set; } = 0;
    public string Type { get; set; } = "";
    public double Hours { get; set; } = 0;
}

public class DebugLoggerDto
{
    public int Id { get; set; }
    public string? Message { get; set; }
}

public class DegreeDto
{
    public int DegreeId { get; set; }
    public string? DegreeName { get; set; }
    public string? DegreeInKhmer { get; set; }
}

public class DisabilityDto
{
    public int Id { get; set; }
    public string? DisabilityName { get; set; }
    public string? DisabilityNameKh { get; set; }
}

public class DiscountDto
{
    public int DiscountId { get; set; }
    public string? StudentId { get; set; }
    public int TermNo { get; set; }
    public decimal Amount { get; set; }
    public string? Reason { get; set; }
}

public class DoctoralContractDto
{
    public int ContractId { get; set; }
    public string? StudentId { get; set; }
    public int? TermNo { get; set; }
    public decimal? Fee { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Note { get; set; }
}

public class ExamDateDto
{
    public int ExamDateId { get; set; }
    public int CoursetermId { get; set; }
    public DateTime Date { get; set; }
}

public class ExchangeRateDto
{
    public int ExchangeRateId { get; set; }
    public DateTime? ExchangeDate { get; set; }
    public string? Description { get; set; }
}

public class ExchangeRateDetailDto
{
    public int DetailId { get; set; }
    public int? ExchangeRateId { get; set; }
    public string? CurrencyNameIn { get; set; }
    public string? CurrencyNameOut { get; set; }
    public decimal? RateIn { get; set; }
    public decimal? RateOut { get; set; }
}

public class ExtendDto
{
    public int ExtendId { get; set; } = 0;
    public string? StudentId { get; set; } = "";
    public int TermNo { get; set; } = 0;
    public string? ExtendFrom { get; set; } = "";
    public int FromId { get; set; } = 0;
    public int? IsCertificateReceived { get; set; } = 0;
    public int? IsTranscriptReceived { get; set; } = 0;
    public DateTime? ExtendDate { get; set; } = null;

    public bool IsCerti { get; set; }
    public bool IsTran { get; set; }

    public ExtendDto()
    {
        IsCerti = IsCertificateReceived != 0;
        IsTran = IsTranscriptReceived != 0;
    }
}

public class ExternalScoreDto
{
    public int ExternalScoreId { get; set; }
    public string? StudentId { get; set; }
    public int TermNo { get; set; }
    public string? CourseName { get; set; }
    public string? CourseNameInKhmer { get; set; }
    public int Credit { get; set; }
    public string? Grade { get; set; }
    public decimal? Total { get; set; }
    public string? CourseCode { get; set; }
    public int? YearStart { get; set; }
    public int? YearEnd { get; set; }
    public string? Username { get; set; }
    public DateTime? DateEdit { get; set; }
}

public class FacultyDto
{
    public decimal FacultyId { get; set; }
    public string? FacultyName { get; set; }
    public string? FacultyNameInKhmer { get; set; }
}

public class FieldDto
{
    public int FieldId { get; set; }
    public string? FieldName { get; set; }
    public string? FieldNameInKhmer { get; set; }
    public int SchoolId { get; set; }
    public int DegreeId { get; set; }
    public string? DegreeName { get; set; }
    public string? DegreeNameInKhmer { get; set; }
    public bool Type { get; set; } = false;
}

public class FieldCertificateDto
{
    public int Id { get; set; }
    public int? DegreeId { get; set; }
    public string? DegreeName { get; set; }
    public string? DegreeNameKhmer { get; set; }
    public int? SchoolId { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolNameKhmer { get; set; }
    public int? FieldId { get; set; }
    public string? FieldName { get; set; }
    public string? FieldNameKhmer { get; set; }
    public int? PromotionNo { get; set; }
    public bool? Status { get; set; }
    public string? Type { get; set; }
    public string? TypeKhmer { get; set; }
}

public class FoundationYearReportCertificateDto
{
    public int CertificateId { get; set; }
    public string? StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? StudentNameInKhmer { get; set; }
    public string? Sex { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public int? CourseId { get; set; }
    public string? CourseFullName { get; set; }
    public string? CourseFullNameInKhmer { get; set; }
    public string? CourseShortName { get; set; }
    public string? CourseShortNameInKhmer { get; set; }
    public int? IsGeneralCourse { get; set; }
    public int? Credit { get; set; }
    public string? GradeLetter { get; set; }
    public double? Gpa { get; set; }
}

public class GradeDto
{
    public int GradeId { get; set; }
    public string? GradeLetter { get; set; }
    public double FromScore { get; set; }
    public double ToScore { get; set; }
    public double Point { get; set; }
    public string? Meaning { get; set; }
}

public class GroupDto
{
    public int GroupId { get; set; }
    public string? GroupName { get; set; }
    public string? StudyTime { get; set; }
    public int StageId { get; set; }
    public int FieldId { get; set; }
    public int CreatedInTermNo { get; set; }
    public string? Note { get; set; }
}

public class GroupRoomDto
{
    public int GroupRoomId { get; set; }
    public int GroupId { get; set; }
    public int TermNo { get; set; }
    public string? RoomName { get; set; }
    public DateTime? StartPayment { get; set; } 

    public GroupRoomDto()
    {
        StartPayment = null;
    }
}

public class HighSchoolDto
{
    public int HighSchoolId { get; set; }
    public string? HighSchoolName { get; set; }
    public string? HighSchoolNameInKhmer { get; set; }
}

public class HighSchoolTypeDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? NameKhmer { get; set; }
}

public class InstructorDto
{
    public int InstructorId { get; set; }
    public string? InstructorName { get; set; }
    public string? InstructorNameInKhmer { get; set; }
    public string? Sex { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PlaceOfBirth { get; set; }
    public string? Race { get; set; }
    public string? Nationality { get; set; }
    public string? MaritalStatus { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    public string? Address { get; set; }
    public string? Degree { get; set; }
    public string? InstructorType { get; set; }
}

public class InstructorCertificateDto
{
    public int InstructorCertificateId { get; set; }
    public int InstructorId { get; set; }
    public string? CertificateName { get; set; }
    public int? YearObtained { get; set; }
    public string? University { get; set; }
    public string? Country { get; set; }
}

public class InstructorCourseDto
{
    public int InstructorCourseId { get; set; }
    public int InstructorId { get; set; }
    public int SchoolId { get; set; }
    public int CourseId { get; set; }
}

public class InstructorGroupDto
{
    public int InstructorGroupId { get; set; }
    public int InstructorId { get; set; }
    public int GroupId { get; set; }
    public int TermNo { get; set; }
    public int CourseId { get; set; }
    public string? DayOfWeek { get; set; }
    public string? Time { get; set; }
    public string? RoomName { get; set; }
    public string? Status { get; set; }
}

public class InstructorSchoolDto
{
    public int InstructorSchoolId { get; set; }
    public int InstructorId { get; set; }
    public int SchoolId { get; set; }
}

public class InstructorTypeDto
{
    public string? Type { get; set; }
}

public class InvoiceItemDetailDto
{
    public int InvoiceItemDetailId { get; set; }
    public int? InvoiceItemId { get; set; }
    public int? DegreeId { get; set; }
    public int? SchoolId { get; set; }
    public int? Vat { get; set; }
    public decimal? Price { get; set; }
}

public class InvoicePaymentDto
{
    public int PaymentId { get; set; }
    public int? InvoiceId { get; set; }
    public DateTime? PaymentDate { get; set; }
    public int? ExchangeId { get; set; }
    public decimal? OweAmount { get; set; }
    public decimal? PayAmount { get; set; }
    public decimal? PayAmountR { get; set; }
}

public class InvoiceReceiveMoneyDto
{
    public int Id { get; set; }
    public int? InvoiceId { get; set; }
    public int? PaymentMethodId { get; set; }
    public decimal? Dollar { get; set; }
    public decimal? Reil { get; set; }
}

public class InvoiceDto
{
    public int InvoiceId { get; set; }

    public int? InvoiceNo { get; set; }

    public string? YearNumber { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public string? StudentId { get; set; }

    public string? DegreeId { get; set; }

    public string? SchoolId { get; set; }

    public string? FieldId { get; set; }

    public string? PromotionId { get; set; }

    public string? StageId { get; set; }
    public string? GroupId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? TermNo { get; set; }

    public int? ExchangeRateId { get; set; }

    public decimal? Vat { get; set; }

    public decimal? GrandTotalUsd { get; set; }
    public decimal? GrandTotalKhr { get; set; }
    
    public decimal? TotalDiscountUsd { get; set; }
    public decimal? TotalDiscountKhr { get; set; }
    
    public decimal? OweUsd { get; set; }
    public decimal? OweKhr { get; set; }
    
    public decimal? TotalOtherUsd { get; set; }
    public decimal? TotalOtherKhr { get; set; }
    

    public decimal? TotalDollar { get; set; }

    public decimal? TotalRiel { get; set; }

    public decimal? TotalBath { get; set; }
    
    public decimal? TotalPayUsd { get; set; }

    public decimal? TotalPayKhr { get; set; }
    
    public string? Description { get; set; }

    public string? Status { get; set; }
    
    public bool? Payment { get; set; }

    public bool? CheckPayment { get; set; }

    public DateTime? DateEdit { get; set; }

    public string? EditBy { get; set; }

    public string? OweReason { get; set; }

    public int? UserId { get; set; }

    public decimal? TotalReturnAmount { get; set; }

    public decimal? ReturnAmount { get; set; }

    public string? ReturnDescription { get; set; }

    public int? PaymentMethodId { get; set; }
    
    public decimal? AmountDollar { get; set; }

    public decimal? AmountReil { get; set; }

    public bool? PayOnApp { get; set; }
}

public class InvoiceDetailDto
{ 
    public int InvoiceDetailId { get; set; } 
    public int? InvoiceId { get; set; } 
    public int? ProductId { get; set; }  
    public int? Qty { get; set; } 
    public string? QtyNote { get; set; } 
    public decimal? Price { get; set; } 
    public string? Note { get; set; } 
    public decimal? Vat { get; set; } 
    public decimal? PayDollar { get; set; } 
    public decimal? PayRiel { get; set; }
    public decimal? PayBath { get; set; } 
    public decimal? Discount { get; set; } 
    public decimal? Owe { get; set; } 
    public int? CategoryId { get; set; } 
    public decimal? Other { get; set; } 
    public decimal? PriceKhr { get; set; } 
    public decimal? DiscountKhr { get; set; } 
    public decimal? OweKhr { get; set; } 
    public int? DiscountPercent { get; set; } 
    public decimal? OtherKhr { get; set; }
}

public class KhmerLunaaCalendarDto
{
    public int Id { get; set; }
    public string? NameKhmer { get; set; }
}

public class LecturerDto
{
    public int LecturerId { get; set; }
    public string? Name { get; set; }
    public string? Sex { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public decimal? Price { get; set; }
    public string? Telephone { get; set; }
    public int? DegreeId { get; set; }
    public int? LecturerFieldId { get; set; }
    public int? SchoolId { get; set; }
    public string? NameInKhmer { get; set; }
}

public class LecturerBranchDto
{
    public int LecturerId { get; set; }

    public int BranchId { get; set; }
}

public class LecturerCourseDto
{
    public int LecturerId { get; set; }

    public int CourseId { get; set; }
}

public class LecturerDegreeDto
{
    public int LecturerDegreeId { get; set; }

    public string? LecturerDegreeName { get; set; }

    public string? LecturerDegreeNameInKhmer { get; set; }
}

public class LecturerFieldDto
{
    public int LecturerFieldId { get; set; }

    public string? Name { get; set; }

    public string? NameInKhmer { get; set; }

    public int? LecturerDegreeId { get; set; }
}

public class LecturerSubjectDto
{
    public int LecturerId { get; set; }

    public int SubjectId { get; set; }
}

public class LetterDto
{
    public int LetterId { get; set; }
    public string? LetterName { get; set; }
}

public class LetterCategoryDto
{
    public short CategoryId { get; set; }
    public string CategoryName { get; set; } = "Other";
    public float UnitPrice { get; set; } = 0;
    public bool Active { get; set; } = true;
    public bool IsAdmin { get; set; } = false;
    public bool IsFoundation { get; set; } = false;
    public bool IsShortCourse { get; set; } = false;
    public bool IsStartNewNumber { get; set; } = false;
    
}

public class LetterCertificationDto
{
    public int Id { get; set; } 
    public int? LetterNo { get; set; } 
    public string? YearNumber { get; set; } 
    public int? CertificateId { get; set; } 
    public DateTime? IssuedDate { get; set; } 
    public bool IssuedStatus { get; set; } 
    public string? StuId { get; set; } 
    public string? NameInKh { get; set; } 
    public string? NameInEng { get; set; } 
    public string? Sex { get; set; } 
    public DateTime? BirthDate { get; set; } 
    public string? Degree { get; set; } 
    public string? School { get; set; } 
    public string? Field { get; set; } 
    public string? Promotion { get; set; } 
    public string? IssuedNo { get; set; } 
    public DateTime? ReceivedDate { get; set; } 
    public short? Amount { get; set; } 
    public short? CategoryId { get; set; } 
    public string? Other { get; set; } 
    public int? FoundationNo { get; set; } 
    public int? FoundationYear { get; set; } 
    public int? ShortCourseNo { get; set; } 
    public int? ShortCourseYear { get; set; }
}

public class MinimumGpaDto
{
    public float Gpa { get; set; }
}

public class NationalityDto
{
    public int NationalityId { get; set; }
    public string? NationalityName { get; set; }
    public string? NationalityInKhmer { get; set; }
}

public class NumberOfYearsStudyDto
{
    public int NumberOfYearsStudyId { get; set; }
    public int DegreeId { get; set; }
    public int SchoolId { get; set; }
    public int NumberOfYears { get; set; }
}

public class OtherBranchScoreDto
{
    public int OtherBranchScoreId { get; set; }
    public string? StudentId { get; set; }
    public int TermNo { get; set; }
    public int? CourseId { get; set; }
    public string? CourseName { get; set; }
    public string? CourseNameInKhmer { get; set; }
    public int Credit { get; set; }
    public float MidTermScore { get; set; }
    public float FinalScore { get; set; }
    public int? YearStart { get; set; }
    public int? YearEnd { get; set; }

    public string? Username { get; set; }
    public DateTime? DateEdit { get; set; }
}

public class OtherBranchScoreUnicodeDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
}

public class PaymentDto
{
    public int PaymentId { get; set; }
    public string? StudentId { get; set; }
    public string? InvoiceNo { get; set; }
    public DateTime InvoiceDate { get; set; }
    public int TermNo { get; set; }
    public decimal Paid { get; set; }
    public decimal Deposit { get; set; }
    public string? Note { get; set; }
    public bool? IsInsurance { get; set; }

    public string? Guardian { get; set; }
}

public class PaymentMethodDto
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? NameKhmer { get; set; }
}

public class PaymentTypeDto
{
    public int PaymentTypeId { get; set; }

    public string? PaymentTypeName { get; set; }
    public bool? Status { get; set; }
}

public class PositionDto
{
    public string? PositionName { get; set; }
}

public class PrivilegeDto
{
    public int PrivilegeId { get; set; }
    public string? PrivilegeName { get; set; }
    public int PrivilegeGroupId { get; set; }
}

public class PrivilegeGroupDto
{ 
    public int Id { get; set; } 
    public string? GroupName { get; set; }
}

public class ProductDetailDto
{
    public int ProductDetailId { get; set; }
    public int? ProductId { get; set; }
    public int? DegreeId { get; set; }
    public int? SchoolId { get; set; }
}

public class ProductDto
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductNameInKhmer { get; set; }
    public string? Description { get; set; }
    public int Vat { get; set; }
    public decimal Price { get; set; } 
    public string? Type { get; set; } 
    public string? Status { get; set; }
    public bool TuitionFees { get; set; } 
    public string? DegreeId { get; set; } 
    public int OrderId { get; set; }
    public int CardCertificate { get; set; }
    public int CategoryId { get; set; }
    public decimal? PriceKhr { get; set; } 
    public bool PaymentType { get; set; } 
    public int? FromPromotion { get; set; }
    public int? ToPromotion { get; set; } 
    public bool Hidden { get; set; }

    public ProductDto()
    {
        Hidden = false;
        PaymentType = true;
    }
}

public class PromotionDto
{
    public int PromotionId { get; set; }
    public int DegreeId { get; set; }
    public int SchoolId { get; set; }
    public int PromotionNo { get; set; }
    public int AcademicYearStart { get; set; }
    public int AcademicYearEnd { get; set; }
    public string? Status { get; set; }
    public DateTime? GraduateDate1 { get; set; }
    public DateTime? GraduateDate2 { get; set; }
}

public class ProvinceDto
{
    public int ProvinceId { get; set; }
    public string? ProvinceName { get; set; }
    public string? ProvinceInKhmer { get; set; }
    public int IsCity { get; set; }
}

public class QrCodeCertificateDto
{
    public string? Id { get; set; }

    public string? StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? StudentNameKhmer { get; set; }
    public string? Sex { get; set; }
    public string? Dob { get; set; }
    public string? DobKhmer { get; set; }

    public string? Status { get; set; }
    public int? DegreeId { get; set; }
    public string? DegreeName { get; set; }
    public string? DegreeNameKhmer { get; set; }
    public int? SchoolId { get; set; }
    public string? SchoolName { get; set; }

    public string? SchoolNameKhmer { get; set; }
    public int? FieldId { get; set; }
    public string? FieldName { get; set; }
    public string? FieldNameKhmer { get; set; }

    public string? Type { get; set; }
    public int? PromotionId { get; set; }
    public int? PromotionNo { get; set; }
    public int? StageNo { get; set; }

    public string? GroupName { get; set; }
    public string? Photo { get; set; }
    public string? GraduateDate { get; set; }
    public string? GraduateDateKhmer { get; set; }
    public string? Url { get; set; }
    public string? DocumentKey { get; set; }
    public string? QrCodeData { get; set; }

    public string? CertificateCode { get; set; }
    public bool? Locked { get; set; }
    public DateTime? Date { get; set; }
    public int? UserId { get; set; }
}

public class QrCodeCertificateHistoryDto
{
    public string? Id { get; set; }
    public string? StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? StudentNameKhmer { get; set; }
    public string? Sex { get; set; }
    public string? Dob { get; set; }
    public string? DobKhmer { get; set; }
    public string? Status { get; set; }
    public int? DegreeId { get; set; }
    public string? DegreeName { get; set; }
    public string? DegreeNameKhmer { get; set; }
    public int? SchoolId { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolNameKhmer { get; set; }
    public int? FieldId { get; set; }
    public string? FieldName { get; set; }
    public string? FieldNameKhmer { get; set; }
    public string? Type { get; set; }
    public int? PromotionId { get; set; }
    public int? PromotionNo { get; set; }
    public int? StageNo { get; set; }
    public string? GroupName { get; set; }
    public string? Photo { get; set; }
    public string? GraduateDate { get; set; }
    public string? GraduateDateKhmer { get; set; }
    public string? Url { get; set; }
    public string? DocumentKey { get; set; }
    public string? QrCodeData { get; set; }
    public string? CertificateCode { get; set; }
    public bool? Locked { get; set; }
    public DateTime? Date { get; set; }
    public DateTime? ResetDate { get; set; }
    public int? UserId { get; set; }
    public int? UserReset { get; set; }
}

public class QuitDto
{
    public int QuitId { get; set; }
    public string? StudentId { get; set; }
    public int TermNo { get; set; }
    public DateTime QuitDate { get; set; }
    public string? ReasonOfQuit { get; set; }
    public int? GroupId { get; set; }
    public int? PromotionId { get; set; }
}

public class RaceDto
{
    public int RaceId { get; set; }
    public string? RaceName { get; set; }
    public string? RaceInKhmer { get; set; }
}

public class ReexamScoreDto
{
    public int ReexamScoreId { get; set; }
    public int StudentGroupId { get; set; }
    public int CourseId { get; set; }
    public int Time { get; set; }
    public float Score { get; set; }
}

public class RegistryDto
{
    public int RegistrationId { get; set; }
    public string StudentId { get; set; }
    public int DegreeId { get; set; }
    public int SchoolId { get; set; }
    public int PromotionNo { get; set; }
    public int StageNo { get; set; }
    public int TermNo { get; set; }
    public string StudyTime { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime DoneDate { get; set; }
    public string HighSchoolResult { get; set; }
    public int HighSchoolTableNo { get; set; }
    public string UpdateBy { get; set; }
    public DateTime UpdateDate { get; set; }

    public RegistryDto()
    {
        RegistrationDate = DateTime.Now;
        DoneDate = DateTime.Now;
        UpdateDate = DateTime.Now;
        UpdateBy = "System";
        HighSchoolResult = "Pass";
        HighSchoolTableNo = 1;
        StudyTime = "";
        TermNo = 0;
        StageNo = 0;
        RegistrationId = 0;
        StudentId = "";
        DegreeId = 0;
        SchoolId = 0;
        PromotionNo = 0;
        StageNo = 0;
        TermNo = 0;
        RegistrationDate = DateTime.Now;
        DoneDate = DateTime.Now;
        UpdateDate = DateTime.Now;
        UpdateBy = "System";
        HighSchoolResult = "Pass";
        HighSchoolTableNo = 1;
    }
}

public class RegistryHistoryDto
{
    public int RegistrationId { get; set; }
    public string? StudentId { get; set; }
    public int DegreeId { get; set; }
    public int SchoolId { get; set; }
    public int PromotionNo { get; set; }
    public int StageNo { get; set; }
    public int TermNo { get; set; }
    public string? StudyTime { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime DoneDate { get; set; }
    public string? HighSchoolResult { get; set; }
    public int? HighSchoolTableNo { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public DateTime? Date { get; set; }
    public string? By { get; set; }
}

public class ReportOfStudentTotalScoreDto
{
    public string? StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? StudentNameInKhmer { get; set; }
    public string? Sex { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Phone { get; set; }
    public float? TotalScore { get; set; }
}

public class ReportPageMarginDto
{
    public int ReportPageMarginId { get; set; }
    public string? ReportName { get; set; }
    public int Top { get; set; }
    public int Bottom { get; set; }
    public int Left { get; set; }
    public int Right { get; set; }
}

public class ReportTempStudentFailStudyDto
{
    public string? StudentName { get; set; }
    public string? StudentNameInKhmer { get; set; }
    public string? Sex { get; set; }
    public string? Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
    public string? NationalityInKhmer { get; set; }
    public string? Province { get; set; }
    public string? ProvinceInKhmer { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolNameInKhmer { get; set; }
    public string? Status { get; set; }
    public string? Degree { get; set; }
    public string? CourseFullName { get; set; }
    public string? CourseFullNameInKhmer { get; set; }
    public float? Credit { get; set; }
    public float? NumberOfHours { get; set; }
    public int? TermNo { get; set; }
    public float? MidTermScore { get; set; }
    public float? FinalScore { get; set; }
    public float? Total { get; set; }
    public int? PromotionNo { get; set; }
    public int? StageNo { get; set; }
    public string? StudentId { get; set; }
}

public class RestudyTblDto
{
    public int RestudyId { get; set; }
    public int? TermNo { get; set; }
    public int? CourseId { get; set; }
    public string? CourseFullName { get; set; }
    public int? ReplaceCourseId { get; set; }
    public string? ReplaceCourseFullName { get; set; }
    public string? Note { get; set; }
    public string? StudentId { get; set; }
}

public class ResumeDto
{
    public int ResumeId { get; set; }
    public DateTime DatePayment { get; set; }
    public string? StudentId { get; set; }
    public int FieldId { get; set; }
    public int FPromotion { get; set; }
    public int FYear { get; set; }
    public int FSemester { get; set; }
    public int CPromotion { get; set; }
    public string? Stage { get; set; }
    public int CYear { get; set; }
    public int CSemester { get; set; }
    public string? Other { get; set; }
    public string? Type { get; set; }
}

public class RoomDto
{
    public int RoomId { get; set; }
    public string? RoomName { get; set; }
    public int? Capacity { get; set; }
    public string? RoomType { get; set; }
}

public class SchoolDto
{
    public int SchoolId { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolNameInKhmer { get; set; }
    public string? SchoolCode { get; set; }
    public decimal FacultyId { get; set; }
    public int IsFoundationSchool { get; set; }
}

public class ScoreDto
{
    public int ScoreId { get; set; }
    public int StudentGroupId { get; set; }
    public int CourseId { get; set; }
    public float? MidTermScore { get; set; }
    public float? FinalScore { get; set; }
    public string? Username { get; set; }
    public DateTime? DateEdit { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsAllow { get; set; }
}

public class ScoreHistoryDto
{
    public int ScoreHistoryId { get; set; }
    public string? StudentId { get; set; }
    public int CourseId { get; set; }
    public float MidTermScore { get; set; }
    public float FinalScore { get; set; }
    public int TermNo { get; set; }
    public int Time { get; set; }
    public string? Username { get; set; }
    public DateTime? DateEdit { get; set; }
}

public class ScoreHistoryUpdateDto
{
    public int ScoreId { get; set; }
    public string? StudentId { get; set; }
    public int CourseId { get; set; }
    public float? MidTermScore { get; set; }
    public float? FinalScore { get; set; }
    public string? Username { get; set; }
    public DateTime? DateEdit { get; set; }
}

public class SpoReportStudentGroupStatisticDto
{
    public int? PromotionNo { get; set; }
    public int? StageNo { get; set; }
    public int? TermId { get; set; }
    public int? TermNo { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? AcademicYearStart { get; set; }
    public DateTime? AcademicYearEnd { get; set; }
    public int? GroupId { get; set; }
    public string? GroupName { get; set; }
    public int? SchoolId { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolNameInKhmer { get; set; }
    public int? FieldId { get; set; }
    public string? FieldName { get; set; }
    public int? DegreeId { get; set; }
    public string? Degree { get; set; }
    public string? RoomName { get; set; }
    public int? TotalFemale { get; set; }
    public int? TotalStudent { get; set; }
}

public class SponsorDto
{
    public int SponsorId { get; set; }
    public string? SponsorName { get; set; }
    public string? SponsorNameInKhmer { get; set; }
    public string? Position { get; set; }
    public string? Note { get; set; }
}

public class StageDto
{
    public int StageId { get; set; }
    public int PromotionId { get; set; }
    public int StageNo { get; set; }
    public string? Status { get; set; }
}

public class StartPromotionDto
{
    public int StartPromotionId { get; set; }
    public int DegreeId { get; set; }
    public int SchoolId { get; set; }
    public int PromotionNo { get; set; }
}

public class StatementDto
{
    public int StatementId { get; set; }
    public string? StudentId { get; set; }
    public int TermNo { get; set; }
    public DateTime StatementDate { get; set; }
    public DateTime DueDate { get; set; }
    public string? Note { get; set; }
}

public class StudentDto
{
    public string? StudentId { get; set; } = "";
    public string? StudentName { get; set; } = "";
    public string? StudentNameInKhmer { get; set; } = "";
    public string? Sex { get; set; } = "";
    public DateTime DateOfBirth { get; set; }
    public int? PlaceOfBirthId { get; set; }
    public int? RaceId { get; set; }
    public int? NationalityId { get; set; }
    public string? MaritalStatus { get; set; }
    public int? HighSchoolGraduatedYear { get; set; }
    public int? FromProvinceId { get; set; }
    public string? FromHighSchoolNameInKhmer { get; set; }
    public int? JobId { get; set; }
    public string? MotherNameInKhmer { get; set; }
    public string? MotherOccupationInKhmer { get; set; }
    public string? FatherNameInKhmer { get; set; }
    public string? FatherOccupationInKhmer { get; set; }
    public string? Phone { get; set; } = "";
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? AddressInKhmer { get; set; }
    public int? ContactPersonId { get; set; }
    public int FieldId { get; set; }
    public int? IsPhotoReceived { get; set; }
    public string? Note { get; set; }
    public string? Status { get; set; }
    public bool IsContinuedStudent { get; set; } = false;
    public bool AssociateToBachelor { get; set; } = false;
    public int BachelorToMaster { get; set; }
    public string? ApprovedDate { get; set; }
    public string? GraduateLetterNo { get; set; }
    public bool IsAcceptCertificate { get; set; }
    public DateTime AcceptDate { get; set; } = DateTime.Now;
    public string CertificateNo { get; set; }
    public bool CertificateOut { get; set; } = false;
    public byte[]? Photo { get; set; }
    public bool? CardIsPrint { get; set; }
    public DateTime? PrintDate { get; set; }
    public bool? FoundCertificateIsPrint { get; set; }
    public bool CheckComplete { get; set; }
    public string CheckCompleteNote { get; set; }
    public int CheckCompleteTerm { get; set; }
    public int? DisabilityId { get; set; }
    public string? DocumentIn { get; set; }
    public string? DocumentOut { get; set; }
    public string? NoteTicket { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsAuthenticated { get; set; }
    public string? AuthenticatedNo { get; set; }
    public string? Url { get; set; }
    public string? DocumentKey { get; set; }
    public string? QrCodeData { get; set; }
    public int? CountPrint { get; set; }
    public bool? IsPrintCertificate { get; set; }
    public bool? IsRequest { get; set; }
    public DateTime? GraduationDate { get; set; }
    public string? CertificateCode { get; set; }
    public bool Ignore { get; set; } = false;
    public string IgnoreReason { get; set; } = "";
    public bool Locked { get; set; } = false;
    public int HighSchoolTypeId { get; set; } = 0;

    public bool IsCon { get; set; } = false;
    public bool IsAssToBac { get; set; } = false;
    public bool IsBacToMas { get; set; } = false;
    public bool IsReceivePhoto { get; set; } = false;

    public StudentDto()
    {
        AcceptDate = DateTime.Now;
        IsAcceptCertificate = false;
        CertificateNo = "";
        DateOfBirth = DateTime.Now.AddYears(-18);
        IsAuthenticated = false;
        IsPhotoReceived = 0; 
        IsPrintCertificate = false;
        IsRequest = false;
        Locked = false; 
        IsPhotoReceived = 0;
        IsPrintCertificate = false;
        IsRequest = false; 
        BachelorToMaster = 0;
        IsPrintCertificate = false;
        IsRequest = false;
        Locked = false; 
        IsPhotoReceived = 0; 
        IsBacToMas = BachelorToMaster != 0;
        CheckComplete = false;
        CheckCompleteNote = "";
        CheckCompleteTerm = 0;
        DisabilityId = 0;
        DocumentIn = "";
        DocumentOut = "";
        NoteTicket = "";
        UpdateBy = "System";
        UpdateDate = DateTime.Now;
        Url = "";
        DocumentKey = "";
    }
}

public class StudentAbsentRecordDto
{
    public int AbsentRecordId { get; set; }
    public string? StudentId { get; set; }
    public int? TermNo { get; set; }
    public int? Month1 { get; set; }
    public int? Month2 { get; set; }
    public int? Month3 { get; set; }
    public int? Month4 { get; set; }
    public int? Month5 { get; set; }
}

public class StudentAbsentRecordNewDto
{
    public int AbsentRecordId { get; set; }
    public string? StudentId { get; set; }
    public int? TermNo { get; set; }
    public int? Subject01 { get; set; }
    public int? Subject02 { get; set; }
    public int? Subject03 { get; set; }
    public int? Subject04 { get; set; }
    public int? Subject05 { get; set; }
    public int? Subject06 { get; set; }
    public DateTime? DateAbsent { get; set; }
}

public class StudentCertificateDto
{
    public int StudentCertificateId { get; set; }
    public string? StudentId { get; set; }
    public int CertificateId { get; set; }
    public string? Grade { get; set; }
    public int? IsReceived { get; set; }
    public string? CertificateIssueNo { get; set; }
}

public class StudentCertificateReturnDto
{
    public int StudentCertificateReturnId { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string? StudentId { get; set; }
    public int? CertificateId { get; set; }
    public int? RecievePicture { get; set; }

    public string? Other { get; set; }
}

public class StudentComplementalPaymentDto
{
    public int StudentComplementalPaymentId { get; set; }
    public string? StudentId { get; set; }
    public string? InvoiceNo { get; set; }
    public DateTime InvoiceDate { get; set; }
    public int Semester { get; set; }
    public decimal Paid { get; set; }
    public decimal Deposit { get; set; }
    public decimal? Discount { get; set; }
    public string? ReasonOfDiscount { get; set; }
    public string? Note { get; set; }
}

public class StudentDiscountDto
{
    public int StudentDiscountId { get; set; }
    public string? StudentId { get; set; }
    public int? Discount { get; set; }
    public int? Term { get; set; }
    public string? Note { get; set; }
}

public class StudentGroupDto
{
    public int StudentGroupId { get; set; }
    public string? StudentId { get; set; }
    public int TermNo { get; set; }
    public int GroupId { get; set; }
}

public class StudentGroupHistoryDto
{
    public int Id { get; set; }
    public int StudentGroupId { get; set; }
    public string? StudentId { get; set; }
    public int TermNo { get; set; }
    public int GroupId { get; set; }
    public DateTime? ChangeDate { get; set; }
    public string? Username { get; set; }
}

public class StudentHistoryDto
{
    public string? StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? StudentNameInKhmer { get; set; }
    public string? Sex { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int? PlaceOfBirthId { get; set; }
    public int? RaceId { get; set; }
    public int? NationalityId { get; set; }
    public string? MaritalStatus { get; set; }
    public int? HighSchoolGraduatedYear { get; set; }
    public int? FromProvinceId { get; set; }
    public string? FromHighSchoolNameInKhmer { get; set; }
    public int? JobId { get; set; }
    public string? MotherNameInKhmer { get; set; }
    public string? MotherOccupationInKhmer { get; set; }
    public string? FatherNameInKhmer { get; set; }
    public string? FatherOccupationInKhmer { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? AddressInKhmer { get; set; }
    public int? ContactPersonId { get; set; }
    public int FieldId { get; set; }
    public int? IsPhotoReceived { get; set; }
    public string? Note { get; set; }
    public string? Status { get; set; }
    public int? IsContinuedStudent { get; set; }
    public int? AssociateToBachelor { get; set; }
    public string? ApprovedDate { get; set; }
    public string? GraduateLetterNo { get; set; }
    public bool? IsAcceptCertificate { get; set; }
    public DateTime? AcceptDate { get; set; }
    public string? CertificateNo { get; set; }
    public bool? CertificateOut { get; set; }
    public byte[]? Photo { get; set; }
    public bool? CardIsPrint { get; set; }
    public DateTime? PrintDate { get; set; }
    public bool? FoundCertificateIsPrint { get; set; }
    public bool? CheckComplete { get; set; }
    public string? CheckCompleteNote { get; set; }
    public int? CheckCompleteTerm { get; set; }
    public int? DisabilityId { get; set; }
    public string? DocumentIn { get; set; }
    public string? DocumentOut { get; set; }
    public string? NoteTicket { get; set; }
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsAuthenticated { get; set; }
    public string? AuthenticatedNo { get; set; }
    public string? Url { get; set; }
    public string? DocumentKey { get; set; }
    public string? QrCodeData { get; set; }
    public int? CountPrint { get; set; }
    public bool? IsPrintCertificate { get; set; }
    public bool? IsRequest { get; set; }
    public DateTime? GraduationDate { get; set; }
    public string? CertificateCode { get; set; }
    public bool? Ignor { get; set; }
    public string? IgnorReason { get; set; }
    public bool? Locked { get; set; }
    public int? HighSchoolTypeId { get; set; }
    public DateTime? Date { get; set; }
    public string? By { get; set; }
}

public class StudentJobDto
{
    public int JobId { get; set; }
    public string? Job { get; set; }
    public string? JobInKhmer { get; set; }
}

public class StudentLetterDto
{
    public int StudentLetterId { get; set; }
    public string? StudentId { get; set; }
    public int LetterId { get; set; }
    public DateTime? DoneDate1 { get; set; }
    public DateTime? DoneDate2 { get; set; }
    public string? IssuedNo { get; set; }
    public DateTime? IssuedDate { get; set; }
    public string? Author { get; set; }
    public DateTime? ReceiveDate { get; set; }
}

public class StudentLibraryAttendantDto
{
    public int StudentLibraryAttendantId { get; set; }
    public DateTime CheckDate { get; set; }
    public string? CheckTimeIn { get; set; }
    public string? CheckTimeOut { get; set; }
    public string? StudentId { get; set; }
    public int? IsOut { get; set; }
}

public class StudentOrientedSubjectPaymentDto
{
    public int StudentOrientedSubjectPaymentId { get; set; }
    public string? StudentId { get; set; }
    public string? InvoiceNo { get; set; }
    public DateTime InvoiceDate { get; set; }
    public int TermNo { get; set; }
    public double Paid { get; set; }
    public string? Note { get; set; }
}

public class StudentProblemDto
{
    public int StudentProblemId { get; set; }
    public string? StudentId { get; set; }
    public int? DegreeId { get; set; }
    public int? SchoolId { get; set; }
    public int? PromotionId { get; set; }
    public int? StageId { get; set; }
    public int? TermId { get; set; }
    public int? FieldId { get; set; }
    public int? GroupId { get; set; }
    public string? AcademicProblem { get; set; }
    public string? FinanceProblem { get; set; }
}

public class StudentReexamPaymentDto
{
    public int StudentReexamPaymentId { get; set; }
    public string? StudentId { get; set; }
    public string? InvoiceNo { get; set; }
    public DateTime InvoiceDate { get; set; }
    public double Paid { get; set; }
    public string? Note { get; set; }
}

public class StudentReexamPaymentDetailDto
{
    public int StudentReexamPaymentDetailId { get; set; }
    public int StudentReexamPaymentId { get; set; }
    public int CourseId { get; set; }
    public int TermNo { get; set; }
    public string? Time { get; set; }
}

public class StudentReexamStatePaymentDto
{
    public int StudentReexamStatePaymentId { get; set; }
    public string? StudentId { get; set; }
    public string? InvoiceNo { get; set; }
    public DateTime InvoiceDate { get; set; }
    public float Paid { get; set; }
    public string? Note { get; set; }
}

public class StudentScholarshipDto
{
    public int StudentScholarshipId { get; set; }
    public string? StudentId { get; set; }
    public int SponsorId { get; set; }
    public int TermNo { get; set; }
    public int IsFullScholarship { get; set; }
    public int Amount { get; set; }
    
}

public class StudentStatisticByAcademicYear2Type1Dto
{
    public int? FieldId { get; set; }
    public string? FieldName { get; set; }
    public int? LessThan18Total { get; set; }
    public int? LessThan18Female { get; set; }
    public int? Total18 { get; set; }
    public int? Female18 { get; set; }
    public int? Total19 { get; set; }
    public int? Female19 { get; set; }
    public int? Total20 { get; set; }
    public int? Female20 { get; set; }
    public int? Total21 { get; set; }
    public int? Female21 { get; set; }
    public int? Total22 { get; set; }
    public int? Female22 { get; set; }
    public int? Total23 { get; set; }
    public int? Female23 { get; set; }
    public int? Total24 { get; set; }
    public int? Female24 { get; set; }
    public int? Total25 { get; set; }
    public int? Female25 { get; set; }
    public int? Total26 { get; set; }
    public int? Female26 { get; set; }
    public int? MoreThan26Total { get; set; }
    public int? MoreThan26Female { get; set; }
}

public class StudentStatisticByAcademicYear2Type2Dto
{
    public int? ProvinceId { get; set; }
    public string? Province { get; set; }
    public int? FoundationYearTotal { get; set; }
    public int? FoundationYearFemale { get; set; }
    public int? Year2Total { get; set; }
    public int? Year2Female { get; set; }
    public int? Year3Total { get; set; }
    public int? Year3Female { get; set; }
    public int? Year4Total { get; set; }
    public int? Year4Female { get; set; }
    public int? Year5Total { get; set; }
    public int? Year5Female { get; set; }
    public int? Year6Total { get; set; }
    public int? Year6Female { get; set; }
    public int? Year7Total { get; set; }
    public int? Year7Female { get; set; }
}

public class StudyTimeDto
{
    public string? StudyTimeValue { get; set; }
}

public class SuppressDto
{
    public int SuppressId { get; set; }
    public string? StudentId { get; set; }
    public int TermNo { get; set; }
    public DateTime SuppressDate { get; set; }
    public DateTime? ExpressDate { get; set; }
    public string? ReasonOfSuppress { get; set; }
}

public class SuppressNewDto
{
    public int SuppressId { get; set; }
    public string? StudentId { get; set; }
    public int TermNo { get; set; }
    public DateTime SuppressDate { get; set; }
    public DateTime? ExpressDate { get; set; }
    public string? ReasonOfSuppress { get; set; }
}

public class SuspendDto
{
    public int SuspendId { get; set; }
    public string? StudentId { get; set; }
    public int TermNo { get; set; }
    public int GroupId { get; set; }
    public int PromotionId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string? ReasonOfSuspend { get; set; }
}

public class TermDto
{
    public int TermId { get; set; }
    public int StageId { get; set; }
    public int TermNo { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int AcademicYearStart { get; set; }
    public int AcademicYearEnd { get; set; }
    public string? Status { get; set; }
    public DateTime? StartPaymentDate { get; set; }
}

public class TestScoreDto
{
    public int ScoreId { get; set; }
    public int StudentGroupId { get; set; }
    public int CourseId { get; set; }
    public double? MidTermScore { get; set; }
    public double? FinalScore { get; set; }
}

public class TimeTableDto
{
    public int TimeTableId { get; set; }
    public string? GroupingDay { get; set; }
    public string? PartOfDay { get; set; }
    public string? Time { get; set; }
}

public class TuitionFeeDto
{
    public int TuitionFeeId { get; set; }
    public int PromotionId { get; set; }
    public int TermNo { get; set; }
    public decimal Fee { get; set; }
}

public class UniversityDto
{
    public int UniversityId { get; set; }
    public string? UniversityName { get; set; }
    public string? UniversityNameInKhmer { get; set; }
    public string? AbbreviationName { get; set; }
}

public class UserDto
{
    public int UserId { get; set; } 
    public string? UserName { get; set; } 
    public string? Password { get; set; }
    public string? UserGroup { get; set; }
    public string? Status { get; set; }
    public string? PasswordHash { get; set; }
    public string? Email { get; set; }
    public string? EmailConfirm { get; set; }
    public string? PhoneNumber { get; set; }
}

public class ResetPasswordDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;
    public string CurrentPassword { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please confirm your password")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class UserPriviledgeDto
{
    public int UserPriviledgeId { get; set; }
    public int UserId { get; set; }
    public int PriviledgeId { get; set; }
}

public class UserSchoolDto
{
    public int UserId { get; set; }
    public int SchoolId { get; set; }
    public int UserSchoolId { get; set; }
}