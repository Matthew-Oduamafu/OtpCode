using OtpCode.Api.Extensions;
using OtpCode.Api.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<OtpCodeConfig>(c =>
    builder.Configuration.GetSection(nameof(OtpCodeConfig)).Bind(c));
builder.Services.AddDatabaseConfiguration();
builder.Services.AddRepositories();
builder.Services.AddServices();


builder.Services.AddControllerConfiguration();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
