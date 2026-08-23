using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BBU_SYSTEM.Models;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("booking-clothes")]
public class BookingClothesController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context) : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";
 
    [Route("all-booking-clothes")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("create-booking-clothes")]
    public IActionResult CreateBookingClothes()
    {
        return View();
    }

    [HttpPost("save-booking-clothes")]
    public async Task<IActionResult> SaveBookingClothes([FromBody] BookingTblDto? bookingDto)
    {
        try
        {
            if (bookingDto == null) 
                return new ServerResponse().BadRequest("Bad Request!"); 
            var db = campusDbContext.DbContext(_campus);
            if (bookingDto.BookingId == 0)
            {
                var booking = mapper.Map<BookingTblDto, Booking>(bookingDto);
                await db.TblBooking.AddAsync(booking);
                await db.SaveChangesAsync();
                return new ServerResponse().Success(booking, "Booking created successfully!");
            }

            var oldBooking = await db.TblBooking
                .Where(x => x.BookingId == bookingDto.BookingId)
                .FirstOrDefaultAsync();

            if (oldBooking == null)
                return new ServerResponse().NotFound("Booking not found!"); 
            
            mapper.Map(bookingDto, oldBooking); 
            await db.SaveChangesAsync(); 
            return new ServerResponse().Success(oldBooking, "Booking updated successfully!");
        }
        catch (Exception e)
        { 
            return new ServerResponse().ErrorInternal(e);
        }
    }

    
    [HttpGet("details-booking-clothes/{bookingId}")]
    public async Task<IActionResult> DetailsBookingClothes(int bookingId)
    {
        try
        { 
            var db = campusDbContext.DbContext(_campus); 
            var booking = await db.TblBooking
                .Where(x => x.BookingId == bookingId)
                .Select(x => new
                {
                    x.BookingId,
                    x.BookingNo,
                    x.BookingDate,
                    x.StudentId,
                    x.Total, 
                    x.Vat, 
                    x.Discount, 
                    x.PayDollar,
                    x.PayRieal,
                    x.Note,
                    x.Active
                })
                .FirstOrDefaultAsync();
            if (booking == null)
                return new ServerResponse().NotFound("Booking not found!");
            var item = await (
                from detail in db.TblBookingDetail
                join bookingItem in db.TblBookingItem
                    on detail.ClothId equals bookingItem.BookingItemId
                where detail.BookingId == bookingId
                select new
                {
                    detail.BookingDetailId,
                    detail.BookingId,
                    detail.ClothId, 
                    bookingItem.ItemName,
                    bookingItem.ItemNameKhmer,
                    bookingItem.Type, 
                    detail.Qty,
                    detail.Price
                }
            ).ToListAsync(); 
            var data = new{ booking, item}; 
            return new ServerResponse().Success(data, "Booking details bind successfully!");
        }
        catch (Exception e)
        { 
            return new ServerResponse().ErrorInternal(e);
        }
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
            var fromDate = FunctionHelper.ParseDate(Request.Form["fromDate"]);
            var toDate = FunctionHelper.ParseDate(Request.Form["toDate"]);  
            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            
            var db = campusDbContext.DbContext(_campus);
            var query = db.TblBooking.AsQueryable();
            var recordsTotal = query.Count();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.StudentId!.Contains(searchValue)).AsQueryable();

            if (fromDate.HasValue)
                query = query.Where(d => d.FromDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(d => d.ToDate < toDate.Value);
            
            var recordsFiltered = query.Count();
            var data = query
                .OrderByDescending(d => d.BookingDate)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            return Json(new
            {
                draw,
                recordsFiltered,
                recordsTotal,
                data
            });
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("get-items")]
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
            var query = db.TblBookingItem.Where(x => x.Hidden == true).AsQueryable();

            var recordsTotal = query.Count();
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.ItemName!.Contains(searchValue) 
                    || d.ItemNameKhmer!.Contains(searchValue)
                    ).AsQueryable();

            query = query.OrderByDescending(d => d.BookingItemId);

            var data = query
                .OrderByDescending(x => x.BookingItemId)
                .Skip(skip)
                .Take(pageSize)
                .Select(x => new
                {
                    id = x.BookingItemId,
                    name = x.ItemName,
                    nameKhmer = x.ItemNameKhmer,
                    price = x.Price
                })
                .ToList();
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
            return new ServerResponse().ErrorInternal(e);
        }
    }
}