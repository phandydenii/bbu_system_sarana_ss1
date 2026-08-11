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
public class BookingClothesController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
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
    public IActionResult CreateBookingClothes()
    {
        return View();
    }

    [HttpPost("create-booking-clothes")]
    public async Task<IActionResult> CreateBookingClothes(
        [FromBody] BookingTblDto? bookingDto)
    {
        try
        {
            if (bookingDto == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }

            var db = campusDbContext.DbContext(_campus);

            if (bookingDto.BookingId == 0)
            {
                var booking = mapper.Map<BookingTblDto, Booking>(bookingDto);

                await db.TblBooking.AddAsync(booking);
                await db.SaveChangesAsync();

                return new ServerResponse()
                    .Success(booking, "Booking created successfully!");
            }

            var oldBooking = await db.TblBooking
                .Where(x => x.BookingId == bookingDto.BookingId)
                .FirstOrDefaultAsync();

            if (oldBooking == null)
            {
                return new ServerResponse()
                    .NotFound("Booking not found!");
            }

            mapper.Map(bookingDto, oldBooking);

            await db.SaveChangesAsync();

            return new ServerResponse()
                .Success(oldBooking, "Booking updated successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

            return new ServerResponse()
                .ErrorInternal(e);
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
            var query = db.TblBookingItem.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.ItemName!.Contains(searchValue) || d.ItemNameKhmer!.Contains(searchValue)).AsQueryable();

            var recordsTotal = query.Count();
            query = query.OrderByDescending(d => d.BookingItemId);
            var data = query.Skip(skip).Take(pageSize).ToList();

            var items = db.TblBookingItem
                .Select(x => new
                {
                    id = x.BookingItemId,
                    name = x.ItemName,
                    nameKhmer = x.ItemNameKhmer,
                    price = x.Price,
                })
                .OrderBy(x => x.name)
                .ToList();
            return Json(new
            {
                draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data = items
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}