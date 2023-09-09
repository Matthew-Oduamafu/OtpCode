using Microsoft.Extensions.Options;
using OtpCode.Api.Models;
using OtpCode.Api.Models.Dtos;
using OtpCode.Api.Options;
using OtpCode.Api.Repositories.Interfaces;
using OtpCode.Api.Services.Interfaces;

namespace OtpCode.Api.Services.Providers;

public class OtpCodeService : IOtpCodeService
{
    private readonly ILogger<OtpCodeService> _logger;
    private readonly OtpCodeConfig _otpCodeConfig;
    private readonly IOtpCodeRepository _otpCodeRepository;

    public OtpCodeService(ILogger<OtpCodeService> logger,
        IOptions<OtpCodeConfig> options,
        IOtpCodeRepository otpCodeRepository)
    {
        _logger = logger;
        _otpCodeConfig = options.Value;
        _otpCodeRepository = otpCodeRepository;
    }

    public async Task<IApiResponse<GenerateOtpCodeRequest>> GenerateOtpCodeAsync(GenerateOtpCodeRequest request)
    {
        try
        {
            // check if there is any unused otp code for this phone number
            var otpEntry = await _otpCodeRepository.GetLatestUnusedOtpForPhoneNumber(request.PhoneNumber);
            if (otpEntry != null)
            {
                if (otpEntry.IsUsed && otpEntry.ExpiryDate < DateTime.UtcNow)
                {
                    request.ToOkApiResponse();
                }
            }
            
            var code = Utils.GenerateNumericOtp(_otpCodeConfig.OtpCodeLength);
            var newOtpEntry = new Data.Entities.OtpEntry
            {
                PhoneNumber = request.PhoneNumber,
                CountryCode = request.CountryCode,
                OtpCode = code,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(_otpCodeConfig.OtpCodeExpirationTime),
                Purpose = request.Purpose,
                IsUsed = false,
                InvalidAttempts = 0
            };
            await _otpCodeRepository.SaveOtp(newOtpEntry);

            return request.ToOkApiResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while generating otp code, with request: {@Request}", request);
            return ApiResponse<GenerateOtpCodeRequest>.Default.ToInternalServerErrorApiResponse();
        }
    }

    public async Task<IApiResponse<GenerateOtpCodeRequest>> ResendOtpCodeAsync(GenerateOtpCodeRequest request)
    {
        try
        {
            var otpEntry = await _otpCodeRepository.GetLatestUnusedOtpForPhoneNumber(request.PhoneNumber);
            if (otpEntry == null)
            {
                return ApiResponse<GenerateOtpCodeRequest>.Default.ToNotFoundApiResponse();
            }

            if (otpEntry.InvalidAttempts >= 3)
            {
                return ApiResponse<GenerateOtpCodeRequest>.Default.ToBadRequestApiResponse("Exceeded maximum number of invalid attempts.");
            }

            if (otpEntry.IsUsed)
            {
                return ApiResponse<GenerateOtpCodeRequest>.Default.ToBadRequestApiResponse("OTP code has already been used.");
            }

            if (otpEntry.ExpiryDate < DateTime.UtcNow)
            {
                return ApiResponse<GenerateOtpCodeRequest>.Default.ToBadRequestApiResponse("OTP code has expired.");
            }

            var code = Utils.GenerateNumericOtp(_otpCodeConfig.OtpCodeLength);
            otpEntry.OtpCode = code;
            otpEntry.ExpiryDate = DateTime.UtcNow.AddMinutes(_otpCodeConfig.OtpCodeExpirationTime);
            otpEntry.InvalidAttempts = 0;
            otpEntry.UpdatedAt = DateTime.UtcNow;
            await _otpCodeRepository.UpdateOtpEntry(otpEntry);

            return request.ToOkApiResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while resending otp code, with request: {@Request}", request);
            return ApiResponse<GenerateOtpCodeRequest>.Default.ToInternalServerErrorApiResponse();
        }
    }

    public async Task<IApiResponse<VerifyOtpCodeRequest>> VerifyOtpCodeAsync(VerifyOtpCodeRequest request)
    {
        try
        {
            var otpEntry = await _otpCodeRepository.GetLatestUnusedOtpForPhoneNumber(request.PhoneNumber);
            if (otpEntry == null)
            {
                return ApiResponse<VerifyOtpCodeRequest>.Default.ToNotFoundApiResponse();
            }

            if (otpEntry.InvalidAttempts >= 3)
            {
                return ApiResponse<VerifyOtpCodeRequest>.Default.ToBadRequestApiResponse("Exceeded maximum number of invalid attempts.");
            }

            if (otpEntry.IsUsed)
            {
                return ApiResponse<VerifyOtpCodeRequest>.Default.ToBadRequestApiResponse("OTP code has already been used.");
            }

            if (otpEntry.ExpiryDate < DateTime.UtcNow)
            {
                return ApiResponse<VerifyOtpCodeRequest>.Default.ToBadRequestApiResponse("OTP code has expired.");
            }

            if (otpEntry.OtpCode != request.OtpCode)
            {
                otpEntry.InvalidAttempts++;
                await _otpCodeRepository.UpdateOtpEntry(otpEntry);
                return ApiResponse<VerifyOtpCodeRequest>.Default.ToBadRequestApiResponse("Invalid OTP code.");
            }

            otpEntry.IsUsed = true;
            otpEntry.UpdatedAt = DateTime.UtcNow;
            await _otpCodeRepository.UpdateOtpEntry(otpEntry);

            return request.ToOkApiResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while verifying otp code, with request: {@Request}", request);
            return ApiResponse<VerifyOtpCodeRequest>.Default.ToInternalServerErrorApiResponse();
        }
    }
}