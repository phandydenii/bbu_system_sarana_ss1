using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("booking-clothes")]
public class BookingClothesController(ICampusDbContext campusDbContext, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    // GET
    [Route("all-booking-clothes")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("create-booking-clothes")]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost("get-booking-clothes")]
    public IActionResult GetBookingClothes()
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
            var query = db.TblBooking.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.StudentId!.Contains(searchValue)).AsQueryable();

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.BookingDate);
            var data = query.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("get-booking-items")]
    public IActionResult GetBookingItems()
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
            var query = db.TblBookingItem.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.ItemName!.Contains(searchValue) || d.ItemNameKhmer!.Contains(searchValue)).AsQueryable();

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.BookingItemId);
            var data = query.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}