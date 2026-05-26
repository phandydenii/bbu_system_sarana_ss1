namespace BBU_SYSTEM.Models.Req;

public class AssignCourseToSchoolReq
{
    public required int SchoolId { get; set; }
    public List<int> CourseIds { get; set; } = new();
}