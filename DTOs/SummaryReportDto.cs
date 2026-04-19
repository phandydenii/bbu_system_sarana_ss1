using System.ComponentModel.DataAnnotations;

namespace BBU_SYSTEM.DTOs;

public class SummaryReportDto
{ 
    public int Id { get; set; } 
    
    [Required(ErrorMessage = "Title is required"), ]
    public string? TitleEng { get; set; } 
    public string? TitleKm { get; set; } 
    public string? Description { get; set; }  
    public string? Query { get; set; }
    public bool Show { get; set; }
    public string? ChartType { get; set; }
    public int Ordering { get; set; }
}