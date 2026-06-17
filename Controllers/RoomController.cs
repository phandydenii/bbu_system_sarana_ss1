using AutoMapper;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("room")]
public class RoomController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";


    [HttpPost("get-rooms")]
    public async Task<IActionResult> GetRooms(bool isAll = false)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = !string.IsNullOrEmpty(length) ? Convert.ToInt32(length) : 10;
            var skip = !string.IsNullOrEmpty(start) ? Convert.ToInt32(start) : 0;

            var db = campusDbContext.DbContext(_campus);
            var query = db.TblRoom.AsQueryable();

            if (isAll)
            {
                var allRooms = await query
                    .OrderByDescending(x => x.RoomId)
                    .ToListAsync();

                return new ServerResponse().Success(allRooms, "Succeeded!");
            }

            var recordsTotal = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(x =>
                    (x.RoomName ?? "").Contains(searchValue) ||
                    (x.RoomType ?? "").Contains(searchValue) ||
                    x.Capacity.ToString().Contains(searchValue)
                );
            }

            var recordsFiltered = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.RoomId)
                .Skip(skip)
                .Take(pageSize)
                .Select(x => new
                {
                    roomId = x.RoomId,
                    roomName = x.RoomName,
                    capacity = x.Capacity,
                    roomType = x.RoomType
                })
                .ToListAsync();

            return Json(new
            {
                draw,
                recordsTotal,
                recordsFiltered,
                data
            });
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    [HttpPost("save-change")]
    public async Task<IActionResult> SaveChange([FromForm] RoomDto? room)
    {
        try
        {
            if (room == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }

            var db = campusDbContext.DbContext(_campus);

            var existingRoom = await db.TblRoom
                .FirstOrDefaultAsync(x => x.RoomId == room.RoomId);

            if (existingRoom != null)
            {
                existingRoom.RoomName = room.RoomName?.Trim();
                existingRoom.Capacity = room.Capacity;
                existingRoom.RoomType = room.RoomType?.Trim();

                await db.SaveChangesAsync();

                return new ServerResponse().Success(existingRoom, "Updated successfully!");
            }

            db.TblRoom.Add(new()
            {
                RoomName = room.RoomName?.Trim(),
                Capacity = room.Capacity,
                RoomType = room.RoomType?.Trim()
            });

            await db.SaveChangesAsync();

            return new ServerResponse().Success(room, "Saved successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            var room = await db.TblRoom
                .FirstOrDefaultAsync(x => x.RoomId == id);

            if (room == null)
            {
                return new ServerResponse().NotFound("Room not found!");
            }

            db.TblRoom.Remove(room);
            await db.SaveChangesAsync();

            return new ServerResponse().Success(room, "Deleted successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
}