using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OtpCode.Api.Data;
using OtpCode.Api.Options;
using OtpCode.Api.Repositories.Interfaces;
using OtpCode.Api.Repositories.Providers;
using OtpCode.Api.Services.Interfaces;
using OtpCode.Api.Services.Providers;

namespace OtpCode.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGenericSpcRepository, GenericSpcRepository>();
        services.AddScoped<IOtpCodeRepository, OtpCodeRepository>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IOtpCodeService, OtpCodeService>();
        services.AddScoped<ISendSmsService, SendSmsService>();
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

    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "OTP API",
                Version = "v1",
                Description = "API to send, resend, and confirm OTP"
            });

            c.EnableAnnotations();

            // Optionally, add XML comments:
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });
    }
    // public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
    // {
    //     app.UseMiddleware<ExceptionMiddleware>(logger);
    // }
}