namespace BBU_SYSTEM.ViewModel;

public class ScoreViewModel
{
    public int ScoreId { get; set; }
    public int StudentGroupId { get; set; }
    public int CourseId { get; set; }
    public string? CourseName { get; set; }
    public string? CourseNameKhmer { get; set; }
    public decimal MidTermScore { get; set; }
    public decimal FinalScore { get; set; }
    public string? Type { get; set; }
    public bool? IsAllow { get; set; }
}

