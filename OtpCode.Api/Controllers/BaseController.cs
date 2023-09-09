using Microsoft.AspNetCore.Mvc;
using OtpCode.Api.Models;

namespace OtpCode.Api.Controllers;

public abstract class BaseController : ControllerBase
{
    public IActionResult ToActionResult<T>(IApiResponse<T> apiResponse)
    {
        return StatusCode(apiResponse.Code, apiResponse);
    }
}