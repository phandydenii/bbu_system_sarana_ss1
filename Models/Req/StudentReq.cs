namespace BBU_SYSTEM.Models.Req;

public class StudentFilterReq
{
    public int DegreeId { get; set; } = 0;
    public int SchoolId { get; set; } = 0;
    public int PromotionId { get; set; } = 0;
    public int FieldId { get; set; } = 0;
    public int StageId { get; set; } = 0; 
    public int TermId { get; set; } = 0;
    public int GroupId { get; set; } = 0;
    public string Filter { get; set; } = "";
}

public class ChangeSchoolReq
{
    public required string StudentId { get; set; }
}