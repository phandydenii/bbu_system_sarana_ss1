using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize, Route("academic")]
public class AcademicController : Controller
{
    [Route("assign-student-group")]
    public ActionResult AssignStudentGroup()
    {
        return View();
    }
    
    [Route("assign-student-group-form/{formType}")]
    public IActionResult AssociateGroup(string formType = "associate")
    {
        return formType switch
        {
            "associate" => PartialView("TabAssignGroup/_AssociateGroupForm"),
            "foundation"  => PartialView("TabAssignGroup/_FoundationGroupForm"),
            "specialize"    => PartialView("TabAssignGroup/_SpecializeGroupForm"),
            "master"    => PartialView("TabAssignGroup/_MasterGroupForm"),
            "doctor"    => PartialView("TabAssignGroup/_DoctorGroupForm"),
            "diploma"    => PartialView("TabAssignGroup/_DiplomaGroupForm"),
            "other"    => PartialView("TabAssignGroup/_OtherGroupForm"),
            "unpromoted"    => PartialView("TabAssignGroup/_UnPromoteGroupForm"),
            _           => Content("Select a form")
        };
    }

    [Route("continue-education")]
    public ActionResult ContinueEducation()
    {
        return View();
    }

    [Route("course")]
    public ActionResult Course()
    {
        return View();
    }

    [Route("doctoral-contract")]
    public ActionResult DoctoralContract()
    {
        return View();
    }

    [Route("gpa")]
    public ActionResult Gpa()
    {
        return View();
    }

    [Route("other-branch-score")]
    public ActionResult OtherBranchScore()
    {
        return View();
    }

    [Route("request-qr-code-certificate")]
    public ActionResult RequestQrCode()
    {
        return View();
    }

    [Route("reset-qr-code-certificate")]
    public ActionResult ResetQrCode()
    {
        return View();
    }

    [Route("room")]
    public ActionResult Room()
    {
        return View();
    }

    [Route("score")]
    public ActionResult Score()
    {
        return View();
    }

    [Route("details")]
    public ActionResult Details(string studentId)
    {
        return View();
    }

    [Route("user")]
    public ActionResult Users()
    {
        return View();
    }
}