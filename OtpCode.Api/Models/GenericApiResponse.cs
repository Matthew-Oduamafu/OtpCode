﻿using System.Net;

namespace OtpCode.Api.Models;

public class GenericApiResponse
{
    
}

public interface IApiResponse<out T>
{
    public string Message { get; }
    public int Code { get; }
    public T? Data { get; }
    public IEnumerable<ErrorResponse>? Errors { get; }
    public static T? Default { get; } = default;
}

public sealed record ApiResponse<T>(string Message, int Code, T? Data = default,
    IEnumerable<ErrorResponse>? Errors = default) : IApiResponse<T>
{
    public static T? Default = default;
}

public sealed record ErrorResponse(string Field, string ErrorMessage);

public static class GenericApiResponseExtensions
{
    public static ApiResponse<T> ToOkApiResponse<T>(this T data, string message = "Success")
        => ToApiResponse(data, message, (int)HttpStatusCode.OK);
    
    public static ApiResponse<T> ToCreatedApiResponse<T>(this T data, string message = "Created")
        => ToApiResponse(data, message, (int)HttpStatusCode.Created);
    
    public static ApiResponse<T> ToAcceptedApiResponse<T>(this T data, string message = "Accepted")
        => ToApiResponse(data, message, (int)HttpStatusCode.Accepted);
    
    public static ApiResponse<T> ToNotFoundApiResponse<T>(this T? data, string message = "Not Found")
        => ToApiResponse<T>(default, message, (int)HttpStatusCode.NotFound);
    
    public static ApiResponse<T> ToBadRequestApiResponse<T>(this T? data, string message = "Request error. Please verify your data and resend")
        => ToApiResponse<T>(default, message, (int)HttpStatusCode.BadRequest);

    public static ApiResponse<T> ToUnAuthorizedApiResponse<T>(this T? data, string message = "UnAuthorized")
        => ToApiResponse<T>(default, message, (int)HttpStatusCode.Unauthorized);
    
    public static ApiResponse<T> ToInternalServerErrorApiResponse<T>(this T? data, string message = "Oh no! Something went wrong")
        => ToApiResponse<T>(default, message, (int)HttpStatusCode.InternalServerError);

    private static ApiResponse<T> ToApiResponse<T>(this T data, string message, int code,
        IEnumerable<ErrorResponse>? errors = null)

    {
        return new ApiResponse<T>(message, code, data, errors);
    }
}