using System.Text.Json.Serialization;

namespace OtpCode.Api.Models.Dtos;

public class GenerateOtpCodeRequest
{
    public string PhoneNumber { get; set; }
    [JsonIgnore]
    public string Purpose { get; set; }
}