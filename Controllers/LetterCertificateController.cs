using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;
using Microsoft.AspNetCore.Mvc;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("letter-certificates")]
public class LetterCertificateController(
    ICampusDbContext campusDbContext,
    IMapper mapper,
    IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("gets/{studentId}")]
    public IActionResult GetLetterCertificates(string studentId)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
        var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "10");
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        studentId = studentId.ToLower().Replace(_campus, "");
        var query = campusDbContext.DbContext(_campus).TblLetterCertifications.Where(x => x.StuId == studentId)
            .AsQueryable();
        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(x => x.LetterNo == Convert.ToInt16(searchValue)).AsQueryable();

        var recordsTotal = query.Count();
        var data = query.Skip(start).Take(length).ToList();

        return Json(new
        {
            draw,
            recordsFiltered = recordsTotal,
            recordsTotal,
            data
        });
    }

    
    [HttpPost("save-letter-certificate")]
    public async Task<IActionResult> Save(LetterCertificationDto letterCertification)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            if (letterCertification.Id == 0)
            {
                var category = await db.TblLetterCategory.Where(x => x.CategoryId == letterCertification.CategoryId)
                    .FirstOrDefaultAsync();
                if (category == null)
                {
                    return BadRequest(new
                    {
                        data = new { },
                        status = new
                        {
                            code = "400",
                            message = "Category not found"
                        }
                    });
                }
                var yearNum = DateTime.Now.Year.ToString();
                var getMaxLetterCert = db.TblLetterCertifications.OrderByDescending(x=>x.Id).AsQueryable(); 
                
                letterCertification.StuId = letterCertification.StuId!.ToLower().Replace(_campus, "");
                if (category.IsStartNewNumber is true)
                {
                    getMaxLetterCert = getMaxLetterCert.Where(x=>x.YearNumber == yearNum).AsQueryable();
                    if (category.IsAdmin is true)
                    {
                        var dataMax = getMaxLetterCert.Where(x=>x.CategoryId == letterCertification.CategoryId)!.FirstOrDefault();
                        if (dataMax != null)
                        {
                            letterCertification.LetterNo = dataMax.LetterNo + 1;
                        }
                        else
                        {
                            letterCertification.LetterNo = 1;
                        }
                        letterCertification.YearNumber = yearNum;
                    }
                    else if (category.IsFoundation is true)
                    {
                        var dataMax = getMaxLetterCert.Where(x=>x.CategoryId == letterCertification.CategoryId)!.FirstOrDefault();
                        if (dataMax != null)
                        {
                            letterCertification.FoundationNo = dataMax.FoundationNo + 1;
                        }
                        else
                        {
                            letterCertification.FoundationNo = 1;
                        }
                        letterCertification.FoundationYear = Convert.ToInt16(yearNum);
                    }
                    else if (category.IsShortCourse is true)
                    {
                        var dataMax = getMaxLetterCert.Where(x=>x.CategoryId == letterCertification.CategoryId)!.FirstOrDefault();
                        if (dataMax != null)
                        {
                            letterCertification.ShortCourseNo = dataMax.ShortCourseNo + 1;
                        }
                        else
                        {
                            letterCertification.ShortCourseNo = 1;
                        }
                        letterCertification.ShortCourseYear = Convert.ToInt16(yearNum);
                    }
                }
                else
                {
                    if (category.IsAdmin is true)
                    {
                        var dataMax = getMaxLetterCert.Where(x=>x.CategoryId == letterCertification.CategoryId)!.FirstOrDefault();
                        if (dataMax != null)
                        {
                            letterCertification.LetterNo = dataMax.LetterNo + 1;
                        }
                        else
                        {
                            letterCertification.LetterNo = 1;
                        }
                        letterCertification.YearNumber = yearNum;
                    }
                    else if (category.IsFoundation is true)
                    {
                        var dataMax = getMaxLetterCert.Where(x=>x.CategoryId == letterCertification.CategoryId)!.FirstOrDefault();
                        if (dataMax != null)
                        {
                            letterCertification.FoundationNo = dataMax.FoundationNo + 1;
                        }
                        else
                        {
                            letterCertification.FoundationNo = 1;
                        }
                        letterCertification.FoundationYear = Convert.ToInt16(yearNum);
                    }
                    else if (category.IsShortCourse is true)
                    {
                        var dataMax = getMaxLetterCert.Where(x=>x.CategoryId == letterCertification.CategoryId)!.FirstOrDefault();
                        if (dataMax != null)
                        {
                            letterCertification.ShortCourseNo = dataMax.ShortCourseNo + 1;
                        }
                        else
                        {
                            letterCertification.ShortCourseNo = 1;
                        }
                        letterCertification.ShortCourseYear = Convert.ToInt16(yearNum);
                    }
                }

                var data = mapper.Map<LetterCertificationDto, LetterCertification>(letterCertification);
                await db.TblLetterCertifications.AddAsync(data);
                await db.SaveChangesAsync();
                return Ok(new
                {
                    data,
                    status = new
                    {
                        code = "200",
                        message = "Letter Certification saved successfully."
                    }
                });
            }

            var dataExist = db.TblLetterCertifications.Where(x => x.Id == letterCertification.Id)!.FirstOrDefault();
            if (dataExist == null)
            {
                return BadRequest(new
                {
                    data = new { },
                    status = new
                    {
                        code = "400",
                        message = "Letter Certification not found"
                    }
                });
            }

            mapper.Map<LetterCertificationDto, LetterCertification>(letterCertification);
            db.TblLetterCertifications.Update(dataExist);
            await db.SaveChangesAsync();
            return Ok(new
            {
                data = letterCertification,
                status = new
                {
                    code = "200",
                    message = "Letter Certification updated successfully."
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
                    message = e.Message
                }
            });
        }
    }
}