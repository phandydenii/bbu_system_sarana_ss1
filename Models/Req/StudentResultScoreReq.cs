namespace BBU_SYSTEM.Models.Req;

public class StudentResultScoreReq
{
    public int StudentGroupId { get; set; }
    public string? StudentId { get; set; }

    public int PromotionYearStart { get; set; }
    public int PromotionYearEnd { get; set; }

    public float Credit { get; set; }
    public string? Type { get; set; }

    public int TermYearStart { get; set; }
    public int TermYearEnd { get; set; }
    public int Term { get; set; }

    public string? CourseCode { get; set; }
    public int CourseID { get; set; }

    public string? CourseFullName { get; set; }
    public string? CourseFullNameKhmer { get; set; }

    public string? Degree { get; set; }

    public float Mid { get; set; }
    public float Final { get; set; }
    public float Total { get; set; }
}