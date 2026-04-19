using BBU_SYSTEM.Data;

namespace BBU_SYSTEM.Repository;

public interface ICampusDbContext
{
    CampusDbContext DbContext(string campusKey);
}