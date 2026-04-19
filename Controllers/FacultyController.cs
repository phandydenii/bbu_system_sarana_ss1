using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("Faculty")]
public class FacultyController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    // GET: FacultyController
    [HttpGet("faculty/{id}")]
    public async Task<IActionResult> GetFaulty(decimal id)
    {
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);
        var faculty = await db.TblFaculty.FindAsync(id);
        if (faculty == null)
            return NotFound();
        return Json(new { faculty });
    }

    [HttpPost("getfaculties")]
    public IActionResult GetFacultyList(bool isAll = false)
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
            var query = db.TblFaculty.AsQueryable();

            if (isAll)
                return Ok(new
                {
                    data = query.ToList(),
                    code = "200",
                    message = "Succeded!"
                });

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(f =>
                    f.FacultyName!.Contains(searchValue) ||
                    f.FacultyNameInKhmer!.Contains(searchValue));

            var recordsTotal = query.Count();
            query = query.OrderByDescending(f => f.FacultyId);
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
            return StatusCode(500, new
            {
                code = "500",
                message = $"Internal Server Error:{e.Message}"
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditFaculty(int id)
    {
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);
        var faculty = await db.TblFaculty.FindAsync(id);
        if (faculty == null) return NotFound();
        return Json(faculty);
    }

    [HttpPost]
    public async Task<IActionResult> SaveFaculty(FacultyDto faculty)
    {
        if (ModelState.IsValid)
        {
            //var campus = HttpContext.Session.GetString("campus");
            var db = campusDbContext.DbContext(_campus);
            var data = mapper.Map<FacultyDto, Faculty>(faculty);
            db.TblFaculty.Add(data);
            await db.SaveChangesAsync();
            return Json(new { success = true });
        }

        return Json(new { success = false });
    }

    [HttpPost("SaveChanage")]
    public async Task<IActionResult> SaveChanage([FromForm] FacultyDto? facultyDto)
    {
        try
        {
            if (facultyDto == null)
                return BadRequest(new
                {
                    code = "400",
                    message = "Bad Request!"
                });
            var db = campusDbContext.DbContext(_campus);
            var faculty = db.TblFaculty.FirstOrDefault(x => x.FacultyId == facultyDto.FacultyId);
            if (faculty != null)
            {
                mapper.Map(facultyDto, faculty);
                db.TblFaculty.Update(faculty);
                await db.SaveChangesAsync();
            }
            else
            {
                await db.TblFaculty.AddAsync(mapper.Map<FacultyDto, Faculty>(facultyDto));
                ;
                await db.SaveChangesAsync();
            }

            return Ok(new
            {
                code = "200",
                message = "Succeded!"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                code = "500",
                message = $"Internal Server Error:{ex.Message}"
            });
        }
    }
}