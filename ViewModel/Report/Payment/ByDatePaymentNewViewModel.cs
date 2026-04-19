namespace BBU_SYSTEM.ViewModel.Report.Payment;

public class ByDatePaymentNewViewModel
{
    public int DegreeId { get; set; } = 0;
    public int SchoolId { get; set; } = 0;
    public int PromotionNo { get; set; } = 0;
    public int StageNo { get; set; } = 0;
    public int TermNo { get; set; } = 0;
    public DateTime? FromDate { get; set; } = null;
    public DateTime? ToDate { get; set; } = null;
    public int CategoryId { get; set; } = 0; 
}