using OtpCode.Api.Data.Entities;

namespace OtpCode.Api.Repositories.Interfaces;

public interface IOtpCodeRepository
{
    Task SaveOtp(OtpEntry otpEntry);
    Task<OtpEntry> GetLatestUnusedOtpForPhoneNumber(string phoneNumber);
    Task<OtpEntry> GetOtpForPhoneNumber(string phoneNumber);
    Task UpdateOtpEntry(OtpEntry otpEntry);
}