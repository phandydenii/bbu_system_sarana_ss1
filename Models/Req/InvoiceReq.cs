namespace BBU_SYSTEM.Models.Req;

public class InvoiceReq
{
    public int InvoiceId { get; set; } = 0;
    public int? InvoiceNo { get; set; } = 0;
    public string? YearNumber { get; set; } = "";
    public DateTime? InvoiceDate { get; set; } = DateTime.Now;
    public string? StudentId { get; set; } = "";
    public string? DegreeId { get; set; } = "";
    public string? SchoolId { get; set; } = "";
    public string? FieldId { get; set; } = "";
    public string? PromotionId { get; set; } = "";
    public string? StageId { get; set; } = "";
    public string? GroupId { get; set; } = string.Empty;
    public DateTime? Startdate { get; set; }
    public DateTime? Enddate { get; set; }
    public string? TermNo { get; set; } = string.Empty;
    public int? ExchangerateId { get; set; } = 0;
    public decimal? Vat { get; set; } = 0;
    public decimal? GrandTotalKhr { get; set; } = 0;
    public decimal? GrandTotal { get; set; } = 0;
    public string? Description { get; set; } = "";
    public string? Status { get; set; } = "";
    public decimal? Totaldollar { get; set; }
    public decimal? Totalriel { get; set; }
    public decimal? Totalbath { get; set; }
    public decimal? Totaldiscount { get; set; }
    public bool? Payment { get; set; }
    public bool? CheckPayment { get; set; }
    public DateTime? DateEdit { get; set; }
    public string? EditBy { get; set; }
    public decimal? OweKhr { get; set; }
    public decimal? Owe { get; set; }
    public string? OweReason { get; set; }
    public int? UserId { get; set; }
    public decimal? TotalReturnAmount { get; set; }
    public decimal? ReturnAmount { get; set; }
    public string? ReturnDescription { get; set; }
    public decimal? Totalother { get; set; }
    public int? PaymentMethodId { get; set; }
    public decimal? AmountDollar { get; set; }
    public decimal? AmountReil { get; set; }
    public bool? PayOnApp { get; set; }
}