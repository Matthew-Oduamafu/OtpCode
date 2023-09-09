namespace OtpCode.Api.Models.Dtos;

public class VerifyOtpCodeRequest
{
    public string PhoneNumber { get; set; }
    public string OtpCode { get; set; }
}