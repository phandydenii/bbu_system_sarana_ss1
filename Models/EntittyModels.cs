using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBU_SYSTEM.Modelsss;

[Table("ABSENCE")]
public class Absence
{
    [Key] [Column("ABSENCE_ID")] public int AbsenceId { get; set; }

    [Required] [Column("INSTRUCTOR_ID")] public int InstructorId { get; set; }

    [Required] [Column("ABSENCE_DATE")] public DateTime AbsenceDate { get; set; }

    [Required]
    [Column("ABSENCE_TIME")]
    [StringLength(15)]
    public string? AbsenceTime { get; set; }

    [Column("REASON")] [StringLength(30)] public string? Reason { get; set; }
}

[Table("ABSENT_TBL")]
public class Absent
{
    [Key] [Column("ABSENT_ID")] public int AbsentId { get; set; }

    [Column("ABSENT_DATE")] public DateTime? AbsentDate { get; set; }

    [Column("DEGREE_ID")] public int? DegreeId { get; set; }

    [Column("SCHOOL_ID")] public int? SchoolId { get; set; }

    [Column("PROMOTION_ID")] public int? PromotionId { get; set; }

    [Column("STAGE_ID")] public int? StageId { get; set; }

    [Column("TERM_ID")] public int? TermId { get; set; }

    [Column("FIELD_ID")] public int? FieldId { get; set; }

    [Column("GROUP_ID")] public int? GroupId { get; set; }
}

[Table("ABSENTCOURSE_TBL")]
public class AbsentCourse
{
    [Key] [Column("ABSENTCOURSE_ID")] public int AbsentCourseId { get; set; }

    [Column("ABSENTDETAIL_ID")] public int? AbsentDetailId { get; set; }

    [Column("ABSENTLETTER_ID")] public int? AbsentLetterId { get; set; }

    [Column("COURSE_ID")] public int? CourseId { get; set; }

    [Column("ABSENT_COURSE_DATE")] public DateTime? AbsentCourseDate { get; set; }
}

[Table("ABSENTDETAIL_TBL")]
public class AbsentDetail
{
    [Key] [Column("ABSENTDETAIL_ID")] public int AbsentDetailId { get; set; }

    [Column("ABSENT_ID")] public int? AbsentId { get; set; }

    [Column("STUDENT_ID")]
    [StringLength(50)]
    public string? StudentId { get; set; }
}

[Table("ABSENTLETTER_TBL")]
public class AbsentLetter
{
    [Key] [Column("ABSENTLETTER_ID")] public int AbsentLetterId { get; set; }

    [Column("LETTER")] [StringLength(50)] public string? Letter { get; set; }

    [Column("LETTERVALUE", TypeName = "decimal(18,2)")]
    public decimal? LetterValue { get; set; }
}

[Table("ACADEMIC_REPORT_CON_EDU_ASSOCIATE_TO_BACHELOR")]
public class AcademicReportConEduAssociateToBachelor
{
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME")]
    [StringLength(50)]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER")]
    [StringLength(50)]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX")] [StringLength(10)] public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH")] public DateTime? DateOfBirth { get; set; }

    [Column("SCHOOL_NAME")]
    [StringLength(50)]
    public string? SchoolName { get; set; }

    [Column("FIELD_ID")] public int? FieldId { get; set; }

    [Column("FIELD_NAME_IN_KHMER")]
    [StringLength(50)]
    public string? FieldNameInKhmer { get; set; }

    [Column("SCHOOL_ID")] public int? SchoolId { get; set; }

    [Column("SCHOOL_NAME_IN_KHMER")]
    [StringLength(50)]
    public string? SchoolNameInKhmer { get; set; }

    [Column("PROMOTION_NO")] public int? PromotionNo { get; set; }

    [Column("FIELD_NAME")]
    [StringLength(50)]
    public string? FieldName { get; set; }

    [Column("TERM_NO")] public int? TermNo { get; set; }

    [Column("CREATE_IN_TERM_NO")] public int? CreateInTermNo { get; set; }
}

[Table("ACADEMIC_REPORT_CON_EDU_ASSOCIATE_TO_BACHELOR_TEMP")]
public class AcademicReportConEduAssociateToBachelorTemp
{
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME")]
    [StringLength(50)]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER")]
    [StringLength(50)]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX")] [StringLength(10)] public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH")] public DateTime? DateOfBirth { get; set; }

    [Column("SCHOOL_NAME")]
    [StringLength(50)]
    public string? SchoolName { get; set; }

    [Column("FIELD_ID")] public int? FieldId { get; set; }

    [Column("FIELD_NAME_IN_KHMER")]
    [StringLength(50)]
    public string? FieldNameInKhmer { get; set; }

    [Column("SCHOOL_ID")] public int? SchoolId { get; set; }

    [Column("SCHOOL_NAME_IN_KHMER")]
    [StringLength(50)]
    public string? SchoolNameInKhmer { get; set; }

    [Column("PROMOTION_NO")] public int? PromotionNo { get; set; }

    [Column("FIELD_NAME")]
    [StringLength(50)]
    public string? FieldName { get; set; }

    [Column("TERM_NO")] public int? TermNo { get; set; }

    [Column("CREATED_IN_TERM_NO")] public int? CreatedInTermNo { get; set; }
}

[Table("ACADEMIC_REPORT_EXAMINATION_RESULT")]
public class AcademicReportExaminationResult
{
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME")]
    [StringLength(30)]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER")]
    [StringLength(30)]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX")] [StringLength(6)] public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH")] public DateTime? DateOfBirth { get; set; }

    [Column("MID1")] public double? Mid1 { get; set; }

    [Column("FINAL1")] public double? Final1 { get; set; }

    [Column("TOTAL1")] public double? Total1 { get; set; }

    [Column("MID2")] public double? Mid2 { get; set; }

    [Column("FINAL2")] public double? Final2 { get; set; }

    [Column("TOTAL2")] public double? Total2 { get; set; }

    [Column("MID3")] public double? Mid3 { get; set; }

    [Column("FINAL3")] public double? Final3 { get; set; }

    [Column("TOTAL3")] public double? Total3 { get; set; }

    [Column("MID4")] public double? Mid4 { get; set; }

    [Column("FINAL4")] public double? Final4 { get; set; }

    [Column("TOTAL4")] public double? Total4 { get; set; }

    [Column("MID5")] public double? Mid5 { get; set; }

    [Column("FINAL5")] public double? Final5 { get; set; }

    [Column("TOTAL5")] public double? Total5 { get; set; }

    [Column("MID6")] public double? Mid6 { get; set; }

    [Column("FINAL6")] public double? Final6 { get; set; }

    [Column("TOTAL6")] public double? Total6 { get; set; }

    [Column("MID7")] public double? Mid7 { get; set; }

    [Column("FINAL7")] public double? Final7 { get; set; }

    [Column("TOTAL7")] public double? Total7 { get; set; }
}

[Table("ACADEMIC_REPORT_RE_STUDY_STUDENT")]
public class AcademicReportReStudyStudent
{
    [Column("STUDENT_ID")]
    [Required]
    [StringLength(50)]
    public string? StudentId { get; set; } = null!;

    [Column("COURSE_ID")] [Required] public int CourseId { get; set; }
}

[Table("ACADEMIC_REPORT_REEXAM_STUDENT")]
public class AcademicReportReexamStudent
{
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME")]
    [StringLength(50)]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER")]
    [StringLength(50)]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX")] [StringLength(6)] public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH")] public DateTime? DateOfBirth { get; set; }

    [Column("GROUP_NAME")]
    [StringLength(10)]
    public string? GroupName { get; set; }

    [Column("TOTAL1")] public double? Total1 { get; set; }

    [Column("TOTAL2")] public double? Total2 { get; set; }

    [Column("TOTAL3")] public double? Total3 { get; set; }

    [Column("TOTAL4")] public double? Total4 { get; set; }

    [Column("TOTAL5")] public double? Total5 { get; set; }

    [Column("TOTAL6")] public double? Total6 { get; set; }
}

[Table("ACADEMIC_REPORT_STATE_EXAMINATION_RESULT")]
public class AcademicReportStateExaminationResult
{
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME")]
    [StringLength(30)]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER")]
    [StringLength(30)]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX")] [StringLength(6)] public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH")] public DateTime? DateOfBirth { get; set; }

    [Column("SCORE1")] public double? Score1 { get; set; }

    [Column("SCORE2")] public double? Score2 { get; set; }

    [Column("SCORE3")] public double? Score3 { get; set; }

    [Column("SCORE4")] public double? Score4 { get; set; }

    [Column("SCORE5")] public double? Score5 { get; set; }

    [Column("SCORE6")] public double? Score6 { get; set; }
}

[Table("ADMIN_REPORT_STATISTIC_BY_PROVINCE")]
public class AdminReportStatisticByProvince
{
    [Column("FROM_PROVINCE_ID")] public int? FromProvinceId { get; set; }

    [Column("PROVINCE")]
    [StringLength(30)]
    public string? Province { get; set; }

    [Column("STUDY_TIME1")] public int? StudyTime1 { get; set; }

    [Column("STUDY_TIME2")] public int? StudyTime2 { get; set; }

    [Column("STUDY_TIME3")] public int? StudyTime3 { get; set; }

    [Column("STUDY_TIME4")] public int? StudyTime4 { get; set; }

    [Column("TOTAL_FEMALE")] public int? TotalFemale { get; set; }
}

[Table("ADMIN_SCORE_SHEET")]
public class AdminScoreSheet
{
    [Column("PROMOTION_NO")] [Required] public int PromotionNo { get; set; }

    [Column("STAGE_NO")] [Required] public int StageNo { get; set; }

    [Column("SCHOOL_ID")] [Required] public int SchoolId { get; set; }

    [Column("SCHOOL_NAME")]
    [Required]
    [StringLength(50)]
    public string? SchoolName { get; set; } = null!;

    [Column("SCHOOL_NAME_IN_KHMER")]
    [StringLength(100)]
    public string? SchoolNameInKhmer { get; set; }

    [Column("FIELD_ID")] [Required] public int FieldId { get; set; }

    [Column("FIELD_NAME")]
    [Required]
    [StringLength(100)]
    public string? FieldName { get; set; } = null!;

    [Column("FIELD_NAME_IN_KHMER")]
    [StringLength(100)]
    public string? FieldNameInKhmer { get; set; }

    [Column("TERM_ID")] [Required] public int TermId { get; set; }

    [Column("TERM_NO")] [Required] public int TermNo { get; set; }

    [Column("GROUP_ID")] [Required] public int GroupId { get; set; }

    [Column("GROUP_NAME")]
    [Required]
    [StringLength(30)]
    public string? GroupName { get; set; } = null!;

    [Column("STUDENT_ID")]
    [Required]
    [StringLength(10)]
    public string? StudentId { get; set; } = null!;

    [Column("STUDENT_NAME")]
    [Required]
    [StringLength(30)]
    public string? StudentName { get; set; } = null!;

    [Column("STUDENT_NAME_IN_KHMER")]
    [StringLength(30)]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX")]
    [Required]
    [StringLength(6)]
    public string? Sex { get; set; } = null!;

    [Column("DATE_OF_BIRTH")] [Required] public DateTime DateOfBirth { get; set; }

    [Column("PHONE")] [StringLength(50)] public string? Phone { get; set; }

    [Column("ROOM_NAME")]
    [StringLength(15)]
    public string? RoomName { get; set; }

    [Column("IS_PHOTO_RECEIVED")] public int? IsPhotoReceived { get; set; }
}

[Table("APPCBank_StudentID")]
public class AppcBankStudentId
{
    [Key]
    [Column("STUDENT_ID")]
    [Required]
    [StringLength(50)]
    public string? StudentId { get; set; } = null!;
}

[Table("AVAILABLE_TIME")]
public class AvailableTime
{
    [Key]
    [Column("AVAILABLE_TIME_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AvailableTimeId { get; set; }

    [Column("INSTRUCTOR_ID")] [Required] public int InstructorId { get; set; }

    [Column("DAY_OF_WEEK")]
    [Required]
    [StringLength(10)]
    public string? DayOfWeek { get; set; } = null!;

    [Column("TIME")]
    [Required]
    [StringLength(15)]
    public string? Time { get; set; } = null!;
}

[Table("BOOK_CLOTHES")]
public class BookClothes
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Column("IS_DEPOSIT")] public bool? IsDeposit { get; set; }

    [Column("IS_RETURN")] public bool? IsReturn { get; set; }

    [Column("INVOICE_NO")]
    [StringLength(10)]
    public string? InvoiceNo { get; set; }

    [Column("CONTACT_NUMBER")]
    [StringLength(30)]
    public string? ContactNumber { get; set; }

    [Column("NOTE")] [StringLength(200)] public string? Note { get; set; }
}

