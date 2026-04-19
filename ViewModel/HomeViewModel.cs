using BBU_SYSTEM.Models;

namespace BBU_SYSTEM.ViewModel;

public class HomeViewModel
{
    public int TotalStudent { get; set; }
    public int TotalActive { get; set; }
    public int TotalRegister { get; set; }
    public int TotalQuit { get; set; }
    public int TotalGraduated { get; set; }
    public int TotalCompleted { get; set; }
    public IEnumerable<UserActivityLog>? UserActivityLogs { get; set; }
}