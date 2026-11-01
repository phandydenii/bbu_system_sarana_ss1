using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Models;

[Table("DAILY_REPORT")]
public class DailyReport
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID",TypeName = "int")]
    public int Id { get; set; }
    [Column("TITLE",TypeName = "varchar(200)"), NotNull]
    public string? Title { get; set; }
    [Column("TITLE_KHMER",TypeName = "nvarchar(200)")]
    public string? TitleKhmer { get; set; }
    [Column("DESCRIPTION",TypeName = "varchar(300)")]
    public string? Description { get; set; }
    [Column("CAMPUS",TypeName = "varchar(3)"), NotNull]
    public string? Campus { get; set; }
    [Column("REPORT_DATE",TypeName = "datetime")]
    public DateTime ReportDate { get; set; }
    [Column("REQUEST_DATE",TypeName = "datetime")]
    public DateTime RequestDate { get; set; }
    [Column("CREATE_DATE",TypeName = "datetime")]
    public DateTime CreateDate { get; set; }
}
[Table("DAILY_REPORT_IMAGES")]
public class DailyReportImages
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID",TypeName = "int")]
    public int Id { get; set; }
    [Required,Column("REPORT_ID",TypeName = "int")]
    public int ReportId { get; set; }
    [Required,Column("IMAGE_ID",TypeName = "VARCHAR(100)")]
    public string? ImageId { get; set; }
}
[Table("UserActivityLogs")]
public class UserActivityLog
{
    [Key] public int LogId { get; set; }
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? Controller { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? RequestBody { get; set; }
    public string? ResponseBody { get; set; }
    public DateTime DateTime { get; set; }
}

[Table("ABSENCE")]
public class Absence
{
    [Key]
    [Column("ABSENCE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AbsenceId { get; set; }

    [Column("INSTRUCTOR_ID", TypeName = "int")]
    public int InstructorId { get; set; }

    [Column("ABSENCE_DATE", TypeName = "datetime")]
    public DateTime AbsenceDate { get; set; }

    [Column("ABSENCE_TIME", TypeName = "varchar(15)")]
    [StringLength(15)]
    public string? AbsenceTime { get; set; }

    [Column("REASON", TypeName = "varchar(30)")]
    [StringLength(30)]
    public string? Reason { get; set; }
}

[Table("ABSENT_TBL")]
public class AbsentTbl
{
    [Key]
    [Column("ABSENT_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AbsentId { get; set; }

    [Column("ABSENT_DATE", TypeName = "datetime")]
    public DateTime? AbsentDate { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("PROMOTION_ID", TypeName = "int")]
    public int? PromotionId { get; set; }

    [Column("STAGE_ID", TypeName = "int")] public int? StageId { get; set; }

    [Column("TERM_ID", TypeName = "int")] public int? TermId { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("GROUP_ID", TypeName = "int")] public int? GroupId { get; set; }
}

[Table("ABSENTCOURSE_TBL")]
public class AbsentCourseTbl
{
    [Key]
    [Column("ABSENTCOURSE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AbsentCourseId { get; set; }

    [Column("ABSENTDETAIL_ID", TypeName = "int")]
    public int? AbsentDetailId { get; set; }

    [Column("ABSENTLETTER_ID", TypeName = "int")]
    public int? AbsentLetterId { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int? CourseId { get; set; }

    [Column("ABSENT_COURSE_DATE", TypeName = "datetime")]
    public DateTime? AbsentCourseDate { get; set; }
}

[Table("ABSENTDETAIL_TBL")]
public class AbsentDetailTbl
{
    [Key]
    [Column("ABSENTDETAIL_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AbsentDetailId { get; set; }

    [Column("ABSENT_ID", TypeName = "int")]
    public int? AbsentId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(50)")]
    public string? StudentId { get; set; }
}

[Table("ABSENTLETTER_TBL")]
public class AbsentLetterTbl
{
    [Key]
    [Column("ABSENTLETTER_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AbsentLetterId { get; set; }

    [Column("LETTER", TypeName = "varchar(50)")]
    public string? Letter { get; set; }

    [Column("LETTERVALUE", TypeName = "decimal(18, 2)")]
    public decimal? LetterValue { get; set; }
}

[Table("ACADEMIC_REPORT_CON_EDU_ASSOCIATE_TO_BACHELOR")]
public class AcademicReportConEduAssociateToBachelor
{
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(50)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "varchar(50)")]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(10)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("SCHOOL_NAME", TypeName = "varchar(50)")]
    public string? SchoolName { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("FIELD_NAME_IN_KHMER", TypeName = "varchar(50)")]
    public string? FieldNameInKhmer { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("SCHOOL_NAME_IN_KHMER", TypeName = "varchar(50)")]
    public string? SchoolNameInKhmer { get; set; }

    [Column("PROMOTION_NO", TypeName = "int")]
    public int? PromotionNo { get; set; }

    [Column("FIELD_NAME", TypeName = "varchar(50)")]
    public string? FieldName { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("CREATE_IN_TERM_NO", TypeName = "int")]
    public int? CreateInTermNo { get; set; }
}

[Table("ACADEMIC_REPORT_CON_EDU_ASSOCIATE_TO_BACHELOR_TEMP")]
public class AcademicReportConEduAssociateToBachelorTemp
{
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(50)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "varchar(50)")]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(10)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("SCHOOL_NAME", TypeName = "varchar(50)")]
    public string? SchoolName { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("FIELD_NAME_IN_KHMER", TypeName = "varchar(50)")]
    public string? FieldNameInKhmer { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("SCHOOL_NAME_IN_KHMER", TypeName = "varchar(50)")]
    public string? SchoolNameInKhmer { get; set; }

    [Column("PROMOTION_NO", TypeName = "int")]
    public int? PromotionNo { get; set; }

    [Column("FIELD_NAME", TypeName = "varchar(50)")]
    public string? FieldName { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("CREATED_IN_TERM_NO", TypeName = "int")]
    public int? CreatedInTermNo { get; set; }
}

[Table("ACADEMIC_REPORT_EXAMINATION_RESULT")]
public class AcademicReportExaminationResult
{
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(30)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "varchar(30)")]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(6)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("MID1", TypeName = "float")] public double? Mid1 { get; set; }

    [Column("FINAL1", TypeName = "float")] public double? Final1 { get; set; }

    [Column("TOTAL1", TypeName = "float")] public double? Total1 { get; set; }

    [Column("MID2", TypeName = "float")] public double? Mid2 { get; set; }

    [Column("FINAL2", TypeName = "float")] public double? Final2 { get; set; }

    [Column("TOTAL2", TypeName = "float")] public double? Total2 { get; set; }

    [Column("MID3", TypeName = "float")] public double? Mid3 { get; set; }

    [Column("FINAL3", TypeName = "float")] public double? Final3 { get; set; }

    [Column("TOTAL3", TypeName = "float")] public double? Total3 { get; set; }

    [Column("MID4", TypeName = "float")] public double? Mid4 { get; set; }

    [Column("FINAL4", TypeName = "float")] public double? Final4 { get; set; }

    [Column("TOTAL4", TypeName = "float")] public double? Total4 { get; set; }

    [Column("MID5", TypeName = "float")] public double? Mid5 { get; set; }

    [Column("FINAL5", TypeName = "float")] public double? Final5 { get; set; }

    [Column("TOTAL5", TypeName = "float")] public double? Total5 { get; set; }

    [Column("MID6", TypeName = "float")] public double? Mid6 { get; set; }

    [Column("FINAL6", TypeName = "float")] public double? Final6 { get; set; }

    [Column("TOTAL6", TypeName = "float")] public double? Total6 { get; set; }

    [Column("MID7", TypeName = "float")] public double? Mid7 { get; set; }

    [Column("FINAL7", TypeName = "float")] public double? Final7 { get; set; }

    [Column("TOTAL7", TypeName = "float")] public double? Total7 { get; set; }
}

[Table("ACADEMIC_REPORT_EXAMINATION_RESULT_TEMP")]
public class AcademicReportExaminationResultTemp
{
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }
}

[Table("ACADEMIC_REPORT_FOUNDATION_YEAR_COURSE")]
public class AcademicReportFoundationYearCourse
{
    [Column("COURSE_ID", TypeName = "int")]
    public int CourseId { get; set; }

    [Column("ORDER", TypeName = "int")] public int? Order { get; set; }
}

[Table("ACADEMIC_REPORT_FOUNDATION_YEAR_EXAMINATION_RESULT")]
public class AcademicReportFoundationYearExaminationResult
{
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(30)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "nvarchar(50)")]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(6)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("SCHOOL_ID", TypeName = "nchar(10)")]
    public string? SchoolId { get; set; }

    [Column("SCHOOL_NAME_IN_KHMER", TypeName = "nvarchar(50)")]
    public string? SchoolNameInKhmer { get; set; }

    [Column("C11", TypeName = "float")] public double? C11 { get; set; }

    [Column("C12", TypeName = "float")] public double? C12 { get; set; }

    [Column("C13", TypeName = "float")] public double? C13 { get; set; }

    [Column("C14", TypeName = "float")] public double? C14 { get; set; }

    [Column("C15", TypeName = "float")] public double? C15 { get; set; }

    [Column("C21", TypeName = "float")] public double? C21 { get; set; }

    [Column("C22", TypeName = "float")] public double? C22 { get; set; }

    [Column("C23", TypeName = "float")] public double? C23 { get; set; }

    [Column("C24", TypeName = "float")] public double? C24 { get; set; }

    [Column("C25", TypeName = "float")] public double? C25 { get; set; }

    [Column("RESULT", TypeName = "float")] public double? Result { get; set; }
}

[Table("ACADEMIC_REPORT_RE_STUDY_STUDENT")]
public class AcademicReportReStudyStudent
{
    [Column("STUDENT_ID", TypeName = "varchar(50)")]
    public string? StudentId { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int CourseId { get; set; }
}

[Table("ACADEMIC_REPORT_REEXAM_STUDENT")]
public class AcademicReportReexamStudent
{
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "nvarchar(50)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "nvarchar(50)")]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(6)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("GROUP_NAME", TypeName = "varchar(10)")]
    public string? GroupName { get; set; }

    [Column("TOTAL1", TypeName = "float")] public double? Total1 { get; set; }

    [Column("TOTAL2", TypeName = "float")] public double? Total2 { get; set; }

    [Column("TOTAL3", TypeName = "float")] public double? Total3 { get; set; }

    [Column("TOTAL4", TypeName = "float")] public double? Total4 { get; set; }

    [Column("TOTAL5", TypeName = "float")] public double? Total5 { get; set; }

    [Column("TOTAL6", TypeName = "float")] public double? Total6 { get; set; }
}

[Table("ACADEMIC_REPORT_STATE_EXAMINATION_RESULT")]
public class AcademicReportStateExaminationResult
{
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(30)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "varchar(30)")]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(6)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("SCORE1", TypeName = "float")] public double? Score1 { get; set; }

    [Column("SCORE2", TypeName = "float")] public double? Score2 { get; set; }

    [Column("SCORE3", TypeName = "float")] public double? Score3 { get; set; }

    [Column("SCORE4", TypeName = "float")] public double? Score4 { get; set; }

    [Column("SCORE5", TypeName = "float")] public double? Score5 { get; set; }

    [Column("SCORE6", TypeName = "float")] public double? Score6 { get; set; }
}

[Table("ADMIN_REPORT_STATISTIC_BY_PROVINCE")]
public class AdminReportStatisticByProvince
{
    [Column("FROM_PROVINCE_ID", TypeName = "int")]
    public int? FromProvinceId { get; set; }

    [Column("PROVINCE", TypeName = "varchar(30)")]
    public string? Province { get; set; }

    [Column("STUDY_TIME1", TypeName = "int")]
    public int? StudyTime1 { get; set; }

    [Column("STUDY_TIME2", TypeName = "int")]
    public int? StudyTime2 { get; set; }

    [Column("STUDY_TIME3", TypeName = "int")]
    public int? StudyTime3 { get; set; }

    [Column("STUDY_TIME4", TypeName = "int")]
    public int? StudyTime4 { get; set; }

    [Column("TOTAL_FEMALE", TypeName = "int")]
    public int? TotalFemale { get; set; }
}

[Table("ADMIN_SCORE_SHEET")]
public class AdminScoreSheet
{
    [Column("PROMOTION_NO", TypeName = "int")]
    public int PromotionNo { get; set; }

