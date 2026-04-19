namespace BBU_SYSTEM.ViewModel;

public class AssignGroupViewModel
{
    public int DegreeId { get; set; }
    public int SchoolId { get; set; }
    public int FieldId { get; set; }
    public int PromotionId { get; set; }
    public int PromotionNo { get; set; }
    public int AcademicYear { get; set; }
    public int StageId { get; set; }
    public int StageNo { get; set; }
    public int TermId { get; set; }
    public string? StudyTime { get; set; }
    public int Group { get; set; }
    public bool IsAll { get; set; } =false;
}