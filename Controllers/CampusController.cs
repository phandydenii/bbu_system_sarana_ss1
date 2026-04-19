using BBU_SYSTEM.Data;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

public class CampusController : Controller
{
    // GET
    public IActionResult Index()
    {
        Console.WriteLine(StringCipher.Encrypt("hello"));
        return View();
    }
}