namespace BBU_SYSTEM.ViewModel.Report.Academic;

public class TranscriptViewModel
{
    public string? StudentId { get; set; } = "";
    public int Term { get; set; } = 0;
    public int ReportType { get; set; } = 0;
    public bool IsSuccess { get; set; } = false;
    public string? Title { get; set; } = "";
    public string? Campus { get; set; }= "";
    public string? BranchName { get; set; }= "";
    public string? Signature { get; set; }= "";
    public string? ShortName { get; set; }= "";
    public string? Description { get; set; }= "";
    public string? Total { get; set; }= "";
    public bool IsKhmer { get; set; } = false;
}