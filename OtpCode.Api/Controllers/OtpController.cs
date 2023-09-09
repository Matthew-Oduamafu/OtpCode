using Microsoft.AspNetCore.Mvc;
using OtpCode.Api.Models.Dtos;
using OtpCode.Api.Services.Interfaces;

namespace OtpCode.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OtpController : BaseController
{
    private readonly ILogger<OtpController> _logger;
    private readonly IOtpCodeService _otpCodeService;

    public OtpController(ILogger<OtpController> logger, IOtpCodeService otpCodeService)
    {
        _logger = logger;
        _otpCodeService = otpCodeService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Send(GenerateOtpCodeRequest request)
    {
        var apiResponse = await _otpCodeService.GenerateOtpCodeAsync(request);
        return ToActionResult(apiResponse);
    }
    
    [HttpPost]
    public async Task<IActionResult> Resend(GenerateOtpCodeRequest request)
    {
        var apiResponse = await _otpCodeService.ResendOtpCodeAsync(request);
        return ToActionResult(apiResponse);
    }
    
    [HttpPost]
    public async Task<IActionResult> Confirm(VerifyOtpCodeRequest request)
    {
        var apiResponse = await _otpCodeService.VerifyOtpCodeAsync(request);
        return ToActionResult(apiResponse);
    }
}