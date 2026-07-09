using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("student-discount")]
public class StudentDiscountController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("gets")]
    public IActionResult GetStudentDiscounts()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);

        var students = db.TblStudent.AsQueryable();
        var query = (from s in students
                join sd in db.TblStudentDiscount on s.StudentId equals sd.StudentId
                select new Student
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentName,
                    StudentNameInKhmer = s.StudentNameInKhmer,
                    Sex = s.Sex,
                    DateOfBirth = s.DateOfBirth,
                    Phone = s.Phone,
                    Address = s.Address,
                    Email = s.Email
                }
            ).Distinct().AsQueryable();
        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(d =>
                d.StudentId!.Contains(searchValue) ||
                d.StudentName!.ToString().Contains(searchValue) ||
                d.StudentNameInKhmer!.ToString().Contains(searchValue) 
            );
        var recordsTotal = query.Count();
        var data = query.Skip(skip).Take(pageSize).ToList();
        return Json(new
        {
            draw,
            recordsFiltered = recordsTotal,
            recordsTotal,
            data
        });
    }
    [HttpPost("save-change")]
    public async Task<IActionResult> SaveChange(StudentDiscountDto studentDiscount)
    {
        var db = campusDbContext.DbContext(_campus);
        try
        { 
            if (studentDiscount.StudentDiscountId == 0)
            {
                var data = mapper.Map<StudentDiscount>(studentDiscount); 
                await db.TblStudentDiscount.AddAsync(data);
                await db.SaveChangesAsync(); 
                return new ServerResponse().Success(data);
            }
            var oldData = await db.TblStudentDiscount.FirstOrDefaultAsync(x => x.StudentDiscountId == studentDiscount.StudentDiscountId);
            if (oldData == null) return new ServerResponse().NotFound("Record found.");
            mapper.Map(studentDiscount, oldData);
            await db.SaveChangesAsync(); 
            return new ServerResponse().Success(oldData);
        }
        catch (Exception e)
        { 
            return new ServerResponse().ErrorInternal(e);
        }
    } 
    [HttpDelete("delete/{studentDiscountId:int}")]
    public async Task<IActionResult> Delete(int studentDiscountId)
    {
        var db = campusDbContext.DbContext(_campus);
        try
        { 
            var oldData = await db.TblStudentDiscount.FirstOrDefaultAsync(x => x.StudentDiscountId == studentDiscountId);
            if (oldData == null) return new ServerResponse().NotFound("Record not found.");
            db.TblStudentDiscount.Remove(oldData);
            await db.SaveChangesAsync();
            return new ServerResponse().Success(oldData);
        }
        catch (Exception e)
        { 
            return new ServerResponse().ErrorInternal(e);
        }
    } 
}