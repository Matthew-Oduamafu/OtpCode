using System.ComponentModel.DataAnnotations;
using OtpCode.Api.CustomAttributes;

namespace OtpCode.Api.Models.Dtos;

public class VerifyOtpCodeRequest
{
    [Phone] [Required] [ValidPhoneNumber] public string PhoneNumber { get; set; }
    [Required] [MaxLength(2)] public string CountryCode { get; set; }
    public string OtpCode { get; set; }
}