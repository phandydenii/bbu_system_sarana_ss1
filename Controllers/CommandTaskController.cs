using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("command-task")]
public class CommandTaskController(ICampusDbContext campusDbContext,IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";
    [Route("promotion-groups")]
    public async Task<IActionResult> PromotionGroup()
    {
        var db = campusDbContext.DbContext(_campus);
        var degrees = await db.TblDegree.ToDictionaryAsync(x => x.DegreeId, x => x.DegreeName);
        return View(degrees);
    }

    [Route("branch")]
    public IActionResult Branch()
    {
        return View();
    }
    
    [Route("degrees")]
    public ActionResult Degree()
    {
        return View();
    }

    [Route("disabilities")]
    public ActionResult Disability()
    {
        return View();
    }

    [Route("faculties")]
    public ActionResult Faculty()
    {
        return View();
    }

    [Route("fields")]
    public ActionResult Field()
    {
        return View();
    }

    [Route("field-certificates")]
    public ActionResult FieldCertificate()
    {
        return View();
    }

    [Route("nationalities")]
    public ActionResult Nationality()
    {
        return View();
    }

    [Route("provinces")]
    public ActionResult Province()
    {
        return View();
    }

    [Route("races")]
    public ActionResult Race()
    {
        return View();
    }

    [Route("schools")]
    public ActionResult School()
    {
        return View();
    }

    [Route("universities")]
    public ActionResult University()
    {
        return View();
    }
}