using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;

namespace BBU_SYSTEM.ViewModel;

public class DegreePageViewModel
{
    public List<Degree>? Degrees { get; set; }
    public DegreeDto DegreeDto { get; set; } = new(); // for form binding
}