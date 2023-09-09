using OtpCode.Api.Data;
using OtpCode.Api.Repositories.Interfaces;

namespace OtpCode.Api.Repositories.Providers;

public class MySqlRepository:IMySqlRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MySqlRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}