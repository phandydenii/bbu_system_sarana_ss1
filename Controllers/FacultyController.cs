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
        var db = campusDbContext.DbContext(_campus);
        var query = db.TblFaculty.AsQueryable();

        // IMPORTANT: put this before Request.Form
        if (isAll)
        {
            return Ok(new
            {
                data = query
                    .OrderBy(x => x.FacultyName)
                    .Select(x => new
                    {
                        facultyId = x.FacultyId,
                        facultyName = x.FacultyName,
                        facultyNameInKhmer = x.FacultyNameInKhmer
                    })
                    .ToList(),
                code = "200",
                message = "Succeeded!"
            });
        }

        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 10;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        if (!string.IsNullOrEmpty(searchValue))
        {
            query = query.Where(x =>
                x.FacultyName!.Contains(searchValue) ||
                x.FacultyNameInKhmer!.Contains(searchValue));
        }

        var recordsTotal = query.Count();

        var data = query
            .OrderByDescending(x => x.FacultyId)
            .Skip(skip)
            .Take(pageSize)
            .ToList();

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
        var realError = ex.InnerException != null
            ? ex.InnerException.Message
            : ex.Message;

        return StatusCode(500, new
        {
            code = "500",
            message = $"Internal Server Error: {realError}"
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
    [HttpPost("SaveChange")]
    public async Task<IActionResult> SaveChange([FromForm] FacultyDto? faculty)
    {
        try
        {
            if (faculty == null)
            {
                return BadRequest(new
                {
                    code = "400",
                    message = "Bad Request!"
                });
            }

            var db = campusDbContext.DbContext(_campus);

            if (faculty.FacultyId > 0)
            {
                var facultyEntity = db.TblFaculty
                    .FirstOrDefault(x => x.FacultyId == faculty.FacultyId);

                if (facultyEntity == null)
                {
                    return BadRequest(new
                    {
                        code = "400",
                        message = "Faculty not found!"
                    });
                }

                facultyEntity.FacultyName = faculty.FacultyName;
                facultyEntity.FacultyNameInKhmer = faculty.FacultyNameInKhmer;

                await db.SaveChangesAsync();

                return Ok(new
                {
                    code = "200",
                    message = "Updated successfully!"
                });
            }
            else
            {
                var newFaculty = new Faculty
                {
                    FacultyName = faculty.FacultyName,
                    FacultyNameInKhmer = faculty.FacultyNameInKhmer
                };

                await db.TblFaculty.AddAsync(newFaculty);
                await db.SaveChangesAsync();

                return Ok(new
                {
                    code = "200",
                    message = "Saved successfully!"
                });
            }
        }
        catch (Exception ex)
        {
            var realError = ex.InnerException != null
                ? ex.InnerException.Message
                : ex.Message;

            return StatusCode(500, new
            {
                code = "500",
                message = $"Internal Server Error: {realError}"
            });
        }
    }
    [HttpDelete("delete/{facultyId}")]
    public async Task<IActionResult> Delete(decimal facultyId)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            var faculty = db.TblFaculty
                .FirstOrDefault(x => x.FacultyId == facultyId);

            if (faculty == null)
            {
                return BadRequest(new
                {
                    code = "400",
                    message = "Faculty not found!"
                });
            }

            db.TblFaculty.Remove(faculty);
            await db.SaveChangesAsync();

            return Ok(new
            {
                code = "200",
                message = "Deleted successfully!"
            });
        }
        catch (Exception ex)
        {
            var realError = ex.InnerException != null
                ? ex.InnerException.Message
                : ex.Message;

            return StatusCode(500, new
            {
                code = "500",
                message = $"Internal Server Error: {realError}"
            });
        }
    }
}