    [Column("STAGE_NO", TypeName = "int")] public int StageNo { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int SchoolId { get; set; }

    [Column("SCHOOL_NAME", TypeName = "varchar(50)")]
    public string? SchoolName { get; set; }

    [Column("SCHOOL_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? SchoolNameInKhmer { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int FieldId { get; set; }

    [Column("FIELD_NAME", TypeName = "varchar(100)")]
    public string? FieldName { get; set; }

    [Column("FIELD_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? FieldNameInKhmer { get; set; }

    [Column("TERM_ID", TypeName = "int")] public int TermId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int TermNo { get; set; }

    [Column("GROUP_ID", TypeName = "int")] public int GroupId { get; set; }

    [Column("GROUP_NAME", TypeName = "varchar(30)")]
    public string? GroupName { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(30)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "nvarchar(30)")]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(6)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime DateOfBirth { get; set; }

    [Column("PHONE", TypeName = "varchar(50)")]
    public string? Phone { get; set; }

    [Column("ROOM_NAME", TypeName = "varchar(15)")]
    public string? RoomName { get; set; }

    [Column("IS_PHOTO_RECEIVED", TypeName = "int")]
    public int? IsPhotoReceived { get; set; }
}

[Table("APPCBank_StudentID")]
public class AppCBankStudentId
{
    [Key]
    [Column("STUDENT_ID", TypeName = "nvarchar(50)")]
    [Required]
    public string? StudentId { get; set; }
}

[Table("AVAILABLE_TIME")]
public class AvailableTime
{
    [Key]
    [Column("AVAILABLE_TIME_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AvailableTimeId { get; set; }

    [Required]
    [Column("INSTRUCTOR_ID", TypeName = "int")]
    public int InstructorId { get; set; }

    [Column("DAY_OF_WEEK", TypeName = "varchar(10)")]
    public string? DayOfWeek { get; set; }

    [Column("TIME", TypeName = "varchar(15)")]
    public string? Time { get; set; }
}

[Table("BOOK_CLOTHES")]
public class BookClothes
{
    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("IS_DEPOSIT", TypeName = "bit")]
    public bool? IsDeposit { get; set; }

    [Column("IS_RETURN", TypeName = "bit")]
    public bool? IsReturn { get; set; }

    [Column("INVOICE_NO", TypeName = "varchar(10)")]
    public string? InvoiceNo { get; set; }

    [Column("CONTACT_NUMBER", TypeName = "varchar(30)")]
    public string? ContactNumber { get; set; }

    [Column("NOTE", TypeName = "varchar(200)")]
    public string? Note { get; set; }
}

[Table("BOOKING_TBL")]
public class Booking
{
    [Key]
    [Column("BOOKINGID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookingId { get; set; }

    [Column("BOOKINGDATE", TypeName = "datetime")]
    public DateTime? BookingDate { get; set; }

    [Column("USERID", TypeName = "int")] public int? UserId { get; set; }

    [Column("STUDENTID", TypeName = "varchar(50)")]
    public string? StudentId { get; set; }

    [Column("EXCHANGEID", TypeName = "decimal(18,2)")]
    public decimal? ExchangeId { get; set; }

    [Column("TOTAL", TypeName = "decimal(18,6)")]
    public decimal? Total { get; set; }

    [Column("VAT", TypeName = "int")] public int? Vat { get; set; }

    [Column("DISCOUNT", TypeName = "decimal(18,6)")]
    public decimal? Discount { get; set; }

    [Column("PAYDOLLAR", TypeName = "decimal(18,6)")]
    public decimal? PayDollar { get; set; }

    [Column("PAYRIEAL", TypeName = "decimal(18,6)")]
    public decimal? PayRieal { get; set; }

    [Column("NOTE", TypeName = "nvarchar(600)")]
    public string? Note { get; set; }

    [Column("ACTIVE", TypeName = "bit")] public bool? Active { get; set; }

    [Column("DEGREE", TypeName = "nvarchar(50)")]
    public string? Degree { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("PROMOTION_NO", TypeName = "int")]
    public int? PromotionNo { get; set; }

    [Column("STAGE_NO", TypeName = "int")] public int? StageNo { get; set; }

    [Column("GROUP_ID", TypeName = "int")] public int? GroupId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("FROM_DATE", TypeName = "date")]
    public DateTime? FromDate { get; set; }

    [Column("TO_DATE", TypeName = "date")] public DateTime? ToDate { get; set; }

    [Column("STUDYTIME", TypeName = "nvarchar(50)")]
    public string? StudyTime { get; set; }

    [Column("UPDATE_BY", TypeName = "nvarchar(50)")]
    public string? UpdateBy { get; set; }

    [Column("UPDATE_DATE", TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("RETURN_ALREADY", TypeName = "bit")]
    public bool? ReturnAlready { get; set; }

    [Column("RETURN_RATE_IN", TypeName = "decimal(18,6)")]
    public decimal? ReturnRateIn { get; set; }

    [Column("RETURN_DATE", TypeName = "date")]
    public DateTime? ReturnDate { get; set; }

    [Column("RETURN_AMOUNT", TypeName = "decimal(18,6)")]
    public decimal? ReturnAmount { get; set; }

    [Column("RETURN_DOLLAR", TypeName = "decimal(18,6)")]
    public decimal? ReturnDollar { get; set; }

    [Column("RETURN_RIEL", TypeName = "decimal(18,6)")]
    public decimal? ReturnRiel { get; set; }

    [Column("BOOKING_NO", TypeName = "int")]
    public int? BookingNo { get; set; }

    [Column("YEAR_NUMBER", TypeName = "varchar(10)")]
    public string? YearNumber { get; set; }
}

[Table("BOOKINGDETAIL_TBL")]
public class BookingDetail
{
    [Column("BOOKINGDETAILID", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookingDetailId { get; set; }

    [Column("BOOKINGID", TypeName = "int")]
    public int? BookingId { get; set; }

    [Column("CLOTHID", TypeName = "int")] public int? ClothId { get; set; }

    [Column("QTY", TypeName = "decimal(18,3)")]
    public decimal? Qty { get; set; }

    [Column("PRICE", TypeName = "decimal(18,3)")]
    public decimal? Price { get; set; }
}

[Table("BOOKINGITEM_TBL")]
public class BookingItem
{
    [Column("BOOKINGITEMID", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookingItemId { get; set; }

    [Column("ITEMNAME", TypeName = "nvarchar(100)")]
    public string? ItemName { get; set; }

    [Column("ITEMNAMEKHMER", TypeName = "nvarchar(150)")]
    public string? ItemNameKhmer { get; set; }

    [Column("PRICE", TypeName = "decimal(18,6)")]
    public decimal? Price { get; set; }
    
    [Column("PRICE_KHR", TypeName = "decimal(18,6)")]
    public decimal? PriceKhr { get; set; }
    
    [Column("TYPE")]
    public string? Type { get; set; }
    
    [Column("DEFAULT_PAYMENT")]
    public string? DefaultPayment { get; set; }
    
    [Column("HIDDEND")]
    public bool? Hidden  { get; set; }
}

[Table("BOOKINGRETURN_TBL")]
public class BookingReturn
{
    [Column("BOOKINGRETURN_ID", TypeName = "int")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookingReturnId { get; set; }

    [Column("BOOKINGID", TypeName = "int")]
    public int? BookingId { get; set; }

    [Column("RETURN_DATE", TypeName = "date")]
    public DateTime? ReturnDate { get; set; }

    [Column("BOOKINGRETURN_NO", TypeName = "int")]
    public int? BookingReturnNo { get; set; }

    [Column("YEAR_NUMBER", TypeName = "varchar(10)")]
    public string? YearNumber { get; set; }

    [Column("USERID", TypeName = "int")] public int? UserId { get; set; }

    [Column("EXCHANGEID", TypeName = "decimal(18,2)")]
    public decimal? ExchangeId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(50)")]
    public string? StudentId { get; set; }

    [Column("DEGREE", TypeName = "nvarchar(50)")]
    public string? Degree { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("PROMOTION_NO", TypeName = "int")]
    public int? PromotionNo { get; set; }

    [Column("STAGE_NO", TypeName = "int")] public int? StageNo { get; set; }

    [Column("GROUP_NO", TypeName = "int")] public int? GroupNo { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("STUDY_TIME", TypeName = "nvarchar(50)")]
    public string? StudyTime { get; set; }

    [Column("FROM_DATE", TypeName = "date")]
    public DateTime? FromDate { get; set; }

    [Column("TO_DATE", TypeName = "date")] public DateTime? ToDate { get; set; }

    [Column("UPDATE_BY", TypeName = "nvarchar(50)")]
    public string? UpdateBy { get; set; }

    [Column("UPDATE_DATE", TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("RETURN_RATE_IN", TypeName = "decimal(18,6)")]
    public decimal? ReturnRateIn { get; set; }

    [Column("RETURN_AMOUNT", TypeName = "decimal(18,6)")]
    public decimal? ReturnAmount { get; set; }

    [Column("VAT", TypeName = "int")] public int? Vat { get; set; }

    [Column("DISCOUNT", TypeName = "decimal(18,6)")]
    public decimal? Discount { get; set; }

    [Column("RETURN_DOLLAR", TypeName = "decimal(18,6)")]
    public decimal? ReturnDollar { get; set; }

    [Column("RETURN_RIEL", TypeName = "decimal(18,6)")]
    public decimal? ReturnRiel { get; set; }

    [Column("NOTE", TypeName = "nvarchar(200)")]
    public string? Note { get; set; }

    [Column("ACTIVE", TypeName = "bit")] public bool? Active { get; set; }
}

[Table("BOOKINGRETURNDETAILTBL")]
public class BookingReturnDetail
{
    [Key]
    [Column("BOOKINGRETURNDETAILID", TypeName = "int")]
    public int BookingReturnDetailId { get; set; }

    [Column("BOOKINGRETURNID", TypeName = "int")]
    public int? BookingReturnId { get; set; }

    [Column("BOOKINGID", TypeName = "int")]
    public int? BookingId { get; set; }

    [Column("CLOTHID", TypeName = "int")] public int? ClothId { get; set; }

    [Column("QTY", TypeName = "decimal(18,3)")]
    public decimal? Qty { get; set; }

    [Column("PRICE", TypeName = "decimal(18,3)")]
    public decimal? Price { get; set; }
}

[Table("BRANCH")]
public class Branch
{
    [Key]
    [Column("BRANCH_ID", TypeName = "int")]
    public int BranchId { get; set; }

    [Column("BRANCH_NAME", TypeName = "varchar(30)")]
    public string? BranchName { get; set; }

    [Column("BRANCH_NAME_IN_KHMER", TypeName = "nvarchar(50)")]
    public string? BranchNameInKhmer { get; set; }

    [Column("SHORT_NAME", TypeName = "varchar(50)")]
    public string? ShortName { get; set; }

    [Column("ADDRESS", TypeName = "nvarchar(200)")]
    public string? Address { get; set; }

    [Column("PHONE", TypeName = "varchar(50)")]
    public string? Phone { get; set; }
}

[Table("CATEGORY_TBL")]
public class Category
{
    [Key]
    [Column("CATEGORY_ID", TypeName = "int")]
    public int CategoryId { get; set; }

    [Column("CATEGORY_NAME", TypeName = "nvarchar(50)")]
    public string? CategoryName { get; set; }

    [Column("DESCRIPTIOIN", TypeName = "nvarchar(100)")]
    public string? Descriptioin { get; set; }
}

[Table("CERTIFICATE")]
public class Certificate
{
    [Key]
    [Column("CERTIFICATE_ID", TypeName = "int")]
    public int CertificateId { get; set; }

    [Column("CERTIFICATE_CODE", TypeName = "varchar(10)")]
    public string? CertificateCode { get; set; }

    [Column("CERTIFICATE_NAME", TypeName = "nvarchar(100)")]
    public string? CertificateName { get; set; }
}

[Table("CHANGE_BRANCH")]
public class ChangeBranch
{
    [Key]
    [Column("CHANGE_BRANCH_ID", TypeName = "int")]
    public int ChangeBranchId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TO_BRANCH_ID", TypeName = "int")]
    public int ToBranchId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int TermNo { get; set; }

    [Column("FROM_DATE", TypeName = "datetime")]
    public DateTime FromDate { get; set; }

    [Column("RETURN_DATE", TypeName = "datetime")]
    public DateTime? ReturnDate { get; set; }

    [Column("DEGREE_ID", TypeName = "varchar(50)")]
    public string? DegreeId { get; set; }

    [Column("SCHOOL_ID", TypeName = "varchar(50)")]
    public string? SchoolId { get; set; }

    [Column("FIELD_ID", TypeName = "varchar(50)")]
    public string? FieldId { get; set; }

    [Column("PROMOTION_ID", TypeName = "varchar(50)")]
    public string? PromotionId { get; set; }

    [Column("STAGE_ID", TypeName = "varchar(50)")]
    public string? StageId { get; set; }

    [Column("GROUP_ID", TypeName = "varchar(50)")]
    public string? GroupId { get; set; }
}

[Table("CHANGEFIELDTBL")]
public class ChangeFieldTbl
{
    [Key]
    [Column("CHANGE_ID", TypeName = "int")]
    public int ChangeId { get; set; }

    [Column("CHANGE_DATE", TypeName = "datetime")]
    public DateTime? ChangeDate { get; set; }

    [Column("STUDENT_ID", TypeName = "nvarchar(20)")]
    public string? StudentId { get; set; }

    [Column("OLD_FIELD_ID", TypeName = "int")]
    public int? OldFieldId { get; set; }

    [Column("NEW_FIELD_ID", TypeName = "int")]
    public int? NewFieldId { get; set; }

    [Column("USER_NAME", TypeName = "nvarchar(20)")]
    public string? UserName { get; set; }

    [Column("DEGREE_ID", TypeName = "nvarchar(20)")]
    public string? DegreeId { get; set; }

    [Column("SCHOOL_ID", TypeName = "nvarchar(50)")]
    public string? SchoolId { get; set; }

    [Column("SCHOOL_ID_NEW", TypeName = "nvarchar(50)")]
    public string? SchoolIdNew { get; set; }

    [Column("PROMOTION_ID", TypeName = "nvarchar(20)")]
    public string? PromotionId { get; set; }

    [Column("STAGE_ID", TypeName = "nvarchar(20)")]
    public string? StageId { get; set; }

    [Column("TERM_NO", TypeName = "nvarchar(20)")]
    public string? TermNo { get; set; }

    [Column("GROUP_ID", TypeName = "nvarchar(20)")]
    public string? GroupId { get; set; }
}

[Table("COMPLEMENT_FAILED_COURSE_SCORE")]
public class ComplementFailedCourseScore
{
    [Key]
    [Column("COMPLEMENT_FAILED_COURSE_SCORE_ID", TypeName = "int")]
    public int ComplementFailedCourseScoreId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int TermNo { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int CourseId { get; set; }

    [Column("MID_TERM_SCORE", TypeName = "float")]
    public double MidTermScore { get; set; }

    [Column("FINAL_SCORE", TypeName = "float")]
    public double FinalScore { get; set; }

    [Column("USERNAME", TypeName = "varchar(50)")]
    public string? UserName { get; set; }

    [Column("DATE_EDIT", TypeName = "datetime")]
    public DateTime? DateEdit { get; set; }
}

[Table("COMPLEMENT_ORIENTED_COURSE_SCORE")]
public class ComplementOrientedCourseScore
{
    [Key]
    [Column("COMPLEMENT_ORIENTED_COURSE_SCORE_ID", TypeName = "int")]
    public int ComplementOrientedCourseScoreId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int TermNo { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int CourseId { get; set; }

    [Column("MID_TERM_SCORE", TypeName = "float")]
    public double MidTermScore { get; set; }

    [Column("FINAL_SCORE", TypeName = "float")]
    public double FinalScore { get; set; }

    [Column("NOTE", TypeName = "nvarchar(500)")]
    public string? Note { get; set; }

    [Column("USERNAME", TypeName = "varchar(50)")]
    public string? UserName { get; set; }

    [Column("DATE_EDIT", TypeName = "datetime")]
    public DateTime? DateEdit { get; set; }
}

[Table("COMPLEMENT_SEMESTER_SCORE")]
public class ComplementSemesterScore
{
    [Key]
    [Column("COMPLEMENT_SEMESTER_SCORE_ID", TypeName = "int")]
    public int ComplementSemesterScoreId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int TermNo { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int CourseId { get; set; }

    [Column("MID_TERM_SCORE", TypeName = "float")]
    public double MidTermScore { get; set; }

    [Column("FINAL_SCORE", TypeName = "float")]
    public double FinalScore { get; set; }

    [Column("USERNAME", TypeName = "varchar(50)")]
    public string? UserName { get; set; }

    [Column("DATE_EDIT", TypeName = "datetime")]
    public DateTime? DateEdit { get; set; }
}

[Table("CONTACT_PERSON")]
public class ContactPerson
{
    [Key]
    [Column("CONTACT_PERSON_ID", TypeName = "int")]
    public int ContactPersonId { get; set; }

    [Column("CONTACT_PERSON_NAME", TypeName = "nvarchar(100)")]
    public string? ContactPersonName { get; set; }

    [Column("JOB", TypeName = "nvarchar(200)")]
    public string? Job { get; set; }

    [Column("PHONE", TypeName = "varchar(24)")]
    public string? Phone { get; set; }

    [Column("ADDRESS", TypeName = "nvarchar(300)")]
    public string? Address { get; set; }
}

[Table("COURSE")]
public class Course
{
    [Key]
    [Column("COURSE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CourseId { get; set; }

    [Column("COURSE_FULL_NAME", TypeName = "varchar(60)")]
    public string? CourseFullName { get; set; }

    [Column("COURSE_FULL_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? CourseFullNameInKhmer { get; set; }

    [Column("COURSE_SHORT_NAME", TypeName = "varchar(30)")]
    public string? CourseShortName { get; set; }

    [Column("COURSE_SHORT_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? CourseShortNameInKhmer { get; set; }

    [Column("CREDIT", TypeName = "float")] public double? Credit { get; set; }

    [Column("NUMBER_OF_HOURS", TypeName = "float")]
    public double? NumberOfHours { get; set; }
}

[Table("COURSE_CODE")]
public class CourseCode
{
    [Key]
    [Column("COURSE_CODE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CourseCodeId { get; set; }

    [Required]
    [Column("COURSE_ID", TypeName = "int")]
    public int CourseId { get; set; }

    [Required]
    [Column("SCHOOL_ID", TypeName = "int")]
    public int SchoolId { get; set; }

    [Required]
    [Column("FIELD_ID", TypeName = "int")]
    public int FieldId { get; set; }

    [Required]
    [Column("DEGREE_ID", TypeName = "int")]
    public int DegreeId { get; set; }

    [Required]
    [Column("TERM_NO", TypeName = "int")]
    public int TermNo { get; set; }

    [Required]
    [Column("CODE", TypeName = "varchar(10)")]
    public string? Code { get; set; }
}

[Table("COURSE_SCHOOL")]
[PrimaryKey("SchoolId", "CourseId")]
public class CourseSchool
{
    [Key]
    [Column("SCHOOL_ID", TypeName = "int")]
    public int SchoolId { get; set; }

    [Key]
    [Column("COURSE_ID", TypeName = "int")]
    public int CourseId { get; set; }
}

[Table("COURSE_TERM")]
public class CourseTerm
{
    [Key]
    [Column("COURSE_TERM_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CourseTermId { get; set; }

    [Required]
    [Column("COURSE_ID", TypeName = "int")]
    public int CourseId { get; set; }

    [Required]
    [Column("FIELD_ID", TypeName = "int")]
    public int FieldId { get; set; }

    [Required]
    [Column("TERM_ID", TypeName = "int")]
    public int TermId { get; set; }

    [Column("CREDIT", TypeName = "float")] public double? Credit { get; set; }

    [Column("TYPE", TypeName = "varchar(20)")]
    public string? Type { get; set; }

    [Column("HOURS", TypeName = "float")] public double? Hours { get; set; }
}

[Table("DEBUG_LOGGER")]
public class DebugLogger
{
    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("MSG", TypeName = "varchar(200)")]
    public string? Msg { get; set; }
}

[Table("DEGREE")]
public class Degree
{
    [Key]
    [Column("DEGREE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DegreeId { get; set; }

    [Required]
    [Column("DEGREE", TypeName = "varchar(30)")]
    public string? DegreeName { get; set; }

    [Column("DEGREE_IN_KHMER", TypeName = "nvarchar(30)")]
    public string? DegreeInKhmer { get; set; }
}

[Table("DISABILITY_TBL")]
public class Disability
{
    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Disability", TypeName = "nvarchar(200)")]
    public string? DisabilityName { get; set; }

    [Column("DisabilityKh", TypeName = "nvarchar(200)")]
    public string? DisabilityNameKh { get; set; }
}

[Table("DISCOUNT")]
public class Discount
{
    [Key]
    [Column("DISCOUNT_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DiscountId { get; set; }

    [Required]
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Required]
    [Column("TERM_NO", TypeName = "int")]
    public int TermNo { get; set; }

    [Required]
    [Column("AMOUNT", TypeName = "money")]
    public decimal Amount { get; set; }

    [Column("REASON", TypeName = "varchar(30)")]
    public string? Reason { get; set; }
}

[Table("DOCTORAL_CONTRACT")]
public class DoctoralContract
{
    [Key]
    [Column("CONTRACT_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ContractId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(50)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("FEE", TypeName = "decimal(10,2)")]
    public decimal? Fee { get; set; }

    [Column("START_DATE", TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column("END_DATE", TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Column("NOTE", TypeName = "nvarchar(100)")]
    public string? Note { get; set; }
}

[Table("EXAM_DATE")]
public class ExamDate
{
    [Key]
    [Column("EXAM_DATE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExamDateId { get; set; }

    [Required]
    [Column("COURSE_TERM_ID", TypeName = "int")]
    public int CourseTermId { get; set; }

    [Required]
    [Column("DATE", TypeName = "datetime")]
    public DateTime Date { get; set; }
}

[Table("ExchangeRate_Tbl")]
public class ExchangeRate
{
    [Key]
    [Column("ExchangeRateID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExchangeRateId { get; set; }

    [Column("ExchangeDate", TypeName = "datetime")]
    public DateTime? ExchangeDate { get; set; }

    [Column("Description", TypeName = "nvarchar(200)")]
    public string? Description { get; set; }
}

[Table("ExchangeRateDetail_Tbl")]
public class ExchangeRateDetail
{
    [Key]
    [Column("DetailID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DetailId { get; set; }

    [Column("ExchangeRateID", TypeName = "int")]
    public int? ExchangeRateId { get; set; }

    [Column("CurrencyNameIn", TypeName = "nvarchar(50)")]
    public string? CurrencyNameIn { get; set; }

    [Column("CurrencyNameOut", TypeName = "nvarchar(50)")]
    public string? CurrencyNameOut { get; set; }

    [Column("RateIn", TypeName = "decimal(18,2)")]
    public decimal? RateIn { get; set; }

    [Column("RateOut", TypeName = "decimal(18,2)")]
    public decimal? RateOut { get; set; }
}

[Table("EXTEND")]
public class Extend
{
    [Key]
    [Column("EXTEND_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExtendId { get; set; }

    [Required]
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Required]
    [Column("TERM_NO", TypeName = "int")]
    public int TermNo { get; set; }

    [Required]
    [Column("EXTEND_FROM", TypeName = "varchar(20)")]
    public string? ExtendFrom { get; set; }

    [Required]
    [Column("FROM_ID", TypeName = "int")]
    public int FromId { get; set; }

    [Column("IS_CERTIFICATE_RECEIVED", TypeName = "int")]
    public int? IsCertificateReceived { get; set; }

    [Column("IS_TRANSCRIPT_RECEIVED", TypeName = "int")]
    public int? IsTranscriptReceived { get; set; }

    [Column("EXTEND_DATE", TypeName = "date")]
    public DateTime? ExtendDate { get; set; }
}

[Table("EXTERNAL_SCORE")]
public class ExternalScore
{
    [Key]
    [Column("EXTERNAL_SCORE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExternalScoreId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int TermNo { get; set; }

    [Column("COURSE_NAME", TypeName = "varchar(30)")]
    public string? CourseName { get; set; }

    [Column("COURSE_NAME_IN_KHMER", TypeName = "varchar(30)")]
    public string? CourseNameInKhmer { get; set; }

    [Column("CREDIT", TypeName = "int")] public int Credit { get; set; }

    [Column("GRADE", TypeName = "varchar(10)")]
    public string? Grade { get; set; }

    [Column("TOTAL", TypeName = "decimal(18,2)")]
    public decimal? Total { get; set; }

    [Column("COURSE_CODE", TypeName = "varchar(10)")]
    public string? CourseCode { get; set; }

    [Column("YEAR_START", TypeName = "int")]
    public int? YearStart { get; set; }

    [Column("YEAR_END", TypeName = "int")] public int? YearEnd { get; set; }

    [Column("USERNAME", TypeName = "varchar(50)")]
    public string? Username { get; set; }

    [Column("DATE_EDIT", TypeName = "datetime")]
    public DateTime? DateEdit { get; set; }
}

[Table("FACULTY")]
public class Faculty
{
    [Key]
    [Column("FACULTY_ID", TypeName = "numeric(28,0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal FacultyId { get; set; }

    [Required]
    [Column("FACULTY_NAME", TypeName = "varchar(60)")]
    public string? FacultyName { get; set; }

    [Required]
    [Column("FACULTY_NAME_IN_KHMER", TypeName = "nvarchar(60)")]
    public string? FacultyNameInKhmer { get; set; }
}

[Table("FIELD")]
public class Field
{
    [Key]
    [Column("FIELD_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FieldId { get; set; }

    [Column("FIELD_NAME", TypeName = "varchar(200)")]
    public string? FieldName { get; set; }

    [Column("FIELD_NAME_IN_KHMER", TypeName = "nvarchar(200)")]
    public string? FieldNameInKhmer { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int SchoolId { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int DegreeId { get; set; }

    [Column("DEGREE_NAME", TypeName = "varchar(100)")]
    public string? DegreeName { get; set; }

    [Column("DEGREE_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? DegreeNameInKhmer { get; set; }

    [Column("TYPE", TypeName = "bit")] public bool? Type { get; set; }
}

[Table("FIELD_CERTIFICATE")]
public class FieldCertificate
{
    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [Column("DEGREE_NAME", TypeName = "varchar(100)")]
    public string? DegreeName { get; set; }

    [Column("DEGREE_NAME_KHMER", TypeName = "nvarchar(100)")]
    public string? DegreeNameKhmer { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("SCHOOL_NAME", TypeName = "varchar(200)")]
    public string? SchoolName { get; set; }

    [Column("SCHOOL_NAME_KHMER", TypeName = "nvarchar(250)")]
    public string? SchoolNameKhmer { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("FIELD_NAME", TypeName = "varchar(250)")]
    public string? FieldName { get; set; }

    [Column("FIELD_NAME_KHMER", TypeName = "nvarchar(250)")]
    public string? FieldNameKhmer { get; set; }

    [Column("PROMOTION_NO", TypeName = "int")]
    public int? PromotionNo { get; set; }

    [Column("STATUS", TypeName = "bit")] public bool? Status { get; set; }

    [Column("TYPE", TypeName = "varchar(5)")]
    public string? Type { get; set; }

    [Column("TYPE_KHMER", TypeName = "nvarchar(20)")]
    public string? TypeKhmer { get; set; }
}

[Table("FOUNDATION_YEAR_REPORT_CERTIFICATE_OF_FOUNDATION_YEAR_COURSE")]
public class FoundationYearReportCertificateOfFoundationYearCourse
{
    [Key]
    [Column("CERTIFICATE_OF_FOUNDATION_YEAR_COURSE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CertificateOfFoundationYearCourseId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(30)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(6)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int? CourseId { get; set; }

    [Column("COURSE_FULL_NAME", TypeName = "varchar(60)")]
    public string? CourseFullName { get; set; }

    [Column("COURSE_FULL_NAME_IN_KHMER", TypeName = "nvarchar(60)")]
    public string? CourseFullNameInKhmer { get; set; }

    [Column("COURSE_SHORT_NAME", TypeName = "varchar(30)")]
    public string? CourseShortName { get; set; }

    [Column("COURSE_SHORT_NAME_IN_KHMER", TypeName = "varchar(30)")]
    public string? CourseShortNameInKhmer { get; set; }

    [Column("IS_GENERAL_COURSE", TypeName = "int")]
    public int? IsGeneralCourse { get; set; }

    [Column("CREDIT", TypeName = "int")] public int? Credit { get; set; }

    [Column("GRADE_LETTER", TypeName = "varchar(15)")]
    public string? GradeLetter { get; set; }

    [Column("GPA", TypeName = "float")] public double? Gpa { get; set; }
}

[Table("GRADE")]
public class Grade
{
    [Key]
    [Column("GRADE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GradeId { get; set; }

    [Column("GRADE_LETTER", TypeName = "varchar(15)")]
    public string? GradeLetter { get; set; }

    [Column("FROM_SCORE", TypeName = "float")]
    public double? FromScore { get; set; }

    [Column("TO_SCORE", TypeName = "float")]
    public double? ToScore { get; set; }

    [Column("POINT", TypeName = "float")] public double? Point { get; set; }

    [Column("MEANING", TypeName = "varchar(15)")]
    public string? Meaning { get; set; }
}

[Table("GROUP")]
public class Group
{
    [Key]
    [Column("GROUP_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GroupId { get; set; }

    [Column("GROUP_NAME", TypeName = "varchar(10)")]
    public string? GroupName { get; set; }

    [Column("STUDY_TIME", TypeName = "varchar(15)")]
    public string? StudyTime { get; set; }

    [Column("STAGE_ID", TypeName = "int")] public int StageId { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("CREATED_IN_TERM_NO", TypeName = "int")]
    public int? CreatedInTermNo { get; set; }

    [Column("NOTE", TypeName = "varchar(50)")]
    public string? Note { get; set; }
}

[Table("GROUP_ROOM")]
public class GroupRoom
{
    [Key]
    [Column("GROUP_ROOM_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GroupRoomId { get; set; }

    [Column("GROUP_ID", TypeName = "int")] public int GroupId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int TermNo { get; set; }

    [Column("ROOM_NAME", TypeName = "varchar(15)")]
    public string? RoomName { get; set; }

    [Column("START_PAYMENT", TypeName = "datetime")]
    public DateTime? StartPayment { get; set; }
}

[Table("HIGH_SCHOOL")]
public class HighSchool
{
    [Key]
    [Column("HIGH_SCHOOL_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int HighSchoolId { get; set; }

    [Column("HIGH_SCHOOL_NAME", TypeName = "nvarchar(50)")]
    public string? HighSchoolName { get; set; }

    [Column("HIGH_SCHOOL_NAME_IN_KHMER", TypeName = "nvarchar(50)")]
    public string? HighSchoolNameInKhmer { get; set; }
}

[Table("HIGH_SCHOOL_TYPE")]
public class HighSchoolType
{
    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }

    [Column("NAME", TypeName = "varchar(50)")]
    public string? Name { get; set; }

    [Column("NAME_KHMER", TypeName = "nvarchar(50)")]
    public string? NameKhmer { get; set; }
}

[Table("INSTRUCTOR")]
public class Instructor
{
    [Key]
    [Column("INSTRUCTOR_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? InstructorId { get; set; }

    [Column("INSTRUCTOR_NAME", TypeName = "varchar(30)")]
    public string? InstructorName { get; set; }

    [Column("INSTRUCTOR_NAME_IN_KHMER", TypeName = "varchar(30)")]
    public string? InstructorNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(6)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("PLACE_OF_BIRTH", TypeName = "varchar(30)")]
    public string? PlaceOfBirth { get; set; }

    [Column("RACE", TypeName = "varchar(30)")]
    public string? Race { get; set; }

    [Column("NATIONALITY", TypeName = "varchar(30)")]
    public string? Nationality { get; set; }

    [Column("MARITAL_STATUS", TypeName = "varchar(15)")]
    public string? MaritalStatus { get; set; }

    [Column("PHONE", TypeName = "varchar(15)")]
    public string? Phone { get; set; }

    [Column("EMAIL", TypeName = "varchar(30)")]
    public string? Email { get; set; }

    [Column("ADDRESS", TypeName = "varchar(50)")]
    public string? Address { get; set; }

    [Column("DEGREE", TypeName = "varchar(15)")]
    public string? Degree { get; set; }

    [Column("INSTRUCTOR_TYPE", TypeName = "varchar(30)")]
    public string? InstructorType { get; set; }
}

[Table("INSTRUCTOR_CERTIFICATE")]
public class InstructorCertificate
{
    [Key]
    [Column("INSTRUCTOR_CERTIFICATE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InstructorCertificateId { get; set; }

    [Column("INSTRUCTOR_ID", TypeName = "int")]
    public int? InstructorId { get; set; }

    [Column("CERTIFICATE_NAME", TypeName = "varchar(70)")]
    public string? CertificateName { get; set; }

    [Column("YEAR_OBTAINED", TypeName = "int")]
    public int? YearObtained { get; set; }

    [Column("UNIVERSITY", TypeName = "varchar(70)")]
    public string? University { get; set; }

    [Column("COUNTRY", TypeName = "varchar(30)")]
    public string? Country { get; set; }
}

[Table("INSTRUCTOR_COURSE")]
public class InstructorCourse
{
    [Key]
    [Column("INSTRUCTOR_COURSE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InstructorCourseId { get; set; }

    [Column("INSTRUCTOR_ID", TypeName = "int")]
    public int? InstructorId { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int? CourseId { get; set; }
}

[Table("INSTRUCTOR_GROUP")]
public class InstructorGroup
{
    [Key]
    [Column("INSTRUCTOR_GROUP_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InstructorGroupId { get; set; }

    [Column("INSTRUCTOR_ID", TypeName = "int")]
    public int? InstructorId { get; set; }

    [Column("GROUP_ID", TypeName = "int")] public int? GroupId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int? CourseId { get; set; }

    [Column("DAY_OF_WEEK", TypeName = "varchar(15)")]
    public string? DayOfWeek { get; set; }

    [Column("TIME", TypeName = "varchar(15)")]
    public string? Time { get; set; }

    [Column("ROOM_NAME", TypeName = "varchar(15)")]
    public string? RoomName { get; set; }

    [Column("STATUS", TypeName = "varchar(15)")]
    public string? Status { get; set; }
}

[Table("INSTRUCTOR_SCHOOL")]
public class InstructorSchool
{
    [Key]
    [Column("INSTRUCTOR_SCHOOL_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InstructorSchoolId { get; set; }

    [Column("INSTRUCTOR_ID", TypeName = "int")]
    public int? InstructorId { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }
}

[Table("INSTRUCTOR_TYPE")]
public class InstructorType
{
    [Key]
    [Column("INSTRUCTOR_TYPE", TypeName = "varchar(30)")]
    public string? InstructorTypeValue { get; set; }
}

[Table("INVOICE_ITEM_DETAIL")]
public class InvoiceItemDetail
{
    [Key]
    [Column("INVOICE_ITEM_DETAIL_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InvoiceItemDetailId { get; set; }

    [Column("INVOICE_ITEM_ID", TypeName = "int")]
    public int? InvoiceItemId { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("VAT", TypeName = "int")] public int? Vat { get; set; }

    [Column("PRICE", TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }
}

[Table("INVOICE_PAYMENT_TBL")]
public class InvoicePaymentTbl
{
    [Key]
    [Column("PaymentID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? PaymentId { get; set; }

    [Column("InvoiceID", TypeName = "int")]
    public int? InvoiceId { get; set; }

    [Column("PaymentDate", TypeName = "datetime")]
    public DateTime? PaymentDate { get; set; }

    [Column("ExchangeID", TypeName = "int")]
    public int? ExchangeId { get; set; }

    [Column("OweAmount", TypeName = "decimal(18,2)")]
    public decimal? OweAmount { get; set; }

    [Column("PayAmount", TypeName = "decimal(18,2)")]
    public decimal? PayAmount { get; set; }

    [Column("PayAmountR", TypeName = "decimal(18,2)")]
    public decimal? PayAmountR { get; set; }
}

[Table("INVOICE_RECEIVE_MONEY")]
public class InvoiceReceiveMoney
{
    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("INVOICE_ID", TypeName = "int")]
    public int? InvoiceId { get; set; }

    [Column("PAYMENT_METHOD_ID", TypeName = "int")]
    public int? PaymentMethodId { get; set; }

    [Column("DOLLAR", TypeName = "decimal(18,6)")]
    public decimal? Dollar { get; set; }

    [Column("REIL", TypeName = "decimal(18,6)")]
    public decimal? Reil { get; set; }
}

[Table("INVOICE_TBL")]
public class Invoice
{
    [Key]
    [Column("INVOICE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InvoiceId { get; set; }

    [Column("INVOICE_NO", TypeName = "int")]
    public int? InvoiceNo { get; set; }

    [Column("YEAR_NUMBER", TypeName = "varchar(10)")]
    public string? YearNumber { get; set; }

    [Column("INVOICE_DATE", TypeName = "datetime")]
    public DateTime? InvoiceDate { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("DEGREE_ID", TypeName = "varchar(50)")]
    public string? DegreeId { get; set; }

    [Column("SCHOOL_ID", TypeName = "varchar(50)")]
    public string? SchoolId { get; set; }

    [Column("FIELD_ID", TypeName = "varchar(50)")]
    public string? FieldId { get; set; }

    [Column("PROMOTION_ID", TypeName = "varchar(10)")]
    public string? PromotionId { get; set; }

    [Column("STAGE_ID", TypeName = "varchar(10)")]
    public string? StageId { get; set; }

    [Column("GROUP_ID", TypeName = "varchar(10)")]
    public string? GroupId { get; set; }

    [Column("STARTDATE", TypeName = "date")]
    public DateTime? StartDate { get; set; }

    [Column("ENDDATE", TypeName = "date")] public DateTime? EndDate { get; set; }

    [Column("TERM_NO", TypeName = "varchar(10)")]
    public string? TermNo { get; set; }

    [Column("EXCHANGERATE_ID", TypeName = "int")]
    public int? ExchangeRateId { get; set; }

    [Column("VAT", TypeName = "decimal(18,2)")]
    public decimal? Vat { get; set; }

    [Column("GRAND_TOTAL", TypeName = "decimal(18,2)")]
    public decimal? GrandTotalUsd { get; set; }

    [Column("DESCRIPTION", TypeName = "nvarchar(100)")]
    public string? Description { get; set; }

    [Column("STATUS", TypeName = "varchar(10)")]
    public string? Status { get; set; }

    [Column("TOTALDOLLAR", TypeName = "decimal(18,2)")]
    public decimal? TotalDollar { get; set; }

    [Column("TOTALRIEL", TypeName = "decimal(18,2)")]
    public decimal? TotalRiel { get; set; }

    [Column("TOTALBATH", TypeName = "decimal(18,2)")]
    public decimal? TotalBath { get; set; }

    [Column("TOTALDISCOUNT", TypeName = "decimal(18,2)")]
    public decimal? TotalDiscountUsd { get; set; }

    [Column("PAYMENT", TypeName = "bit")] public bool? Payment { get; set; }

    [Column("CHECK_PAYMENT", TypeName = "bit")]
    public bool? CheckPayment { get; set; }

    [Column("DATE_EDIT", TypeName = "datetime")]
    public DateTime? DateEdit { get; set; }

    [Column("EDIT_BY", TypeName = "varchar(50)")]
    public string? EditBy { get; set; }

    [Column("OWE", TypeName = "decimal(18,2)")]
    public decimal? OweUsd { get; set; }

    [Column("OWE_REASON", TypeName = "nvarchar(100)")]
    public string? OweReason { get; set; }

    [Column("USER_ID", TypeName = "int")] public int? UserId { get; set; }

    [Column("TOTAL_RETURN_AMOUNT", TypeName = "decimal(18,2)")]
    public decimal? TotalReturnAmount { get; set; }

    [Column("RETURN_AMOUNT", TypeName = "decimal(18,2)")]
    public decimal? ReturnAmount { get; set; }

    [Column("RETURN_DESCRIPTION", TypeName = "nvarchar(50)")]
    public string? ReturnDescription { get; set; }

    [Column("TOTALOTHER", TypeName = "decimal(18,6)")]
    public decimal? TotalOtherUsd { get; set; }

    [Column("PAYMENT_METHOD_ID", TypeName = "int")]
    public int? PaymentMethodId { get; set; }

    [Column("AMOUNT_DOLLAR", TypeName = "decimal(18,0)")]
    public decimal? AmountDollar { get; set; }

    [Column("AMOUNT_REIL", TypeName = "decimal(18,0)")]
    public decimal? AmountReil { get; set; }

    [Column("PAY_ON_APP", TypeName = "bit")]
    public bool? PayOnApp { get; set; }

    [Column("GRAND_TOTAL_KHR", TypeName = "decimal(18,2)")]
    public decimal? GrandTotalKhr { get; set; }

    [Column("OWE_KHR", TypeName = "decimal(18,2)")]
    public decimal? OweKhr { get; set; }
}

[Table("INVOICEDETAIL_TBL")]
public class InvoiceDetail
{
    [Key]
    [Column("INVOICEDETAIL_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InvoiceDetailId { get; set; }

    [Column("INVOICE_ID", TypeName = "int")]
    public int? InvoiceId { get; set; }

    [Column("PRODUCT_ID", TypeName = "int")]
    public int? ProductId { get; set; }

    [Column("QTY", TypeName = "int")] public int? Qty { get; set; }

    [Column("QTYNOTE", TypeName = "varchar(10)")]
    public string? QtyNote { get; set; }

    [Column("PRICE", TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }

    [Column("NOTE", TypeName = "varchar(100)")]
    public string? Note { get; set; }

    [Column("VAT", TypeName = "decimal(18,2)")]
    public decimal? Vat { get; set; }

    [Column("P_DOLLAR", TypeName = "decimal(18,2)")]
    public decimal? PDollar { get; set; }

    [Column("P_RIEL", TypeName = "decimal(18,2)")]
    public decimal? PRiel { get; set; }

    [Column("P_BATH", TypeName = "decimal(18,2)")]
    public decimal? PBath { get; set; }

    [Column("DISCOUNT", TypeName = "decimal(18,2)")]
    public decimal? Discount { get; set; }

    [Column("OWE", TypeName = "decimal(18,2)")]
    public decimal? Owe { get; set; }

    [Column("CATEGORYID", TypeName = "int")]
    public int? CategoryId { get; set; }

    [Column("OTHER", TypeName = "decimal(18,6)")]
    public decimal? Other { get; set; }

    [Column("PRICE_KHR", TypeName = "decimal(18,2)")]
    public decimal? PriceKhr { get; set; }

    [Column("DISCOUNT_KHR", TypeName = "decimal(18,2)")]
    public decimal? DiscountKhr { get; set; }

    [Column("OWE_KHR", TypeName = "decimal(18,2)")]
    public decimal? OweKhr { get; set; }

    [Column("DISCOUNT_PERCENT", TypeName = "int")]
    public int? DiscountPercent { get; set; }

    [Column("OTHER_KHR", TypeName = "decimal(18,2)")]
    public decimal? OtherKhr { get; set; }
}

[Table("KHMER_LUNAA_CALENDAR")]
public class KhmerLunaaCalendar
{
    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("NAME_KHMER", TypeName = "nvarchar(100)")]
    public string? NameKhmer { get; set; }
}

[Table("LECTURER")]
public class Lecturer
{
    [Key]
    [Column("LECTURER_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LecturerId { get; set; }

    [Column("NAME", TypeName = "varchar(100)")]
    public string? Name { get; set; }

    [Column("SEX", TypeName = "varchar(1)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("PRICE", TypeName = "money")] public decimal? Price { get; set; }

    [Column("TELEPHONE", TypeName = "varchar(15)")]
    public string? Telephone { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [Column("LECTURER_FIELD_ID", TypeName = "int")]
    public int? LecturerFieldId { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("NAME_IN_KHMER", TypeName = "varchar(100)")]
    public string? NameInKhmer { get; set; }
}

[Table("LECTURER_BRANCH")]
public class LecturerBranch
{
    [Column("LECTURER_ID", TypeName = "int")]
    public int LecturerId { get; set; }

    [Column("BRANCH_ID", TypeName = "int")]
    public int BranchId { get; set; }
}

[Table("LECTURER_COURSE")]
public class LecturerCourse
{
    [Column("LECTURER_ID", TypeName = "int")]
    public int LecturerId { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int CourseId { get; set; }
}

[Table("LECTURER_DEGREE")]
public class LecturerDegree
{
    [Key]
    [Column("LECTURER_DEGREE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LecturerDegreeId { get; set; }

    [Column("LECTURER_DEGREE_NAME", TypeName = "varchar(50)")]
    public string? LecturerDegreeName { get; set; }

    [Column("LECTURER_DEGREE_NAME_IN_KHMER", TypeName = "varchar(50)")]
    public string? LecturerDegreeNameInKhmer { get; set; }
}

[Table("LECTURER_FIELD")]
public class LecturerField
{
    [Key]
    [Column("LECTURER_FIELD_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LecturerFieldId { get; set; }

    [Column("NAME", TypeName = "varchar(50)")]
    public string? Name { get; set; }

    [Column("NAME_IN_KHMER", TypeName = "varchar(50)")]
    public string? NameInKhmer { get; set; }

    [Column("LECTURER_DEGREE_ID", TypeName = "int")]
    public int? LecturerDegreeId { get; set; }
}

[Table("LECTURER_SUBJECT")]
public class LecturerSubject
{
    [Column("LECTURER_ID", TypeName = "int")]
    public int LecturerId { get; set; }

    [Column("SUBJECT_ID", TypeName = "int")]
    public int SubjectId { get; set; }
}

[Table("LETTER")]
public class Letter
{
    [Key]
    [Column("LETTER_ID", TypeName = "int")]
    public int LetterId { get; set; }

    [Required]
    [Column("LETTER_NAME", TypeName = "nvarchar(40)")]
    public string? LetterName { get; set; }
}

[Table("LETTER_CATEGORY_TBL")]
public class LetterCategory
{
    [Key]
    [Column("categoryID", TypeName = "smallint")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }

    [Column("categoryName", TypeName = "nvarchar(50)")]
    public string? CategoryName { get; set; }

    [Column("unitPrice", TypeName = "float")]
    public float? UnitPrice { get; set; }

    [Column("active", TypeName = "bit")] 
    public bool? Active { get; set; }

    [Column("IsAdmin", TypeName = "bit")] 
    public bool? IsAdmin { get; set; }

    [Column("IsFoundation", TypeName = "bit")]
    public bool? IsFoundation { get; set; }

    [Column("IsShortCourse", TypeName = "bit")]
    public bool? IsShortCourse { get; set; }
    
    // [Column("IsStartNewNumber", TypeName = "bit")]
    [NotMapped]
    public bool? IsStartNewNumber { get; set; }
}

[Table("LETTER_CERTIFICATION_TBL")]
public class LetterCertification
{
    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("LetterNo", TypeName = "int")] public int? LetterNo { get; set; }

    [Column("YearNumber", TypeName = "varchar(10)")]
    public string? YearNumber { get; set; }

    [Column("certificateID", TypeName = "int")]
    public int? CertificateId { get; set; }

    [Column("issuedDate", TypeName = "datetime")]
    public DateTime? IssuedDate { get; set; }

    [Required]
    [Column("issuedStatus", TypeName = "bit")]
    public bool IssuedStatus { get; set; }

    [Column("stuID", TypeName = "nvarchar(50)")]
    public string? StuId { get; set; }

    [Column("nameInkh", TypeName = "nvarchar(50)")]
    public string? NameInKh { get; set; }

    [Column("nameInEng", TypeName = "nvarchar(40)")]
    public string? NameInEng { get; set; }

    [Column("sex", TypeName = "nvarchar(10)")]
    public string? Sex { get; set; }

    [Column("BirthDate", TypeName = "datetime")]
    public DateTime? BirthDate { get; set; }

    [Column("Degree", TypeName = "nvarchar(50)")]
    public string? Degree { get; set; }

    [Column("School", TypeName = "nvarchar(50)")]
    public string? School { get; set; }

    [Column("Field", TypeName = "nvarchar(50)")]
    public string? Field { get; set; }

    [Column("Promotion", TypeName = "nvarchar(50)")]
    public string? Promotion { get; set; }

    [Column("issuedNo", TypeName = "nvarchar(10)")]
    public string? IssuedNo { get; set; }

    [Column("receivedDate", TypeName = "datetime")]
    public DateTime? ReceivedDate { get; set; }

    [Column("amount", TypeName = "smallint")]
    public short? Amount { get; set; }

    [Column("categoryID", TypeName = "smallint")]
    public short? CategoryId { get; set; }

    [Column("other", TypeName = "nvarchar(60)")]
    public string? Other { get; set; }

    [Column("FoundationNo", TypeName = "int")]
    public int? FoundationNo { get; set; }

    [Column("FoundationYear", TypeName = "int")]
    public int? FoundationYear { get; set; }

    [Column("ShortCourseNo", TypeName = "int")]
    public int? ShortCourseNo { get; set; }

    [Column("ShortCourseYear", TypeName = "int")]
    public int? ShortCourseYear { get; set; }
}

[Table("MINIMUM_GPA")]
public class MinimumGpa
{
    [Column("GPA", TypeName = "float")] public double Gpa { get; set; }
}

[Table("NATIONALITY")]
public class Nationality
{
    [Column("NATIONALITY_ID", TypeName = "int")]
    public int NationalityId { get; set; }

    [Column("NATIONALITY", TypeName = "varchar(30)")]
    public string? NationalityName { get; set; }

    [Column("NATIONALITY_IN_KHMER", TypeName = "varchar(30)")]
    public string? NationalityInKhmer { get; set; }
}

[Table("NUMBER_OF_YEARS_STUDY")]
public class NumberOfYearsStudy
{
    [Column("NUMBER_OF_YEARS_STUDY_ID", TypeName = "int")]
    public int NumberOfYearsStudyId { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int DegreeId { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int SchoolId { get; set; }

    [Column("NUMBER_OF_YEARS", TypeName = "int")]
    public int NumberOfYears { get; set; }
}

[Table("OTHER_BRANCH_SCORE")]
public class OtherBranchScore
{
    [Column("OTHER_BRANCH_SCORE_ID", TypeName = "int")]
    public int OtherBranchScoreId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int TermNo { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int? CourseId { get; set; }

    [Column("COURSE_NAME", TypeName = "varchar(50)")]
    public string? CourseName { get; set; }

    [Column("COURSE_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? CourseNameInKhmer { get; set; }

    [Column("CREDIT", TypeName = "int")] public int Credit { get; set; }

    [Column("MID_TERM_SCORE", TypeName = "float")]
    public double? MidTermScore { get; set; }

    [Column("FINAL_SCORE", TypeName = "float")]
    public double? FinalScore { get; set; }

    [Column("YEAR_START", TypeName = "int")]
    public int? YearStart { get; set; }

    [Column("YEAR_END", TypeName = "int")] public int? YearEnd { get; set; }

    [Column("USERNAME", TypeName = "varchar(50)")]
    public string? Username { get; set; }

    [Column("DATE_EDIT", TypeName = "datetime")]
    public DateTime? DateEdit { get; set; }
}

[Table("OTHER_BRANCH_SCORE_UNICODE")]
public class OtherBranchScoreUnicode
{
    [Column("ID", TypeName = "int")] public int? Id { get; set; }

    [Column("NAME", TypeName = "nvarchar(100)")]
    public string? Name { get; set; }
}

[Table("PAYMENT")]
public class Payment
{
    [Column("PAYMENT_ID", TypeName = "int")]
    public int PaymentId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("INVOICE_NO", TypeName = "varchar(10)")]
    public string? InvoiceNo { get; set; }

    [Column("INVOICE_DATE", TypeName = "datetime")]
    public DateTime InvoiceDate { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int TermNo { get; set; }

    [Column("PAID", TypeName = "money")] public decimal Paid { get; set; }

    [Column("DEPOSIT", TypeName = "money")]
    public decimal Deposit { get; set; }

    [Column("NOTE", TypeName = "varchar(200)")]
    public string? Note { get; set; }

    [Column("IS_INSURANCE", TypeName = "bit")]
    public bool? IsInsurance { get; set; }

    [Column("GUARDIAN", TypeName = "varchar(50)")]
    public string? Guardian { get; set; }
}

[Table("PAYMENT_METHOD")]
public class PaymentMethod
{
    [Column("ID", TypeName = "int")] public int Id { get; set; }

    [Column("NAME", TypeName = "varchar(50)")]
    public string? Name { get; set; }

    [Column("NAME_KHMER", TypeName = "nvarchar(100)")]
    public string? NameKhmer { get; set; }
}

[Table("PAYMENT_TYPE")]
public class PaymentType
{
    [Column("PAYMENT_TYPE_ID", TypeName = "int")]
    public int PaymentTypeId { get; set; }

    [Column("PAYMENT_TYPE", TypeName = "nvarchar(50)")]
    public string? PaymentTypeName { get; set; }

    [Column("STATUS", TypeName = "bit")] public bool? Status { get; set; }
}

[Table("POSITION")]
public class Position
{
    [Column("POSITION", TypeName = "varchar(30)")]
    public string? PositionName { get; set; }
}

[Table("PRIVILEDGE")]
public class Privilege
{
    [Column("PRIVILEDGE_ID", TypeName = "int")]
    public int PrivilegeId { get; set; }

    [Column("PRIVILEDGE_NAME", TypeName = "varchar(60)")]
    public string? PrivilegeName { get; set; }

    [Column("PRIVILEDGE_GROUP_ID", TypeName = "int")]
    public int? PrivilegeGroupId { get; set; }
}

[Table("PRIVILEDGE_GROUP")]
public class PrivilegeGroup
{
    [Key]
    [Column("ID", TypeName = "int")]
    public int Id { get; set; }

    [Column("GROUP_NAME", TypeName = "varchar(50)")]
    public string? GroupName { get; set; }
}

[Table("PRODUCT_DETAIL")]
public class ProductDetail
{
    [Column("PRODUCT_DETAIL_ID", TypeName = "int")]
    public int ProductDetailId { get; set; }

    [Column("PRODUCT_ID", TypeName = "int")]
    public int? ProductId { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("FROM_PROMOTION_NO", TypeName = "int")]
    public int? FromPromotionNo { get; set; }
}

[Table("PRODUCT_TBL")]
public class Product
{
    [Column("PRODUCT_ID", TypeName = "int")]
    public int ProductId { get; set; }

    [Column("PRODUCT_NAME", TypeName = "varchar(50)")]
    public string? ProductName { get; set; }

    [Column("PRODUCT_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? ProductNameInKhmer { get; set; }

    [Column("DESCRIPTION", TypeName = "varchar(100)")]
    public string? Description { get; set; }

    [Column("VAT", TypeName = "int")] public int? Vat { get; set; }

    [Column("PRICE", TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }

    [Column("TYPE", TypeName = "varchar(50)")]
    public string? Type { get; set; }

    [Column("STATUS", TypeName = "nvarchar(10)")]
    public string? Status { get; set; }

    [Column("TuitionFees", TypeName = "bit")]
    public bool? TuitionFees { get; set; }

    [Column("DEGREEID", TypeName = "varchar(50)")]
    public string? DegreeId { get; set; }

    [Column("OrderID", TypeName = "int")] public int? OrderId { get; set; }

    [Column("CARD_CERTIFICATE", TypeName = "int")]
    public int? CardCertificate { get; set; }

    [Column("CATEGORY_ID", TypeName = "int")]
    public int? CategoryId { get; set; }

    [Column("PRICE_KHR", TypeName = "decimal(18,2)")]
    public decimal? PriceKhr { get; set; }

    [Column("PAYMENT_TYPE", TypeName = "bit")]
    public bool? PaymentType { get; set; }

    [Column("FROM_PROMOTION", TypeName = "int")]
    public int? FromPromotion { get; set; }

    [Column("TO_PROMOTION", TypeName = "int")]
    public int? ToPromotion { get; set; }

    [Column("HIDDEN", TypeName = "bit")] public bool? Hidden { get; set; }
}

[Table("PROMOTION")]
public class Promotion
{
    [Column("PROMOTION_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PromotionId { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int DegreeId { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int SchoolId { get; set; }

    [Column("PROMOTION_NO", TypeName = "int")]
    public int PromotionNo { get; set; }

    [Column("ACADEMIC_YEAR_START", TypeName = "int")]
    public int AcademicYearStart { get; set; }

    [Column("ACADEMIC_YEAR_END", TypeName = "int")]
    public int AcademicYearEnd { get; set; }

    [Column("STATUS", TypeName = "varchar(15)")]
    public string? Status { get; set; }

    [Column("GRADUATE_DATE1", TypeName = "date")]
    public DateTime? GraduateDate1 { get; set; }

    [Column("GRADUATE_DATE2", TypeName = "date")]
    public DateTime? GraduateDate2 { get; set; }
}

[Table("PROVINCE")]
public class Province
{
    [Column("PROVINCE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProvinceId { get; set; }

    [Column("PROVINCE", TypeName = "varchar(30)")]
    public string? ProvinceName { get; set; }

    [Column("PROVINCE_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? ProvinceInKhmer { get; set; }

    [Column("IS_CITY", TypeName = "int")] public int IsCity { get; set; }
}

[Table("QR_CODE_CERTIFICATE")]
public class QrCodeCertificate
{
    [Column("ID", TypeName = "varchar(100)")]
    public string? Id { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(50)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(100)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_KHMER", TypeName = "nvarchar(100)")]
    public string? StudentNameKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(10)")]
    public string? Sex { get; set; }

    [Column("DOB", TypeName = "varchar(100)")]
    public string? Dob { get; set; }

    [Column("DOB_KHMER", TypeName = "nvarchar(100)")]
    public string? DobKhmer { get; set; }

    [Column("STATUS", TypeName = "varchar(50)")]
    public string? Status { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [Column("DEGREE_NAME", TypeName = "varchar(200)")]
    public string? DegreeName { get; set; }

    [Column("DEGREE_NAME_KHMER", TypeName = "nvarchar(250)")]
    public string? DegreeNameKhmer { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("SCHOOL_NAME", TypeName = "varchar(100)")]
    public string? SchoolName { get; set; }

    [Column("SCHOOL_NAME_KHMER", TypeName = "nvarchar(50)")]
    public string? SchoolNameKhmer { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("FIELD_NAME", TypeName = "varchar(200)")]
    public string? FieldName { get; set; }

    [Column("FIELD_NAME_KHMER", TypeName = "nvarchar(250)")]
    public string? FieldNameKhmer { get; set; }

    [Column("TYPE", TypeName = "varchar(50)")]
    public string? Type { get; set; }

    [Column("PROMOTION_ID", TypeName = "int")]
    public int? PromotionId { get; set; }

    [Column("PROMOTION_NO", TypeName = "int")]
    public int? PromotionNo { get; set; }

    [Column("STAGE_NO", TypeName = "int")] public int? StageNo { get; set; }

    [Column("GROUP_NAME", TypeName = "varchar(50)")]
    public string? GroupName { get; set; }

    [Column("PHOTO", TypeName = "nvarchar(max)")]
    public string? Photo { get; set; }

    [Column("GRADUATE_DATE", TypeName = "varchar(100)")]
    public string? GraduateDate { get; set; }

    [Column("GRADUATE_DATE_KHMER", TypeName = "nvarchar(100)")]
    public string? GraduateDateKhmer { get; set; }

    [Column("URL", TypeName = "varchar(max)")]
    public string? Url { get; set; }

    [Column("DOCUMENT_KEY", TypeName = "varchar(max)")]
    public string? DocumentKey { get; set; }

    [Column("QRCODE_DATA", TypeName = "varchar(max)")]
    public string? QrCodeData { get; set; }

    [Column("CERTIFICATE_CODE", TypeName = "varchar(50)")]
    public string? CertificateCode { get; set; }

    [Column("LOCKED", TypeName = "bit")] public bool? Locked { get; set; }

    [Column("DATE", TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [Column("USER_ID", TypeName = "int")] public int? UserId { get; set; }
}

[Table("QR_CODE_CERTIFICATE_HISTORY")]
public class QrCodeCertificateHistory
{
    [Key]
    [Column("ID", TypeName = "varchar(100)")]
    public string? Id { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(50)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(100)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_KHMER", TypeName = "nvarchar(100)")]
    public string? StudentNameKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(10)")]
    public string? Sex { get; set; }

    [Column("DOB", TypeName = "varchar(100)")]
    public string? Dob { get; set; }

    [Column("DOB_KHMER", TypeName = "nvarchar(100)")]
    public string? DobKhmer { get; set; }

    [Column("STATUS", TypeName = "varchar(50)")]
    public string? Status { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [Column("DEGREE_NAME", TypeName = "varchar(200)")]
    public string? DegreeName { get; set; }

    [Column("DEGREE_NAME_KHMER", TypeName = "nvarchar(250)")]
    public string? DegreeNameKhmer { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("SCHOOL_NAME", TypeName = "varchar(100)")]
    public string? SchoolName { get; set; }

    [Column("SCHOOL_NAME_KHMER", TypeName = "nvarchar(50)")]
    public string? SchoolNameKhmer { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("FIELD_NAME", TypeName = "varchar(200)")]
    public string? FieldName { get; set; }

    [Column("FIELD_NAME_KHMER", TypeName = "nvarchar(250)")]
    public string? FieldNameKhmer { get; set; }

    [Column("TYPE", TypeName = "varchar(50)")]
    public string? Type { get; set; }

    [Column("PROMOTION_ID", TypeName = "int")]
    public int? PromotionId { get; set; }

    [Column("PROMOTION_NO", TypeName = "int")]
    public int? PromotionNo { get; set; }

    [Column("STAGE_NO", TypeName = "int")] public int? StageNo { get; set; }

    [Column("GROUP_NAME", TypeName = "varchar(50)")]
    public string? GroupName { get; set; }

    [Column("PHOTO", TypeName = "nvarchar(max)")]
    public string? Photo { get; set; }

    [Column("GRADUATE_DATE", TypeName = "varchar(100)")]
    public string? GraduateDate { get; set; }

    [Column("GRADUATE_DATE_KHMER", TypeName = "nvarchar(100)")]
    public string? GraduateDateKhmer { get; set; }

    [Column("URL", TypeName = "varchar(max)")]
    public string? Url { get; set; }

    [Column("DOCUMENT_KEY", TypeName = "varchar(max)")]
    public string? DocumentKey { get; set; }

    [Column("QRCODE_DATA", TypeName = "varchar(max)")]
    public string? QrCodeData { get; set; }

    [Column("CERTIFICATE_CODE", TypeName = "varchar(50)")]
    public string? CertificateCode { get; set; }

    [Column("LOCKED", TypeName = "bit")] public bool? Locked { get; set; }

    [Column("DATE", TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [Column("RESET_DATE", TypeName = "datetime")]
    public DateTime? ResetDate { get; set; }

    [Column("USER_ID", TypeName = "int")] public int? UserId { get; set; }

    [Column("USER_RESET", TypeName = "int")]
    public int? UserReset { get; set; }
}

[Table("QUESTIONABLE_STUDENT")]
public class QuestionableStudent
{
    [Column("QUESTIONABLE_STUDENT_ID", TypeName = "int")]
    public int QuestionableStudentId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(50)")]
    public string? StudentId { get; set; }

    [Column("NOTE", TypeName = "varchar(200)")]
    public string? Note { get; set; }
}

[Table("QUIT")]
public class Quit
{
    [Column("QUIT_ID", TypeName = "int")] public int QuitId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int TermNo { get; set; }

    [Column("QUIT_DATE", TypeName = "datetime")]
    public DateTime QuitDate { get; set; }

    [Column("REASON_OF_QUIT", TypeName = "nvarchar(100)")]
    public string? ReasonOfQuit { get; set; }

    [Column("GROUP_ID", TypeName = "int")] public int? GroupId { get; set; }

    [Column("PROMOTION_ID", TypeName = "int")]
    public int? PromotionId { get; set; }
}

[Table("RACE")]
public class Race
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("RACE_ID", TypeName = "int")] public int? RaceId { get; set; }

    [Column("RACE", TypeName = "varchar(30)")]
    public string? RaceName { get; set; }

    [Column("RACE_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? RaceInKhmer { get; set; }
}

[Table("REEXAM_DATE")]
public class ReexamDate
{
    [Column("REEXAM_DATE_ID", TypeName = "int")]
    public int? ReexamDateId { get; set; }

    [Column("COURSE_TERM_ID", TypeName = "int")]
    public int? CourseTermId { get; set; }

    [Column("TIME", TypeName = "int")] public int? Time { get; set; }

    [Column("DATE", TypeName = "datetime")]
    public DateTime? Date { get; set; }
}

[Table("REEXAM_SCORE")]
public class ReexamScore
{
    [Column("REEXAM_SCORE_ID", TypeName = "int")]
    public int? ReexamScoreId { get; set; }

    [Column("STUDENT_GROUP_ID", TypeName = "int")]
    public int? StudentGroupId { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int? CourseId { get; set; }

    [Column("TIME", TypeName = "int")] public int? Time { get; set; }

    [Column("SCORE", TypeName = "float")] public double? Score { get; set; }
}

[Table("REGISTRY")]
public class Registry
{
    [Key]
    [Column("REGISTRATION_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RegistrationId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("PROMOTION_NO", TypeName = "int")]
    public int PromotionNo { get; set; }

    [Column("STAGE_NO", TypeName = "int")] public int? StageNo { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("STUDY_TIME", TypeName = "varchar(15)")]
    public string? StudyTime { get; set; }

    [Column("REGISTRATION_DATE", TypeName = "datetime")]
    public DateTime? RegistrationDate { get; set; }

    [Column("DONE_DATE", TypeName = "datetime")]
    public DateTime? DoneDate { get; set; }

    [Column("HIGH_SCHOOL_RESULT", TypeName = "varchar(5)")]
    public string? HighSchoolResult { get; set; }

    [Column("HIGH_SCHOOL_TABLE_NO", TypeName = "int")]
    public int? HighSchoolTableNo { get; set; }

    [Column("UPDATE_BY", TypeName = "varchar(50)")]
    public string? UpdateBy { get; set; }

    [Column("UPDATE_DATE", TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }
}

[Table("REGISTRY_HISTORY")]
public class RegistryHistory
{
    [Column("REGISTRATION_ID", TypeName = "int")]
    public int? RegistrationId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("PROMOTION_NO", TypeName = "int")]
    public int? PromotionNo { get; set; }

    [Column("STAGE_NO", TypeName = "int")] public int? StageNo { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("STUDY_TIME", TypeName = "varchar(15)")]
    public string? StudyTime { get; set; }

    [Column("REGISTRATION_DATE", TypeName = "datetime")]
    public DateTime? RegistrationDate { get; set; }

    [Column("DONE_DATE", TypeName = "datetime")]
    public DateTime? DoneDate { get; set; }

    [Column("HIGH_SCHOOL_RESULT", TypeName = "varchar(5)")]
    public string? HighSchoolResult { get; set; }

    [Column("HIGH_SCHOOL_TABLE_NO", TypeName = "int")]
    public int? HighSchoolTableNo { get; set; }

    [Column("UPDATE_BY", TypeName = "varchar(50)")]
    public string? UpdateBy { get; set; }

    [Column("UPDATE_DATE", TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("DATE", TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [Column("BY", TypeName = "nvarchar(100)")]
    public string? By { get; set; }
}

[Table("REPORT_OF_STUDENT_TOTAL_SCORE")]
public class ReportOfStudentTotalScore
{
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(30)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "varchar(30)")]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(6)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("PHONE", TypeName = "varchar(24)")]
    public string? Phone { get; set; }

    [Column("TOTAL_SCORE", TypeName = "float")]
    public double? TotalScore { get; set; }
}

[Table("REPORT_PAGE_MARGIN")]
public class ReportPageMargin
{
    [Column("REPORT_PAGE_MARGIN_ID", TypeName = "int")]
    public int? ReportPageMarginId { get; set; }

    [Column("REPORT_NAME", TypeName = "varchar(80)")]
    public string? ReportName { get; set; }

    [Column("TOP", TypeName = "int")] public int? Top { get; set; }

    [Column("BOTTOM", TypeName = "int")] public int? Bottom { get; set; }

    [Column("LEFT", TypeName = "int")] public int? Left { get; set; }

    [Column("RIGHT", TypeName = "int")] public int? Right { get; set; }
}

[Table("ReportTempStudentFailStudy")]
public class ReportTempStudentFailStudy
{
    [Column("STUDENT_NAME", TypeName = "varchar(100)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(10)")]
    public string? Sex { get; set; }

    [Column("PHONE", TypeName = "nvarchar(50)")]
    public string? Phone { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "date")]
    public DateTime? DateOfBirth { get; set; }

    [Column("NATIONALITY", TypeName = "varchar(50)")]
    public string? Nationality { get; set; }

    [Column("NATIONALITY_IN_KHMER", TypeName = "nvarchar(50)")]
    public string? NationalityInKhmer { get; set; }

    [Column("PROVINCE", TypeName = "varchar(50)")]
    public string? Province { get; set; }

    [Column("PROVINCE_IN_KHMER", TypeName = "nvarchar(50)")]
    public string? ProvinceInKhmer { get; set; }

    [Column("SCHOOL_NAME", TypeName = "varchar(50)")]
    public string? SchoolName { get; set; }

    [Column("SCHOOL_NAME_IN_KHMER", TypeName = "nvarchar(50)")]
    public string? SchoolNameInKhmer { get; set; }

    [Column("STATUS", TypeName = "varchar(50)")]
    public string? Status { get; set; }

    [Column("DEGREE", TypeName = "varchar(50)")]
    public string? Degree { get; set; }

    [Column("COURSE_FULL_NAME", TypeName = "varchar(max)")]
    public string? CourseFullName { get; set; }

    [Column("COURSE_FULL_NAME_IN_KHMER", TypeName = "nvarchar(max)")]
    public string? CourseFullNameInKhmer { get; set; }

    [Column("CREDIT", TypeName = "float")] public double? Credit { get; set; }

    [Column("NUMBER_OF_HOURS", TypeName = "float")]
    public double? NumberOfHours { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("MID_TERM_SCORE", TypeName = "float")]
    public double? MidTermScore { get; set; }

    [Column("FINAL_SCORE", TypeName = "float")]
    public double? FinalScore { get; set; }

    [Column("TOTAL", TypeName = "float")] public double? Total { get; set; }

    [Column("PROMOTION_NO", TypeName = "int")]
    public int? PromotionNo { get; set; }

    [Column("STAGE_NO", TypeName = "int")] public int? StageNo { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(50)")]
    public string? StudentId { get; set; }
}

[Table("RESTUDY_TBL")]
public class RestudyTbl
{
    [Column("Restudy_ID", TypeName = "int")]
    public int? RestudyId { get; set; }

    [Column("Term_No", TypeName = "int")] public int? TermNo { get; set; }

    [Column("Course_ID", TypeName = "int")]
    public int? CourseId { get; set; }

    [Column("Course_Full_Name", TypeName = "varchar(50)")]
    public string? CourseFullName { get; set; }

    [Column("Replace_Course_ID", TypeName = "int")]
    public int? ReplaceCourseId { get; set; }

    [Column("Replace_Course_Full_Name", TypeName = "varchar(50)")]
    public string? ReplaceCourseFullName { get; set; }

    [Column("Note", TypeName = "nvarchar(200)")]
    public string? Note { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(50)")]
    public string? StudentId { get; set; }
}

[Table("RESUME")]
public class Resume
{
    [Column("RESUME_ID", TypeName = "int")]
    public int ResumeId { get; set; }

    [Column("DATE_PAYMENT", TypeName = "datetime")]
    public DateTime? DatePayment { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(15)")]
    public string? StudentId { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("F_PROMOTION", TypeName = "int")]
    public int? FPromotion { get; set; }

    [Column("F_YEAR", TypeName = "int")] public int? FYear { get; set; }

    [Column("F_SEMESTER", TypeName = "int")]
    public int? FSemester { get; set; }

    [Column("C_PROMOTION", TypeName = "int")]
    public int? CPromotion { get; set; }

    [Column("STAGE", TypeName = "char(10)")]
    public string? Stage { get; set; }

    [Column("C_YEAR", TypeName = "int")] public int? CYear { get; set; }

    [Column("C_SEMESTER", TypeName = "int")]
    public int? CSemester { get; set; }

    [Column("OTHER", TypeName = "varchar(250)")]
    public string? Other { get; set; }

    [Column("TYPE", TypeName = "varchar(30)")]
    public string? Type { get; set; }
}

[Table("ROOM")]
public class Room
{
    [Column("ROOM_ID", TypeName = "int")] public int? RoomId { get; set; }

    [Column("ROOM_NAME", TypeName = "varchar(15)")]
    public string? RoomName { get; set; }

    [Column("CAPACITY", TypeName = "int")] public int? Capacity { get; set; }

    [Column("ROOM_TYPE", TypeName = "varchar(15)")]
    public string? RoomType { get; set; }
}

[Table("SCHOOL")]
public class School
{
    [Column("SCHOOL_ID", TypeName = "int")]
    public int SchoolId { get; set; }

    [Column("SCHOOL_NAME", TypeName = "varchar(50)")]
    public string? SchoolName { get; set; }

    [Column("SCHOOL_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? SchoolNameInKhmer { get; set; }

    [Column("SCHOOL_CODE", TypeName = "varchar(10)")]
    public string? SchoolCode { get; set; }

    [Column("FACULTY_ID", TypeName = "numeric(28, 0)")]
    public decimal? FacultyId { get; set; }

    [Column("IS_FOUNDATION_SCHOOL", TypeName = "int")]
    public int? IsFoundationSchool { get; set; }
}

[Table("SCORE")]
public class Score
{
    [Column("SCORE_ID", TypeName = "int")] public int? ScoreId { get; set; }

    [Column("STUDENT_GROUP_ID", TypeName = "int")]
    public int? StudentGroupId { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int? CourseId { get; set; }

    [Column("MID_TERM_SCORE", TypeName = "float")]
    public double? MidTermScore { get; set; }

    [Column("FINAL_SCORE", TypeName = "float")]
    public double? FinalScore { get; set; }

    [Column("USERNAME", TypeName = "varchar(50)")]
    public string? Username { get; set; }

    [Column("DATE_EDIT", TypeName = "datetime")]
    public DateTime? DateEdit { get; set; }

    [Column("UPDATEBY", TypeName = "varchar(50)")]
    public string? UpdateBy { get; set; }

    [Column("UPDATEDATE", TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("IS_ALLOW", TypeName = "bit")] public bool? IsAllow { get; set; }
}

[Table("SCORE_HISTORY")]
public class ScoreHistory
{
    [Column("SCORE_HISTORY_ID", TypeName = "int")]
    public int? ScoreHistoryId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int? CourseId { get; set; }

    [Column("MID_TERM_SCORE", TypeName = "float")]
    public double? MidTermScore { get; set; }

    [Column("FINAL_SCORE", TypeName = "float")]
    public double? FinalScore { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("TIME", TypeName = "int")] public int? Time { get; set; }

    [Column("USERNAME", TypeName = "varchar(50)")]
    public string? Username { get; set; }

    [Column("DATE_EDIT", TypeName = "datetime")]
    public DateTime? DateEdit { get; set; }
}

[Table("SCORE_HISTORY_UPDATE")]
public class ScoreHistoryUpdate
{
    [Key]
    [Column("SCORE_ID", TypeName = "int")] 
    public int? ScoreId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(50)")]
    public string? StudentId { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int? CourseId { get; set; }

    [Column("MID_TERM_SCORE", TypeName = "float")]
    public double? MidTermScore { get; set; }

    [Column("FINAL_SCORE", TypeName = "float")]
    public double? FinalScore { get; set; }

    [Column("USERNAME", TypeName = "varchar(50)")]
    public string? Username { get; set; }

    [Column("DATE_EDIT", TypeName = "datetime")]
    public DateTime? DateEdit { get; set; }
}

[Table("SPO_REPORT_STUDENT_GROUP_STATISTIC")]
public class SpoReportStudentGroupStatistic
{
    [Column("Promotion_No", TypeName = "int")]
    public int? PromotionNo { get; set; }

    [Column("Stage_No", TypeName = "int")] public int? StageNo { get; set; }

    [Column("Term_ID", TypeName = "int")] public int? TermId { get; set; }

    [Column("Term_No", TypeName = "int")] public int? TermNo { get; set; }

    [Column("Start_Date", TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column("End_Date", TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Column("Academic_Year_Start", TypeName = "datetime")]
    public DateTime? AcademicYearStart { get; set; }

    [Column("Academic_Year_End", TypeName = "datetime")]
    public DateTime? AcademicYearEnd { get; set; }

    [Column("Group_ID", TypeName = "int")] public int? GroupId { get; set; }

    [Column("Group_Name", TypeName = "varchar(10)")]
    public string? GroupName { get; set; }

    [Column("School_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("School_Name", TypeName = "varchar(50)")]
    public string? SchoolName { get; set; }

    [Column("School_Name_In_Khmer", TypeName = "nvarchar(50)")]
    public string? SchoolNameInKhmer { get; set; }

    [Column("Field_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("Field_Name", TypeName = "varchar(80)")]
    public string? FieldName { get; set; }

    [Column("Degree_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [Column("Degree", TypeName = "varchar(30)")]
    public string? Degree { get; set; }

    [Column("Room_Name", TypeName = "varchar(15)")]
    public string? RoomName { get; set; }

    [Column("Total_Female", TypeName = "int")]
    public int? TotalFemale { get; set; }

    [Column("Total_Student", TypeName = "int")]
    public int? TotalStudent { get; set; }

    [Column("Table_Name", TypeName = "varchar(100)")]
    public string? TableName { get; set; }
}

[Table("SPONSOR")]
public class Sponsor
{
    [Key]
    [Column("SPONSOR_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? SponsorId { get; set; }

    [Column("SPONSOR_NAME", TypeName = "varchar(30)")]
    public string? SponsorName { get; set; }

    [Column("SPONSOR_NAME_IN_KHMER", TypeName = "varchar(30)")]
    public string? SponsorNameInKhmer { get; set; }

    [Column("POSITION", TypeName = "varchar(30)")]
    public string? Position { get; set; }

    [Column("NOTE", TypeName = "varchar(100)")]
    public string? Note { get; set; }
}

[Table("STAGE")]
public class Stage
{
    [Key]
    [Column("STAGE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StageId { get; set; }

    [Column("PROMOTION_ID", TypeName = "int")]
    public int PromotionId { get; set; }

    [Column("STAGE_NO", TypeName = "int")] public int StageNo { get; set; }

    [Column("STATUS", TypeName = "varchar(10)")]
    public string? Status { get; set; }

    [NotMapped] public string TableName => "STAGE";
}

[Table("START_PROMOTION")]
public class StartPromotion
{
    [Key]
    [Column("START_PROMOTION_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? StartPromotionId { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("PROMOTION_NO", TypeName = "int")]
    public int? PromotionNo { get; set; }

    [NotMapped] public string TableName => "START_PROMOTION";
}

[Table("STATEMENT")]
public class Statement
{
    [Key]
    [Column("STATEMENT_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? StatementId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("STATEMENT_DATE", TypeName = "datetime")]
    public DateTime? StatementDate { get; set; }

    [Column("DUE_DATE", TypeName = "datetime")]
    public DateTime? DueDate { get; set; }

    [Column("NOTE", TypeName = "varchar(50)")]
    public string? Note { get; set; }

    [NotMapped] public string TableName => "STATEMENT";
}

[Table("STUDENT")]
public class Student
{
    [Key]
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(30)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "nvarchar(30)")]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(6)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("PLACE_OF_BIRTH_ID", TypeName = "int")]
    public int? PlaceOfBirthId { get; set; }

    [Column("RACE_ID", TypeName = "int")] public int? RaceId { get; set; }

    [Column("NATIONALITY_ID", TypeName = "int")]
    public int? NationalityId { get; set; }

    [Column("MARITAL_STATUS", TypeName = "varchar(15)")]
    public string? MaritalStatus { get; set; }

    [Column("HIGH_SCHOOL_GRADUATED_YEAR", TypeName = "int")]
    public int? HighSchoolGraduatedYear { get; set; }

    [Column("FROM_PROVINCE_ID", TypeName = "int")]
    public int? FromProvinceId { get; set; }

    [Column("FROM_HIGH_SCHOOL_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? FromHighSchoolNameInKhmer { get; set; }

    [Column("JOB_ID", TypeName = "int")] public int? JobId { get; set; }

    [Column("MOTHER_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? MotherNameInKhmer { get; set; }

    [Column("MOTHER_OCCUPATION_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? MotherOccupationInKhmer { get; set; }

    [Column("FATHER_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? FatherNameInKhmer { get; set; }

    [Column("FATHER_OCCUPATION_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? FatherOccupationInKhmer { get; set; }

    [Column("PHONE", TypeName = "varchar(45)")]
    public string? Phone { get; set; }

    [Column("EMAIL", TypeName = "varchar(40)")]
    public string? Email { get; set; }

    [Column("ADDRESS", TypeName = "varchar(150)")]
    public string? Address { get; set; }

    [Column("ADDRESS_IN_KHMER", TypeName = "nvarchar(200)")]
    public string? AddressInKhmer { get; set; }

    [Column("CONTACT_PERSON_ID", TypeName = "int")]
    public int? ContactPersonId { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("IS_PHOTO_RECEIVED", TypeName = "int")]
    public int? IsPhotoReceived { get; set; }

    [Column("NOTE", TypeName = "nvarchar(600)")]
    public string? Note { get; set; }

    [Column("STATUS", TypeName = "varchar(15)")]
    public string? Status { get; set; }

    [Column("IS_CONTINUED_STUDENT", TypeName = "int")]
    public int? IsContinuedStudent { get; set; }

    [Column("ASSOCIATE_TO_BACHELOR", TypeName = "int")]
    public int? AssociateToBachelor { get; set; }

    [Column("APPROVED_DATE", TypeName = "text")]
    [StringLength(50)]
    public string? ApprovedDate { get; set; }

    [Column("GRADUATE_LETTER_NO", TypeName = "varchar(10)")]
    public string? GraduateLetterNo { get; set; }

    [Column("IS_ACCEPT_CERTIFICATE", TypeName = "bit")]
    public bool? IsAcceptCertificate { get; set; }

    [Column("ACCEPT_DATE", TypeName = "datetime")]
    public DateTime? AcceptDate { get; set; }

    [Column("CERTIFICATE_NO", TypeName = "varchar(10)")]
    public string? CertificateNo { get; set; }

    [Column("CERTIFICATE_OUT", TypeName = "bit")]
    public bool? CertificateOut { get; set; }

    [Column("PHOTO", TypeName = "image")] public byte[]? Photo { get; set; }

    [Column("CARD_IS_PRINT", TypeName = "bit")]
    public bool? CardIsPrint { get; set; }

    [Column("PRINT_DATE", TypeName = "datetime")]
    public DateTime? PrintDate { get; set; }

    [Column("FOUND_CERTIFICATE_IS_PRINT", TypeName = "bit")]
    public bool? FoundCertificateIsPrint { get; set; }

    [Column("CHECKCOMPLETE", TypeName = "bit")]
    public bool? CheckComplete { get; set; }

    [Column("CHECKCOMPLETENOTE", TypeName = "nvarchar(500)")]
    public string? CheckCompleteNote { get; set; }

    [Column("CHECKCOMPLETE_TERM", TypeName = "int")]
    public int? CheckCompleteTerm { get; set; }

    [Column("DISABILITYID", TypeName = "int")]
    public int? DisabilityId { get; set; }

    [Column("documentin", TypeName = "nvarchar(50)")]
    public string? DocumentIn { get; set; }

    [Column("documentout", TypeName = "nvarchar(50)")]
    public string? DocumentOut { get; set; }

    [Column("noteticket", TypeName = "nvarchar(200)")]
    public string? NoteTicket { get; set; }

    [Column("UPDATE_BY", TypeName = "varchar(50)")]
    public string? UpdateBy { get; set; }

    [Column("UPDATE_DATE", TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("IS_AUTHENTICATED", TypeName = "bit")]
    public bool? IsAuthenticated { get; set; }

    [Column("AUTHENTICATED_NO", TypeName = "varchar(50)")]
    public string? AuthenticatedNo { get; set; }

    [Column("URL", TypeName = "varchar(max)")]
    [StringLength(int.MaxValue)]
    public string? Url { get; set; }

    [Column("DOCUMENT_KEY", TypeName = "varchar(max)")]
    [StringLength(int.MaxValue)]
    public string? DocumentKey { get; set; }

    [Column("QRCODE_DATA", TypeName = "varchar(max)")]
    [StringLength(int.MaxValue)]
    public string? QrCodeData { get; set; }

    [Column("COUNT_PRINT", TypeName = "int")]
    public int? CountPrint { get; set; }

    [Column("IS_PRINT_CERTIFICATE", TypeName = "bit")]
    public bool? IsPrintCertificate { get; set; }

    [Column("IS_REQUEST", TypeName = "bit")]
    public bool? IsRequest { get; set; }

    [Column("GRADUATION_DATE", TypeName = "date")]
    public DateTime? GraduationDate { get; set; }

    [Column("CERTIFICATE_CODE", TypeName = "varchar(50)")]
    public string? CertificateCode { get; set; }

    [Column("IGNOR", TypeName = "bit")] public bool? Ignor { get; set; }

    [Column("IGNOR_REASON", TypeName = "nvarchar(100)")]
    public string? IgnorReason { get; set; }
    
    [Column("LOCKED", TypeName = "bit")] public bool? Locked { get; set; }
    
    // [Column("HIGHT_SCHOOL_TYPE_ID", TypeName = "int")]
    // public int? HightSchoolTypeId { get; set; }
}

[Table("STUDENT_ABSENT_RECORD")]
public class StudentAbsentRecord
{
    [Key]
    [Column("ABSENT_RECORD_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? AbsentRecordId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("MONTH_1", TypeName = "int")] public int? Month1 { get; set; }

    [Column("MONTH_2", TypeName = "int")] public int? Month2 { get; set; }

    [Column("MONTH_3", TypeName = "int")] public int? Month3 { get; set; }

    [Column("MONTH_4", TypeName = "int")] public int? Month4 { get; set; }

    [Column("MONTH_5", TypeName = "int")] public int? Month5 { get; set; }

    [NotMapped] public string TableName => "STUDENT_ABSENT_RECORD";
}

[Table("STUDENT_ABSENT_RECORD_NEW")]
public class StudentAbsentRecordNew
{
    [Key]
    [Column("ABSENT_RECORD_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? AbsentRecordId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("SUBJECT_01", TypeName = "int")]
    public int? Subject01 { get; set; }

    [Column("SUBJECT_02", TypeName = "int")]
    public int? Subject02 { get; set; }

    [Column("SUBJECT_03", TypeName = "int")]
    public int? Subject03 { get; set; }

    [Column("SUBJECT_04", TypeName = "int")]
    public int? Subject04 { get; set; }

    [Column("SUBJECT_05", TypeName = "int")]
    public int? Subject05 { get; set; }

    [Column("SUBJECT_06", TypeName = "int")]
    public int? Subject06 { get; set; }

    [Column("DATE_ABSENT", TypeName = "datetime")]
    public DateTime? DateAbsent { get; set; }

    [NotMapped] public string TableName => "STUDENT_ABSENT_RECORD_NEW";
}

[Table("STUDENT_CERTIFICATE")]
public class StudentCertificate
{
    [Key]
    [Column("STUDENT_CERTIFICATE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudentCertificateId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("CERTIFICATE_ID", TypeName = "int")]
    public int? CertificateId { get; set; }

    [Column("GRADE", TypeName = "nvarchar(20)")]
    public string? Grade { get; set; }

    [Column("IS_RECEIVED", TypeName = "int")]
    public int? IsReceived { get; set; }

    [Column("CERTIFICATE_ISSUE_NO", TypeName = "nvarchar(20)")]
    public string? CertificateIssueNo { get; set; }

    [NotMapped] public string TableName => "STUDENT_CERTIFICATE";
}

[Table("STUDENT_CERTIFICATE_RETURN")]
public class StudentCertificateReturn
{
    [Key]
    [Column("STUDENT_CERTIFICATE_RETURN_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? StudentCertificateReturnId { get; set; }

    [Column("RETURN_DATE", TypeName = "datetime")]
    public DateTime? ReturnDate { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("CERTIFICATE_ID", TypeName = "int")]
    public int? CertificateId { get; set; }

    [Column("RECIEVE_PICTURE", TypeName = "int")]
    public int? RecievePicture { get; set; }

    [Column("OTHER", TypeName = "varchar(50)")]
    public string? Other { get; set; }

    [NotMapped] public string TableName => "STUDENT_CERTIFICATE_RETURN";
}

[Table("STUDENT_COMPLEMENTAL_PAYMENT")]
public class StudentComplementalPayment
{
    [Key]
    [Column("STUDENT_COMPLEMENTAL_PAYMENT_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? StudentComplementPaymentId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("INVOICE_NO", TypeName = "varchar(10)")]
    public string? InvoiceNo { get; set; }

    [Column("INVOICE_DATE", TypeName = "datetime")]
    public DateTime? InvoiceDate { get; set; }

    [Column("SEMESTER", TypeName = "int")] public int? Semester { get; set; }

    [Column("PAID", TypeName = "money")] public decimal? Paid { get; set; }

    [Column("DEPOSIT", TypeName = "money")]
    public decimal? Deposit { get; set; }

    [Column("DISCOUNT", TypeName = "money")]
    public decimal? Discount { get; set; }

    [Column("REASON_OF_DISCOUNT", TypeName = "varchar(50)")]
    public string? ReasonOfDiscount { get; set; }

    [Column("NOTE", TypeName = "varchar(50)")]
    public string? Note { get; set; }

    [NotMapped] public string TableName => "STUDENT_COMPLEMENTAL_PAYMENT";
}

[Table("STUDENT_DISCOUNT")]
public class StudentDiscount
{
    [Key]
    [Column("STUDENT_DISCOUNT_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudentDiscountId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(50)")]
    public string? StudentId { get; set; }

    [Column("DISCOUNT", TypeName = "int")] public int? Discount { get; set; }

    [Column("TERM", TypeName = "int")] public int? Term { get; set; }

    [Column("NOTE", TypeName = "nvarchar(100)")]
    public string? Note { get; set; }

    [NotMapped] public string TableName => "STUDENT_DISCOUNT";
}

[Table("STUDENT_GROUP")]
public class StudentGroup
{
    [Key]
    [Column("STUDENT_GROUP_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? StudentGroupId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int TermNo { get; set; }

    [Column("GROUP_ID", TypeName = "int")] public int? GroupId { get; set; }

    [NotMapped] public string TableName => "STUDENT_GROUP";
}

[Table("STUDENT_GROUP_HISTORY")]
public class StudentGroupHistory
{
    [Key]
    [Column("ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }

    [Column("STUDENT_GROUP_ID", TypeName = "int")]
    public int? StudentGroupId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("GROUP_ID", TypeName = "int")] public int? GroupId { get; set; }

    [Column("CHANGE_DATE", TypeName = "datetime")]
    public DateTime? ChangeDate { get; set; }

    [Column("USERNAME", TypeName = "varchar(50)")]
    public string? Username { get; set; }

    [NotMapped] public string TableName => "STUDENT_GROUP_HISTORY";
}

[Table("STUDENT_HISTORY")]
public class StudentHistory
{
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(30)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "nvarchar(30)")]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(6)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("PLACE_OF_BIRTH_ID", TypeName = "int")]
    public int? PlaceOfBirthId { get; set; }

    [Column("RACE_ID", TypeName = "int")] public int? RaceId { get; set; }

    [Column("NATIONALITY_ID", TypeName = "int")]
    public int? NationalityId { get; set; }

    [Column("MARITAL_STATUS", TypeName = "varchar(15)")]
    public string? MaritalStatus { get; set; }

    [Column("HIGH_SCHOOL_GRADUATED_YEAR", TypeName = "int")]
    public int? HighSchoolGraduatedYear { get; set; }

    [Column("FROM_PROVINCE_ID", TypeName = "int")]
    public int? FromProvinceId { get; set; }

    [Column("FROM_HIGH_SCHOOL_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? FromHighSchoolNameInKhmer { get; set; }

    [Column("JOB_ID", TypeName = "int")] public int? JobId { get; set; }

    [Column("MOTHER_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? MotherNameInKhmer { get; set; }

    [Column("MOTHER_OCCUPATION_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? MotherOccupationInKhmer { get; set; }

    [Column("FATHER_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? FatherNameInKhmer { get; set; }

    [Column("FATHER_OCCUPATION_IN_KHMER", TypeName = "nvarchar(100)")]
    public string? FatherOccupationInKhmer { get; set; }

    [Column("PHONE", TypeName = "varchar(45)")]
    public string? Phone { get; set; }

    [Column("EMAIL", TypeName = "varchar(40)")]
    public string? Email { get; set; }

    [Column("ADDRESS", TypeName = "varchar(150)")]
    public string? Address { get; set; }

    [Column("ADDRESS_IN_KHMER", TypeName = "nvarchar(200)")]
    public string? AddressInKhmer { get; set; }

    [Column("CONTACT_PERSON_ID", TypeName = "int")]
    public int? ContactPersonId { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("IS_PHOTO_RECEIVED", TypeName = "int")]
    public int? IsPhotoReceived { get; set; }

    [Column("NOTE", TypeName = "nvarchar(600)")]
    public string? Note { get; set; }

    [Column("STATUS", TypeName = "varchar(15)")]
    public string? Status { get; set; }

    [Column("IS_CONTINUED_STUDENT", TypeName = "int")]
    public int? IsContinuedStudent { get; set; }

    [Column("ASSOCIATE_TO_BACHELOR", TypeName = "int")]
    public int? AssociateToBachelor { get; set; }

    [Column("APPROVED_DATE", TypeName = "text")]
    public string? ApprovedDate { get; set; }

    [Column("GRADUATE_LETTER_NO", TypeName = "varchar(10)")]
    public string? GraduateLetterNo { get; set; }

    [Column("IS_ACCEPT_CERTIFICATE", TypeName = "bit")]
    public bool? IsAcceptCertificate { get; set; }

    [Column("ACCEPT_DATE", TypeName = "datetime")]
    public DateTime? AcceptDate { get; set; }

    [Column("CERTIFICATE_NO", TypeName = "varchar(10)")]
    public string? CertificateNo { get; set; }

    [Column("CERTIFICATE_OUT", TypeName = "bit")]
    public bool? CertificateOut { get; set; }

    [Column("PHOTO", TypeName = "image")] public byte[]? Photo { get; set; }

    [Column("CARD_IS_PRINT", TypeName = "bit")]
    public bool? CardIsPrint { get; set; }

    [Column("PRINT_DATE", TypeName = "datetime")]
    public DateTime? PrintDate { get; set; }

    [Column("FOUND_CERTIFICATE_IS_PRINT", TypeName = "bit")]
    public bool? FoundCertificateIsPrint { get; set; }

    [Column("CHECKCOMPLETE", TypeName = "bit")]
    public bool? CheckComplete { get; set; }

    [Column("CHECKCOMPLETENOTE", TypeName = "nvarchar(500)")]
    public string? CheckCompleteNote { get; set; }

    [Column("CHECKCOMPLETE_TERM", TypeName = "int")]
    public int? CheckCompleteTerm { get; set; }

    [Column("DISABILITYID", TypeName = "int")]
    public int? DisabilityId { get; set; }

    [Column("documentin", TypeName = "nvarchar(50)")]
    public string? DocumentIn { get; set; }

    [Column("documentout", TypeName = "nvarchar(50)")]
    public string? DocumentOut { get; set; }

    [Column("noteticket", TypeName = "nvarchar(200)")]
    public string? NoteTicket { get; set; }

    [Column("UPDATE_BY", TypeName = "varchar(50)")]
    public string? UpdateBy { get; set; }

    [Column("UPDATE_DATE", TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("IS_AUTHENTICATED", TypeName = "bit")]
    public bool? IsAuthenticated { get; set; }

    [Column("AUTHENTICATED_NO", TypeName = "varchar(50)")]
    public string? AuthenticatedNo { get; set; }

    [Column("URL", TypeName = "varchar(max)")]
    public string? Url { get; set; }

    [Column("DOCUMENT_KEY", TypeName = "varchar(max)")]
    public string? DocumentKey { get; set; }

    [Column("QRCODE_DATA", TypeName = "varchar(max)")]
    public string? QrCodeData { get; set; }

    [Column("COUNT_PRINT", TypeName = "int")]
    public int? CountPrint { get; set; }

    [Column("IS_PRINT_CERTIFICATE", TypeName = "bit")]
    public bool? IsPrintCertificate { get; set; }

    [Column("IS_REQUEST", TypeName = "bit")]
    public bool? IsRequest { get; set; }

    [Column("GRADUATION_DATE", TypeName = "date")]
    public DateTime? GraduationDate { get; set; }

    [Column("CERTIFICATE_CODE", TypeName = "varchar(50)")]
    public string? CertificateCode { get; set; }

    [Column("IGNOR", TypeName = "bit")] public bool? Ignor { get; set; }

    [Column("IGNOR_REASON", TypeName = "nvarchar(100)")]
    public string? IgnorReason { get; set; }

    [Column("LOCKED", TypeName = "bit")] public bool? Locked { get; set; }

    [Column("HIGHT_SCHOOL_TYPE_ID", TypeName = "int")]
    public int? HightSchoolTypeId { get; set; }

    [Column("DATE", TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [Column("BY", TypeName = "nvarchar(100)")]
    public string? By { get; set; }

    [NotMapped] public string TableName => "STUDENT_HISTORY";
}

[Table("STUDENT_JOB")]
public class StudentJob
{
    [Key]
    [Column("JOB_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int JobId { get; set; }

    [Column("JOB", TypeName = "varchar(30)")]
    public string? JobName { get; set; }

    [Column("JOB_IN_KHMER", TypeName = "varchar(30)")]
    public string? JobNameKhmer { get; set; }

    [NotMapped] public string TableName => "STUDENT_JOB";
}

[Table("STUDENT_LETTER")]
public class StudentLetter
{
    [Key]
    [Column("STUDENT_LETTER_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? StudentLetterId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(50)")]
    public string? StudentId { get; set; }

    [Column("LETTER_ID", TypeName = "int")]
    public int? LetterId { get; set; }

    [Column("DONE_DATE_1", TypeName = "datetime")]
    public DateTime? DoneDate1 { get; set; }

    [Column("DONE_DATE_2", TypeName = "datetime")]
    public DateTime? DoneDate2 { get; set; }

    [Column("ISSUED_NO", TypeName = "varchar(10)")]
    public string? IssuedNo { get; set; }

    [Column("ISSUED_DATE", TypeName = "datetime")]
    public DateTime? IssuedDate { get; set; }

    [Column("AUTHOR", TypeName = "varchar(30)")]
    public string? Author { get; set; }

    [Column("RECEIVE_DATE", TypeName = "datetime")]
    public DateTime? ReceiveDate { get; set; }

    [NotMapped] public string TableName => "STUDENT_LETTER";
}

[Table("STUDENT_LIBRARY_ATTENDANT")]
public class StudentLibraryAttendant
{
    [Key]
    [Column("STUDENT_LIBRARY_ATTENDANT_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? StudentLibraryAttendantId { get; set; }

    [Column("CHECK_DATE", TypeName = "datetime")]
    public DateTime? CheckDate { get; set; }

    [Column("CHECK_TIME_IN", TypeName = "varchar(10)")]
    public string? CheckTimeIn { get; set; }

    [Column("CHECK_TIME_OUT", TypeName = "varchar(10)")]
    public string? CheckTimeOut { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("IS_OUT", TypeName = "int")] public int? IsOut { get; set; }

    [NotMapped] public string TableName => "STUDENT_LIBRARY_ATTENDANT";
}

[Table("STUDENT_ORIENTED_SUBJECT_PAYMENT")]
public class StudentOrientedSubjectPayment
{
    [Key]
    [Column("STUDENT_ORIENTED_SUBJECT_PAYMENT_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? StudentOrientedSubjectPaymentId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("INVOICE_NO", TypeName = "varchar(20)")]
    public string? InvoiceNo { get; set; }

    [Column("INVOICE_DATE", TypeName = "datetime")]
    public DateTime? InvoiceDate { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("PAID", TypeName = "float")] public double? Paid { get; set; }

    [Column("NOTE", TypeName = "varchar(100)")]
    public string? Note { get; set; }

    [NotMapped] public string TableName => "STUDENT_ORIENTED_SUBJECT_PAYMENT";
}

[Table("STUDENT_PROBLEM")]
public class StudentProblem
{
    [Column("STUDENTPROBLEMID", TypeName = "int")]
    public int? StudentProblemId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("PROMOTION_ID", TypeName = "int")]
    public int? PromotionId { get; set; }

    [Column("STAGE_ID", TypeName = "int")] public int? StageId { get; set; }

    [Column("TERM_ID", TypeName = "int")] public int? TermId { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("GROUP_ID", TypeName = "int")] public int? GroupId { get; set; }

    [Column("ACADEMIC_PROBLEM", TypeName = "nvarchar(200)")]
    public string? AcademicProblem { get; set; }

    [Column("FINANCE_PROBLEM", TypeName = "nvarchar(200)")]
    public string? FinanceProblem { get; set; }

    [NotMapped] public string TableName => "STUDENT_PROBLEM";
}

[Table("STUDENT_READY_MAIL")]
public class StudentReadyMail
{
    [Column("STUDENT_ID", TypeName = "nchar(10)")]
    public string? StudentId { get; set; }

    [NotMapped] public string TableName => "STUDENT_READY_MAIL";
}

[Table("STUDENT_REEXAM_PAYMENT")]
public class StudentReexamPayment
{
    [Column("STUDENT_REEXAM_PAYMENT_ID", TypeName = "int")]
    public int? StudentReExamPaymentId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("INVOICE_NO", TypeName = "varchar(20)")]
    public string? InvoiceNo { get; set; }

    [Column("INVOICE_DATE", TypeName = "datetime")]
    public DateTime? InvoiceDate { get; set; }

    [Column("PAID", TypeName = "float")] public double? Paid { get; set; }

    [Column("NOTE", TypeName = "varchar(100)")]
    public string? Note { get; set; }

    [NotMapped] public string TableName => "STUDENT_REEXAM_PAYMENT";
}

[Table("STUDENT_REEXAM_PAYMENT_DETAIL")]
public class StudentReexamPaymentDetail
{
    [Column("STUDENT_REEXAM_PAYMENT_DETAIL_ID", TypeName = "int")]
    public int? StudentReexamPaymentDetailId { get; set; }

    [Column("STUDENT_REEXAM_PAYMENT_ID", TypeName = "int")]
    public int? StudentReexamPaymentId { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int? CourseId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("TIME", TypeName = "varchar(10)")]
    public string? Time { get; set; }

    [NotMapped] public string TableName => "STUDENT_REEXAM_PAYMENT_DETAIL";
}

[Table("STUDENT_REEXAM_STATE_PAYMENT")]
public class StudentReexamStatePayment
{
    [Column("STUDENT_REEXAM_STATE_PAYMENT_ID", TypeName = "int")]
    public int? StudentReexamStatePaymentId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("INVOICE_NO", TypeName = "varchar(20)")]
    public string? InvoiceNo { get; set; }

    [Column("INVOICE_DATE", TypeName = "datetime")]
    public DateTime? InvoiceDate { get; set; }

    [Column("PAID", TypeName = "float")] public double? Paid { get; set; }

    [Column("NOTE", TypeName = "varchar(100)")]
    public string? Note { get; set; }

    [NotMapped] public string TableName => "STUDENT_REEXAM_STATE_PAYMENT";
}

[Table("STUDENT_SCHOOLARSHIP")]
public class StudentScholarship
{
    [Column("STUDENT_SCHOOLARSHIP_ID", TypeName = "int")]
    public int StudentScholarshipId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("IS_FULL_SCHOOLARSHIP", TypeName = "int")]
    public int? IsFullScholarship { get; set; }

    [Column("AMOUNT", TypeName = "int")] public int? Amount { get; set; }

    [Column("SPONSOR_ID", TypeName = "int")]
    public int? SponsorId { get; set; }

    [NotMapped] public string TableName => "STUDENT_SCHOOLARSHIP";
}

[Table("StudentStatisticByAcademicYear2Type1")]
public class StudentStatisticByAcademicYear2Type1
{
    [Column("Field_Id", TypeName = "int")] public int? FieldId { get; set; }

    [Column("Field_Name", TypeName = "varchar(50)")]
    public string? FieldName { get; set; }

    [Column("LessThan18Total", TypeName = "int")]
    public int? LessThan18Total { get; set; }

    [Column("LessThan18Female", TypeName = "int")]
    public int? LessThan18Female { get; set; }

    [Column("Total18", TypeName = "int")] public int? Total18 { get; set; }

    [Column("Female18", TypeName = "int")] public int? Female18 { get; set; }

    [Column("Total19", TypeName = "int")] public int? Total19 { get; set; }

    [Column("Female19", TypeName = "int")] public int? Female19 { get; set; }

    [Column("Total20", TypeName = "int")] public int? Total20 { get; set; }

    [Column("Female20", TypeName = "int")] public int? Female20 { get; set; }

    [Column("Total21", TypeName = "int")] public int? Total21 { get; set; }

    [Column("Female21", TypeName = "int")] public int? Female21 { get; set; }

    [Column("Total22", TypeName = "int")] public int? Total22 { get; set; }

    [Column("Female22", TypeName = "int")] public int? Female22 { get; set; }

    [Column("Total23", TypeName = "int")] public int? Total23 { get; set; }

    [Column("Female23", TypeName = "int")] public int? Female23 { get; set; }

    [Column("Total24", TypeName = "int")] public int? Total24 { get; set; }

    [Column("Female24", TypeName = "int")] public int? Female24 { get; set; }

    [Column("Total25", TypeName = "int")] public int? Total25 { get; set; }

    [Column("Female25", TypeName = "int")] public int? Female25 { get; set; }

    [Column("Total26", TypeName = "int")] public int? Total26 { get; set; }

    [Column("Female26", TypeName = "int")] public int? Female26 { get; set; }

    [Column("MoreThan26Total", TypeName = "int")]
    public int? MoreThan26Total { get; set; }

    [Column("MoreThan26Female", TypeName = "int")]
    public int? MoreThan26Female { get; set; }

    [NotMapped] public string TableName => "StudentStatisticByAcademicYear2Type1";
}

[Table("STUDY_TIME")]
public class StudyTime
{
    [Key]
    [Column("STUDY_TIME", TypeName = "varchar(15)")]
    public string? StudyTimeValue { get; set; }
}

[Table("StudentStatisticByAcademicYear2Type2")]
public class StudentStatisticByAcademicYear2Type2
{
    [Column("Province_Id", TypeName = "int")]
    public int? ProvinceId { get; set; }

    [Column("Province", TypeName = "varchar(30)")]
    public string? Province { get; set; }

    [Column("FoundationYearTotal", TypeName = "int")]
    public int? FoundationYearTotal { get; set; }

    [Column("FoundationYearFemale", TypeName = "int")]
    public int? FoundationYearFemale { get; set; }

    [Column("Year2Total", TypeName = "int")]
    public int? Year2Total { get; set; }

    [Column("Year2Female", TypeName = "int")]
    public int? Year2Female { get; set; }

    [Column("Year3Total", TypeName = "int")]
    public int? Year3Total { get; set; }

    [Column("Year3Female", TypeName = "int")]
    public int? Year3Female { get; set; }

    [Column("Year4Total", TypeName = "int")]
    public int? Year4Total { get; set; }

    [Column("Year4Female", TypeName = "int")]
    public int? Year4Female { get; set; }

    [Column("Year5Total", TypeName = "int")]
    public int? Year5Total { get; set; }

    [Column("Year5Female", TypeName = "int")]
    public int? Year5Female { get; set; }

    [Column("Year6Total", TypeName = "int")]
    public int? Year6Total { get; set; }

    [Column("Year6Female", TypeName = "int")]
    public int? Year6Female { get; set; }

    [Column("Year7Total", TypeName = "int")]
    public int? Year7Total { get; set; }

    [Column("Year7Female", TypeName = "int")]
    public int? Year7Female { get; set; }

    [NotMapped] public string TableName => "StudentStatisticByAcademicYear2Type2";
}

[Table("SUPPRESS")]
public class Suppress
{
    [Key]
    [Column("SUPPRESS_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SuppressId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("SUPPRESS_DATE", TypeName = "datetime")]
    public DateTime? SuppressDate { get; set; }

    [Column("EXPRESS_DATE", TypeName = "datetime")]
    public DateTime? ExpressDate { get; set; }

    [Column("REASON_OF_SUPPRESS", TypeName = "nvarchar(200)")]
    public string? ReasonOfSuppress { get; set; }

    [NotMapped] public string TableName => "SUPPRESS";
}

[Table("SUPPRESS_NEW")]
public class SuppressNew
{
    [Key]
    [Column("SUPPRESS_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? SuppressId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("SUPPRESS_DATE", TypeName = "datetime")]
    public DateTime? SuppressDate { get; set; }

    [Column("EXPRESS_DATE", TypeName = "datetime")]
    public DateTime? ExpressDate { get; set; }

    [Column("REASON_OF_SUPPRESS", TypeName = "varchar(50)")]
    public string? ReasonOfSuppress { get; set; }

    [NotMapped] public string TableName => "SUPPRESS_NEW";
}

[Table("SUSPEND")]
public class Suspend
{ 
    [Key]
    [Column("SUSPEND_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SuspendId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("GROUP_ID", TypeName = "int")] public int? GroupId { get; set; }

    [Column("PROMOTION_ID", TypeName = "int")]
    public int? PromotionId { get; set; }

    [Column("FROM_DATE", TypeName = "datetime")]
    public DateTime? FromDate { get; set; }

    [Column("TO_DATE", TypeName = "datetime")]
    public DateTime? ToDate { get; set; }

    [Column("REASON_OF_SUSPEND", TypeName = "nvarchar(100)")]
    public string? ReasonOfSuspend { get; set; }

    [NotMapped] public string TableName => "SUSPEND";
}

[Table("TERM")]
public class Term
{
    [Key]
    [Column("TERM_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TermId { get; set; }

    [Column("STAGE_ID", TypeName = "int")] public int StageId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int TermNo { get; set; }

    [Column("START_DATE", TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column("END_DATE", TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Column("ACADEMIC_YEAR_START", TypeName = "int")]
    public int? AcademicYearStart { get; set; }

    [Column("ACADEMIC_YEAR_END", TypeName = "int")]
    public int? AcademicYearEnd { get; set; }

    [Column("STATUS", TypeName = "varchar(10)")]
    public string? Status { get; set; }

    [Column("START_PAYMENT_DATE", TypeName = "date")]
    public DateTime? StartPaymentDate { get; set; }
}

[Table("TEST_SCORE")]
public class TestScore
{
    [Key]
    [Column("SCORE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? ScoreId { get; set; }

    [Column("STUDENT_GROUP_ID", TypeName = "int")]
    public int? StudentGroupId { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int? CourseId { get; set; }

    [Column("MID_TERM_SCORE", TypeName = "float")]
    public double? MidTermScore { get; set; }

    [Column("FINAL_SCORE", TypeName = "float")]
    public double? FinalScore { get; set; }

    [NotMapped] public string TableName => "TEST_SCORE";
}

[Table("TIME_TABLE")]
public class TimeTable
{
    [Key]
    [Column("TIME_TABLE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? TimeTableId { get; set; }

    [Column("GROUPING_DAY", TypeName = "varchar(30)")]
    public string? GroupingDay { get; set; }

    [Column("PART_OF_DAY", TypeName = "varchar(15)")]
    public string? PartOfDay { get; set; }

    [Column("TIME", TypeName = "varchar(15)")]
    public string? Time { get; set; }

    [NotMapped] public string TableName => "TIME_TABLE";
}

[Table("TUITION_FEE")]
public class TuitionFee
{
    [Key]
    [Column("TUITION_FEE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? TuitionFeeId { get; set; }

    [Column("PROMOTION_ID", TypeName = "int")]
    public int? PromotionId { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("FEE", TypeName = "money")] public decimal? Fee { get; set; }

    [NotMapped] public string TableName => "TUITION_FEE";
}

[Table("UNIVERSITY")]
public class University
{
    [Key]
    [Column("UNIVERSITY_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? UniversityId { get; set; }

    [Column("UNIVERSITY_NAME", TypeName = "varchar(70)")]
    public string? UniversityName { get; set; }

    [Column("UNIVERSITY_NAME_IN_KHMER", TypeName = "nvarchar(70)")]
    public string? UniversityNameInKhmer { get; set; }

    [Column("ABBREVIATION_NAME", TypeName = "varchar(10)")]
    public string? AbbreviationName { get; set; }

    [NotMapped] public string TableName => "UNIVERSITY";
}

[Table("USER")]
public class User
{
    [Key]
    [Column("USER_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Column("USER_NAME", TypeName = "varchar(30)")]
    public string? UserName { get; set; }

    [Column("PASSWORD", TypeName = "varchar(30)")]
    public string? Password { get; set; }

    [Column("USER_GROUP", TypeName = "varchar(50)")]
    public string? UserGroup { get; set; }

    [Column("STATUS", TypeName = "varchar(15)")]
    public string? Status { get; set; }
    [Column("PASSWORD_HASH",TypeName = "varchar(200)")] 
    public string? PasswordHash { get; set; }
    [Column("EMAIL",TypeName = "varchar(50)")] 
    public string? Email { get; set; }
    [Column("EMAIL_CONFIRM",TypeName = "bit")] 
    public bool? EmailConfirm { get; set; }
    [Column("PHONE_NUMBER",TypeName = "varchar(50)")] 
    public string? PhoneNumber { get; set; }
}

[Table("USER_PRIVILEDGE")]
public class UserPriviledge
{
    [Key]
    [Column("USER_PRIVILEDGE_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? UserPriviledgeId { get; set; }

    [Column("USER_ID", TypeName = "int")] public int? UserId { get; set; }

    [Column("PRIVILEDGE_ID", TypeName = "int")]
    public int? PriviledgeId { get; set; }

    [NotMapped] public string TableName => "USER_PRIVILEDGE";
}

[Table("USER_SCHOOL")]
public class UserSchool
{
    [Key]
    [Column("USER_SCHOOL_ID", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? UserSchoolId { get; set; }

    [Column("USER_ID", TypeName = "int")] public int? UserId { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [NotMapped] public string TableName => "USER_SCHOOL";
}

[Table("V_RE_EXAMINATION_RESULT")]
public class VReExaminationResult
{
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(50)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "nvarchar(50)")]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(10)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("COURSE_ID", TypeName = "int")]
    public int? CourseId { get; set; }

    [Column("COURSE_FULL_NAME", TypeName = "varchar(50)")]
    public string? CourseFullName { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("GROUP_ID", TypeName = "int")] public int? GroupId { get; set; }

    [Column("GROUP_NAME", TypeName = "varchar(10)")]
    public string? GroupName { get; set; }

    [Column("STUDY_TIME", TypeName = "varchar(20)")]
    public string? StudyTime { get; set; }

    [Column("STAGE_ID", TypeName = "int")] public int? StageId { get; set; }

    [Column("STAGE_NO", TypeName = "int")] public int? StageNo { get; set; }

    [Column("PROMOTION_ID", TypeName = "int")]
    public int? PromotionId { get; set; }

    [Column("PROMOTION_NO", TypeName = "int")]
    public int? PromotionNo { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("SCHOOL_NAME", TypeName = "varchar(50)")]
    public string? SchoolName { get; set; }

    [Column("SCHOOL_NAME_IN_KHMER", TypeName = "varchar(50)")]
    public string? SchoolNameInKhmer { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("FIELD_NAME", TypeName = "varchar(50)")]
    public string? FieldName { get; set; }

    [Column("FIELD_NAME_IN_KHMER", TypeName = "varchar(50)")]
    public string? FieldNameInKhmer { get; set; }

    [Column("MID_TERM_SCORE", TypeName = "float")]
    public double? MidTermScore { get; set; }

    [Column("FINAL_SCORE", TypeName = "float")]
    public double? FinalScore { get; set; }

    [Column("TOTAL_SCORE", TypeName = "float")]
    public double? TotalScore { get; set; }

    [NotMapped] public string TableName => "V_RE_EXAMINATION_RESULT";
}

[Table("V_REPORT_STUDENT_QUIT")]
public class VReportStudentQuit
{
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(30)")]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "varchar(30)")]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX", TypeName = "varchar(30)")]
    public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("STAGE_ID", TypeName = "int")] public int? StageId { get; set; }

    [Column("STAGE_NO", TypeName = "int")] public int? StageNo { get; set; }

    [Column("SCHOOL_ID", TypeName = "int")]
    public int? SchoolId { get; set; }

    [Column("SCHOOL_NAME", TypeName = "varchar(50)")]
    public string? SchoolName { get; set; }

    [Column("SCHOOL_NAME_IN_KHMER", TypeName = "varchar(50)")]
    public string? SchoolNameInKhmer { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("PROMOTION_ID", TypeName = "int")]
    public int? PromotionId { get; set; }

    [Column("PROMOTION_NO", TypeName = "int")]
    public int? PromotionNo { get; set; }

    [Column("GROUP_ID", TypeName = "int")] public int? GroupId { get; set; }

    [Column("GROUP_NAME", TypeName = "varchar(10)")]
    public string? GroupName { get; set; }

    [Column("QUIT_ID", TypeName = "int")] public int? QuitId { get; set; }

    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("FIELD_NAME", TypeName = "varchar(80)")]
    public string? FieldName { get; set; }

    [Column("FIELD_NAME_IN_KHMER", TypeName = "varchar(80)")]
    public string? FieldNameInKhmer { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [Column("DEGREE", TypeName = "varchar(30)")]
    public string? Degree { get; set; }

    [Column("DEGREE_IN_KHMER", TypeName = "varchar(30)")]
    public string? DegreeInKhmer { get; set; }

    [NotMapped] public string TableName => "V_REPORT_STUDENT_QUIT";
}

[Table("V_STUDENT_STATISTIC_BY_AGE")]
public class VStudentStatisticByAge
{
    [Column("AGE", TypeName = "varchar(100)")]
    public string? Age { get; set; }

    [Column("YEAR_1_PASS_TOTAL", TypeName = "int")]
    public int? Year1PassTotal { get; set; }

    [Column("YEAR_1_PASS_FEMALE", TypeName = "int")]
    public int? Year1PassFemale { get; set; }

    [Column("YEAR_1_FAIL_TOTAL", TypeName = "int")]
    public int? Year1FailTotal { get; set; }

    [Column("YEAR_1_FAIL_FEMAIL", TypeName = "int")]
    public int? Year1FailFemail { get; set; }

    [Column("YEAR_2_PASS_TOTAL", TypeName = "int")]
    public int? Year2PassTotal { get; set; }

    [Column("YEAR_2_PASS_FEMALE", TypeName = "int")]
    public int? Year2PassFemale { get; set; }

    [Column("YEAR_2_FAIL_TOTAL", TypeName = "int")]
    public int? Year2FailTotal { get; set; }

    [Column("YEAR_2_FAIL_FEMAIL", TypeName = "int")]
    public int? Year2FailFemail { get; set; }

    [Column("YEAR_3_PASS_TOTAL", TypeName = "int")]
    public int? Year3PassTotal { get; set; }

    [Column("YEAR_3_PASS_FEMALE", TypeName = "int")]
    public int? Year3PassFemale { get; set; }

    [Column("YEAR_3_FAIL_TOTAL", TypeName = "int")]
    public int? Year3FailTotal { get; set; }

    [Column("YEAR_3_FAIL_FEMAIL", TypeName = "int")]
    public int? Year3FailFemail { get; set; }

    [Column("YEAR_4_PASS_TOTAL", TypeName = "int")]
    public int? Year4PassTotal { get; set; }

    [Column("YEAR_4_PASS_FEMALE", TypeName = "int")]
    public int? Year4PassFemale { get; set; }

    [Column("YEAR_4_FAIL_TOTAL", TypeName = "int")]
    public int? Year4FailTotal { get; set; }

    [Column("YEAR_4_FAIL_FEMAIL", TypeName = "int")]
    public int? Year4FailFemail { get; set; }

    [Column("YEAR_5_PASS_TOTAL", TypeName = "int")]
    public int? Year5PassTotal { get; set; }

    [Column("YEAR_5_PASS_FEMALE", TypeName = "int")]
    public int? Year5PassFemale { get; set; }

    [Column("YEAR_5_FAIL_TOTAL", TypeName = "int")]
    public int? Year5FailTotal { get; set; }

    [Column("YEAR_5_FAIL_FEMAIL", TypeName = "int")]
    public int? Year5FailFemail { get; set; }

    [Column("TOTAL_PASS", TypeName = "int")]
    public int? TotalPass { get; set; }

    [Column("TOTAL_PASS_FEMALE", TypeName = "int")]
    public int? TotalPassFemale { get; set; }

    [Column("TOTAL_FAIL", TypeName = "int")]
    public int? TotalFail { get; set; }

    [Column("TOTAL_FAIL_FEMALE", TypeName = "int")]
    public int? TotalFailFemale { get; set; }

    [NotMapped] public string TableName => "V_STUDENT_STATISTIC_BY_AGE";
}

[Table("V_STUDENT_STATISTIC_BY_FIELD")]
public class VStudentStatisticByField
{
    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("FIELD_NAME", TypeName = "varchar(100)")]
    public string? FieldName { get; set; }

    [Column("FIELD_NAME_IN_KHMER", TypeName = "varchar(100)")]
    public string? FieldNameInKhmer { get; set; }

    [Column("YEAR_1_PASS_TOTAL", TypeName = "int")]
    public int? Year1PassTotal { get; set; }

    [Column("YEAR_1_PASS_FEMALE", TypeName = "int")]
    public int? Year1PassFemale { get; set; }

    [Column("YEAR_1_FAIL_TOTAL", TypeName = "int")]
    public int? Year1FailTotal { get; set; }

    [Column("YEAR_1_FAIL_FEMAIL", TypeName = "int")]
    public int? Year1FailFemail { get; set; }

    [Column("YEAR_2_PASS_TOTAL", TypeName = "int")]
    public int? Year2PassTotal { get; set; }

    [Column("YEAR_2_PASS_FEMALE", TypeName = "int")]
    public int? Year2PassFemale { get; set; }

    [Column("YEAR_2_FAIL_TOTAL", TypeName = "int")]
    public int? Year2FailTotal { get; set; }

    [Column("YEAR_2_FAIL_FEMAIL", TypeName = "int")]
    public int? Year2FailFemail { get; set; }

    [Column("YEAR_3_PASS_TOTAL", TypeName = "int")]
    public int? Year3PassTotal { get; set; }

    [Column("YEAR_3_PASS_FEMALE", TypeName = "int")]
    public int? Year3PassFemale { get; set; }

    [Column("YEAR_3_FAIL_TOTAL", TypeName = "int")]
    public int? Year3FailTotal { get; set; }

    [Column("YEAR_3_FAIL_FEMAIL", TypeName = "int")]
    public int? Year3FailFemail { get; set; }

    [Column("YEAR_4_PASS_TOTAL", TypeName = "int")]
    public int? Year4PassTotal { get; set; }

    [Column("YEAR_4_PASS_FEMALE", TypeName = "int")]
    public int? Year4PassFemale { get; set; }

    [Column("YEAR_4_FAIL_TOTAL", TypeName = "int")]
    public int? Year4FailTotal { get; set; }

    [Column("YEAR_4_FAIL_FEMAIL", TypeName = "int")]
    public int? Year4FailFemail { get; set; }

    [Column("YEAR_5_PASS_TOTAL", TypeName = "int")]
    public int? Year5PassTotal { get; set; }

    [Column("YEAR_5_PASS_FEMALE", TypeName = "int")]
    public int? Year5PassFemale { get; set; }

    [Column("YEAR_5_FAIL_TOTAL", TypeName = "int")]
    public int? Year5FailTotal { get; set; }

    [Column("YEAR_5_FAIL_FEMAIL", TypeName = "int")]
    public int? Year5FailFemail { get; set; }

    [Column("TOTAL_PASS", TypeName = "int")]
    public int? TotalPass { get; set; }

    [Column("TOTAL_PASS_FEMALE", TypeName = "int")]
    public int? TotalPassFemale { get; set; }

    [Column("TOTAL_FAIL", TypeName = "int")]
    public int? TotalFail { get; set; }

    [Column("TOTAL_FAIL_FEMALE", TypeName = "int")]
    public int? TotalFailFemale { get; set; }

    [NotMapped] public string TableName => "V_STUDENT_STATISTIC_BY_FIELD";
}

[Table("VERIFY_QR_CODE_AUTHORIZATION")]
public class VerifyQrCodeAuthorization
{
    [Column("ID", TypeName = "int")] public int? Id { get; set; }

    [Column("BASE_URL", TypeName = "varchar(100)")]
    public string? BaseUrl { get; set; }

    [Column("API_KEY_ASSOCIATE", TypeName = "varchar(100)")]
    public string? ApiKeyAssociate { get; set; }

    [Column("API_SECRET_ASSOCIATE", TypeName = "varchar(100)")]
    public string? ApiSecretAssociate { get; set; }

    [Column("API_KEY_BACHELOR", TypeName = "varchar(100)")]
    public string? ApiKeyBachelor { get; set; }

    [Column("API_SECRET_BACHELOR", TypeName = "varchar(100)")]
    public string? ApiSecretBachelor { get; set; }

    [Column("API_KEY_MASTER", TypeName = "varchar(100)")]
    public string? ApiKeyMaster { get; set; }

    [Column("API_SECRET_MASTER", TypeName = "varchar(100)")]
    public string? ApiSecretMaster { get; set; }

    [Column("API_KEY_DOCTOR", TypeName = "varchar(100)")]
    public string? ApiKeyDoctor { get; set; }

    [Column("API_SECRET_DOCTOR", TypeName = "varchar(100)")]
    public string? ApiSecretDoctor { get; set; }

    [Column("END_POINT_URL", TypeName = "varchar(100)")]
    public string? EndPointUrl { get; set; }

    [Column("TYPE", TypeName = "varchar(10)")]
    public string? Type { get; set; }

    [Column("ALLOW", TypeName = "bit")] public bool? Allow { get; set; }

    [NotMapped] public string TableName => "VERIFY_QR_CODE_AUTHORIZATION";
}

[Table("VIRTUAL_VIEW_STUDENT_STATISTIC_1")]
public class VirtualViewStudentStatistic1
{
    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("ACADEMIC_YEAR", TypeName = "varchar(9)")]
    public string? AcademicYear { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("YEAR", TypeName = "int")] public int? Year { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("SEX", TypeName = "varchar(6)")]
    public string? Sex { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(30)")]
    public string? StudentName { get; set; }

    [Column("STATUS", TypeName = "varchar(15)")]
    public string? Status { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }

    [NotMapped] public string TableName => "VIRTUAL_VIEW_STUDENT_STATISTIC_1";
}

[Table("VIRTUAL_VIEW_STUDENT_STATISTIC_2")]
public class VirtualViewStudentStatistic2
{
    [Column("FIELD_ID", TypeName = "int")] public int? FieldId { get; set; }

    [Column("ACADEMIC_YEAR", TypeName = "varchar(9)")]
    public string? AcademicYear { get; set; }

    [Column("TERM_NO", TypeName = "int")] public int? TermNo { get; set; }

    [Column("YEAR", TypeName = "int")] public int? Year { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Column("SEX", TypeName = "varchar(6)")]
    public string? Sex { get; set; }

    [Column("STUDENT_NAME", TypeName = "varchar(30)")]
    public string? StudentName { get; set; }

    [Column("STATUS", TypeName = "varchar(15)")]
    public string? Status { get; set; }

    [Column("DATE_OF_BIRTH", TypeName = "datetime")]
    public DateTime? DateOfBirth { get; set; }

    [Column("DEGREE_ID", TypeName = "int")]
    public int? DegreeId { get; set; }
}