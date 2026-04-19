using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Controllers;

[Authorize]
[Route("product")]
public class ProductController(ICampusDbContext campusDbContext, IMapper mapper, IHttpContextAccessor context)
    : Controller
{
    private readonly IMapper _mapper = mapper;
    private readonly string _campus = context.HttpContext?.User?.FindFirst("CampusKey")?.Value ?? "pp";

    [Route("all")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("get-product-category-list")]
    public IActionResult GetProductCategoryList(bool isAll = false)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query = db.TblCategory.AsQueryable();
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
            
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.CategoryName!.Contains(searchValue) ||
                    d.CategoryId.ToString().Contains(searchValue)
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
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                code = "500",
                message = $"Internal Server Error:{e.Message}"
            });
        }
    }

    [HttpPost("get-product-list")]
    public IActionResult GetProductList(bool isAll = false)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);
            var query = db.TblProduct.Where(x => x.Status == "ACTIVE").AsQueryable();
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
            
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.ProductId.ToString().Contains(searchValue) ||
                    d.ProductName!.Contains(searchValue) ||
                    d.ProductNameInKhmer!.Contains(searchValue)
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
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                code = "500",
                message = $"Internal Server Error:{e.Message}"
            });
        }
    }

    [HttpPost("get-product-detail-list")]
    public IActionResult GetProductDetailList(bool isAll = false)
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
            var query = (from pd in db.TblProductDetails
                    join p in db.TblProduct on pd.ProductId equals p.ProductId
                    join d in db.TblDegree on pd.DegreeId equals d.DegreeId
                    join s in db.TblSchool on pd.SchoolId equals s.SchoolId
                    select new
                    {
                        pd.ProductDetailId, pd.ProductId, p.ProductName, p.Price, p.PriceKhr, d.DegreeId, d.DegreeName,
                        d.DegreeInKhmer, s.SchoolId, s.SchoolName, s.SchoolNameInKhmer, pd.FromPromotionNo
                    }
                ).AsQueryable();
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
            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(d =>
                    d.ProductId.ToString()!.Contains(searchValue)
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
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                code = "500",
                message = $"Internal Server Error:{e.Message}"
            });
        }
    }
    
    [HttpPost("save-change-product")]
    public async Task<IActionResult> SaveChange(ProductDto product)
    {
        try
        {
            product.Status = "ACTIVE";
            var db = campusDbContext.DbContext(_campus);
            var isExist = db.TblProduct.Any(x => x.ProductId == product.ProductId);
            if (!isExist)
            {
                var newData =  _mapper.Map<ProductDto, Product>(product);
                await db.TblProduct.AddAsync(newData);
                await db.SaveChangesAsync();
                return Ok(new
                {
                    data=newData,
                    status = new
                    {
                        code = "200",
                        message = "Saved successfully"
                    }
                });
            }
            var data =await db.TblProduct.FirstOrDefaultAsync(x => x.ProductId == product.ProductId);
            if (data == null)
                return BadRequest(new
                {
                    data = new { },
                    status = new
                    {
                        code = "400",
                        message = "Product not found"
                    }
                });
            _mapper.Map(product, data);
            db.TblProduct.Update(data);
            await db.SaveChangesAsync();
            return Ok(new
            {
                data,
                status = new
                {
                    code = "200",
                    message = "Updated successfully"
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
                    message = $"Internal Server Error:{e.InnerException!.Message}"
                }
            });
        }
    }
}