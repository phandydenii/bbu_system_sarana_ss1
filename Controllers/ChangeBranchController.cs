using AutoMapper;
using BBU_SYSTEM.Helper;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Controllers;
[Authorize]
[Route("change-branch")]
public class ChangeBranchController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("get-student-change-branch/{studentId}")]
    public IActionResult GetStudentChangeBranch(string studentId)
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
            var query =  from cb in db.TblChangeBranch
                join b in db.TblBranch on cb.ToBranchId equals b.BranchId
                where cb.StudentId == studentId
                select new
                {
                    cb.ChangeBranchId,
                    cb.StudentId,
                    cb.ToBranchId,
                    BranchName = b.BranchName,
                    BranchNameInKhmer = b.BranchNameInKhmer,
                    cb.TermNo,
                    cb.FromDate,
                    cb.ReturnDate,
                    cb.DegreeId,
                    cb.SchoolId,
                    cb.FieldId,
                    cb.PromotionId,
                    cb.StageId,
                    cb.GroupId
                };
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
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }
}