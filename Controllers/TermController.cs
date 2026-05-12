using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("term")]
public class TermController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-terms")]
    public IActionResult GetPromotions(int stageId, bool isAll = false)
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? int.Parse(length) != -1 ? Convert.ToInt32(length) : 10 : 10;
            var skip = start != null ? Convert.ToInt32(start) : 0;

            // var pageSize = length != null ? Convert.ToInt32(length) : 0;
            // var skip = start != null ? Convert.ToInt32(start) : 0;

            var db = campusDbContext.DbContext(_campus);
            var query = db.TblTerm.AsQueryable();
            if (stageId > 0) query = query.Where(x => x.StageId == stageId).AsQueryable();
            if (isAll)
                return new ServerResponse().Success(query.ToList());

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.TermNo == int.Parse(searchValue) ||
                    d.TermId == int.Parse(searchValue));

            var recordsTotal = query.Count();
            query = query.OrderBy(d => d.TermId);
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
            return new ServerResponse().ErrorInternal(e);
        }
    }
    
    [HttpPost("get-last-term/{stageId:int}")]
    public IActionResult GetLastTerm(int stageId)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var data = db.TblTerm.OrderByDescending(x=>x.TermId).FirstOrDefault(x => x.StageId == stageId);
            if (data == null)
            {
                return new ServerResponse().NotFound("Term not found");
            } 
            return new ServerResponse().Success(data);
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }
    

    [HttpPost("save-change")]
    public async Task<IActionResult> CreteTerm(TermDto term)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(term);
        await Helper.Telegram.SendDebugToMyTelegramDirect($"This is term:\n{json}");
        var db = campusDbContext.DbContext(_campus);
        ArgumentNullException.ThrowIfNull(term);
        try
        {
            term.Status = "ACTIVE";
            if (term.TermId > 0)
            {
                var termUpdate = await db.TblTerm.Where(x => x.TermId == term.TermId).FirstOrDefaultAsync();
                if (termUpdate == null)
                {
                    return new ServerResponse().BadRequest();
                }
                mapper.Map(term, termUpdate);
                db.TblTerm.Update(termUpdate);
                await db.SaveChangesAsync();
                return new ServerResponse().Success(termUpdate);
            }
            
            //get old term and update to passed
            var oldTerm =  await db.TblTerm.OrderByDescending(x=>x.TermId).FirstOrDefaultAsync(x => x.StageId == term.StageId);  
            if (oldTerm != null)
            {
                oldTerm.Status = "PASSED"; 
                await db.SaveChangesAsync();
            } 
            
            if (term.TermNo > 1 && oldTerm == null)
            {
                return new ServerResponse().BadRequest("Old term not found. Cannot copy students to new term.");
            }
            
            //save new term
            term.Status = "ACTIVE";
            var data = mapper.Map<TermDto, Term>(term);
            await db.TblTerm.AddAsync(data);
            await db.SaveChangesAsync();
            if (term.TermNo > 1)
            {
                //get student in old term by each group id
                var groups = await db.TblGroup.Where(x => x.StageId == term.StageId).ToListAsync();
                foreach (var group in groups)
                {
                    //save student group to new term with each old group id
                    var student = db.TblStudentGroup.FirstOrDefault(x => x.GroupId == group.GroupId && x.TermNo == oldTerm.TermNo);
                    var studGroup = new StudentGroup()
                    {
                        StudentId = student!.StudentId,
                        GroupId = group.GroupId,
                        TermNo =  term.TermNo,
                    };
                    await db.TblStudentGroup.AddAsync(studGroup);
                }
            }
            return new ServerResponse().Success(data);
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }
    
    [HttpDelete("delete/{termId:int}")]
    public IActionResult DeleteTerm(int termId)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var data = db.TblTerm.OrderByDescending(x=>x.TermId).FirstOrDefault(x => x.TermId == termId);
            if (data == null)
            {
                return new ServerResponse().BadRequest();
            }
            db.TblTerm.Remove(data);
            db.SaveChangesAsync();
            return new ServerResponse().Success(data);
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }
}