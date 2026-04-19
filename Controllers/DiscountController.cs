using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("student-discount")]
public class DiscountController(ICampusDbContext campusDbContext, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost]
    [Route("student-id/{studentId}")]
    public IActionResult Discount(string studentId)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;

            var db = campusDbContext.DbContext(_campus);
            var student = db.TblStudent.FirstOrDefault(s => s.StudentId == studentId);
            if (student == null) return NotFound("Student not found.");
            var discounts = db.TblStudentDiscount.Where(d => d.StudentId == studentId).AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
                discounts = discounts.Where(d => d.StudentId == studentId).AsQueryable();
            var recordsTotal = discounts.Count();
            var data = discounts.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            });
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving discount information.", ex);
        }
    }
}