using System.Data.SqlClient;
using BBU_SYSTEM.Data;
using BBU_SYSTEM.Repository;
using Microsoft.EntityFrameworkCore;

namespace BBU_SYSTEM.Service;

public class CampusDbContextService(IConfiguration configuration) : ICampusDbContext
{
    public CampusDbContext DbContext(string campusKey)
    {
        
        var connectionString = configuration.GetConnectionString($"{campusKey}_campus");
        var optionsBuilder = new DbContextOptionsBuilder<CampusDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        
  
        return new CampusDbContext(optionsBuilder.Options);
    }
}