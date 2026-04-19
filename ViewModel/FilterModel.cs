namespace BBU_SYSTEM.ViewModel;

public class FilterModel
{
    public int? DegreeId { get; set; }
    public int? SchoolId { get; set; }
    public int? FieldId { get; set; }
    public int? PromotionId { get; set; }
    public int? PromotionNo { get; set; }
    public int? StageId { get; set; }
    public int? StageNo { get; set; }
    public int? TermId { get; set; }
    public int? TermNo { get; set; }
    public int? GroupId { get; set; }
    public string? StudyTime { get; set; } = string.Empty;
    public string? FromDate { get; set; } = string.Empty;
    public string? ToDate { get; set; } = string.Empty;
}