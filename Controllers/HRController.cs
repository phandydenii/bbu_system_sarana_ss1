using AspNetCore.Reporting;
using BBU_SYSTEM.Helper;
using Microsoft.AspNetCore.Mvc;


namespace BBU_SYSTEM.Controllers;
public class HrController : Controller
{
    // GET: /<controller>/
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost] 
    public async Task<IActionResult> UploadFile()
    {
        try
        {
            if (Request.Form.Files.Count == 0)
                return BadRequest("No file uploaded");

            var file = Request.Form.Files[0];

            if (file.Length == 0)
                return BadRequest("Empty file");

            // Folder path
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Files");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // Generate unique file name
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            // Save file
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new
            {
                success = true,
                originalName = file.FileName,
                savedName = fileName,
                size = file.Length
            });
        }
        catch (Exception ex)
        {
            return new ServerResponse().ErrorInternal(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> UploadMultiple()
    {
        try
        {
            if (Request.Form.Files.Count == 0)
                return new ServerResponse().BadRequest("No file uploaded");

            var uploadPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Files"
            );

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (var file in Request.Form.Files)
            {
                if (file.Length == 0) continue;

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadPath, fileName);

                await using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);
            }

            return new ServerResponse().Success();
        }
        catch (Exception e)
        {
            return new ServerResponse().ErrorInternal(e);
        }
    }

}