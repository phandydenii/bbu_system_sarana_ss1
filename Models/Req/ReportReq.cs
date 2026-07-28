namespace BBU_SYSTEM.Models.Req;

public class AdministrationNotPaymentReq
{
    public int DegreeId  { get; set; }
    public int SchoolId  { get; set; }
    public int FieldId  { get; set; }
    public int PromotionId  { get; set; }
    public int Term  { get; set; }
    public DateTime? FromDate  { get; set; }
    public DateTime? ToDate  { get; set; }
}

public class RegistrationStudentGenerateReq
{
    public int DegreeId { get; set; }
    public int PromotionNo { get; set; }
    public int StageNo { get; set; } 
    public DateTime? FromDate {get;set;}
    public DateTime? ToDate { get; set; }
    public string? Reporter { get; set; }
    public string? Receiver { get; set; }
}

public class StudentListAcceptCertificateGenerateReq
{ 
    public int DegreeId { get; set; }
    public int SchoolId { get; set; } 
    public int FromPromotionNo { get; set; }
    public int ToPromotionNo { get; set; } 
    public DateTime? FromDate {get;set;}
    public DateTime? ToDate { get; set; } 
    public string? Title { get; set; }
    public bool? IsAcceptCertificate { get; set; } = false;
    public string? Status { get; set; }
}