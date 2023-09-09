using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using OtpCode.Api.Models;
using OtpCode.Api.Models.Dtos;
using OtpCode.Api.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OtpCode.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
[ApiVersion("1.0")]
public class OtpController : BaseController
{
    private readonly ILogger<OtpController> _logger;
    private readonly IOtpCodeService _otpCodeService;

    public OtpController(ILogger<OtpController> logger, IOtpCodeService otpCodeService)
    {
        _logger = logger;
        _otpCodeService = otpCodeService;
    }

    /// <summary>
    /// Sends an OTP code to the specified phone number
    /// </summary>
    /// <param name="request">must be valid</param>
    /// <returns></returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<GenerateOtpCodeRequest>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
    [SwaggerOperation(nameof(Send), OperationId = nameof(Send))]
    public async Task<IActionResult> Send(GenerateOtpCodeRequest request)
    {
        request.Purpose = CommonConstants.OtpCodePurpose.PhoneVerification;
        var apiResponse = await _otpCodeService.GenerateOtpCodeAsync(request);
        return ToActionResult(apiResponse);
    }

    /// <summary>
    /// Resends an OTP code to the specified phone number
    /// </summary>
    /// <param name="request">must be valid</param>
    /// <returns></returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<GenerateOtpCodeRequest>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
    [SwaggerOperation(nameof(Resend), OperationId = nameof(Resend))]
    public async Task<IActionResult> Resend(GenerateOtpCodeRequest request)
    {
        var apiResponse = await _otpCodeService.ResendOtpCodeAsync(request);
        return ToActionResult(apiResponse);
    }

    /// <summary>
    /// Confirms an OTP code to the specified phone number
    /// </summary>
    /// <param name="request">must be valid</param>
    /// <returns></returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<VerifyOtpCodeRequest>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
    [SwaggerOperation(nameof(Confirm), OperationId = nameof(Confirm))]
    public async Task<IActionResult> Confirm(VerifyOtpCodeRequest request)
    {
        var apiResponse = await _otpCodeService.VerifyOtpCodeAsync(request);
        return ToActionResult(apiResponse);
    }
}