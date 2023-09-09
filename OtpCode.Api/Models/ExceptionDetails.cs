namespace OtpCode.Api.Models;

public class ExceptionDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Details { get; set; }
}