namespace OtpCode.Api.Services.Interfaces;

public interface ISendSmsService
{
    Task SendSmsAsync(string phoneNumber, string message);
}