using Microsoft.AspNetCore.Mvc;

namespace BBU_SYSTEM.Helper;

public class ServerResponse : ControllerBase
{
    // public IActionResult Success<T>(IQueryable<T>? data1 =null,string msg="Success")
    // {
    //     return Ok(new
    //     {
    //         data = data1 ? data1.ToList() : new{},
    //         status = new
    //         {
    //             code = "200",
    //             message = msg
    //         }
    //     });
    // }
    
    public IActionResult Success(object? data1 =null,string msg="Success")
    {
        return Ok(new
        {
            data = data1 ?? new{},
            status = new
            {
                code = "200",
                message = msg
            }
        });
    }
    public IActionResult BadRequest(string msg = "Bad Request")
    {
        return BadRequest(new
        {
            data = new { },
            status = new
            {
                code = "400",
                message = msg
            }
        });
    }
    
    public IActionResult ErrorInternal(Exception e)
    {
        var message = e.InnerException?.Message ?? e.Message;
        return StatusCode(500, new
        {
            data = new { },
            status = new
            {
                code = "500",
                message = $"Internal Server Error:{message}"
            }
        });
    }
    public IActionResult NotFound(string msg = "Not Found")
    {
        return NotFound(new
        {
            data = new{},
            status = new
            {
                code = "404",
                message = msg
            }
        });
    }
}