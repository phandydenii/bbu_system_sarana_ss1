using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("user")]
public class UserController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [Route("all")]
    public ActionResult Index()
    {
        return View();
    }
    
    [Route("privilege")]
    public ActionResult Privilege()
    {
        return View();
    }
    
    [Route("privilege-group")]
    public ActionResult PrivilegeGroup()
    {
        return View();
    }
    [HttpPost("get-users")]
    public IActionResult GetAcademicUser()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);

        var query = db.TblUser.Where(x => x.Status == "ENABLED").AsQueryable();
        if (!string.IsNullOrEmpty(searchValue))
        {
            query = query.Where(x=>x.UserName!.Contains(searchValue) || x.UserId.ToString().Contains(searchValue));
        }
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

    [HttpPost("get-privileges")]
    public IActionResult GetAdministrationUser(bool isAll = false)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);

        var query = (from p in db.TblPrivilege
                join pg in db.TblPrivilegeGroup on p.PrivilegeGroupId equals pg.Id into pgs
                from pg in pgs.DefaultIfEmpty()
                select new
                {
                    p.PrivilegeId,p.PrivilegeName,p.PrivilegeGroupId
                    ,PrivilegeGroupName = pg != null ? pg.GroupName : null
                }
            ).AsQueryable();
        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(x=>x.PrivilegeId.ToString().Contains(searchValue) || x.PrivilegeName!.Contains(searchValue));
        var recordsTotal = query.Count();
        if (isAll)
            return Ok(new
            {
                data = query.ToList(),
                code = "200",
                message = "Succeeded!"
            });
        var data = query.Skip(skip).Take(pageSize).ToList();
        return Json(new
        {
            draw,
            recordsFiltered = recordsTotal,
            recordsTotal,
            data
        });
    }

    [HttpPost("get-user-privileges")]
    public IActionResult GetUserPrivilege(bool isAll = false, int userId = 0)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);

        var query = (from p in db.TblPrivilege
                join up in db.TblUserPrivilege on p.PrivilegeId equals up.PriviledgeId
                join pg in db.TblPrivilegeGroup on p.PrivilegeGroupId equals pg.Id into pgs
                from pg in pgs.DefaultIfEmpty() 
                select new
                {
                    up.UserId,p.PrivilegeId,p.PrivilegeName,p.PrivilegeGroupId
                    ,PrivilegeGroupName = pg != null ? pg.GroupName : null
                }
            ).AsQueryable();
        if (userId != 0)
        {
            query = query.Where(x => x.UserId==userId);
        }
        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(x=>x.PrivilegeId.ToString().Contains(searchValue) || x.PrivilegeName!.Contains(searchValue));
        var recordsTotal = query.Count();
        if (isAll)
            return Ok(new
            {
                data = query.ToList(),
                code = "200",
                message = "Succeeded!"
            });
        var data = query.Skip(skip).Take(pageSize).ToList();
        return Json(new
        {
            draw,
            recordsFiltered = recordsTotal,
            recordsTotal,
            data
        });
    }
    
    [HttpPost("get-privileges-group")]
    public IActionResult GetPrivilegeGroup(bool isAll = false)
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
        //var campus = HttpContext.Session.GetString("campus");
        var db = campusDbContext.DbContext(_campus);

        var query = db.TblPrivilegeGroup.AsQueryable();
        if (!string.IsNullOrEmpty(searchValue))
            query = query.Where(x=>x.Id.ToString().Contains(searchValue) || x.GroupName!.Contains(searchValue));
        if (isAll)
            return Ok(new
            {
                data = query.ToList(),
                status = new
                {
                    code = "200",
                    message = "Succeeded!"
                }
            });
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
    
    
    [HttpPost("save-privileges-group")]
    public async Task<IActionResult> SavePrivilegeGroup(PrivilegeGroupDto? privilegeGroup)
    {
        
        try
        {
            if (privilegeGroup == null)
            {
                return BadRequest(new
                {
                    data = new {},
                    status = new
                    {
                        code = "400",
                        message = "Bad Request"
                    }
                });
            }
            var db = campusDbContext.DbContext(_campus);
            var existData = db.TblPrivilegeGroup.Any(x => x.GroupName == privilegeGroup.GroupName);
            if (existData)
            {
                return BadRequest(new
                {
                    data = new {},
                    status = new
                    {
                        code = "400",
                        message = "Group name already exists!"
                    }
                });
            }
            if (privilegeGroup.Id == 0)
            {
                var data = mapper.Map<PrivilegeGroupDto, PrivilegeGroup>(privilegeGroup);
                await db.TblPrivilegeGroup.AddAsync(data);
                await db.SaveChangesAsync();
                return Ok(new
                {
                    data,
                    status = new
                    {
                        code = "200",
                        message = "Save successfully"
                    }
                });
            }
            var dataExist = await db.TblPrivilegeGroup.FindAsync(privilegeGroup.Id);
            if (dataExist == null)
            {
                return BadRequest(new
                {
                    data = new {},
                    status = new
                    {
                        code = "400",
                        message = "Data not exist!"
                    }
                });
            }
            mapper.Map(privilegeGroup, dataExist);
            db.TblPrivilegeGroup.Update(dataExist);
            await db.SaveChangesAsync();
            return Ok(new
            {
                data= privilegeGroup,
                status = new
                {
                    code = "200",
                    message = "Update successfully"
                }
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                data = new{},
                code = "500",
                message = e.Message
            });
        }
    }
    
    
    [HttpPost("save-privileges")]
    public async Task<IActionResult> SavePrivilege(PrivilegeDto? privilege)
    {
        if (privilege == null)
        {
            return BadRequest(new
            {
                data = new {},
                status = new
                {
                    code = "400",
                    message = "Bad Request"
                }
            });
        }
        try
        {
            var db = campusDbContext.DbContext(_campus);

            if (privilege.PrivilegeId == 0)
            {
                var data = mapper.Map<PrivilegeDto, Privilege>(privilege);
                await db.TblPrivilege.AddAsync(data);
                await db.SaveChangesAsync();
                return Ok(new
                {
                    data,
                    status = new
                    {
                        code = "200",
                        message = "Save successfully"
                    }
                });
            }
            var dataExist = await db.TblPrivilege.FindAsync(privilege.PrivilegeId);
            if (dataExist == null)
            {
                return BadRequest(new
                {
                    data = new {},
                    status = new
                    {
                        code = "400",
                        message = "Data not exist!"
                    }
                });
            }
            mapper.Map(privilege, dataExist);
            db.TblPrivilege.Update(dataExist);
            await db.SaveChangesAsync();
            return Ok(new
            {
                data= privilege,
                status = new
                {
                    code = "200",
                    message = "Update successfully"
                }
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                data = new{},
                code = "500",
                message = e.Message
            });
        }
    }
    
    [HttpPost("set-user-privileges")]
    public async Task<IActionResult> AssignUserPrivilege(int privilegeId,int userId,bool assign)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            if (assign)
            {
                var data = new UserPriviledge()
                {
                    UserId = userId,
                    PriviledgeId = privilegeId
                };
                await db.TblUserPrivilege.AddAsync(data);
                await db.SaveChangesAsync();
                return Ok(new
                {
                    data,
                    status = new
                    {
                        code = "200",
                        message = "Assign successfully"
                    }
                });
            }
            var dataExist = await db.TblUserPrivilege.Where(x=>x.PriviledgeId==privilegeId && x.UserId == userId).FirstOrDefaultAsync();
            if (dataExist == null)
            {
                return BadRequest(new
                {
                    data = new {},
                    status = new
                    {
                        code = "400",
                        message = "Data not exist!"
                    }
                });
            } 
            db.TblUserPrivilege.Remove(dataExist);
            await db.SaveChangesAsync();
            return Ok(new
            {
                data= new{},
                status = new
                {
                    code = "200",
                    message = "Remove successfully"
                }
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                data = new{},
                code = "500",
                message = e.Message
            });
        }
    }
}