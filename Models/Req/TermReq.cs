namespace BBU_SYSTEM.Models.Req;

public class TermReq
{
    public int TermNo { get; set; }
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}