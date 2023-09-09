namespace OtpCode.Api.Options;

public class OtpCodeConfig
{
    public int OtpCodeLength { get; set; }
    public int OtpCodeExpirationTime { get; set; }
    public int OtpCodeResendTime { get; set; }
}