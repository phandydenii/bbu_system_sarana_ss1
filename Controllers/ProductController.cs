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

            var query = db.TblProduct
                .Where(x => x.Status == "ACTIVE")
                .AsQueryable();

            if (isAll)
            {
                var allData = query
                    .OrderByDescending(x => x.ProductId)
                    .ToList();

                return new ServerResponse().Success(allData);
            }

            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = !string.IsNullOrEmpty(length) ? Convert.ToInt32(length) : 10;
            var skip = !string.IsNullOrEmpty(start) ? Convert.ToInt32(start) : 0;

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            var recordsTotal = query.Count();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(x =>
                    x.ProductId.ToString().Contains(searchValue) ||
                    (x.ProductName ?? "").Contains(searchValue) ||
                    (x.ProductNameInKhmer ?? "").Contains(searchValue)
                );
            }

            var recordsFiltered = query.Count();

            var data = query
                .OrderByDescending(x => x.ProductId)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            return Json(new
            {
                draw,
                recordsTotal,
                recordsFiltered,
                data
            });
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

    [HttpPost("get-product-detail-list")]
    public IActionResult GetProductDetailList(bool isAll = false)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            var query =
                (
                    from pd in db.TblProductDetails
                    join p in db.TblProduct on pd.ProductId equals p.ProductId
                    join d in db.TblDegree on pd.DegreeId equals d.DegreeId
                    join s in db.TblSchool on pd.SchoolId equals s.SchoolId
                    where p.Status == "ACTIVE"
                    select new
                    {
                        productDetailId = pd.ProductDetailId,
                        productId = pd.ProductId,

                        productName = p.ProductName,
                        price = p.Price,
                        priceKhr = p.PriceKhr,

                        degreeId = d.DegreeId,
                        degreeName = d.DegreeName,
                        degreeInKhmer = d.DegreeInKhmer,

                        schoolId = s.SchoolId,
                        schoolName = s.SchoolName,
                        schoolNameInKhmer = s.SchoolNameInKhmer,

                        fromPromotionNo = pd.FromPromotionNo
                    }
                )
                .AsQueryable();

            if (isAll)
            {
                var allData = query
                    .OrderByDescending(x => x.productDetailId)
                    .ToList();

                return new ServerResponse().Success(allData, "Succeeded!");
            }

            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var pageSize = !string.IsNullOrEmpty(length) ? Convert.ToInt32(length) : 10;
            var skip = !string.IsNullOrEmpty(start) ? Convert.ToInt32(start) : 0;

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            var recordsTotal = query.Count();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(x =>
                    x.productId.ToString().Contains(searchValue) ||
                    (x.productName ?? "").Contains(searchValue) ||
                    (x.degreeName ?? "").Contains(searchValue) ||
                    (x.degreeInKhmer ?? "").Contains(searchValue) ||
                    (x.schoolName ?? "").Contains(searchValue) ||
                    (x.schoolNameInKhmer ?? "").Contains(searchValue)
                );
            }

            var recordsFiltered = query.Count();

            var data = query
                .OrderByDescending(x => x.productDetailId)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            return Json(new
            {
                draw,
                recordsTotal,
                recordsFiltered,
                data
            });
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }
    [HttpPost("save-change-product-detail")]
    public async Task<IActionResult> SaveChangeProductDetail([FromForm] ProductDetailDto? productDetail)
    {
        try
        {
            if (productDetail == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }

            if (productDetail.ProductId == null || productDetail.ProductId <= 0)
            {
                return new ServerResponse().BadRequest("Product is required.");
            }

            if (productDetail.DegreeId == null || productDetail.DegreeId <= 0)
            {
                return new ServerResponse().BadRequest("Degree is required.");
            }

            if (productDetail.SchoolId == null || productDetail.SchoolId <= 0)
            {
                return new ServerResponse().BadRequest("School is required.");
            }

            var db = campusDbContext.DbContext(_campus);

            var data = await db.TblProductDetails
                .FirstOrDefaultAsync(x => x.ProductDetailId == productDetail.ProductDetailId);

            if (data == null)
            {
                var newData = new ProductDetail
                {
                    ProductId = productDetail.ProductId.Value,
                    DegreeId = productDetail.DegreeId.Value,
                    SchoolId = productDetail.SchoolId.Value,
                    FromPromotionNo = productDetail.FromPromotionNo ?? 0
                };

                await db.TblProductDetails.AddAsync(newData);
                await db.SaveChangesAsync();

                return new ServerResponse().Success(newData, "Saved successfully!");
            }

            data.ProductId = productDetail.ProductId.Value;
            data.DegreeId = productDetail.DegreeId.Value;
            data.SchoolId = productDetail.SchoolId.Value;
            data.FromPromotionNo = productDetail.FromPromotionNo ?? 0;

            db.TblProductDetails.Update(data);
            await db.SaveChangesAsync();

            return new ServerResponse().Success(data, "Updated successfully!");
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }
    
    [HttpPost("save-change-product")]
    public async Task<IActionResult> SaveChange([FromForm] ProductDto? product)
    {
        try
        {
            if (product == null)
            {
                return new ServerResponse().BadRequest("Bad Request!");
            }

            var db = campusDbContext.DbContext(_campus);

            product.Status = "ACTIVE";

            var data = await db.TblProduct
                .FirstOrDefaultAsync(x => x.ProductId == product.ProductId);

            if (data == null)
            {
                var newData = _mapper.Map<ProductDto, Product>(product);
                newData.ProductNameInKhmer = product.ProductNameInKhmer;

                await db.TblProduct.AddAsync(newData);
                await db.SaveChangesAsync();

                return new ServerResponse().Success(newData, "Saved successfully");
            }
            
            data.ProductName = product.ProductName;
            data.ProductNameInKhmer = product.ProductNameInKhmer;
            data.PriceKhr = product.PriceKhr;
            data.Price = product.Price;
            data.CategoryId = product.CategoryId;
            data.DegreeId = product.DegreeId;
            data.FromPromotion = product.FromPromotion;
            data.Vat = product.Vat;
            data.CardCertificate = product.CardCertificate;
            data.Type = product.Type;
            data.TuitionFees = product.TuitionFees;
            data.Hidden = product.Hidden;
            data.PaymentType = product.PaymentType;
            data.Status = "ACTIVE";

            db.TblProduct.Update(data);
            await db.SaveChangesAsync();

            return new ServerResponse().Success(data, "Updated successfully");
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }
    
    [HttpDelete("delete-product/{productId}")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        try
        {
            var db = campusDbContext.DbContext(_campus);

            var product = await db.TblProduct
                .FirstOrDefaultAsync(x => x.ProductId == productId);

            if (product == null)
            {
                return new ServerResponse().NotFound("Product not found.");
            }

            product.Status = "DELETED";

            db.TblProduct.Update(product);
            await db.SaveChangesAsync();

            return new ServerResponse().Success(product, "Deleted successfully!");
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }
}