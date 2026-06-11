using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models.Req;
using Microsoft.AspNetCore.Mvc;
using BBU_SYSTEM.Service;
using Microsoft.AspNetCore.Authorization;

namespace BBU_SYSTEM.Controllers;

public class AccountController(IHttpContextAccessor context,IConfiguration configuration, AuthService authService) : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(AuthenticationReq req, string? returnUrl = null) 
    {
        returnUrl ??= Url.Content("~/");
        if (string.IsNullOrEmpty(req.Username) || string.IsNullOrEmpty(req.Password) ||
            string.IsNullOrEmpty(req.Campus))
        {
            ModelState.AddModelError("", "Please enter username and password.");
            return View();
        }

        try
        {
            var isAuthenticated = await authService.Login(req);
            if (isAuthenticated)
            {
                return LocalRedirect(returnUrl); // Redirect to a protected page
            }
          

            ModelState.AddModelError("", "Invalid username or password.");
            return View(req);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"{ex.Message}");
            return View(req);
        }
    }

    [Authorize]  
    public async Task<IActionResult> Logout(string? returnUrl = null)
    {
        await authService.Logout();
        if (returnUrl != null)
        {
            return LocalRedirect(returnUrl);
        } 
        return RedirectToPage("/Account/Login"); 
    }

    [Authorize] 
    public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
    {
        if (!ModelState.IsValid)
        {
            // Get all messages (flattened)
            var allErrors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(new
            {
                status = new
                {
                    code = "400",
                    message = allErrors
                }
            });
        }

        try
        {
            var result = await authService.ResetPasswordHash(model, _campus);
            if (!result)
                return BadRequest();
            return Ok(new
            {
                data = new { },
                status = new
                {
                    code = "200",
                    message = "Reset Password Success"
                }
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                data = new { },
                status = new
                {
                    code = "500",
                    message = $"Reset Password Failed: {e.Message}"
                }
            });
        }
    }
}