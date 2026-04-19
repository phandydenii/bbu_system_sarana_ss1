namespace BBU_SYSTEM.Models.Req;

public class InvoiceDetailReq
{
    public int InvoiceDetailId { get; set; }
    public int? InvoiceId { get; set; }
    public int? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductNameKhmer { get; set; }
    public int? Qty { get; set; }
    public decimal? PriceKhr { get; set; }
    public decimal? PriceUsd { get; set; }
    public decimal? TotalKhr { get; set; }
    public decimal? TotalUsd { get; set; }
    public string? Type { get; set; }
    public decimal? Vat { get; set; }
    public int? DiscountPercent { get; set; }
    public decimal? DiscountKhr { get; set; }
    public decimal? DiscountUsd { get; set; }
    public decimal? OweKhr { get; set; }
    public decimal? OweUsd { get; set; }
    public decimal? GrandTotalKhr { get; set; }
    public decimal? GrandTotalUsd { get; set; }
    public decimal? PayKhr { get; set; }
    public decimal? PayUsd { get; set; }
    public decimal? PayBath { get; set; }
    public bool? Tuitionfees { get; set; }
    public int? CardCertificate { get; set; }
    public int? CategoryId { get; set; }
    public decimal? OtherKhr { get; set; }
    public decimal? OtherUsd { get; set; }
    public bool? PaymentType { get; set; }
}