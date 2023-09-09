using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OtpCode.Api.CustomAttributes;

namespace OtpCode.Api.Models.Dtos;

public class GenerateOtpCodeRequest
{
    [Phone] [Required] [ValidPhoneNumber] public string PhoneNumber { get; set; }
    [Required] [MaxLength(2)] public string CountryCode { get; set; }
    [JsonIgnore] public string Purpose { get; set; }
}