namespace BBU_SYSTEM.Data;

public class SessionAuthMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        //var routeData = context.GetRouteData();

        //if (routeData != null)
        //{
        //    context.Items["ControllerName"] = routeData.Values["controller"]?.ToString();
        //    context.Items["ActionName"] = routeData.Values["action"]?.ToString();
        //}


        var path = context.Request.Path;

        // Allow anonymous paths
        if (path.StartsWithSegments("/Account") || path.StartsWithSegments("/css") || path.StartsWithSegments("/js"))
        {
            await next(context);
            return;
        }

        // Check session
        var username = context.Session.GetString("username");
        if (string.IsNullOrEmpty(username))
        {
            context.Response.Redirect("/account/login");
            return;
        }

        await next(context);
    }
}