using OtpCode.Api.Extensions;
using OtpCode.Api.Options;

var builder = WebApplication.CreateBuilder(args);

const string corsPolicyName = "OtpCode.Api.PolicyName";

// Add services to the container.
builder.Services.Configure<OtpCodeConfig>(c =>
    builder.Configuration.GetSection(nameof(OtpCodeConfig)).Bind(c));
builder.Services.AddDatabaseConfiguration();
builder.Services.AddRepositories();
builder.Services.AddServices();


builder.Services.AddControllerConfiguration();

builder.Services.AddCors(options => options
    .AddPolicy(corsPolicyName, policy => policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();
{
    var logger = app.Logger;
    await app.MigrateDatabaseAsync();
// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }else
    {
        app.UseCustomExceptionHandler(logger);
    }
    
    app.UseSwaggerDocumentation();

    app.UseCors(corsPolicyName);
    app.UseRouting();
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    await app.RunAsync();
}