using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OtpCode.Api.Data;
using OtpCode.Api.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureOptions<DatabaseConfigSetup>();

builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    var databaseConfig = serviceProvider.GetRequiredService<IOptions<DatabaseConfig>>().Value;
    options.UseMySql(databaseConfig.DbConnectionString, ServerVersion.AutoDetect(databaseConfig.DbConnectionString),
        optionsBuilder =>
        {
            optionsBuilder.CommandTimeout(databaseConfig.CommandTimeout);
            if (databaseConfig.EnableRetryOnFailure)
            {
                optionsBuilder.EnableRetryOnFailure(databaseConfig.MaxRetryCount, 
                    TimeSpan.FromSeconds(databaseConfig.MaxRetryDelay), 
                    databaseConfig.ErrorNumbersToAdd);
            }
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
