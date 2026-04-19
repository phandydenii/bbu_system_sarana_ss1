using System.Data.SqlClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.Models.Req;
using BBU_SYSTEM.Repository;
using Microsoft.AspNetCore.Identity;

namespace BBU_SYSTEM.Service;

public class AuthService(IHttpContextAccessor httpContextAccessor, ICampusDbContext campusDbContext)
{
    public async Task<bool> Login(AuthenticationReq model)
    {
        var db = campusDbContext.DbContext(model.Campus ?? "pp");
        var builder = new SqlConnectionStringBuilder(db.Database.GetDbConnection().ConnectionString);
        // 1. Authenticate the User
        var user = await db.TblUser.Where(u => u.UserName == model.Username && u.Password == model.Password)
            .FirstOrDefaultAsync();

        if (user == null) return false;
        try
        {
            // 2. Retrieve User and Privilege Data
            var userPrivileges = await (from up in db.TblUserPrivilege
                join p in db.TblPrivilege on up.PriviledgeId equals p.PrivilegeId
                where up.UserId == user.UserId
                select new
                {
                    p.PrivilegeId,
                    p.PrivilegeName,
                    p.PrivilegeGroupId
                }).ToListAsync();

            // 3. Create Claims
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserId.ToString()), // Unique identifier for the user
                new(ClaimTypes.Name, user.UserName ?? string.Empty), // User's name
                new(ClaimTypes.Role, user.UserGroup ?? string.Empty), // User's email address'
                new("UserGroup", user.UserGroup ?? string.Empty), // Custom claim for usergroup
                new("Status", user.Status ?? string.Empty), // Custom claim for status
                new("CampusKey", model.Campus ?? "pp"), // Custom claim for status
                new("DataSource", builder.DataSource),
                new("InitialCatalog", builder.InitialCatalog),
            };

            // Add privileges as claims (e.g., using a custom claim type or roles)
            foreach (var privilege in userPrivileges)
                // You might have a separate table for Priviledge_ID to map to a meaningful name
                // For now, let's just add the ID as a claim.
                claims.Add(new Claim("Privilege", privilege.PrivilegeName));
            // If you had a PriviledgeName, you'd use: claims.Add(new Claim("Privilege", privilege.PriviledgeName));
            // Or if you want to use roles: claims. Add(new Claim(ClaimTypes.Role, "Admin"));
            // 4. Construct ClaimsIdentity and ClaimsPrincipal
            var claimsIdentity =
                new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults
                        .AuthenticationScheme); // "CookieAuthentication" matches your authentication scheme name
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // 5. Sign In the User
            // Ensure you have access to HttpContext. You might pass it as a parameter or use IHttpContextAccessor.
            if (httpContextAccessor.HttpContext != null)
            {
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20) // Set an expiration for the cookie
                };
                await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal, authProperties);
                // Console.WriteLine($@"User {model.Username} successfully logged in.");
                return true;
            }
            else
            {
                throw new Exception("HttpContext is null, cannot sign in user.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task Logout()
    { 
        if (httpContextAccessor.HttpContext != null)
        {
            await httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Console.WriteLine(@"User successfully logged out.");
        }
        else
        {
            Console.WriteLine($@"HttpContext is null, cannot sign out user.");
        }
    }

    public async Task<bool> ResetPasswordHash(ResetPasswordDto model, string campusKey)
    {
        var db = campusDbContext.DbContext(campusKey); 
        var user = await db.TblUser.Where(u => u.UserName == model.Username && u.Password == model.CurrentPassword)
            .FirstOrDefaultAsync();
        if (user == null) return false;
        try
        {
            user.PasswordHash = GeneratePasswordHash(model.NewPassword);
            db.TblUser.Update(user);
            await db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private string GeneratePasswordHash(string password)
    { 
        var hasher = new PasswordHasher<object>();
        var hashedPassword = hasher.HashPassword(null!, password);
        return hashedPassword;
    }

    private bool VerifyPasswordHash(string hashedPasswordFromDb, string enteredPassword)
    {
        var hasher = new PasswordHasher<object>();
        var result = hasher.VerifyHashedPassword(null!, hashedPasswordFromDb, enteredPassword);
        if (result == PasswordVerificationResult.Success)
        {
            Console.WriteLine("Password is correct");
            return true;
        }
        Console.WriteLine("Invalid password");
        return false;
    }
}