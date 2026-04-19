using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBU_SYSTEM.Models;

[Table("SUMMARY_REPORT")]
public class SummaryReport
{
    [Key,Column("ID")]
    public int Id { get; set; }
    [Required, Column("TITLE",TypeName = "VARCHAR (100)")]
    public string? TitleEng { get; set; }
    [Column("TITLE_KHMER",TypeName = "NVARCHAR (100)")]
    public string? TitleKm { get; set; }
    [Column("DESCRIPTION",TypeName = "VARCHAR (100)")]
    public string? Description { get; set; }
    [Required,Column("QUERY",TypeName = "VARCHAR (500)")]
    public string? Query { get; set; }
    [Required,Column("SHOW",TypeName = "BIT")]
    public bool Show { get; set; }
    [Required,Column("CHART_TYPE",TypeName = "VARCHAR (20)")]
    public string? ChartType { get; set; }
    [Required,Column("ORDERING",TypeName = "int")]
    public int Ordering { get; set; }
}