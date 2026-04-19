using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;
[Route("settings"),Authorize]
public class SettingController : Controller
{
    
    [Route("dashboard-chart-report")]
    public IActionResult ChartReport()
    {
        return View();
    }
}