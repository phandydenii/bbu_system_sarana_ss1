using BBU_SYSTEM.Helper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("doctor-contract")]
public class DoctorContractController(ICampusDbContext campusDbContext, IHttpContextAccessor context)
    : Controller
{
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [HttpPost("gets")]
    public async Task<IActionResult> GetDoctorContracts(bool isAll = false)
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
            var query = db.TblDoctoralContract.AsQueryable();

            if (isAll)
            {
                var allContracts = await query
                    .OrderByDescending(x => x.ContractId)
                    .ToListAsync();

                return new ServerResponse().Success(allContracts, "Succeeded!");
            }

            var recordsTotal = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(x =>
                    (x.StudentId ?? "").Contains(searchValue) ||
                    (x.Note ?? "").Contains(searchValue) ||
                    x.TermNo.ToString()!.Contains(searchValue) ||
                    x.Fee.ToString()!.Contains(searchValue)
                );
            }

            var recordsFiltered = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.ContractId)
                .Skip(skip)
                .Take(pageSize)
                .Select(x => new
                {
                    contractId = x.ContractId,
                    studentId = x.StudentId,
                    termNo = x.TermNo,
                    fee = x.Fee,
                    startDate = x.StartDate,
                    endDate = x.EndDate,
                    note = x.Note
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
    public async Task<IActionResult> SaveChange([FromForm] DoctoralContractDto? doctorContract)
    {
        try
        {
            if (doctorContract == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }

            var db = campusDbContext.DbContext(_campus);

            var existingDoctorContract = await db.TblDoctoralContract
                .FirstOrDefaultAsync(x => x.ContractId == doctorContract.ContractId);

            if (existingDoctorContract != null)
            {
                existingDoctorContract.StudentId = doctorContract.StudentId?.Trim();
                existingDoctorContract.TermNo = doctorContract.TermNo;
                existingDoctorContract.Fee = doctorContract.Fee;
                existingDoctorContract.StartDate = doctorContract.StartDate;
                existingDoctorContract.EndDate = doctorContract.EndDate;
                existingDoctorContract.Note = doctorContract.Note?.Trim();

                await db.SaveChangesAsync();

                return new ServerResponse().Success(existingDoctorContract, "Updated successfully!");
            }

            db.TblDoctoralContract.Add(new()
            {
                StudentId = doctorContract.StudentId?.Trim(),
                TermNo = doctorContract.TermNo,
                Fee = doctorContract.Fee,
                StartDate = doctorContract.StartDate,
                EndDate = doctorContract.EndDate,
                Note = doctorContract.Note?.Trim()
            });

            await db.SaveChangesAsync();

            return new ServerResponse().Success(doctorContract, "Saved successfully!");
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

            var doctorContract = await db.TblDoctoralContract
                .FirstOrDefaultAsync(x => x.ContractId == id);

            if (doctorContract == null)
            {
                return new ServerResponse().NotFound("Doctoral contract not found!");
            }

            db.TblDoctoralContract.Remove(doctorContract);
            await db.SaveChangesAsync();

            return new ServerResponse().Success(doctorContract, "Deleted successfully!");
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }
}