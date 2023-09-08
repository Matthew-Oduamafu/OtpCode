using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OtpCode.Api.Data.Entities;

namespace OtpCode.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public DbSet<OtpEntry> OtpEntries => Set<OtpEntry>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<OtpEntry>().ToTable("OtpEntries");
    }
}