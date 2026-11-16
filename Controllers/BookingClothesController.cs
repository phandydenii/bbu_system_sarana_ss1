using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BBU_SYSTEM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Globalization;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("booking-clothes")]
public class BookingClothesController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context) : Controller
{
    private const decimal MaxDetailValue = 999999999999999.999m; // decimal(18,3)
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
    public async Task<IActionResult> SaveBookingClothes(
        [FromBody] BookingTblDto? bookingDto, [FromQuery] bool editorHeader = false)
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

            if (editorHeader)
            {
                // The modal doesn't load every DTO field. Update only the editable
                // header fields so omitted fields and existing clothes are preserved.
                var changes = mapper.Map<BookingTblDto, Booking>(bookingDto);
                oldBooking.StudentId = changes.StudentId;
                oldBooking.BookingDate = changes.BookingDate;
                oldBooking.Note = changes.Note;
                oldBooking.Active = changes.Active;
                oldBooking.PayDollar = changes.PayDollar;
                oldBooking.PayRieal = changes.PayRieal;
            }
            else
            {
                mapper.Map(bookingDto, oldBooking);
            }

            await db.SaveChangesAsync();
            return new ServerResponse().Success(oldBooking, "Booking updated successfully!");
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    public sealed class BookingWithItemsRequest
    {
        public BookingTblDto? Booking { get; set; }
        public List<BookingItemWrite?>? Items { get; set; }
        public List<BookingItemWrite?>? OriginalItems { get; set; }
    }

    public sealed class BookingItemWrite
    {
        public int BookingDetailId { get; set; }
        public int? ClothId { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Price { get; set; }
    }

