using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OtpCode.Api.Data;
using OtpCode.Api.Options;
using OtpCode.Api.Repositories.Interfaces;
using OtpCode.Api.Repositories.Providers;

namespace OtpCode.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGenericSpcRepository, GenericSpcRepository>();
        services.AddScoped<IOtpCodeRepository, OtpCodeRepository>();
        return services;
    }

    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services)
    {
        services.ConfigureOptions<DatabaseConfigSetup>();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var databaseConfig = serviceProvider.GetRequiredService<IOptions<DatabaseConfig>>().Value;
            options.UseMySql(databaseConfig.DbConnectionString,
                ServerVersion.AutoDetect(databaseConfig.DbConnectionString),
                optionsBuilder =>
                {
                    optionsBuilder.CommandTimeout(databaseConfig.CommandTimeout);
                    if (databaseConfig.EnableRetryOnFailure)
                    {
                        optionsBuilder.EnableRetryOnFailure(databaseConfig.MaxRetryCount,
                            TimeSpan.FromSeconds(databaseConfig.MaxRetryDelay),
                            databaseConfig.ErrorNumbersToAdd);
                    }
                    optionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                });
        });
        services.AddTransient(typeof(DatabaseConnect));
        return services;
    }
    
    public static IServiceCollection AddControllerConfiguration(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        services.AddControllers(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
        return services;
    }
}