[Table("BOOKING_TBL")]
public class Booking
{
    [Key]
    [Column("BOOKINGID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookingId { get; set; }

    [Column("BOOKINGDATE")] public DateTime? BookingDate { get; set; }

    [Column("USERID")] public int? UserId { get; set; }

    [Column("STUDENTID")]
    [StringLength(50)]
    public string? StudentId { get; set; }

    [Column("EXCHANGEID", TypeName = "decimal(18,2)")]
    public decimal? ExchangeId { get; set; }

    [Column("TOTAL", TypeName = "decimal(18,6)")]
    public decimal? Total { get; set; }

    [Column("VAT")] public int? Vat { get; set; }

    [Column("DISCOUNT", TypeName = "decimal(18,6)")]
    public decimal? Discount { get; set; }

    [Column("PAYDOLLAR", TypeName = "decimal(18,6)")]
    public decimal? PayDollar { get; set; }

    [Column("PAYRIEAL", TypeName = "decimal(18,6)")]
    public decimal? PayRieal { get; set; }

    [Column("NOTE")] [StringLength(600)] public string? Note { get; set; }

    [Column("ACTIVE")] public bool? Active { get; set; }

    [Column("DEGREE")] [StringLength(50)] public string? Degree { get; set; }

    [Column("SCHOOL_ID")] public int? SchoolId { get; set; }

    [Column("FIELD_ID")] public int? FieldId { get; set; }

    [Column("PROMOTION_NO")] public int? PromotionNo { get; set; }

    [Column("STAGE_NO")] public int? StageNo { get; set; }

    [Column("GROUP_ID")] public int? GroupId { get; set; }

    [Column("TERM_NO")] public int? TermNo { get; set; }

    [Column("FROM_DATE")] public DateTime? FromDate { get; set; }

    [Column("TO_DATE")] public DateTime? ToDate { get; set; }

    [Column("STUDYTIME")]
    [StringLength(50)]
    public string? StudyTime { get; set; }

    [Column("UPDATE_BY")]
    [StringLength(50)]
    public string? UpdateBy { get; set; }

    [Column("UPDATE_DATE")] public DateTime? UpdateDate { get; set; }

    [Column("RETURN_ALREADY")] public bool? ReturnAlready { get; set; }

    [Column("RETURN_RATE_IN", TypeName = "decimal(18,6)")]
    public decimal? ReturnRateIn { get; set; }

    [Column("RETURN_DATE")] public DateTime? ReturnDate { get; set; }

    [Column("RETURN_AMOUNT", TypeName = "decimal(18,6)")]
    public decimal? ReturnAmount { get; set; }

    [Column("RETURN_DOLLAR", TypeName = "decimal(18,6)")]
    public decimal? ReturnDollar { get; set; }

    [Column("RETURN_RIEL", TypeName = "decimal(18,6)")]
    public decimal? ReturnRiel { get; set; }

    [Column("BOOKING_NO")] public int? BookingNo { get; set; }

    [Column("YEAR_NUMBER")]
    [StringLength(10)]
    public string? YearNumber { get; set; }
}

[Table("BOOKINGDETAIL_TBL")]
public class BookingDetail
{
    [Key]
    [Column("BOOKINGDETAILID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookingDetailId { get; set; }

    [Column("BOOKINGID")] public int? BookingId { get; set; }

    [Column("CLOTHID")] public int? ClothId { get; set; }

    [Column("QTY", TypeName = "decimal(18,3)")]
    public decimal? Qty { get; set; }

    [Column("PRICE", TypeName = "decimal(18,3)")]
    public decimal? Price { get; set; }
}

[Table("BOOKINGITEM_TBL")]
public class BookingItem
{
    [Key]
    [Column("BOOKINGITEMID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookingItemId { get; set; }

    [Column("ITEMNAME")]
    [StringLength(100)]
    public string? ItemName { get; set; }

    [Column("ITEMNAMEKHMER")]
    [StringLength(150)]
    public string? ItemNameKhmer { get; set; }

    [Column("PRICE", TypeName = "decimal(18,6)")]
    public decimal? Price { get; set; }
}

[Table("BOOKINGRETURN_TBL")]
public class BookingReturn
{
    [Key]
    [Column("BOOKINGRETURN_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookingReturnId { get; set; }

    [Column("BOOKINGID")] public int? BookingId { get; set; }

    [Column("RETURN_DATE")] public DateTime? ReturnDate { get; set; }

    [Column("BOOKINGRETURN_NO")] public int? BookingReturnNo { get; set; }

    [Column("YEAR_NUMBER")]
    [StringLength(10)]
    public string? YearNumber { get; set; }

    [Column("USERID")] public int? UserId { get; set; }

    [Column("EXCHANGEID", TypeName = "decimal(18,2)")]
    public decimal? ExchangeId { get; set; }

    [Column("STUDENT_ID")]
    [StringLength(50)]
    public string? StudentId { get; set; }

    [Column("DEGREE")] [StringLength(50)] public string? Degree { get; set; }

    [Column("SCHOOL_ID")] public int? SchoolId { get; set; }

    [Column("FIELD_ID")] public int? FieldId { get; set; }

    [Column("PROMOTION_NO")] public int? PromotionNo { get; set; }

    [Column("STAGE_NO")] public int? StageNo { get; set; }

    [Column("GROUP_NO")] public int? GroupNo { get; set; }

    [Column("TERM_NO")] public int? TermNo { get; set; }

    [Column("STUDY_TIME")]
    [StringLength(50)]
    public string? StudyTime { get; set; }

    [Column("FROM_DATE")] public DateTime? FromDate { get; set; }

    [Column("TO_DATE")] public DateTime? ToDate { get; set; }

    [Column("UPDATE_BY")]
    [StringLength(50)]
    public string? UpdateBy { get; set; }

    [Column("UPDATE_DATE")] public DateTime? UpdateDate { get; set; }

    [Column("RETURN_RATE_IN", TypeName = "decimal(18,6)")]
    public decimal? ReturnRateIn { get; set; }

    [Column("RETURN_AMOUNT", TypeName = "decimal(18,6)")]
    public decimal? ReturnAmount { get; set; }

    [Column("VAT")] public int? Vat { get; set; }

    [Column("DISCOUNT", TypeName = "decimal(18,6)")]
    public decimal? Discount { get; set; }

    [Column("RETURN_DOLLAR", TypeName = "decimal(18,6)")]
    public decimal? ReturnDollar { get; set; }

    [Column("RETURN_RIEL", TypeName = "decimal(18,6)")]
    public decimal? ReturnRiel { get; set; }

    [Column("NOTE")] [StringLength(200)] public string? Note { get; set; }

    [Column("ACTIVE")] public bool? Active { get; set; }
}

[Table("BOOKINGRETURNDETAIL_TBL")]
public class BookingReturnDetail
{
    [Key]
    [Column("BOOKINGRETURNDETAILID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookingReturnDetailId { get; set; }

    [Column("BOOKINGRETURNID")] public int? BookingReturnId { get; set; }

    [Column("BOOKINGID")] public int? BookingId { get; set; }

    [Column("CLOTHID")] public int? ClothId { get; set; }

    [Column("QTY", TypeName = "decimal(18,3)")]
    public decimal? Qty { get; set; }

    [Column("PRICE", TypeName = "decimal(18,3)")]
    public decimal? Price { get; set; }
}

[Table("BRANCH")]
public class Branch
{
    [Key]
    [Column("BRANCH_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BranchId { get; set; }

    [Column("BRANCH_NAME")]
    [StringLength(30)]
    public string? BranchName { get; set; }

    [Column("BRANCH_NAME_IN_KHMER")]
    [StringLength(50)]
    public string? BranchNameInKhmer { get; set; }

    [Column("SHORT_NAME")]
    [StringLength(50)]
    public string? ShortName { get; set; }

    [Column("ADDRESS")]
    [StringLength(200)]
    public string? Address { get; set; }

    [Column("PHONE")] [StringLength(50)] public string? Phone { get; set; }
}

[Table("CATEGORY_TBL")]
public class Category
{
    [Key]
    [Column("CATEGORY_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }

    [Column("CATEGORY_NAME")]
    [StringLength(50)]
    public string? CategoryName { get; set; }

    [Column("DESCRIPTIOIN")]
    [StringLength(100)]
    public string? Description { get; set; }
}

[Table("CERTIFICATE")]
public class Certificate
{
    [Key]
    [Column("CERTIFICATE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CertificateId { get; set; }

    [Required]
    [Column("CERTIFICATE_CODE")]
    [StringLength(10)]
    public string? CertificateCode { get; set; } = null!;

    [Column("CERTIFICATE_NAME")]
    [StringLength(100)]
    public string? CertificateName { get; set; }
}

[Table("CHANGE_BRANCH")]
public class ChangeBranch
{
    [Key]
    [Column("CHANGE_BRANCH_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ChangeBranchId { get; set; }

    [Required]
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; } = null!;

    [Required] [Column("TO_BRANCH_ID")] public int ToBranchId { get; set; }

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required] [Column("FROM_DATE")] public DateTime FromDate { get; set; }

    [Column("RETURN_DATE")] public DateTime? ReturnDate { get; set; }

    [Column("DEGREE_ID")]
    [StringLength(50)]
    public string? DegreeId { get; set; }

    [Column("SCHOOL_ID")]
    [StringLength(50)]
    public string? SchoolId { get; set; }

    [Column("FIELD_ID")]
    [StringLength(50)]
    public string? FieldId { get; set; }

    [Column("PROMOTION_ID")]
    [StringLength(50)]
    public string? PromotionId { get; set; }

    [Column("STAGE_ID")]
    [StringLength(50)]
    public string? StageId { get; set; }

    [Column("GROUP_ID")]
    [StringLength(50)]
    public string? GroupId { get; set; }
}

[Table("CHANGE_FIELD_TBL")]
public class ChangeField
{
    [Key]
    [Column("CHANGE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ChangeId { get; set; }

    [Column("CHANGE_DATE")] public DateTime? ChangeDate { get; set; }

    [Column("STUDENT_ID")]
    [StringLength(20)]
    public string? StudentId { get; set; }

    [Column("OLD_FIELD_ID")] public int? OldFieldId { get; set; }

    [Column("NEW_FIELD_ID")] public int? NewFieldId { get; set; }

    [Column("USER_NAME")]
    [StringLength(20)]
    public string? UserName { get; set; }

    [Column("DEGREE_ID")]
    [StringLength(20)]
    public string? DegreeId { get; set; }

    [Column("SCHOOL_ID")]
    [StringLength(50)]
    public string? SchoolId { get; set; }

    [Column("SCHOOL_ID_NEW")]
    [StringLength(50)]
    public string? SchoolIdNew { get; set; }

    [Column("PROMOTION_ID")]
    [StringLength(20)]
    public string? PromotionId { get; set; }

    [Column("STAGE_ID")]
    [StringLength(20)]
    public string? StageId { get; set; }

    [Column("TERM_NO")] [StringLength(20)] public string? TermNo { get; set; }

    [Column("GROUP_ID")]
    [StringLength(20)]
    public string? GroupId { get; set; }
}

[Table("COMPLEMENT_FAILED_COURSE_SCORE")]
public class ComplementFailedCourseScore
{
    [Key]
    [Column("COMPLEMENT_FAILED_COURSE_SCORE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ComplementFailedCourseScoreId { get; set; }

    [Required]
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; } = null!;

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required] [Column("COURSE_ID")] public int CourseId { get; set; }

    [Required] [Column("MID_TERM_SCORE")] public double MidTermScore { get; set; }

    [Required] [Column("FINAL_SCORE")] public double FinalScore { get; set; }

    [Column("USERNAME")]
    [StringLength(50)]
    public string? Username { get; set; }

    [Column("DATE_EDIT")] public DateTime? DateEdit { get; set; }
}

[Table("COMPLEMENT_ORIENTED_COURSE_SCORE")]
public class ComplementOrientedCourseScore
{
    [Key]
    [Column("COMPLEMENT_ORIENTED_COURSE_SCORE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ComplementOrientedCourseScoreId { get; set; }

    [Required]
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; } = null!;

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required] [Column("COURSE_ID")] public int CourseId { get; set; }

    [Required] [Column("MID_TERM_SCORE")] public double MidTermScore { get; set; }

    [Required] [Column("FINAL_SCORE")] public double FinalScore { get; set; }

    [Column("NOTE")] [StringLength(500)] public string? Note { get; set; }

    [Column("USERNAME")]
    [StringLength(50)]
    public string? Username { get; set; }

    [Column("DATE_EDIT")] public DateTime? DateEdit { get; set; }
}

[Table("COMPLEMENT_SEMESTER_SCORE")]
public class ComplementSemesterScore
{
    [Key]
    [Column("COMPLEMENT_SEMESTER_SCORE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ComplementSemesterScoreId { get; set; }

    [Required]
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; } = null!;

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required] [Column("COURSE_ID")] public int CourseId { get; set; }

    [Required] [Column("MID_TERM_SCORE")] public double MidTermScore { get; set; }

    [Required] [Column("FINAL_SCORE")] public double FinalScore { get; set; }

    [Column("USERNAME")]
    [StringLength(50)]
    public string? Username { get; set; }

    [Column("DATE_EDIT")] public DateTime? DateEdit { get; set; }
}

[Table("CONTACT_PERSON")]
public class ContactPerson
{
    [Key]
    [Column("CONTACT_PERSON_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ContactPersonId { get; set; }

    [Column("CONTACT_PERSON_NAME")]
    [StringLength(100)]
    public string? ContactPersonName { get; set; }

    [Column("JOB")] [StringLength(200)] public string? Job { get; set; }

    [Column("PHONE")] [StringLength(24)] public string? Phone { get; set; }

    [Column("ADDRESS")]
    [StringLength(300)]
    public string? Address { get; set; }
}

[Table("COURSE")]
public class Course
{
    [Key]
    [Column("COURSE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CourseId { get; set; }

    [Required]
    [Column("COURSE_FULL_NAME")]
    [StringLength(60)]
    public string? CourseFullName { get; set; } = null!;

    [Required]
    [Column("COURSE_FULL_NAME_IN_KHMER")]
    [StringLength(100)]
    public string? CourseFullNameInKhmer { get; set; } = null!;

    [Required]
    [Column("COURSE_SHORT_NAME")]
    [StringLength(30)]
    public string? CourseShortName { get; set; } = null!;

    [Required]
    [Column("COURSE_SHORT_NAME_IN_KHMER")]
    [StringLength(100)]
    public string? CourseShortNameInKhmer { get; set; } = null!;

    [Column("CREDIT")] public double? Credit { get; set; }

    [Column("NUMBER_OF_HOURS")] public double? NumberOfHours { get; set; }
}

[Table("COURSE_CODE")]
public class CourseCode
{
    [Key]
    [Column("COURSE_CODE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CourseCodeId { get; set; }

    [Required] [Column("COURSE_ID")] public int CourseId { get; set; }

    [Required] [Column("SCHOOL_ID")] public int SchoolId { get; set; }

    [Required] [Column("FIELD_ID")] public int FieldId { get; set; }

    [Required] [Column("DEGREE_ID")] public int DegreeId { get; set; }

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required]
    [Column("CODE")]
    [StringLength(10)]
    public string? Code { get; set; } = null!;
}

[Table("COURSE_SCHOOL")]
public class CourseSchool
{
    [Required] [Column("SCHOOL_ID")] public int SchoolId { get; set; }

    [Required] [Column("COURSE_ID")] public int CourseId { get; set; }
}

[Table("COURSE_TERM")]
public class Courseterm
{
    [Key]
    [Column("COURSE_TERM_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CoursetermId { get; set; }

    [Required] [Column("COURSE_ID")] public int CourseId { get; set; }

    [Required] [Column("FIELD_ID")] public int FieldId { get; set; }

    [Required] [Column("TERM_ID")] public int TermId { get; set; }

    [Column("CREDIT")] public double? Credit { get; set; }

    [Column("TYPE")] [StringLength(20)] public string? Type { get; set; }

    [Column("HOURS")] public double? Hours { get; set; }
}

[Table("DEBUG_LOGGER")]
public class DebugLogger
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("MSG")] [StringLength(200)] public string? Message { get; set; }
}

[Table("DEGREE")]
public class Degree
{
    [Key]
    [Column("DEGREE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DegreeId { get; set; }

    [Required]
    [Column("DEGREE")]
    [StringLength(30)]
    public string? DegreeName { get; set; } = null!;

    [Column("DEGREE_IN_KHMER")]
    [StringLength(30)]
    public string? DegreeInKhmer { get; set; }
}

[Table("DISABILITY_TBL")]
public class Disability
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Disability")]
    [StringLength(200)]
    public string? DisabilityName { get; set; }

    [Column("DisabilityKh")]
    [StringLength(200)]
    public string? DisabilityNameKh { get; set; }
}

[Table("DISCOUNT")]
public class Discount
{
    [Key]
    [Column("DISCOUNT_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DiscountId { get; set; }

    [Required]
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; } = null!;

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required]
    [Column("AMOUNT", TypeName = "money")]
    public decimal Amount { get; set; }

    [Column("REASON")] [StringLength(30)] public string? Reason { get; set; }
}

[Table("DOCTORAL_CONTRACT")]
public class DoctoralContract
{
    [Key]
    [Column("CONTRACT_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ContractId { get; set; }

    [Column("STUDENT_ID")]
    [StringLength(50)]
    public string? StudentId { get; set; }

    [Column("TERM_NO")] public int? TermNo { get; set; }

    [Column("FEE", TypeName = "decimal(10,2)")]
    public decimal? Fee { get; set; }

    [Column("START_DATE")] public DateTime? StartDate { get; set; }

    [Column("END_DATE")] public DateTime? EndDate { get; set; }

    [Column("NOTE")] [StringLength(100)] public string? Note { get; set; }
}

[Table("EXAM_DATE")]
public class ExamDate
{
    [Key]
    [Column("EXAM_DATE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExamDateId { get; set; }

    [Required] [Column("COURSE_TERM_ID")] public int CoursetermId { get; set; }

    [Required] [Column("DATE")] public DateTime Date { get; set; }
}

[Table("ExchangeRate_Tbl")]
public class ExchangeRate
{
    [Key]
    [Column("ExchangeRateID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExchangeRateId { get; set; }

    [Column("ExchangeDate")] public DateTime? ExchangeDate { get; set; }

    [Column("Description")]
    [StringLength(200)]
    public string? Description { get; set; }
}

[Table("ExchangeRateDetail_Tbl")]
public class ExchangeRateDetail
{
    [Key]
    [Column("DetailID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DetailId { get; set; }

    [Column("ExchangeRateID")] public int? ExchangeRateId { get; set; }

    [Column("CurrencyNameIn")]
    [StringLength(50)]
    public string? CurrencyNameIn { get; set; }

    [Column("CurrencyNameOut")]
    [StringLength(50)]
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
    [Column("EXTEND_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExtendId { get; set; }

    [Required]
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; } = null!;

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required]
    [Column("EXTEND_FROM")]
    [StringLength(20)]
    public string? ExtendFrom { get; set; } = null!;

    [Required] [Column("FROM_ID")] public int FromId { get; set; }

    [Column("IS_CERTIFICATE_RECEIVED")] public int? IsCertificateReceived { get; set; }

    [Column("IS_TRANSCRIPT_RECEIVED")] public int? IsTranscriptReceived { get; set; }

    [Column("EXTEND_DATE", TypeName = "date")]
    public DateTime? ExtendDate { get; set; }
}

[Table("EXTERNAL_SCORE")]
public class ExternalScore
{
    [Key]
    [Column("EXTERNAL_SCORE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExternalScoreId { get; set; }

    [Required]
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; } = null!;

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required]
    [Column("COURSE_NAME")]
    [StringLength(30)]
    public string? CourseName { get; set; } = null!;

    [Required]
    [Column("COURSE_NAME_IN_KHMER")]
    [StringLength(30)]
    public string? CourseNameInKhmer { get; set; } = null!;

    [Required] [Column("CREDIT")] public int Credit { get; set; }

    [Required]
    [Column("GRADE")]
    [StringLength(10)]
    public string? Grade { get; set; } = null!;

    [Column("TOTAL", TypeName = "decimal(18,2)")]
    public decimal? Total { get; set; }

    [Column("COURSE_CODE")]
    [StringLength(10)]
    public string? CourseCode { get; set; }

    [Column("YEAR_START")] public int? YearStart { get; set; }

    [Column("YEAR_END")] public int? YearEnd { get; set; }

    [Column("USERNAME")]
    [StringLength(50)]
    public string? Username { get; set; }

    [Column("DATE_EDIT")] public DateTime? DateEdit { get; set; }
}

[Table("FACULTY")]
public class Faculty
{
    [Key]
    [Column("FACULTY_ID", TypeName = "numeric(28,0)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal FacultyId { get; set; }

    [Required]
    [Column("FACULTY_NAME")]
    [StringLength(60)]
    public string? FacultyName { get; set; } = null!;

    [Required]
    [Column("FACULTY_NAME_IN_KHMER")]
    [StringLength(60)]
    public string? FacultyNameInKhmer { get; set; } = null!;
}

[Table("FIELD")]
public class Field
{
    [Key]
    [Column("FIELD_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FieldId { get; set; }

    [Required]
    [Column("FIELD_NAME")]
    [StringLength(200)]
    public string? FieldName { get; set; } = null!;

    [Column("FIELD_NAME_IN_KHMER")]
    [StringLength(200)]
    public string? FieldNameInKhmer { get; set; }

    [Required] [Column("SCHOOL_ID")] public int SchoolId { get; set; }

    [Required] [Column("DEGREE_ID")] public int DegreeId { get; set; }

    [Column("DEGREE_NAME")]
    [StringLength(100)]
    public string? DegreeName { get; set; }

    [Column("DEGREE_NAME_IN_KHMER")]
    [StringLength(100)]
    public string? DegreeNameInKhmer { get; set; }

    [Column("TYPE")] public bool? Type { get; set; }
}

[Table("FIELD_CERTIFICATE")]
public class FieldCertificate
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("DEGREE_ID")] public int? DegreeId { get; set; }

    [Column("DEGREE_NAME")]
    [StringLength(100)]
    public string? DegreeName { get; set; }

    [Column("DEGREE_NAME_KHMER")]
    [StringLength(100)]
    public string? DegreeNameKhmer { get; set; }

    [Column("SCHOOL_ID")] public int? SchoolId { get; set; }

    [Column("SCHOOL_NAME")]
    [StringLength(200)]
    public string? SchoolName { get; set; }

    [Column("SCHOOL_NAME_KHMER")]
    [StringLength(250)]
    public string? SchoolNameKhmer { get; set; }

    [Column("FIELD_ID")] public int? FieldId { get; set; }

    [Column("FIELD_NAME")]
    [StringLength(250)]
    public string? FieldName { get; set; }

    [Column("FIELD_NAME_KHMER")]
    [StringLength(250)]
    public string? FieldNameKhmer { get; set; }

    [Column("PROMOTION_NO")] public int? PromotionNo { get; set; }

    [Column("STATUS")] public bool? Status { get; set; }
}

[Table("FOUNDATION_YEAR_REPORT_CERTIFICATE_OF_FOUNDATION_YEAR_COURSE")]
public class FoundationYearReportCertificate
{
    [Key]
    [Column("CERTIFICATE_OF_FOUNDATION_YEAR_COURSE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CertificateId { get; set; }

    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME")]
    [StringLength(30)]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER")]
    [StringLength(100)]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX")] [StringLength(6)] public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH")] public DateTime? DateOfBirth { get; set; }

    [Column("COURSE_ID")] public int? CourseId { get; set; }

    [Column("COURSE_FULL_NAME")]
    [StringLength(60)]
    public string? CourseFullName { get; set; }

    [Column("COURSE_FULL_NAME_IN_KHMER")]
    [StringLength(60)]
    public string? CourseFullNameInKhmer { get; set; }

    [Column("COURSE_SHORT_NAME")]
    [StringLength(30)]
    public string? CourseShortName { get; set; }

    [Column("COURSE_SHORT_NAME_IN_KHMER")]
    [StringLength(30)]
    public string? CourseShortNameInKhmer { get; set; }

    [Column("IS_GENERAL_COURSE")] public int? IsGeneralCourse { get; set; }

    [Column("CREDIT")] public int? Credit { get; set; }

    [Column("GRADE_LETTER")]
    [StringLength(15)]
    public string? GradeLetter { get; set; }

    [Column("GPA")] public double? Gpa { get; set; }
}

[Table("GRADE")]
public class Grade
{
    [Key]
    [Column("GRADE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GradeId { get; set; }

    [Required]
    [Column("GRADE_LETTER")]
    [StringLength(15)]
    public string? GradeLetter { get; set; } = null!;

    [Required] [Column("FROM_SCORE")] public double FromScore { get; set; }

    [Required] [Column("TO_SCORE")] public double ToScore { get; set; }

    [Required] [Column("POINT")] public double Point { get; set; }

    [Required]
    [Column("MEANING")]
    [StringLength(15)]
    public string? Meaning { get; set; } = null!;
}

[Table("GROUP")]
public class Group
{
    [Key]
    [Column("GROUP_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GroupId { get; set; }

    [Required]
    [Column("GROUP_NAME")]
    [StringLength(10)]
    public string? GroupName { get; set; } = null!;

    [Required]
    [Column("STUDY_TIME")]
    [StringLength(15)]
    public string? StudyTime { get; set; } = null!;

    [Required] [Column("STAGE_ID")] public int StageId { get; set; }

    [Required] [Column("FIELD_ID")] public int FieldId { get; set; }

    [Required]
    [Column("CREATED_IN_TERM_NO")]
    public int CreatedInTermNo { get; set; }

    [Column("NOTE")] [StringLength(50)] public string? Note { get; set; }
}

[Table("GROUP_ROOM")]
public class GroupRoom
{
    [Key]
    [Column("GROUP_ROOM_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GroupRoomId { get; set; }

    [Required] [Column("GROUP_ID")] public int GroupId { get; set; }

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required]
    [Column("ROOM_NAME")]
    [StringLength(15)]
    public string? RoomName { get; set; } = null!;

    [Column("START_PAYMENT")] public DateTime? StartPayment { get; set; }
}

[Table("HIGH_SCHOOL")]
public class HighSchool
{
    [Key]
    [Column("HIGH_SCHOOL_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int HighSchoolId { get; set; }

    [Required]
    [Column("HIGH_SCHOOL_NAME")]
    [StringLength(50)]
    public string? HighSchoolName { get; set; } = null!;

    [Column("HIGH_SCHOOL_NAME_IN_KHMER")]
    [StringLength(50)]
    public string? HighSchoolNameInKhmer { get; set; }
}

[Table("HIGH_SCHOOL_TYPE")]
public class HighSchoolType
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("NAME")] [StringLength(50)] public string? Name { get; set; }

    [Column("NAME_KHMER")]
    [StringLength(50)]
    public string? NameKhmer { get; set; }
}

[Table("INSTRUCTOR")]
public class Instructor
{
    [Key]
    [Column("INSTRUCTOR_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InstructorId { get; set; }

    [Required]
    [Column("INSTRUCTOR_NAME")]
    [StringLength(30)]
    public string? InstructorName { get; set; } = null!;

    [Required]
    [Column("INSTRUCTOR_NAME_IN_KHMER")]
    [StringLength(30)]
    public string? InstructorNameInKhmer { get; set; } = null!;

    [Required]
    [Column("SEX")]
    [StringLength(6)]
    public string? Sex { get; set; } = null!;

    [Column("DATE_OF_BIRTH")] public DateTime? DateOfBirth { get; set; }

    [Column("PLACE_OF_BIRTH")]
    [StringLength(30)]
    public string? PlaceOfBirth { get; set; }

    [Column("RACE")] [StringLength(30)] public string? Race { get; set; }

    [Column("NATIONALITY")]
    [StringLength(30)]
    public string? Nationality { get; set; }

    [Column("MARITAL_STATUS")]
    [StringLength(15)]
    public string? MaritalStatus { get; set; }

    [Column("PHONE")] [StringLength(15)] public string? Phone { get; set; }

    [Column("EMAIL")] [StringLength(30)] public string? Email { get; set; }

    [Column("ADDRESS")] [StringLength(50)] public string? Address { get; set; }

    [Column("DEGREE")] [StringLength(15)] public string? Degree { get; set; }

    [Column("INSTRUCTOR_TYPE")]
    [StringLength(30)]
    public string? InstructorType { get; set; }
}

[Table("INSTRUCTOR_CERTIFICATE")]
public class InstructorCertificate
{
    [Key]
    [Column("INSTRUCTOR_CERTIFICATE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InstructorCertificateId { get; set; }

    [Required] [Column("INSTRUCTOR_ID")] public int InstructorId { get; set; }

    [Required]
    [Column("CERTIFICATE_NAME")]
    [StringLength(70)]
    public string? CertificateName { get; set; } = null!;

    [Column("YEAR_OBTAINED")] public int? YearObtained { get; set; }

    [Column("UNIVERSITY")]
    [StringLength(70)]
    public string? University { get; set; }

    [Column("COUNTRY")] [StringLength(30)] public string? Country { get; set; }
}

[Table("INSTRUCTOR_COURSE")]
public class InstructorCourse
{
    [Key]
    [Column("INSTRUCTOR_COURSE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InstructorCourseId { get; set; }

    [Required] [Column("INSTRUCTOR_ID")] public int InstructorId { get; set; }

    [Required] [Column("SCHOOL_ID")] public int SchoolId { get; set; }

    [Required] [Column("COURSE_ID")] public int CourseId { get; set; }
}

[Table("INSTRUCTOR_GROUP")]
public class InstructorGroup
{
    [Key]
    [Column("INSTRUCTOR_GROUP_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InstructorGroupId { get; set; }

    [Required] [Column("INSTRUCTOR_ID")] public int InstructorId { get; set; }

    [Required] [Column("GROUP_ID")] public int GroupId { get; set; }

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required] [Column("COURSE_ID")] public int CourseId { get; set; }

    [Column("DAY_OF_WEEK")]
    [StringLength(15)]
    public string? DayOfWeek { get; set; }

    [Column("TIME")] [StringLength(15)] public string? Time { get; set; }

    [Column("ROOM_NAME")]
    [StringLength(15)]
    public string? RoomName { get; set; }

    [Column("STATUS")] [StringLength(15)] public string? Status { get; set; }
}

[Table("INSTRUCTOR_SCHOOL")]
public class InstructorSchool
{
    [Key]
    [Column("INSTRUCTOR_SCHOOL_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InstructorSchoolId { get; set; }

    [Required] [Column("INSTRUCTOR_ID")] public int InstructorId { get; set; }

    [Required] [Column("SCHOOL_ID")] public int SchoolId { get; set; }
}

[Table("INSTRUCTOR_TYPE")]
public class InstructorType
{
    [Key]
    [Column("INSTRUCTOR_TYPE")]
    [StringLength(30)]
    public string? Type { get; set; } = null!;
}

[Table("INVOICE_ITEM_DETAIL")]
public class InvoiceItemDetail
{
    [Key]
    [Column("INVOICE_ITEM_DETAIL_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InvoiceItemDetailId { get; set; }

    [Column("INVOICE_ITEM_ID")] public int? InvoiceItemId { get; set; }

    [Column("DEGREE_ID")] public int? DegreeId { get; set; }

    [Column("SCHOOL_ID")] public int? SchoolId { get; set; }

    [Column("VAT")] public int? Vat { get; set; }

    [Column("PRICE", TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }
}

[Table("INVOICE_PAYMENT_TBL")]
public class InvoicePayment
{
    [Key]
    [Column("PaymentID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentId { get; set; }

    [Column("InvoiceID")] public int? InvoiceId { get; set; }

    [Column("PaymentDate")] public DateTime? PaymentDate { get; set; }

    [Column("ExchangeID")] public int? ExchangeId { get; set; }

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
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("INVOICE_ID")] public int? InvoiceId { get; set; }

    [Column("PAYMENT_METHOD_ID")] public int? PaymentMethodId { get; set; }

    [Column("DOLLAR", TypeName = "decimal(18,6)")]
    public decimal? Dollar { get; set; }

    [Column("REIL", TypeName = "decimal(18,6)")]
    public decimal? Reil { get; set; }
}

[Table("INVOICE_TBL")]
public class Invoice
{
    [Key] [Column("INVOICE_ID")] public int InvoiceId { get; set; }

    [Column("INVOICE_NO")] public int? InvoiceNo { get; set; }

    [Column("YEAR_NUMBER")] public string YearNumber { get; set; }

    [Column("INVOICE_DATE")] public DateTime? InvoiceDate { get; set; }

    [Column("STUDENT_ID")] public string StudentId { get; set; }

    [Column("DEGREE_ID")] public string DegreeId { get; set; }

    [Column("SCHOOL_ID")] public string SchoolId { get; set; }

    [Column("FIELD_ID")] public string FieldId { get; set; }

    [Column("PROMOTION_ID")] public string PromotionId { get; set; }

    [Column("STAGE_ID")] public string StageId { get; set; }

    [Column("GROUP_ID")] public string GroupId { get; set; }

    [Column("STARTDATE")] public DateTime? StartDate { get; set; }

    [Column("ENDDATE")] public DateTime? EndDate { get; set; }

    [Column("TERM_NO")] public string TermNo { get; set; }

    [Column("EXCHANGERATE_ID")] public int? ExchangeRateId { get; set; }

    [Column("VAT")] public decimal? Vat { get; set; }

    [Column("GRAND_TOTAL")] public decimal? GrandTotal { get; set; }

    [Column("DESCRIPTION")] public string Description { get; set; }

    [Column("STATUS")] public string Status { get; set; }

    [Column("TOTALDOLLAR")] public decimal? TotalDollar { get; set; }

    [Column("TOTALRIEL")] public decimal? TotalRiel { get; set; }

    [Column("TOTALBATH")] public decimal? TotalBath { get; set; }

    [Column("TOTALDISCOUNT")] public decimal? TotalDiscount { get; set; }

    [Column("PAYMENT")] public bool? Payment { get; set; }

    [Column("CHECK_PAYMENT")] public bool? CheckPayment { get; set; }

    [Column("DATE_EDIT")] public DateTime? DateEdit { get; set; }

    [Column("EDIT_BY")] public string EditBy { get; set; }

    [Column("OWE")] public decimal? Owe { get; set; }

    [Column("OWE_REASON")] public string OweReason { get; set; }

    [Column("USER_ID")] public int? UserId { get; set; }

    [Column("TOTAL_RETURN_AMOUNT")] public decimal? TotalReturnAmount { get; set; }

    [Column("RETURN_AMOUNT")] public decimal? ReturnAmount { get; set; }

    [Column("RETURN_DESCRIPTION")] public string ReturnDescription { get; set; }

    [Column("TOTALOTHER")] public decimal? TotalOther { get; set; }

    [Column("PAYMENT_METHOD_ID")] public int? PaymentMethodId { get; set; }

    [Column("AMOUNT_DOLLAR")] public decimal? AmountDollar { get; set; }

    [Column("AMOUNT_REIL")] public decimal? AmountReil { get; set; }

    [Column("PAY_ON_APP")] public bool? PayOnApp { get; set; }

    [Column("GRAND_TOTAL_KHR")] public decimal? GrandTotalKhr { get; set; }

    [Column("OWE_KHR")] public decimal? OweKhr { get; set; }
}

[Table("INVOICEDETAIL_TBL")]
public class InvoiceDetail
{
    [Key] [Column("INVOICEDETAIL_ID")] public int InvoiceDetailId { get; set; }

    [Column("INVOICE_ID")] public int? InvoiceId { get; set; }

    [Column("PRODUCT_ID")] public int? ProductId { get; set; }

    [Column("QTY")] public int? Qty { get; set; }

    [Column("QTYNOTE")] public string QtyNote { get; set; }

    [Column("PRICE")] public decimal? Price { get; set; }

    [Column("NOTE")] public string Note { get; set; }

    [Column("VAT")] public decimal? Vat { get; set; }

    [Column("P_DOLLAR")] public decimal? PDollar { get; set; }

    [Column("P_RIEL")] public decimal? PRiel { get; set; }

    [Column("P_BATH")] public decimal? PBath { get; set; }

    [Column("DISCOUNT")] public decimal? Discount { get; set; }

    [Column("OWE")] public decimal? Owe { get; set; }

    [Column("CATEGORYID")] public int? CategoryId { get; set; }

    [Column("OTHER")] public decimal? Other { get; set; }

    [Column("PRICE_KHR")] public decimal? PriceKhr { get; set; }

    [Column("DISCOUNT_KHR")] public decimal? DiscountKhr { get; set; }

    [Column("OWE_KHR")] public decimal? OweKhr { get; set; }

    [Column("DISCOUNT_PERCENT")] public int? DiscountPercent { get; set; }

    [Column("OTHER_KHR")] public decimal? OtherKhr { get; set; }
}

[Table("KHMER_LUNAA_CALENDAR")]
public class KhmerLunaaCalendar
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("NAME_KHMER")]
    [StringLength(100)]
    public string? NameKhmer { get; set; }
}

[Table("LECTURER")]
public class Lecturer
{
    [Key]
    [Column("LECTURER_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LecturerId { get; set; }

    [Column("NAME")] [StringLength(100)] public string? Name { get; set; }

    [Column("SEX")] [StringLength(1)] public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH")] public DateTime? DateOfBirth { get; set; }

    [Column("PRICE", TypeName = "money")] public decimal? Price { get; set; }

    [Column("TELEPHONE")]
    [StringLength(15)]
    public string? Telephone { get; set; }

    [Column("DEGREE_ID")] public int? DegreeId { get; set; }

    [Column("LECTURER_FIELD_ID")] public int? LecturerFieldId { get; set; }

    [Column("SCHOOL_ID")] public int? SchoolId { get; set; }

    [Column("NAME_IN_KHMER")]
    [StringLength(100)]
    public string? NameInKhmer { get; set; }
}

[Table("LECTURER_BRANCH")]
public class LecturerBranch
{
    [Column("LECTURER_ID")] public int LecturerId { get; set; }

    [Column("BRANCH_ID")] public int BranchId { get; set; }
}

[Table("LECTURER_COURSE")]
public class LecturerCourse
{
    [Column("LECTURER_ID")] public int LecturerId { get; set; }

    [Column("COURSE_ID")] public int CourseId { get; set; }
}

[Table("LECTURER_DEGREE")]
public class LecturerDegree
{
    [Key]
    [Column("LECTURER_DEGREE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LecturerDegreeId { get; set; }

    [Column("LECTURER_DEGREE_NAME")]
    [StringLength(50)]
    public string? LecturerDegreeName { get; set; }

    [Column("LECTURER_DEGREE_NAME_IN_KHMER")]
    [StringLength(50)]
    public string? LecturerDegreeNameInKhmer { get; set; }
}

[Table("LECTURER_FIELD")]
public class LecturerField
{
    [Key]
    [Column("LECTURER_FIELD_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LecturerFieldId { get; set; }

    [Column("NAME")] [StringLength(50)] public string? Name { get; set; }

    [Column("NAME_IN_KHMER")]
    [StringLength(50)]
    public string? NameInKhmer { get; set; }

    [Column("LECTURER_DEGREE_ID")] public int? LecturerDegreeId { get; set; }

    // Navigation property
    // public virtual LecturerDegree? LecturerDegree { get; set; }
}

[Table("LECTURER_SUBJECT")]
public class LecturerSubject
{
    [Column("LECTURER_ID")] public int LecturerId { get; set; }

    [Column("SUBJECT_ID")] public int SubjectId { get; set; }
}

[Table("LETTER")]
public class Letter
{
    [Key] [Column("LETTER_ID")] public int LetterId { get; set; }

    [Required]
    [Column("LETTER_NAME")]
    [StringLength(40)]
    public string? LetterName { get; set; } = null!;
}

[Table("LETTER_CATEGORY_TBL")]
public class LetterCategory
{
    [Key]
    [Column("categoryID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }

    [Column("categoryName", TypeName = "nvarchar(50)")]
    [Required]
    public string? CategoryName { get; set; }

    [Column("unitPrice", TypeName = "decimal(18,2)")]
    public decimal? UnitPrice { get; set; }

    [Column("active")] [Required] public bool? Active { get; set; }

    [Column("IsAdmin")] [Required] public bool? IsAdmin { get; set; }

    [Column("IsFoundation")] [Required] public bool? IsFoundation { get; set; }

    [Column("IsShortCourse")] [Required] public bool? IsShortCourse { get; set; }
}

[Table("LETTER_CERTIFICATION_TBL")]
public class LetterCertification
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("LetterNo")] public int? LetterNo { get; set; }

    [Column("YearNumber")]
    [StringLength(10)]
    public string? YearNumber { get; set; }

    [Column("certificateID")] public int? CertificateId { get; set; }

    [Column("issuedDate")] public DateTime? IssuedDate { get; set; }

    [Required] [Column("issuedStatus")] public bool IssuedStatus { get; set; }

    [Column("stuID")] [StringLength(50)] public string? StudentId { get; set; }

    [Column("nameInkh")]
    [StringLength(50)]
    public string? NameInKhmer { get; set; }

    [Column("nameInEng")]
    [StringLength(40)]
    public string? NameInEnglish { get; set; }

    [Column("sex")] [StringLength(10)] public string? Sex { get; set; }

    [Column("BirthDate")] public DateTime? BirthDate { get; set; }

    [Column("Degree")] [StringLength(50)] public string? Degree { get; set; }

    [Column("School")] [StringLength(50)] public string? School { get; set; }

    [Column("Field")] [StringLength(50)] public string? Field { get; set; }

    [Column("Promotion")]
    [StringLength(50)]
    public string? Promotion { get; set; }

    [Column("issuedNo")]
    [StringLength(10)]
    public string? IssuedNo { get; set; }

    [Column("receivedDate")] public DateTime? ReceivedDate { get; set; }

    [Column("amount")] public short? Amount { get; set; }

    [Column("categoryID")] public short? CategoryId { get; set; }

    [Column("other")] [StringLength(60)] public string? Other { get; set; }

    [Column("FoundationNo")] public int? FoundationNo { get; set; }

    [Column("FoundationYear")] public int? FoundationYear { get; set; }

    [Column("ShortCourseNo")] public int? ShortCourseNo { get; set; }

    [Column("ShortCourseYear")] public int? ShortCourseYear { get; set; }
}

[Table("MINIMUM_GPA")]
public class MinimumGpa
{
    [Key] [Column("GPA")] public float Gpa { get; set; }
}

[Table("NATIONALITY")]
public class Nationality
{
    [Key]
    [Column("NATIONALITY_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NationalityId { get; set; }

    [Required]
    [Column("NATIONALITY")]
    [StringLength(30)]
    public string? NationalityName { get; set; } = null!;

    [Required]
    [Column("NATIONALITY_IN_KHMER")]
    [StringLength(30)]
    public string? NationalityInKhmer { get; set; } = null!;
}

[Table("NUMBER_OF_YEARS_STUDY")]
public class NumberOfYearsStudy
{
    [Key]
    [Column("NUMBER_OF_YEARS_STUDY_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NumberOfYearsStudyId { get; set; }

    [Required] [Column("DEGREE_ID")] public int DegreeId { get; set; }

    [Required] [Column("SCHOOL_ID")] public int SchoolId { get; set; }

    [Required] [Column("NUMBER_OF_YEARS")] public int NumberOfYears { get; set; }
}

[Table("OTHER_BRANCH_SCORE")]
public class OtherBranchScore
{
    [Key]
    [Column("OTHER_BRANCH_SCORE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OtherBranchScoreId { get; set; }

    [Required]
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; } = null!;

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Column("COURSE_ID")] public int? CourseId { get; set; }

    [Required]
    [Column("COURSE_NAME")]
    [StringLength(50)]
    public string? CourseName { get; set; } = null!;

    [Required]
    [Column("COURSE_NAME_IN_KHMER")]
    [StringLength(100)]
    public string? CourseNameInKhmer { get; set; } = null!;

    [Required] [Column("CREDIT")] public int Credit { get; set; }

    [Required] [Column("MID_TERM_SCORE")] public float MidTermScore { get; set; }

    [Required] [Column("FINAL_SCORE")] public float FinalScore { get; set; }

    [Column("YEAR_START")] public int? YearStart { get; set; }

    [Column("YEAR_END")] public int? YearEnd { get; set; }

    [Column("USERNAME")]
    [StringLength(50)]
    public string? Username { get; set; }

    [Column("DATE_EDIT")] public DateTime? DateEdit { get; set; }
}

[Table("OTHER_BRANCH_SCORE_UNICODE")]
public class OtherBranchScoreUnicode
{
    [Column("ID")] public int? Id { get; set; }

    [Column("NAME")] [StringLength(100)] public string? Name { get; set; }
}

[Table("PAYMENT")]
public class Payment
{
    [Key]
    [Column("PAYMENT_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentId { get; set; }

    [Required]
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; } = null!;

    [Required]
    [Column("INVOICE_NO")]
    [StringLength(10)]
    public string? InvoiceNo { get; set; }

    [Required] [Column("INVOICE_DATE")] public DateTime InvoiceDate { get; set; }

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required]
    [Column("PAID", TypeName = "money")]
    public decimal Paid { get; set; }

    [Required]
    [Column("DEPOSIT", TypeName = "money")]
    public decimal Deposit { get; set; }

    [Column("NOTE")] [StringLength(200)] public string? Note { get; set; }

    [Column("IS_INSURANCE")] public bool? IsInsurance { get; set; }

    [Column("GUARDIAN")]
    [StringLength(50)]
    public string? Guardian { get; set; }
}

[Table("PAYMENT_METHOD")]
public class PaymentMethod
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("NAME")] [StringLength(50)] public string? Name { get; set; }

    [Column("NAME_KHMER")]
    [StringLength(100)]
    public string? NameKhmer { get; set; }
}

[Table("PAYMENT_TYPE")]
public class PaymentType
{
    [Key]
    [Column("PAYMENT_TYPE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentTypeId { get; set; }

    [Column("PAYMENT_TYPE")]
    [StringLength(50)]
    public string? PaymentTypeName { get; set; }

    [Column("STATUS")] public bool? Status { get; set; }
}

[Table("POSITION")]
public class Position
{
    [Column("POSITION")]
    [StringLength(30)]
    public string? PositionName { get; set; }
}

[Table("PRIVILEDGE")]
public class Priviledge
{
    [Key] [Column("PRIVILEDGE_ID")] public int PriviledgeId { get; set; }

    [Column("PRIVILEDGE_NAME")]
    [StringLength(60)]
    public string? PriviledgeName { get; set; }

    [Column("USER_GROUP")]
    [StringLength(20)]
    public string? UserGroup { get; set; }
}

[Table("PRODUCT_DETAIL")]
public class ProductDetail
{
    [Key] [Column("PRODUCT_DETAIL_ID")] public int ProductDetailId { get; set; }

    [Column("PRODUCT_ID")] public int? ProductId { get; set; }

    [Column("DEGREE_ID")] public int? DegreeId { get; set; }

    [Column("SCHOOL_ID")] public int? SchoolId { get; set; }
}

[Table("PRODUCT_TBL")]
public class Product
{
    [Key] [Column("PRODUCT_ID")] public int ProductId { get; set; }

    [Column("PRODUCT_NAME")]
    [StringLength(50)]
    public string? ProductName { get; set; }

    [Column("PRODUCT_NAME_IN_KHMER")]
    [StringLength(100)]
    public string? ProductNameInKhmer { get; set; }

    [Column("DESCRIPTION")]
    [StringLength(100)]
    public string? Description { get; set; }

    [Column("VAT")] public int? Vat { get; set; }

    [Column("PRICE")] public decimal? Price { get; set; }

    [Column("TYPE")] [StringLength(50)] public string? Type { get; set; }

    [Column("STATUS")] [StringLength(10)] public string? Status { get; set; }

    [Column("TuitionFees")] public bool? TuitionFees { get; set; }

    [Column("DEGREEID")]
    [StringLength(50)]
    public string? DegreeId { get; set; }

    [Column("OrderID")] public int? OrderId { get; set; }

    [Column("CARD_CERTIFICATE")] public int? CardCertificate { get; set; }

    [Column("CATEGORY_ID")] public int? CategoryId { get; set; }

    [Column("PRICE_KHR")] public decimal? PriceKhr { get; set; }

    [Column("PAYMENT_TYPE")] public bool? PaymentType { get; set; }

    [Column("FROM_PROMOTION")] public int? FromPromotion { get; set; }

    [Column("TO_PROMOTION")] public int? ToPromotion { get; set; }

    [Column("HIDDEN")] public bool? Hidden { get; set; }
}

[Table("PROMOTION")]
public class Promotion
{
    [Key] [Column("PROMOTION_ID")] public int PromotionId { get; set; }

    [Column("DEGREE_ID")] public int DegreeId { get; set; }

    [Column("SCHOOL_ID")] public int SchoolId { get; set; }

    [Column("PROMOTION_NO")] public int PromotionNo { get; set; }

    [Column("ACADEMIC_YEAR_START")] public int AcademicYearStart { get; set; }

    [Column("ACADEMIC_YEAR_END")] public int AcademicYearEnd { get; set; }

    [Column("STATUS")] [StringLength(15)] public string? Status { get; set; }

    [Column("GRADUATE_DATE1")] public DateTime? GraduateDate1 { get; set; }

    [Column("GRADUATE_DATE2")] public DateTime? GraduateDate2 { get; set; }
}

[Table("PROVINCE")]
public class Province
{
    [Key] [Column("PROVINCE_ID")] public int ProvinceId { get; set; }

    [Column("PROVINCE")]
    [StringLength(30)]
    public string? ProvinceName { get; set; }

    [Column("PROVINCE_IN_KHMER")]
    [StringLength(30)]
    public string? ProvinceInKhmer { get; set; }

    [Column("IS_CITY")] public int IsCity { get; set; }
}

[Table("QR_CODE_CERTIFICATE")]
public class QrCodeCertificate
{
    [Key]
    [Column("ID")]
    [StringLength(100)]
    public string? Id { get; set; }

    [Column("STUDENT_ID")]
    [StringLength(50)]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME")]
    [StringLength(100)]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_KHMER")]
    [StringLength(100)]
    public string? StudentNameKhmer { get; set; }

    [Column("SEX")] [StringLength(10)] public string? Sex { get; set; }

    [Column("DOB")] [StringLength(100)] public string? Dob { get; set; }

    [Column("DOB_KHMER")]
    [StringLength(100)]
    public string? DobKhmer { get; set; }

    [Column("STATUS")] [StringLength(50)] public string? Status { get; set; }

    [Column("DEGREE_ID")] public int? DegreeId { get; set; }

    [Column("DEGREE_NAME")]
    [StringLength(200)]
    public string? DegreeName { get; set; }

    [Column("DEGREE_NAME_KHMER")]
    [StringLength(250)]
    public string? DegreeNameKhmer { get; set; }

    [Column("SCHOOL_ID")] public int? SchoolId { get; set; }

    [Column("SCHOOL_NAME")]
    [StringLength(100)]
    public string? SchoolName { get; set; }

    [Column("SCHOOL_NAME_KHMER")]
    [StringLength(50)]
    public string? SchoolNameKhmer { get; set; }

    [Column("FIELD_ID")] public int? FieldId { get; set; }

    [Column("FIELD_NAME")]
    [StringLength(200)]
    public string? FieldName { get; set; }

    [Column("FIELD_NAME_KHMER")]
    [StringLength(250)]
    public string? FieldNameKhmer { get; set; }

    [Column("TYPE")] [StringLength(50)] public string? Type { get; set; }

    [Column("PROMOTION_ID")] public int? PromotionId { get; set; }

    [Column("PROMOTION_NO")] public int? PromotionNo { get; set; }

    [Column("STAGE_NO")] public int? StageNo { get; set; }

    [Column("GROUP_NAME")]
    [StringLength(50)]
    public string? GroupName { get; set; }

    [Column("PHOTO")] public string? Photo { get; set; }

    [Column("GRADUATE_DATE")]
    [StringLength(100)]
    public string? GraduateDate { get; set; }

    [Column("GRADUATE_DATE_KHMER")]
    [StringLength(100)]
    public string? GraduateDateKhmer { get; set; }

    [Column("URL")] public string? Url { get; set; }

    [Column("DOCUMENT_KEY")] public string? DocumentKey { get; set; }

    [Column("QRCODE_DATA")] public string? QrCodeData { get; set; }

    [Column("CERTIFICATE_CODE")]
    [StringLength(50)]
    public string? CertificateCode { get; set; }

    [Column("LOCKED")] public bool? Locked { get; set; }

    [Column("DATE")] public DateTime? Date { get; set; }

    [Column("USER_ID")] public int? UserId { get; set; }
}

[Table("QR_CODE_CERTIFICATE_HISTORY")]
public class QrCodeCertificateHistory
{
    [Key] [Column("ID")] public string? Id { get; set; }

    [Column("STUDENT_ID")] public string? StudentId { get; set; }

    [Column("STUDENT_NAME")] public string? StudentName { get; set; }

    [Column("STUDENT_NAME_KHMER")] public string? StudentNameKhmer { get; set; }

    [Column("SEX")] public string? Sex { get; set; }

    [Column("DOB")] public string? Dob { get; set; }

    [Column("DOB_KHMER")] public string? DobKhmer { get; set; }

    [Column("STATUS")] public string? Status { get; set; }

    [Column("DEGREE_ID")] public int? DegreeId { get; set; }

    [Column("DEGREE_NAME")] public string? DegreeName { get; set; }

    [Column("DEGREE_NAME_KHMER")] public string? DegreeNameKhmer { get; set; }

    [Column("SCHOOL_ID")] public int? SchoolId { get; set; }

    [Column("SCHOOL_NAME")] public string? SchoolName { get; set; }

    [Column("SCHOOL_NAME_KHMER")] public string? SchoolNameKhmer { get; set; }

    [Column("FIELD_ID")] public int? FieldId { get; set; }

    [Column("FIELD_NAME")] public string? FieldName { get; set; }

    [Column("FIELD_NAME_KHMER")] public string? FieldNameKhmer { get; set; }

    [Column("TYPE")] public string? Type { get; set; }

    [Column("PROMOTION_ID")] public int? PromotionId { get; set; }

    [Column("PROMOTION_NO")] public int? PromotionNo { get; set; }

    [Column("STAGE_NO")] public int? StageNo { get; set; }

    [Column("GROUP_NAME")] public string? GroupName { get; set; }

    [Column("PHOTO")] public string? Photo { get; set; }

    [Column("GRADUATE_DATE")] public string? GraduateDate { get; set; }

    [Column("GRADUATE_DATE_KHMER")] public string? GraduateDateKhmer { get; set; }

    [Column("URL")] public string? Url { get; set; }

    [Column("DOCUMENT_KEY")] public string? DocumentKey { get; set; }

    [Column("QRCODE_DATA")] public string? QrCodeData { get; set; }

    [Column("CERTIFICATE_CODE")] public string? CertificateCode { get; set; }

    [Column("LOCKED")] public bool? Locked { get; set; }

    [Column("DATE")] public DateTime? Date { get; set; }

    [Column("REset_DATE")] public DateTime? ResetDate { get; set; }

    [Column("USER_ID")] public int? UserId { get; set; }

    [Column("USER_REset")] public int? UserReset { get; set; }
}

[Table("QUIT")]
public class Quit
{
    [Key] [Column("QUIT_ID")] public int QuitId { get; set; }

    [Column("STUDENT_ID")] public string? StudentId { get; set; }

    [Column("TERM_NO")] public int TermNo { get; set; }

    [Column("QUIT_DATE")] public DateTime QuitDate { get; set; }

    [Column("REASON_OF_QUIT")] public string? ReasonOfQuit { get; set; }

    [Column("GROUP_ID")] public int? GroupId { get; set; }

    [Column("PROMOTION_ID")] public int? PromotionId { get; set; }
}

[Table("RACE")]
public class Race
{
    [Key] [Column("RACE_ID")] public int RaceId { get; set; }

    [Column("RACE")] [StringLength(20)] public string? RaceName { get; set; }

    [Column("RACE_IN_KHMER")]
    [StringLength(20)]
    public string? RaceInKhmer { get; set; }
}

[Table("REEXAM_SCORE")]
public class ReexamScore
{
    [Key] [Column("REEXAM_SCORE_ID")] public int ReexamScoreId { get; set; }

    [Column("STUDENT_GROUP_ID")] public int StudentGroupId { get; set; }

    [Column("COURSE_ID")] public int CourseId { get; set; }

    [Column("TIME")] public int Time { get; set; }

    [Column("SCORE")] public float Score { get; set; }
}

[Table("REGISTRY")]
public class Registry
{
    [Key] [Column("REGISTRATION_ID")] public int RegistrationId { get; set; }

    [Column("STUDENT_ID")] public string? StudentId { get; set; }

    [Column("DEGREE_ID")] public int DegreeId { get; set; }

    [Column("SCHOOL_ID")] public int SchoolId { get; set; }

    [Column("PROMOTION_NO")] public int PromotionNo { get; set; }

    [Column("STAGE_NO")] public int StageNo { get; set; }

    [Column("TERM_NO")] public int TermNo { get; set; }

    [Column("STUDY_TIME")] public string? StudyTime { get; set; }

    [Column("REGISTRATION_DATE")] public DateTime RegistrationDate { get; set; }

    [Column("DONE_DATE")] public DateTime DoneDate { get; set; }

    [Column("HIGH_SCHOOL_RESULT")] public string? HighSchoolResult { get; set; }

    [Column("HIGH_SCHOOL_TABLE_NO")] public int? HighSchoolTableNo { get; set; }

    [Column("UPDATE_BY")] public string? UpdateBy { get; set; }

    [Column("UPDATE_DATE")] public DateTime? UpdateDate { get; set; }
}

[Table("REGISTRY_HISTORY")]
public class RegistryHistory
{
    [Key] [Column("REGISTRATION_ID")] public int RegistrationId { get; set; }

    [Column("STUDENT_ID")] public string? StudentId { get; set; }

    [Column("DEGREE_ID")] public int DegreeId { get; set; }

    [Column("SCHOOL_ID")] public int SchoolId { get; set; }

    [Column("PROMOTION_NO")] public int PromotionNo { get; set; }

    [Column("STAGE_NO")] public int StageNo { get; set; }

    [Column("TERM_NO")] public int TermNo { get; set; }

    [Column("STUDY_TIME")] public string? StudyTime { get; set; }

    [Column("REGISTRATION_DATE")] public DateTime RegistrationDate { get; set; }

    [Column("DONE_DATE")] public DateTime DoneDate { get; set; }

    [Column("HIGH_SCHOOL_RESULT")] public string? HighSchoolResult { get; set; }

    [Column("HIGH_SCHOOL_TABLE_NO")] public int? HighSchoolTableNo { get; set; }

    [Column("UPDATE_BY")] public string? UpdateBy { get; set; }

    [Column("UPDATE_DATE")] public DateTime? UpdateDate { get; set; }

    [Column("DATE")] public DateTime? Date { get; set; }

    [Column("BY")] public string? By { get; set; }
}

[Table("REPORT_OF_STUDENT_TOTAL_SCORE")]
public class ReportOfStudentTotalScore
{
    [Column("STUDENT_ID")] public string? StudentId { get; set; }

    [Column("STUDENT_NAME")] public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER")] public string? StudentNameInKhmer { get; set; }

    [Column("SEX")] public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH")] public DateTime? DateOfBirth { get; set; }

    [Column("PHONE")] public string? Phone { get; set; }

    [Column("TOTAL_SCORE")] public float? TotalScore { get; set; }
}

[Table("REPORT_PAGE_MARGIN")]
public class ReportPageMargin
{
    [Key]
    [Column("REPORT_PAGE_MARGIN_ID")]
    public int ReportPageMarginId { get; set; }

    [Column("REPORT_NAME")] public string? ReportName { get; set; }

    [Column("TOP")] public int Top { get; set; }

    [Column("BOTTOM")] public int Bottom { get; set; }

    [Column("LEFT")] public int Left { get; set; }

    [Column("RIGHT")] public int Right { get; set; }
}

[Table("ReportTempStudentFailStudy")]
public class ReportTempStudentFailStudy
{
    [Column("STUDENT_NAME")] public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER")] public string? StudentNameInKhmer { get; set; }

    [Column("SEX")] public string? Sex { get; set; }

    [Column("PHONE")] public string? Phone { get; set; }

    [Column("DATE_OF_BIRTH")] public DateTime? DateOfBirth { get; set; }

    [Column("NATIONALITY")] public string? Nationality { get; set; }

    [Column("NATIONALITY_IN_KHMER")] public string? NationalityInKhmer { get; set; }

    [Column("PROVINCE")] public string? Province { get; set; }

    [Column("PROVINCE_IN_KHMER")] public string? ProvinceInKhmer { get; set; }

    [Column("SCHOOL_NAME")] public string? SchoolName { get; set; }

    [Column("SCHOOL_NAME_IN_KHMER")] public string? SchoolNameInKhmer { get; set; }

    [Column("STATUS")] public string? Status { get; set; }

    [Column("DEGREE")] public string? Degree { get; set; }

    [Column("COURSE_FULL_NAME")] public string? CourseFullName { get; set; }

    [Column("COURSE_FULL_NAME_IN_KHMER")] public string? CourseFullNameInKhmer { get; set; }

    [Column("CREDIT")] public float? Credit { get; set; }

    [Column("NUMBER_OF_HOURS")] public float? NumberOfHours { get; set; }

    [Column("TERM_NO")] public int? TermNo { get; set; }

    [Column("MID_TERM_SCORE")] public float? MidTermScore { get; set; }

    [Column("FINAL_SCORE")] public float? FinalScore { get; set; }

    [Column("TOTAL")] public float? Total { get; set; }

    [Column("PROMOTION_NO")] public int? PromotionNo { get; set; }

    [Column("STAGE_NO")] public int? StageNo { get; set; }

    [Column("STUDENT_ID")] public string? StudentId { get; set; }
}

[Table("RESTUDY_TBL")]
public class RestudyTbl
{
    [Key] [Column("Restudy_ID")] public int RestudyId { get; set; }

    [Column("Term_No")] public int? TermNo { get; set; }

    [Column("Course_ID")] public int? CourseId { get; set; }

    [Column("Course_Full_Name")] public string? CourseFullName { get; set; }

    [Column("Replace_Course_ID")] public int? ReplaceCourseId { get; set; }

    [Column("Replace_Course_Full_Name")] public string? ReplaceCourseFullName { get; set; }

    [Column("Note")] public string? Note { get; set; }

    [Column("STUDENT_ID")] public string? StudentId { get; set; }
}

[Table("RESUME")]
public class Resume
{
    [Key] [Column("RESUME_ID")] public int ResumeId { get; set; }

    [Column("DATE_PAYMENT")] public DateTime DatePayment { get; set; }

    [Column("STUDENT_ID")] public string? StudentId { get; set; }

    [Column("FIELD_ID")] public int FieldId { get; set; }

    [Column("F_PROMOTION")] public int FPromotion { get; set; }

    [Column("F_YEAR")] public int FYear { get; set; }

    [Column("F_SEMESTER")] public int FSemester { get; set; }

    [Column("C_PROMOTION")] public int CPromotion { get; set; }

    [Column("STAGE")] public string? Stage { get; set; }

    [Column("C_YEAR")] public int CYear { get; set; }

    [Column("C_SEMESTER")] public int CSemester { get; set; }

    [Column("OTHER")] public string? Other { get; set; }

    [Column("TYPE")] public string? Type { get; set; }
}

[Table("ROOM")]
public class Room
{
    [Key] [Column("ROOM_ID")] public int RoomId { get; set; }

    [Column("ROOM_NAME")] public string? RoomName { get; set; }

    [Column("CAPACITY")] public int? Capacity { get; set; }

    [Column("ROOM_TYPE")] public string? RoomType { get; set; }
}

[Table("SCHOOL")]
public class School
{
    [Key] [Column("SCHOOL_ID")] public int SchoolId { get; set; }

    [Column("SCHOOL_NAME")] public string? SchoolName { get; set; }

    [Column("SCHOOL_NAME_IN_KHMER")] public string? SchoolNameInKhmer { get; set; }

    [Column("SCHOOL_CODE")] public string? SchoolCode { get; set; }

    [Column("FACULTY_ID")] public decimal FacultyId { get; set; }

    [Column("IS_FOUNDATION_SCHOOL")] public int IsFoundationSchool { get; set; }
}

[Table("SCORE")]
public class Score
{
    [Key] [Column("SCORE_ID")] public int ScoreId { get; set; }

    [Column("STUDENT_GROUP_ID")] public int StudentGroupId { get; set; }

    [Column("COURSE_ID")] public int CourseId { get; set; }

    [Column("MID_TERM_SCORE")] public float? MidTermScore { get; set; }

    [Column("FINAL_SCORE")] public float? FinalScore { get; set; }

    [Column("USERNAME")] public string? Username { get; set; }

    [Column("DATE_EDIT")] public DateTime? DateEdit { get; set; }

    [Column("UPDATEBY")] public string? UpdateBy { get; set; }

    [Column("UPDATEDATE")] public DateTime? UpdateDate { get; set; }

    [Column("IS_ALLOW")] public bool? IsAllow { get; set; }
}

[Table("SCORE_HISTORY")]
public class ScoreHistory
{
    [Key] [Column("SCORE_HISTORY_ID")] public int ScoreHistoryId { get; set; }

    [Column("STUDENT_ID")] public string? StudentId { get; set; }

    [Column("COURSE_ID")] public int CourseId { get; set; }

    [Column("MID_TERM_SCORE")] public float MidTermScore { get; set; }

    [Column("FINAL_SCORE")] public float FinalScore { get; set; }

    [Column("TERM_NO")] public int TermNo { get; set; }

    [Column("TIME")] public int Time { get; set; }

    [Column("USERNAME")] public string? Username { get; set; }

    [Column("DATE_EDIT")] public DateTime? DateEdit { get; set; }
}

[Table("SCORE_HISTORY_UPDATE")]
public class ScoreHistoryUpdate
{
    [Key] [Column("SCORE_ID")] public int ScoreId { get; set; }

    [Column("STUDENT_ID")] public string? StudentId { get; set; }

    [Column("COURSE_ID")] public int CourseId { get; set; }

    [Column("MID_TERM_SCORE")] public float? MidTermScore { get; set; }

    [Column("FINAL_SCORE")] public float? FinalScore { get; set; }

    [Column("USERNAME")] public string? Username { get; set; }

    [Column("DATE_EDIT")] public DateTime? DateEdit { get; set; }
}

[Table("SPO_REPORT_STUDENT_GROUP_STATISTIC")]
public class SpoReportStudentGroupStatistic
{
    [Column("Promotion_No")] public int? PromotionNo { get; set; }

    [Column("Stage_No")] public int? StageNo { get; set; }

    [Column("Term_ID")] public int? TermId { get; set; }

    [Column("Term_No")] public int? TermNo { get; set; }

    [Column("Start_Date")] public DateTime? StartDate { get; set; }

    [Column("End_Date")] public DateTime? EndDate { get; set; }

    [Column("Academic_Year_Start")] public DateTime? AcademicYearStart { get; set; }

    [Column("Academic_Year_End")] public DateTime? AcademicYearEnd { get; set; }

    [Column("Group_ID")] public int? GroupId { get; set; }

    [Column("Group_Name")] public string? GroupName { get; set; }

    [Column("School_ID")] public int? SchoolId { get; set; }

    [Column("School_Name")] public string? SchoolName { get; set; }

    [Column("School_Name_In_Khmer")] public string? SchoolNameInKhmer { get; set; }

    [Column("Field_ID")] public int? FieldId { get; set; }

    [Column("Field_Name")] public string? FieldName { get; set; }

    [Column("Degree_ID")] public int? DegreeId { get; set; }

    [Column("Degree")] public string? Degree { get; set; }

    [Column("Room_Name")] public string? RoomName { get; set; }

    [Column("Total_Female")] public int? TotalFemale { get; set; }

    [Column("Total_Student")] public int? TotalStudent { get; set; }
}

[Table("SPONSOR")]
public class Sponsor
{
    [Key] [Column("SPONSOR_ID")] public int SponsorId { get; set; }

    [Column("SPONSOR_NAME")] public string? SponsorName { get; set; }

    [Column("SPONSOR_NAME_IN_KHMER")] public string? SponsorNameInKhmer { get; set; }

    [Column("POSITION")] public string? Position { get; set; }

    [Column("NOTE")] public string? Note { get; set; }
}

[Table("STAGE")]
public class Stage
{
    [Key] [Column("STAGE_ID")] public int StageId { get; set; }

    [Column("PROMOTION_ID")] public int PromotionId { get; set; }

    [Column("STAGE_NO")] public int StageNo { get; set; }

    [Column("STATUS")] public string? Status { get; set; }
}

[Table("START_PROMOTION")]
public class StartPromotion
{
    [Key] [Column("START_PROMOTION_ID")] public int StartPromotionId { get; set; }

    [Column("DEGREE_ID")] public int DegreeId { get; set; }

    [Column("SCHOOL_ID")] public int SchoolId { get; set; }

    [Column("PROMOTION_NO")] public int PromotionNo { get; set; }
}

[Table("STATEMENT")]
public class Statement
{
    [Key] [Column("STATEMENT_ID")] public int StatementId { get; set; }

    [Column("STUDENT_ID")] public string? StudentId { get; set; }

    [Column("TERM_NO")] public int TermNo { get; set; }

    [Column("STATEMENT_DATE")] public DateTime StatementDate { get; set; }

    [Column("DUE_DATE")] public DateTime DueDate { get; set; }

    [Column("NOTE")] public string? Note { get; set; }
}

[Table("STUDENT")]
public class Student
{
    [Key]
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Column("STUDENT_NAME")]
    [StringLength(50)]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER")]
    [StringLength(50)]
    public string? StudentNameInKhmer { get; set; }

    [Column("SEX")] [StringLength(5)] public string? Sex { get; set; }

    [Column("DATE_OF_BIRTH")] public DateTime DateOfBirth { get; set; }

    [Column("PLACE_OF_BIRTH_ID")] public int? PlaceOfBirthId { get; set; }

    [Column("RACE_ID")] public int? RaceId { get; set; }

    [Column("NATIONALITY_ID")] public int? NationalityId { get; set; }

    [Column("MARITAL_STATUS")]
    [StringLength(10)]
    public string? MaritalStatus { get; set; }

    [Column("HIGH_SCHOOL_GRADUATED_YEAR")] public int? HighSchoolGraduatedYear { get; set; }

    [Column("FROM_PROVINCE_ID")] public int? FromProvinceId { get; set; }

    [Column("FROM_HIGH_SCHOOL_NAME_IN_KHMER")]
    [StringLength(100)]
    public string? FromHighSchoolNameInKhmer { get; set; }

    [Column("JOB_ID")] public int? JobId { get; set; }

    [Column("MOTHER_NAME_IN_KHMER")]
    [StringLength(50)]
    public string? MotherNameInKhmer { get; set; }

    [Column("MOTHER_OCCUPATION_IN_KHMER")]
    [StringLength(50)]
    public string? MotherOccupationInKhmer { get; set; }

    [Column("FATHER_NAME_IN_KHMER")]
    [StringLength(50)]
    public string? FatherNameInKhmer { get; set; }

    [Column("FATHER_OCCUPATION_IN_KHMER")]
    [StringLength(50)]
    public string? FatherOccupationInKhmer { get; set; }

    [Column("PHONE")] [StringLength(50)] public string? Phone { get; set; }

    [Column("EMAIL")] [StringLength(50)] public string? Email { get; set; }

    [Column("ADDRESS")] [StringLength(50)] public string? Address { get; set; }

    [Column("ADDRESS_IN_KHMER")]
    [StringLength(100)]
    public string? AddressInKhmer { get; set; }

    [Column("CONTACT_PERSON_ID")] public int? ContactPersonId { get; set; }

    [Column("FIELD_ID")] public int FieldId { get; set; }

    [Column("IS_PHOTO_RECEIVED")] public int? IsPhotoReceived { get; set; }

    [Column("NOTE")] [StringLength(100)] public string? Note { get; set; }

    [Column("STATUS")] [StringLength(20)] public string? Status { get; set; }

    [Column("IS_CONTINUED_STUDENT")] public int? IsContinuedStudent { get; set; }

    [Column("ASSOCIATE_TO_BACHELOR")] public int? AssociateToBachelor { get; set; }

    [Column("APPROVED_DATE")]
    [StringLength(50)]
    public string? ApprovedDate { get; set; }

    [Column("GRADUATE_LETTER_NO")]
    [StringLength(50)]
    public string? GraduateLetterNo { get; set; }

    [Column("IS_ACCEPT_CERTIFICATE")] public bool? IsAcceptCertificate { get; set; }

    [Column("ACCEPT_DATE")] public DateTime? AcceptDate { get; set; }

    [Column("CERTIFICATE_NO")]
    [StringLength(10)]
    public string? CertificateNo { get; set; }

    [Column("CERTIFICATE_OUT")] public bool? CertificateOut { get; set; }

    [Column("PHOTO")] public byte[]? Photo { get; set; }

    [Column("CARD_IS_PRINT")] public bool? CardIsPrint { get; set; }

    [Column("PRINT_DATE")] public DateTime? PrintDate { get; set; }

    [Column("FOUND_CERTIFICATE_IS_PRINT")] public bool? FoundCertificateIsPrint { get; set; }

    [Column("CHECKCOMPLETE")] public bool? CheckComplete { get; set; }

    [Column("CHECKCOMPLETENOTE")]
    [StringLength(50)]
    public string? CheckCompleteNote { get; set; }

    [Column("CHECKCOMPLETE_TERM")] public int? CheckCompleteTerm { get; set; }

    [Column("DISABILITYID")] public int? DisabilityId { get; set; }

    [Column("documentin")]
    [StringLength(50)]
    public string? DocumentIn { get; set; }

    [Column("documentout")]
    [StringLength(50)]
    public string? DocumentOut { get; set; }

    [Column("noteticket")]
    [StringLength(50)]
    public string? NoteTicket { get; set; }

    [Column("UPDATE_BY")]
    [StringLength(50)]
    public string? UpdateBy { get; set; }

    [Column("UPDATE_DATE")] public DateTime? UpdateDate { get; set; }

    [Column("IS_AUTHENTICATED")] public bool? IsAuthenticated { get; set; }

    [Column("AUTHENTICATED_NO")]
    [StringLength(50)]
    public string? AuthenticatedNo { get; set; }

    [Column("URL")]
    [StringLength(int.MaxValue)]
    public string? Url { get; set; }

    [Column("DOCUMENT_KEY")]
    [StringLength(int.MaxValue)]
    public string? DocumentKey { get; set; }

    [Column("QRCODE_DATA")]
    [StringLength(int.MaxValue)]
    public string? QrCodeData { get; set; }

    [Column("COUNT_PRINT")] public int? CountPrint { get; set; }

    [Column("IS_PRINT_CERTIFICATE")] public bool? IsPrintCertificate { get; set; }

    [Column("IS_REQUEST")] public bool? IsRequest { get; set; }

    [Column("GRADUATION_DATE")] public DateTime? GraduationDate { get; set; }

    [Column("CERTIFICATE_CODE")]
    [StringLength(50)]
    public string? CertificateCode { get; set; }

    [Column("IGNOR")] public bool? Ignor { get; set; }

    [Column("IGNOR_REASON")]
    [StringLength(100)]
    public string? IgnorReason { get; set; }

    [Column("LOCKED")] public bool? Locked { get; set; }

    [Column("HIGHT_SCHOOL_TYPE_ID")] public int? HighSchoolTypeId { get; set; }
}

[Table("STUDENT_ABSENT_RECORD")]
public class StudentAbsentRecord
{
    [Key] [Column("ABSENT_RECORD_ID")] public int AbsentRecordId { get; set; }

    [Column("STUDENT_ID")] public string? StudentId { get; set; }

    [Column("TERM_NO")] public int? TermNo { get; set; }

    [Column("MONTH_1")] public int? Month1 { get; set; }

    [Column("MONTH_2")] public int? Month2 { get; set; }

    [Column("MONTH_3")] public int? Month3 { get; set; }

    [Column("MONTH_4")] public int? Month4 { get; set; }

    [Column("MONTH_5")] public int? Month5 { get; set; }
}

[Table("STUDENT_ABSENT_RECORD_NEW")]
public class StudentAbsentRecordNew
{
    [Key] [Column("ABSENT_RECORD_ID")] public int AbsentRecordId { get; set; }

    [Column("STUDENT_ID")] public string? StudentId { get; set; }

    [Column("TERM_NO")] public int? TermNo { get; set; }

    [Column("SUBJECT_01")] public int? Subject01 { get; set; }

    [Column("SUBJECT_02")] public int? Subject02 { get; set; }

    [Column("SUBJECT_03")] public int? Subject03 { get; set; }

    [Column("SUBJECT_04")] public int? Subject04 { get; set; }

    [Column("SUBJECT_05")] public int? Subject05 { get; set; }

    [Column("SUBJECT_06")] public int? Subject06 { get; set; }

    [Column("DATE_ABSENT")] public DateTime? DateAbsent { get; set; }
}

[Table("STUDENT_CERTIFICATE")]
public class StudentCertificate
{
    [Key]
    [Column("STUDENT_CERTIFICATE_ID")]
    public int StudentCertificateId { get; set; }

    [Column("STUDENT_ID")] public string? StudentId { get; set; }

    [Column("CERTIFICATE_ID")] public int CertificateId { get; set; }

    [Column("GRADE")] public string? Grade { get; set; }

    [Column("IS_RECEIVED")] public int? IsReceived { get; set; }

    [Column("CERTIFICATE_ISSUE_NO")] public string? CertificateIssueNo { get; set; }
}

[Table("STUDENT_CERTIFICATE_RETURN")]
public class StudentCertificateReturn
{
    [Key]
    [Column("STUDENT_CERTIFICATE_RETURN_ID")]
    public int StudentCertificateReturnId { get; set; }

    [Column("RETURN_DATE")] public DateTime? ReturnDate { get; set; }

    [Column("STUDENT_ID")]
    [StringLength(10)]
    [Required]
    public string? StudentId { get; set; }

    [Column("CERTIFICATE_ID")] public int? CertificateId { get; set; }

    [Column("RECIEVE_PICTURE")] public int? RecievePicture { get; set; }

    [Column("OTHER")] [StringLength(50)] public string? Other { get; set; }
}

[Table("STUDENT_COMPLEMENTAL_PAYMENT")]
public class StudentComplementalPayment
{
    [Key]
    [Column("STUDENT_COMPLEMENTAL_PAYMENT_ID")]
    public int StudentComplementalPaymentId { get; set; }

    [Required]
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Required]
    [Column("INVOICE_NO")]
    [StringLength(10)]
    public string? InvoiceNo { get; set; }

    [Required] [Column("INVOICE_DATE")] public DateTime InvoiceDate { get; set; }

    [Required] [Column("SEMESTER")] public int Semester { get; set; }

    [Required]
    [Column("PAID", TypeName = "money")]
    public decimal Paid { get; set; }

    [Required]
    [Column("DEPOSIT", TypeName = "money")]
    public decimal Deposit { get; set; }

    [Column("DISCOUNT", TypeName = "money")]
    public decimal? Discount { get; set; }

    [Column("REASON_OF_DISCOUNT")]
    [StringLength(50)]
    public string? ReasonOfDiscount { get; set; }

    [Column("NOTE")] [StringLength(50)] public string? Note { get; set; }
}

[Table("STUDENT_DISCOUNT")]
public class StudentDiscount
{
    [Key] [Column("STUDENT_DISCOUNT_ID")] public int StudentDiscountId { get; set; }

    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Column("DISCOUNT")] public int? Discount { get; set; }

    [Column("TERM")] public int? Term { get; set; }

    [Column("NOTE")] [StringLength(50)] public string? Note { get; set; }
}

[Table("STUDENT_GROUP")]
public class StudentGroup
{
    [Key] [Column("STUDENT_GROUP_ID")] public int StudentGroupId { get; set; }

    [Required]
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; }

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required] [Column("GROUP_ID")] public int GroupId { get; set; }
}

[Table("STUDENT_GROUP_HISTORY")]
public class StudentGroupHistory
{
    [Key] [Column("ID")] public int Id { get; set; }

    [Required]
    [Column("STUDENT_GROUP_ID")]
    public int StudentGroupId { get; set; }

    [Required]
    [Column("STUDENT_ID")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required] [Column("GROUP_ID")] public int GroupId { get; set; }

    [Column("CHANGE_DATE")] public DateTime? ChangeDate { get; set; }

    [Column("USERNAME")]
    [StringLength(50)]
    public string? Username { get; set; }
}

[Table("STUDENT_HISTORY")]
public class StudentHistory
{
    [Key]
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Required]
    [Column("STUDENT_NAME", TypeName = "varchar(30)")]
    [StringLength(30)]
    public string? StudentName { get; set; }

    [Column("STUDENT_NAME_IN_KHMER", TypeName = "nvarchar(30)")]
    [StringLength(30)]
    public string? StudentNameInKhmer { get; set; }

    [Required]
    [Column("SEX", TypeName = "varchar(6)")]
    [StringLength(6)]
    public string? Sex { get; set; }

    [Required] [Column("DATE_OF_BIRTH")] public DateTime DateOfBirth { get; set; }

    [Column("PLACE_OF_BIRTH_ID")] public int? PlaceOfBirthId { get; set; }

    [Column("RACE_ID")] public int? RaceId { get; set; }

    [Column("NATIONALITY_ID")] public int? NationalityId { get; set; }

    [Column("MARITAL_STATUS", TypeName = "varchar(15)")]
    [StringLength(15)]
    public string? MaritalStatus { get; set; }

    [Column("HIGH_SCHOOL_GRADUATED_YEAR")] public int? HighSchoolGraduatedYear { get; set; }

    [Column("FROM_PROVINCE_ID")] public int? FromProvinceId { get; set; }

    [Column("FROM_HIGH_SCHOOL_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    [StringLength(100)]
    public string? FromHighSchoolNameInKhmer { get; set; }

    [Column("JOB_ID")] public int? JobId { get; set; }

    [Column("MOTHER_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    [StringLength(100)]
    public string? MotherNameInKhmer { get; set; }

    [Column("MOTHER_OCCUPATION_IN_KHMER", TypeName = "nvarchar(100)")]
    [StringLength(100)]
    public string? MotherOccupationInKhmer { get; set; }

    [Column("FATHER_NAME_IN_KHMER", TypeName = "nvarchar(100)")]
    [StringLength(100)]
    public string? FatherNameInKhmer { get; set; }

    [Column("FATHER_OCCUPATION_IN_KHMER", TypeName = "nvarchar(100)")]
    [StringLength(100)]
    public string? FatherOccupationInKhmer { get; set; }

    [Column("PHONE", TypeName = "varchar(45)")]
    [StringLength(45)]
    public string? Phone { get; set; }

    [Column("EMAIL", TypeName = "varchar(40)")]
    [StringLength(40)]
    public string? Email { get; set; }

    [Column("ADDRESS", TypeName = "varchar(150)")]
    [StringLength(150)]
    public string? Address { get; set; }

    [Column("ADDRESS_IN_KHMER", TypeName = "nvarchar(200)")]
    [StringLength(200)]
    public string? AddressInKhmer { get; set; }

    [Column("CONTACT_PERSON_ID")] public int? ContactPersonId { get; set; }

    [Required] [Column("FIELD_ID")] public int FieldId { get; set; }

    [Column("IS_PHOTO_RECEIVED")] public int? IsPhotoReceived { get; set; }

    [Column("NOTE", TypeName = "nvarchar(600)")]
    [StringLength(600)]
    public string? Note { get; set; }

    [Required]
    [Column("STATUS", TypeName = "varchar(15)")]
    [StringLength(15)]
    public string? Status { get; set; }

    [Column("IS_CONTINUED_STUDENT")] public int? IsContinuedStudent { get; set; }

    [Column("ASSOCIATE_TO_BACHELOR")] public int? AssociateToBachelor { get; set; }

    [Column("APPROVED_DATE", TypeName = "text")]
    public string? ApprovedDate { get; set; }

    [Column("GRADUATE_LETTER_NO", TypeName = "varchar(10)")]
    [StringLength(10)]
    public string? GraduateLetterNo { get; set; }

    [Column("IS_ACCEPT_CERTIFICATE")] public bool? IsAcceptCertificate { get; set; }

    [Column("ACCEPT_DATE")] public DateTime? AcceptDate { get; set; }

    [Column("CERTIFICATE_NO", TypeName = "varchar(10)")]
    [StringLength(10)]
    public string? CertificateNo { get; set; }

    [Column("CERTIFICATE_OUT")] public bool? CertificateOut { get; set; }

    [Column("PHOTO", TypeName = "image")] public byte[]? Photo { get; set; }

    [Column("CARD_IS_PRINT")] public bool? CardIsPrint { get; set; }

    [Column("PRINT_DATE")] public DateTime? PrintDate { get; set; }

    [Column("FOUND_CERTIFICATE_IS_PRINT")] public bool? FoundCertificateIsPrint { get; set; }

    [Column("CHECKCOMPLETE")] public bool? CheckComplete { get; set; }

    [Column("CHECKCOMPLETENOTE", TypeName = "nvarchar(500)")]
    [StringLength(500)]
    public string? CheckCompleteNote { get; set; }

    [Column("CHECKCOMPLETE_TERM")] public int? CheckCompleteTerm { get; set; }

    [Column("DISABILITYID")] public int? DisabilityId { get; set; }

    [Column("documentin", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    public string? DocumentIn { get; set; }

    [Column("documentout", TypeName = "nvarchar(50)")]
    [StringLength(50)]
    public string? DocumentOut { get; set; }

    [Column("noteticket", TypeName = "nvarchar(200)")]
    [StringLength(200)]
    public string? NoteTicket { get; set; }

    [Column("UPDATE_BY", TypeName = "varchar(50)")]
    [StringLength(50)]
    public string? UpdateBy { get; set; }

    [Column("UPDATE_DATE")] public DateTime? UpdateDate { get; set; }

    [Column("IS_AUTHENTICATED")] public bool? IsAuthenticated { get; set; }

    [Column("AUTHENTICATED_NO", TypeName = "varchar(50)")]
    [StringLength(50)]
    public string? AuthenticatedNo { get; set; }

    [Column("URL", TypeName = "varchar(max)")]
    public string? Url { get; set; }

    [Column("DOCUMENT_KEY", TypeName = "varchar(max)")]
    public string? DocumentKey { get; set; }

    [Column("QRCODE_DATA", TypeName = "varchar(max)")]
    public string? QrCodeData { get; set; }

    [Column("COUNT_PRINT")] public int? CountPrint { get; set; }

    [Column("IS_PRINT_CERTIFICATE")] public bool? IsPrintCertificate { get; set; }

    [Column("IS_REQUEST")] public bool? IsRequest { get; set; }

    [Column("GRADUATION_DATE", TypeName = "date")]
    public DateTime? GraduationDate { get; set; }

    [Column("CERTIFICATE_CODE", TypeName = "varchar(50)")]
    [StringLength(50)]
    public string? CertificateCode { get; set; }

    [Column("IGNOR")] public bool? Ignor { get; set; }

    [Column("IGNOR_REASON", TypeName = "nvarchar(100)")]
    [StringLength(100)]
    public string? IgnorReason { get; set; }

    [Column("LOCKED")] public bool? Locked { get; set; }

    [Column("HIGHT_SCHOOL_TYPE_ID")] public int? HighSchoolTypeId { get; set; }

    [Column("DATE")] public DateTime? Date { get; set; }

    [Column("BY", TypeName = "nvarchar(100)")]
    [StringLength(100)]
    public string? By { get; set; }
}

[Table("STUDENT_JOB")]
public class StudentJob
{
    [Key] [Column("JOB_ID")] public int JobId { get; set; }

    [Required]
    [Column("JOB", TypeName = "varchar(30)")]
    [StringLength(30)]
    public string? Job { get; set; }

    [Required]
    [Column("JOB_IN_KHMER", TypeName = "varchar(30)")]
    [StringLength(30)]
    public string? JobInKhmer { get; set; }
}

[Table("STUDENT_LETTER")]
public class StudentLetter
{
    [Key] [Column("STUDENT_LETTER_ID")] public int StudentLetterId { get; set; }

    [Required]
    [Column("STUDENT_ID", TypeName = "varchar(50)")]
    [StringLength(50)]
    public string? StudentId { get; set; }

    [Required] [Column("LETTER_ID")] public int LetterId { get; set; }

    [Column("DONE_DATE_1")] public DateTime? DoneDate1 { get; set; }

    [Column("DONE_DATE_2")] public DateTime? DoneDate2 { get; set; }

    [Column("ISSUED_NO", TypeName = "varchar(10)")]
    [StringLength(10)]
    public string? IssuedNo { get; set; }

    [Column("ISSUED_DATE")] public DateTime? IssuedDate { get; set; }

    [Required]
    [Column("AUTHOR", TypeName = "varchar(30)")]
    [StringLength(30)]
    public string? Author { get; set; }

    [Column("RECEIVE_DATE")] public DateTime? ReceiveDate { get; set; }
}

[Table("STUDENT_LIBRARY_ATTENDANT")]
public class StudentLibraryAttendant
{
    [Key]
    [Column("STUDENT_LIBRARY_ATTENDANT_ID")]
    public int StudentLibraryAttendantId { get; set; }

    [Required] [Column("CHECK_DATE")] public DateTime CheckDate { get; set; }

    [Required]
    [Column("CHECK_TIME_IN", TypeName = "varchar(10)")]
    [StringLength(10)]
    public string? CheckTimeIn { get; set; }

    [Column("CHECK_TIME_OUT", TypeName = "varchar(10)")]
    [StringLength(10)]
    public string? CheckTimeOut { get; set; }

    [Required]
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Column("IS_OUT")] public int? IsOut { get; set; }
}

[Table("STUDENT_ORIENTED_SUBJECT_PAYMENT")]
public class StudentOrientedSubjectPayment
{
    [Key]
    [Column("STUDENT_ORIENTED_SUBJECT_PAYMENT_ID")]
    public int StudentOrientedSubjectPaymentId { get; set; }

    [Required]
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Required]
    [Column("INVOICE_NO", TypeName = "varchar(20)")]
    [StringLength(20)]
    public string? InvoiceNo { get; set; }

    [Required] [Column("INVOICE_DATE")] public DateTime InvoiceDate { get; set; }

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required] [Column("PAID")] public double Paid { get; set; }

    [Column("NOTE", TypeName = "varchar(100)")]
    [StringLength(100)]
    public string? Note { get; set; }
}

[Table("STUDENT_PROBLEM")]
public class StudentProblem
{
    [Key] [Column("STUDENTPROBLEMID")] public int StudentProblemId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    [StringLength(10)]
    public string? StudentId { get; set; }

    [Column("DEGREE_ID")] public int? DegreeId { get; set; }

    [Column("SCHOOL_ID")] public int? SchoolId { get; set; }

    [Column("PROMOTION_ID")] public int? PromotionId { get; set; }

    [Column("STAGE_ID")] public int? StageId { get; set; }

    [Column("TERM_ID")] public int? TermId { get; set; }

    [Column("FIELD_ID")] public int? FieldId { get; set; }

    [Column("GROUP_ID")] public int? GroupId { get; set; }

    [Column("ACADEMIC_PROBLEM", TypeName = "nvarchar(200)")]
    [StringLength(200)]
    public string? AcademicProblem { get; set; }

    [Column("FINANCE_PROBLEM", TypeName = "nvarchar(200)")]
    [StringLength(200)]
    public string? FinanceProblem { get; set; }
}

[Table("STUDENT_REEXAM_PAYMENT")]
public class StudentReexamPayment
{
    [Key]
    [Column("STUDENT_REEXAM_PAYMENT_ID")]
    public int StudentReexamPaymentId { get; set; }

    [Required]
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    [StringLength(10)]
    public string? StudentId { get; set; } = null!;

    [Required]
    [Column("INVOICE_NO", TypeName = "varchar(20)")]
    [StringLength(20)]
    public string? InvoiceNo { get; set; } = null!;

    [Required] [Column("INVOICE_DATE")] public DateTime InvoiceDate { get; set; }

    [Required] [Column("PAID")] public double Paid { get; set; }

    [Column("NOTE", TypeName = "varchar(100)")]
    [StringLength(100)]
    public string? Note { get; set; }
}

[Table("STUDENT_REEXAM_PAYMENT_DETAIL")]
public class StudentReexamPaymentDetail
{
    [Key]
    [Column("STUDENT_REEXAM_PAYMENT_DETAIL_ID")]
    public int StudentReexamPaymentDetailId { get; set; }

    [Required]
    [Column("STUDENT_REEXAM_PAYMENT_ID")]
    public int StudentReexamPaymentId { get; set; }

    [Required] [Column("COURSE_ID")] public int CourseId { get; set; }

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required]
    [Column("TIME", TypeName = "varchar(10)")]
    [StringLength(10)]
    public string? Time { get; set; } = null!;
}

[Table("STUDENT_REEXAM_STATE_PAYMENT")]
public class StudentReexamStatePayment
{
    [Key]
    [Column("STUDENT_REEXAM_STATE_PAYMENT_ID")]
    public int StudentReexamStatePaymentId { get; set; }

    [Required]
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    [StringLength(10)]
    public string? StudentId { get; set; } = null!;

    [Required]
    [Column("INVOICE_NO", TypeName = "varchar(20)")]
    [StringLength(20)]
    public string? InvoiceNo { get; set; } = null!;

    [Required] [Column("INVOICE_DATE")] public DateTime InvoiceDate { get; set; }

    [Required] [Column("PAID")] public float Paid { get; set; }

    [Column("NOTE", TypeName = "varchar(100)")]
    [StringLength(100)]
    public string? Note { get; set; }
}

[Table("STUDENT_SCHOOLARSHIP")]
public class StudentSchoolarship
{
    [Key]
    [Column("STUDENT_SCHOOLARSHIP_ID")]
    public int StudentSchoolarshipId { get; set; }

    [Required]
    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    [StringLength(10)]
    public string? StudentId { get; set; } = null!;

    [Required] [Column("TERM_NO")] public int TermNo { get; set; }

    [Required]
    [Column("IS_FULL_SCHOOLARSHIP")]
    public int IsFullSchoolarship { get; set; }

    [Required] [Column("AMOUNT")] public int Amount { get; set; }

    [Required] [Column("SPONSOR_ID")] public int SponsorId { get; set; }
}

[Table("StudentStatisticByAcademicYear2Type1")]
public class StudentStatisticByAcademicYear2Type1
{
    [Column("Field_Id")] public int? FieldId { get; set; }

    [Column("Field_Name", TypeName = "varchar(50)")]
    public string? FieldName { get; set; }

    [Column("LessThan18Total")] public int? LessThan18Total { get; set; }

    [Column("LessThan18Female")] public int? LessThan18Female { get; set; }

    [Column("Total18")] public int? Total18 { get; set; }

    [Column("Female18")] public int? Female18 { get; set; }

    [Column("Total19")] public int? Total19 { get; set; }

    [Column("Female19")] public int? Female19 { get; set; }

    [Column("Total20")] public int? Total20 { get; set; }

    [Column("Female20")] public int? Female20 { get; set; }

    [Column("Total21")] public int? Total21 { get; set; }

    [Column("Female21")] public int? Female21 { get; set; }

    [Column("Total22")] public int? Total22 { get; set; }

    [Column("Female22")] public int? Female22 { get; set; }

    [Column("Total23")] public int? Total23 { get; set; }

    [Column("Female23")] public int? Female23 { get; set; }

    [Column("Total24")] public int? Total24 { get; set; }

    [Column("Female24")] public int? Female24 { get; set; }

    [Column("Total25")] public int? Total25 { get; set; }

    [Column("Female25")] public int? Female25 { get; set; }

    [Column("Total26")] public int? Total26 { get; set; }

    [Column("Female26")] public int? Female26 { get; set; }

    [Column("MoreThan26Total")] public int? MoreThan26Total { get; set; }

    [Column("MoreThan26Female")] public int? MoreThan26Female { get; set; }
}

[Table("StudentStatisticByAcademicYear2Type2")]
public class StudentStatisticByAcademicYear2Type2
{
    [Column("Province_Id")] public int? ProvinceId { get; set; }

    [Column("Province", TypeName = "varchar(30)")]
    public string? Province { get; set; }

    [Column("FoundationYearTotal")] public int? FoundationYearTotal { get; set; }

    [Column("FoundationYearFemale")] public int? FoundationYearFemale { get; set; }

    [Column("Year2Total")] public int? Year2Total { get; set; }

    [Column("Year2Female")] public int? Year2Female { get; set; }

    [Column("Year3Total")] public int? Year3Total { get; set; }

    [Column("Year3Female")] public int? Year3Female { get; set; }

    [Column("Year4Total")] public int? Year4Total { get; set; }

    [Column("Year4Female")] public int? Year4Female { get; set; }

    [Column("Year5Total")] public int? Year5Total { get; set; }

    [Column("Year5Female")] public int? Year5Female { get; set; }

    [Column("Year6Total")] public int? Year6Total { get; set; }

    [Column("Year6Female")] public int? Year6Female { get; set; }

    [Column("Year7Total")] public int? Year7Total { get; set; }

    [Column("Year7Female")] public int? Year7Female { get; set; }
}

[Table("STUDY_TIME")]
public class StudyTime
{
    [Key]
    [Column("STUDY_TIME", TypeName = "varchar(15)")]
    public string? StudyTimeValue { get; set; }
}

[Table("SUPPRESS")]
public class Suppress
{
    [Key] [Column("SUPPRESS_ID")] public int SuppressId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; } = null!;

    [Column("TERM_NO")] public int TermNo { get; set; }

    [Column("SUPPRESS_DATE")] public DateTime SuppressDate { get; set; }

    [Column("EXPRESS_DATE")] public DateTime? ExpressDate { get; set; }

    [Column("REASON_OF_SUPPRESS", TypeName = "nvarchar(200)")]
    public string? ReasonOfSuppress { get; set; }
}

[Table("SUPPRESS_NEW")]
public class SuppressNew
{
    [Key] [Column("SUPPRESS_ID")] public int SuppressId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; } = null!;

    [Column("TERM_NO")] public int TermNo { get; set; }

    [Column("SUPPRESS_DATE")] public DateTime SuppressDate { get; set; }

    [Column("EXPRESS_DATE")] public DateTime? ExpressDate { get; set; }

    [Column("REASON_OF_SUPPRESS", TypeName = "varchar(50)")]
    public string? ReasonOfSuppress { get; set; }
}

[Table("SUSPEND")]
public class Suspend
{
    [Key] [Column("SUSPEND_ID")] public int SuspendId { get; set; }

    [Column("STUDENT_ID", TypeName = "varchar(10)")]
    public string? StudentId { get; set; } = null!;

    [Column("TERM_NO")] public int TermNo { get; set; }

    [Column("GROUP_ID")] public int GroupId { get; set; }

    [Column("PROMOTION_ID")] public int PromotionId { get; set; }

    [Column("FROM_DATE")] public DateTime FromDate { get; set; }

    [Column("TO_DATE")] public DateTime ToDate { get; set; }

    [Column("REASON_OF_SUSPEND", TypeName = "nvarchar(100)")]
    public string? ReasonOfSuspend { get; set; }
}

[Table("TERM")]
public class Term
{
    [Key] [Column("TERM_ID")] public int TermId { get; set; }

    [Column("STAGE_ID")] public int StageId { get; set; }

    [Column("TERM_NO")] public int TermNo { get; set; }

    [Column("START_DATE")] public DateTime StartDate { get; set; }

    [Column("END_DATE")] public DateTime EndDate { get; set; }

    [Column("ACADEMIC_YEAR_START")] public int AcademicYearStart { get; set; }

    [Column("ACADEMIC_YEAR_END")] public int AcademicYearEnd { get; set; }

    [Column("STATUS", TypeName = "varchar(10)")]
    public string? Status { get; set; } = null!;

    [Column("START_PAYMENT_DATE", TypeName = "date")]
    public DateTime? StartPaymentDate { get; set; }
}

[Table("TEST_SCORE")]
public class TestScore
{
    [Key] [Column("SCORE_ID")] public int ScoreId { get; set; }

    [Column("STUDENT_GROUP_ID")] public int StudentGroupId { get; set; }

    [Column("COURSE_ID")] public int CourseId { get; set; }

    [Column("MID_TERM_SCORE")] public double? MidTermScore { get; set; }

    [Column("FINAL_SCORE")] public double? FinalScore { get; set; }
}

[Table("TIME_TABLE")]
public class TimeTable
{
    [Key] [Column("TIME_TABLE_ID")] public int TimeTableId { get; set; }

    [Column("GROUPING_DAY")] public string? GroupingDay { get; set; } = null!;

    [Column("PART_OF_DAY")] public string? PartOfDay { get; set; } = null!;

    [Column("TIME")] public string? Time { get; set; } = null!;
}

[Table("TUITION_FEE")]
public class TuitionFee
{
    [Key] [Column("TUITION_FEE_ID")] public int TuitionFeeId { get; set; }

    [Column("PROMOTION_ID")] public int PromotionId { get; set; }

    [Column("TERM_NO")] public int TermNo { get; set; }

    [Column("FEE", TypeName = "money")] public decimal Fee { get; set; }
}

[Table("UNIVERSITY")]
public class University
{
    [Key] [Column("UNIVERSITY_ID")] public int UniversityId { get; set; }

    [Column("UNIVERSITY_NAME")] public string? UniversityName { get; set; } = null!;

    [Column("UNIVERSITY_NAME_IN_KHMER")] public string? UniversityNameInKhmer { get; set; } = null!;

    [Column("ABBREVIATION_NAME")] public string? AbbreviationName { get; set; } = null!;
}

[Table("USER")]
public class User
{
    [Key] [Column("USER_ID")] public int UserId { get; set; }

    [Column("USER_NAME")] public string? UserName { get; set; } = null!;

    [Column("PASSWORD")] public string? Password { get; set; } = null!;

    [Column("USER_GROUP")] public string? UserGroup { get; set; } = null!;

    [Column("STATUS")] public string? Status { get; set; } = null!;
}

[Table("USER_PRIVILEDGE")]
public class UserPriviledge
{
    [Key] [Column("USER_PRIVILEDGE_ID")] public int UserPriviledgeId { get; set; }

    [Column("USER_ID")] public int UserId { get; set; }

    [Column("PRIVILEDGE_ID")] public int PriviledgeId { get; set; }
}

[Table("USER_SCHOOL")]
public class UserSchool
{
    [Column("USER_ID")] public int UserId { get; set; }

    [Column("SCHOOL_ID")] public int SchoolId { get; set; }

    [Key] [Column("USER_SCHOOL_ID")] public int UserSchoolId { get; set; }
}