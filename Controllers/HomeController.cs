using BBU_SYSTEM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BBU_SYSTEM.Repository;
using BBU_SYSTEM.ViewModel;
using Microsoft.AspNetCore.Localization;

namespace BBU_SYSTEM.Controllers;

[Authorize]
public class HomeController(
    ILogger<HomeController> logger,
    ICampusDbContext campusDbContext,
    IHttpContextAccessor context)
    : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    public IActionResult Index()
    {
        var db = campusDbContext.DbContext(_campus);
        var student = db.TblStudent.AsQueryable();
        var active = db.TblStudent.Where(x => x.Status!.ToLower() == "active").AsQueryable();
        var register = db.TblStudent.Where(x => x.Status!.ToLower() == "register").AsQueryable();
        var quit = db.TblStudent.Where(x => x.Status!.ToLower() == "quit").AsQueryable();
        var graduated = db.TblStudent.Where(x => x.Status!.ToLower() == "graduated").AsQueryable();
        var completed = db.TblStudent.Where(x => x.Status!.ToLower() == "completed").AsQueryable();
        var viewModel = new HomeViewModel
        {
            TotalStudent = student.Count(),
            TotalActive = active.Count(),
            TotalRegister = register.Count(),
            TotalQuit = quit.Count(),
            TotalGraduated = graduated.Count(),
            TotalCompleted = completed.Count(),
            UserActivityLogs = db.UserActivityLogs.ToList()
        };
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult ReportChart()
    {
        return View();
    }
    
    public IActionResult SwitchLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            });

        return LocalRedirect(returnUrl);
    }
}