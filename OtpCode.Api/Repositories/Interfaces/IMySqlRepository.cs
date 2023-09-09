namespace OtpCode.Api.Repositories.Interfaces;

public interface IMySqlRepository
{
    Task<int> SaveChangesAsync();
}