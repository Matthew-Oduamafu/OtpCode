using OtpCode.Api.Models;
using OtpCode.Api.Models.Dtos;

namespace OtpCode.Api.Services.Interfaces;

public interface IOtpCodeService
{
    public Task<IApiResponse<GenerateOtpCodeRequest>> GenerateOtpCodeAsync(GenerateOtpCodeRequest request);
    public Task<IApiResponse<GenerateOtpCodeRequest>> ResendOtpCodeAsync(GenerateOtpCodeRequest request);
    public Task<IApiResponse<VerifyOtpCodeRequest>> VerifyOtpCodeAsync(VerifyOtpCodeRequest request);
}