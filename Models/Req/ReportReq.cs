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