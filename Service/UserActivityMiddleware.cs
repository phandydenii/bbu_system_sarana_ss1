using System.IdentityModel.Claims;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Repository;

namespace BBU_SYSTEM.Service;

public class UserActivityMiddleware(RequestDelegate next, ICampusDbContext campusFactory)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0";
        var user = context.User.Identity?.IsAuthenticated == true
            ? context.User.Identity.Name
            : "Anonymous";

        var campusKey = context.User.Claims.FirstOrDefault(c => c.Type == "CampusKey")?.Value ?? "pp";

        var controller = context.GetRouteValue("controller")?.ToString();
        var action = context.GetRouteValue("action")?.ToString();
        var ip = context.Connection.RemoteIpAddress?.ToString();
        var ipaddress = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(ipaddress))
            // sometimes multiple IPs: "client, proxy1, proxy2"
            ip = ipaddress.Split(',')[0];
        var userAgent = context.Request.Headers["User-Agent"].ToString();

        // 🔹 create a new DbContext per request
        var db = campusFactory.DbContext(campusKey);

        if (!string.IsNullOrEmpty(controller) && !string.IsNullOrEmpty(action))
        {
            var log = new UserActivityLog
            {
                UserId = int.Parse(userId),
                UserName = user,
                Controller = controller,
                Action = action,
                IpAddress = ip,
                UserAgent = userAgent,
                RequestBody = "",
                ResponseBody = "",
                DateTime = DateTime.Now
            };

            db.UserActivityLogs.Add(log);
            await db.SaveChangesAsync();
        }

        await next(context);
    }
}