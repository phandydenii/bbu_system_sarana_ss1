namespace BBU_SYSTEM.ViewModel.Report;

public class RegisteredViewModel
{
    public int DegreeId { get; set; } = 0;
    public int PromotionNo { get; set; } = 0;
    public int StageNo { get; set; } = 0;
    public DateTime? FromDate { get; set; } = null;
    public DateTime? ToDate { get; set; } = null;
    public string? Receiver { get; set; } = "";
    public string? Reporter { get; set; } = "";
}