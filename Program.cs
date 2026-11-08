using System.Globalization; 
using BBU_SYSTEM.Repository; 
using BBU_SYSTEM.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;

AppContext.SetSwitch("Switch.Microsoft.ReportingServices.DisableTelemetry", true);

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
//======Add Scope=====
builder.Services.AddSingleton<ICampusDbContext, CampusDbContextService>();
//=====Add Automapper
// builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//======New === Configure Cookie Authentication=======
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.None;
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.Name = "BBU_SYSTEM";
        options.Cookie.Domain = null;
        options.Cookie.Path = "/";
        options.Cookie.IsEssential = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(1); // For sliding expiration if user stays active
        options.SlidingExpiration = true;

        options.Cookie.SameSite = SameSiteMode.Lax;

        options.LoginPath = "/Account/Login"; // Redirect unauthenticated users here
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect unauthorized users here
    });

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor
builder.Services.AddScoped<AuthService>(); // Register your AuthService
builder.Services.AddCors(o => {
    o.AddPolicy("AllowAll", b =>
    {
        b.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Enable detailed logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
//======New
//builder.WebHost.UseUrls("https://192.172.23.168:8000");


builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

var app = builder.Build();

var supportedCultures = new[] {new CultureInfo("km") , new CultureInfo("en")};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("km"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

app.UseRequestLocalization(localizationOptions);

// var app = builder.Build();


app.Use(async (context, next) =>
{
    context.Response.Headers.CacheControl = "no-store, no-cache, must-revalidate";
    context.Response.Headers.Pragma = "no-cache";
    context.Response.Headers.Expires = "0";
    await next();
});

if (app.Environment.IsDevelopment())
{
    // Show full error details for debugging
    app.UseDeveloperExceptionPage();
}
else
{
    // For Production and Staging
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/Files")
        && !context.User.Identity?.IsAuthenticated == true)
    {
        context.Response.StatusCode = 401;
        return;
    }

    await next();
});
app.UseStaticFiles();
// map for testing api
app.MapControllers();
// app.UseMiddleware<UserActivityMiddleware>();
app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");
app.Run();