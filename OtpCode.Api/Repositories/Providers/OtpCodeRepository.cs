using Microsoft.Extensions.Options;
using OtpCode.Api.Data.Entities;
using OtpCode.Api.Options;
using OtpCode.Api.Repositories.Interfaces;

namespace OtpCode.Api.Repositories.Providers;

public class OtpCodeRepository : GenericSpcRepository, IOtpCodeRepository
{
    private string DbConString { get; set; }

    public OtpCodeRepository(IOptions<DatabaseConfig> databaseConfig): base(databaseConfig)
    {
        DbConString = databaseConfig.Value.DbConnectionString;
    }

    public async Task SaveOtp(OtpEntry otpEntry)
    {
        var param = new
        {
            p_Id = otpEntry.Id,
            p_PhoneNumber = otpEntry.PhoneNumber,
            p_OtpCode = otpEntry.OtpCode,
            p_CreatedDate = otpEntry.CreatedDate,
            p_ExpiryDate = otpEntry.ExpiryDate,
            p_IsUsed = otpEntry.IsUsed,
            p_InvalidAttempts = otpEntry.InvalidAttempts,
            p_Purpose = otpEntry.Purpose,
            p_MetaData = otpEntry.Metadata
        };
        await Execute("InsertOtpEntry", param);
    }

    public async Task<OtpEntry> GetLatestUnusedOtpForPhoneNumber(string phoneNumber)
    {
        var param = new
        {
            p_PhoneNumber = phoneNumber
        };
        return await Single<OtpEntry>("GetLatestUnusedOtpForPhoneNumber", param);
    }

    public async Task<OtpEntry> GetOtpForPhoneNumber(string phoneNumber)
    {
        var param = new
        {
            p_PhoneNumber = phoneNumber
        };
        return await Single<OtpEntry>("GetOtpForPhoneNumber", param);
    }

    public async Task UpdateOtpEntry(OtpEntry otpEntry)
    {
        var param = new
        {
            p_Id = otpEntry.Id,
            p_IsUsed = otpEntry.IsUsed,
            p_InvalidAttempts = otpEntry.InvalidAttempts,
            p_UpdatedAt = otpEntry.UpdatedAt
        };
        await Execute("UpdateOtpEntry", param);
    }
}