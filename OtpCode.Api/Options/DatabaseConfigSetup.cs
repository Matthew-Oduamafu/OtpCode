using Microsoft.Extensions.Options;

namespace OtpCode.Api.Options;

public class DatabaseConfigSetup: IConfigureOptions<DatabaseConfig>
{
    private readonly IConfiguration _configuration;

    public DatabaseConfigSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(DatabaseConfig options)
    {
        options.DbConnectionString = _configuration.GetConnectionString("DbConnection") ?? string.Empty;
        options.CommandTimeout = 30;
        options.EnableRetryOnFailure = true;
    }
}