    [HttpPost("save-booking-with-items")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveBookingWithItems(
        [FromBody] BookingWithItemsRequest? request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid || request is null || request.Booking is not { } header ||
            request.Items is not { } submitted || request.OriginalItems is not { } snapshot ||
            submitted.Any(x => x is null) || snapshot.Any(x => x is null))
        {
            return new ServerResponse().BadRequest("Send the booking, its clothing items, and the original item list.");
        }

        var bookingId = header.BookingId ?? 0;
        var isNew = bookingId == 0;
        var studentId = header.StudentId?.Trim();
        if (bookingId < 0 || string.IsNullOrEmpty(studentId) || header.BookingDate is null)
            return new ServerResponse().BadRequest("Select a student and enter a valid booking date.");
        if (header.PayDollar is < 0m || header.PayRieal is < 0m)
            return new ServerResponse().BadRequest("Enter non-negative USD and KHR payment amounts.");

        var items = submitted.Select(x => x!).ToList();
        var originalItems = snapshot.Select(x => x!).ToList();
        if ((isNew || originalItems.Count > 0) && items.Count == 0)
            return new ServerResponse().BadRequest("Keep at least one clothing item on the booking.");

        if (items.Any(x => x.BookingDetailId < 0 || x.ClothId is not > 0 ||
                           x.Qty is not decimal qty || qty <= 0m || !FitsDetailColumn(qty) ||
                           x.Price is not decimal price || price < 0m || !FitsDetailColumn(price)))
        {
            return new ServerResponse().BadRequest("Each item needs a positive quantity and a non-negative USD price, with at most three decimal places.");
        }

        var keptIds = items.Where(x => x.BookingDetailId > 0).Select(x => x.BookingDetailId).ToList();
        var originalIds = originalItems.Select(x => x.BookingDetailId).ToHashSet();
        if (keptIds.Distinct().Count() != keptIds.Count ||
            originalIds.Count != originalItems.Count ||
            originalIds.Any(id => id <= 0) ||
            keptIds.Any(id => !originalIds.Contains(id)) ||
            (isNew && (keptIds.Count > 0 || originalItems.Count > 0)))
        {
            return new ServerResponse().BadRequest("The clothing row IDs are invalid. Reopen the booking and try again.");
        }

        if (isNew && (header.Vat.GetValueOrDefault() != 0 || header.Discount.GetValueOrDefault() != 0m))
            return new ServerResponse().BadRequest("This editor does not set booking-level VAT or discounts.");

        decimal total;
        try
        {
            total = decimal.Round(items.Sum(x => x.Qty!.Value * x.Price!.Value), 2, MidpointRounding.AwayFromZero);
            if (total > MaxDetailValue)
                return new ServerResponse().BadRequest("The booking total is too large.");
        }
        catch (OverflowException)
        {
            return new ServerResponse().BadRequest("The booking total is too large.");
        }

        try
        {
            var db = campusDbContext.DbContext(_campus);
            var strategy = db.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync<IActionResult>(async () =>
            {
                var commitAttempted = false;
                try
                {
                    // Creating a booking needs its generated ID before inserting the items.
                    // The transaction keeps that first write and the item writes atomic.
                    await using var transaction = await db.Database.BeginTransactionAsync(
                        IsolationLevel.Serializable, cancellationToken);

                    Booking booking;
                    var storedItems = new Dictionary<int, BookingDetail>();
                    if (isNew)
                    {
                        booking = new Booking();
                    }
                    else
                    {
                        var existing = await db.TblBooking.AsTracking()
                            .FirstOrDefaultAsync(x => x.BookingId == bookingId, cancellationToken);
                        if (existing == null)
                            return new ServerResponse().NotFound("Booking not found!");
                        booking = existing;

                        storedItems = await db.TblBookingDetail.AsTracking()
                            .Where(x => x.BookingId == bookingId)
                            .ToDictionaryAsync(x => x.BookingDetailId, cancellationToken);

                        // Only allow changes to the rows originally loaded by this editor.
                        if (storedItems.Count != originalItems.Count ||
                            originalItems.Any(x => !storedItems.TryGetValue(x.BookingDetailId, out var stored) ||
                                stored.ClothId != x.ClothId || stored.Qty != x.Qty || stored.Price != x.Price))
                        {
                            return Conflict(new
                            {
                                status = new { code = "409", message = "The saved clothing items changed. Reopen the booking before saving again." }
                            });
                        }

                        if (items.Any(x => x.BookingDetailId > 0 && storedItems[x.BookingDetailId].ClothId != x.ClothId))
                            return new ServerResponse().BadRequest("Remove an existing row and add the new item instead of changing its item ID.");
                    }

                    var newClothIds = items.Where(x => x.BookingDetailId == 0)
                        .Select(x => x.ClothId!.Value).Distinct();
                    foreach (var clothId in newClothIds)
                    {
                        // A scalar comparison avoids the OPENJSON translation of
                        // list.Contains(), which requires SQL compatibility level 130.
                        var isAvailable = await db.TblBookingItem.AnyAsync(
                            x => x.BookingItemId == clothId && x.Hidden == true,
                            cancellationToken);
                        if (!isAvailable)
                            return new ServerResponse().BadRequest("One or more selected clothing items are no longer available.");
                    }

                    var retained = keptIds.ToHashSet();
                    var removed = storedItems.Values.Where(x => !retained.Contains(x.BookingDetailId)).ToList();
                    var detailsChanged = removed.Count > 0 || items.Any(x => x.BookingDetailId == 0 ||
                        storedItems[x.BookingDetailId].Qty != x.Qty ||
                        (storedItems[x.BookingDetailId].Price ?? 0m) != x.Price);

                    // Preserve any billing rules not represented by the clothing editor.
                    if (!isNew && detailsChanged && (Convert.ToDecimal(booking.Vat) != 0m ||
                                                     Convert.ToDecimal(booking.Discount) != 0m))
                    {
                        return new ServerResponse().BadRequest(
                            "This booking has a saved VAT or discount. Item changes were not saved because the adjusted total cannot be recalculated here.");
                    }

                    // Copy only fields this editor owns. The booking key and other
                    // existing data must never be overwritten by the request DTO.
                    var entry = db.Entry(booking);
                    SetBookingValue(entry, nameof(Booking.StudentId), studentId);
                    SetBookingValue(entry, nameof(Booking.BookingDate), header.BookingDate);
                    SetBookingValue(entry, nameof(Booking.Note), header.Note);
                    SetBookingValue(entry, nameof(Booking.Active), header.Active);
                    SetBookingValue(entry, nameof(Booking.PayDollar), header.PayDollar);
                    SetBookingValue(entry, nameof(Booking.PayRieal), header.PayRieal);
                    if (isNew || detailsChanged)
                        SetBookingValue(entry, nameof(Booking.Total), total);

                    if (isNew)
                    {
                        SetBookingValue(entry, nameof(Booking.Vat), 0);
                        SetBookingValue(entry, nameof(Booking.Discount), 0m);
                        if (header.BookingNo is int bookingNo)
                            SetBookingValue(entry, nameof(Booking.BookingNo), bookingNo);
                        if (header.SchoolId is int schoolId)
                            SetBookingValue(entry, nameof(BookingTblDto.SchoolId), schoolId);
                        if (header.FieldId is int fieldId)
                            SetBookingValue(entry, nameof(BookingTblDto.FieldId), fieldId);
                        if (header.GroupId is int groupId)
                            SetBookingValue(entry, nameof(BookingTblDto.GroupId), groupId);
                        if (header.TermNo is int termNo)
                            SetBookingValue(entry, nameof(BookingTblDto.TermNo), termNo);

                        db.TblBooking.Add(booking);
                        await db.SaveChangesAsync(cancellationToken);
                    }

                    int? savedBookingId = booking.BookingId;
                    if (savedBookingId is not > 0)
                        throw new InvalidOperationException("The database did not return a booking ID.");

                    foreach (var item in items)
                    {
                        if (item.BookingDetailId == 0)
                        {
                            db.TblBookingDetail.Add(new BookingDetail
                            {
                                BookingId = savedBookingId,
                                ClothId = item.ClothId,
                                Qty = item.Qty,
                                Price = item.Price
                            });
                            continue;
                        }

                        var stored = storedItems[item.BookingDetailId];
                        stored.Qty = item.Qty;
                        if ((stored.Price ?? 0m) != item.Price)
                            stored.Price = item.Price;
                    }
                    db.TblBookingDetail.RemoveRange(removed);
                    await db.SaveChangesAsync(cancellationToken);
                    commitAttempted = true;
                    await transaction.CommitAsync(cancellationToken);

                    return new ServerResponse().Success(
                        new { bookingId = savedBookingId, itemCount = items.Count },
                        isNew ? "Booking and clothing items saved successfully." : "Booking and clothing items updated successfully.");
                }
                catch (Exception) when (commitAttempted)
                {
                    // A lost acknowledgement during commit is not safe to replay.
                    return StatusCode(500, new
                    {
                        status = new { code = "500", message = "The save result could not be confirmed. Search for the booking before retrying." }
                    });
                }
                catch (Exception e)
                {
                    // Disposing an uncommitted transaction rolls back both writes.
                    return new ServerResponse().ErrorInternal(e);
                }
            });
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    private static void SetBookingValue(EntityEntry<Booking> entry, string name, object? value)
    {
        // Some optional DTO fields are not mapped in every booking model.
        // Use the model's scalar type rather than relying on an AutoMapper profile.
        var property = entry.Metadata.FindProperty(name);
        if (property is null || (value is null && !property.IsNullable))
            return;

        var type = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType;
        entry.Property(name).CurrentValue = value is null || type.IsInstanceOfType(value)
            ? value
            : Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
    }

    private static bool FitsDetailColumn(decimal value) =>
        value <= MaxDetailValue && decimal.Round(value, 3) == value;

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
                    on detail.ClothId equals bookingItem.BookingItemId into catalog
                from bookingItem in catalog.DefaultIfEmpty()
                where detail.BookingId == bookingId
                select new
                {
                    detail.BookingDetailId,
                    detail.BookingId,
                    detail.ClothId,
                    ItemName = bookingItem == null ? null : bookingItem.ItemName,
                    ItemNameKhmer = bookingItem == null ? null : bookingItem.ItemNameKhmer,
                    Type = bookingItem == null ? default : bookingItem.Type,
                    detail.Qty,
                    detail.Price
                }
            ).ToListAsync();
            var data = new { booking, item, editorHeaderSaveSupported = true, editorItemSaveSupported = true };
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
