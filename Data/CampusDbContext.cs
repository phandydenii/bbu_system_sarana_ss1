using BBU_SYSTEM.Models;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Data;

public class CampusDbContext(DbContextOptions<CampusDbContext> options) : DbContext(options)
{
    public DbSet<UserActivityLog> UserActivityLogs { get; set; }
    public DbSet<Branch> TblBranch { get; set; }
    public DbSet<ChangeBranch> TblChangeBranch { get; set; }
    public DbSet<Degree> TblDegree { get; set; }
    public DbSet<Faculty> TblFaculty { get; set; }
    public DbSet<Field> TblField { get; set; }
    public DbSet<FieldCertificate> TblFieldCertificate { get; set; }
    public DbSet<Group> TblGroup { get; set; }
    public DbSet<GroupRoom> TblGroupRoom { get; set; }
    public DbSet<Promotion> TblPromotion { get; set; }
    public DbSet<School> TblSchool { get; set; }
    public DbSet<Student> TblStudent { get; set; }
    public DbSet<Term> TblTerm { get; set; }
    public DbSet<Stage> TblStage { get; set; }
    public DbSet<Registry> TblRegistry { get; set; }
    public DbSet<Invoice> TblInvoice { get; set; }
    public DbSet<InvoiceDetail> TblInvoiceDetail { get; set; }
    public DbSet<User> TblUser { get; set; }

    public DbSet<Privilege> TblPrivilege { get; set; }
    public DbSet<PrivilegeGroup> TblPrivilegeGroup { get; set; }
    public DbSet<UserPriviledge> TblUserPrivilege { get; set; }
    public DbSet<ContactPerson> TblContactPerson { get; set; }
    public DbSet<StudentScholarship> TblScholarship { get; set; }
    public DbSet<StudentCertificate> TblStudentCertificate { get; set; }
    public DbSet<Extend> TblExtend { get; set; }
    public DbSet<ExchangeRate> TblExchangeRate { get; set; }
    public DbSet<ExchangeRateDetail> TblExchangeRateDetail { get; set; }
    public DbSet<Resume> TblResume { get; set; }
    public DbSet<StudentJob> TblStudentJob { get; set; }
    public DbSet<HighSchool> TblHighSchool { get; set; }
    public DbSet<Disability> TblDisability { get; set; }
    public DbSet<StudentGroup> TblStudentGroup { get; set; }
    public DbSet<Nationality> TblNationality { get; set; }
    public DbSet<Race> TblRace { get; set; }
    public DbSet<Province> TblProvince { get; set; }
    public DbSet<University> TblUsersity { get; set; }
    public DbSet<StudentDiscount> TblStudentDiscount { get; set; }
    public DbSet<Sponsor> TblSponsor { get; set; } 
    public DbSet<StudyTime> TblStudyTime { get; set; }
    public DbSet<Room> TblRoom { get; set; }
    public DbSet<DoctoralContract> TblDoctoralContract { get; set; }
    public DbSet<Course> TblCourses { get; set; }

    public DbSet<BookClothes> TblBookClothes { get; set; }
    public DbSet<Booking> TblBooking { get; set; }
    public DbSet<BookingDetail> TblBookingDetail { get; set; }
    public DbSet<BookingItem> TblBookingItem { get; set; }
    public DbSet<BookingReturn> TblBookingReturn { get; set; }
    public DbSet<BookingReturnDetail> TblBookingReturnDetail { get; set; }
    public DbSet<Quit> TblQuit { get; set; }
    public DbSet<Suspend> TblSuspend { get; set; }
    public DbSet<Suppress> TblSuppress { get; set; } 
    public DbSet<Payment> TblPayment { get; set; }
    public DbSet<Category> TblCategory { get; set; }
    public DbSet<LetterCategory> TblLetterCategory { get; set; }
    public DbSet<Product> TblProduct { get; set; }
    public DbSet<ProductDetail> TblProductDetails { get; set; }
    public DbSet<Letter> TblLetter { get; set; }
    public DbSet<StudentLetter> TblStudentLetter { get; set; }
    public DbSet<LetterCertification> TblLetterCertifications { get; set; }
    public DbSet<Score> TblScore { get; set; }
    public DbSet<OtherBranchScore> TblOtherBranchScores { get; set; }
    public DbSet<CourseSchool> TblCourseSchools { get; set; }
    public DbSet<CourseTerm> TblCourseTerms { get; set; }
    public DbSet<CourseCode> TblCourseCodes { get; set; }
    public DbSet<Province> TblProvinces { get; set; }
    public DbSet<QrCodeCertificate> TblQrCodeCertificates { get; set; }
    public DbSet<ExternalScore> TblExternalScores { get; set; }
    public DbSet<ComplementSemesterScore> TblComplementSemesterScores { get; set; }
    public DbSet<ComplementOrientedCourseScore> TblComplementOrientedCourseScores { get; set; }
    public DbSet<ComplementFailedCourseScore> TblComplementFailedCourseScores { get; set; }
    public DbSet<DailyReport> TblDailyReport { get; set; }
    public DbSet<DailyReportImages> TblDailyReportImages { get; set; }
    public DbSet<Certificate> TblCertificate { get; set; }
    public DbSet<StudentComplementalPayment> TblStudentComplementPayment { get; set; }
    public DbSet<ScoreHistory> TblScoreHistory { get; set; }
    public DbSet<ScoreHistoryUpdate> TblScoreHistoryUpdate { get; set; }
    public DbSet<SummaryReport> TblSummaryReport { get; set; }
    public DbSet<StudentReexamPayment> TblReExamPayment { get; set; }
    public DbSet<StudentReexamPaymentDetail> TblReExamPaymentDetail { get; set; }
}