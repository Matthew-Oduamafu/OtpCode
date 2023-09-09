using System.ComponentModel.DataAnnotations;
using System.Globalization;
using OtpCode.Api.Models.Dtos;

namespace OtpCode.Api.CustomAttributes;

/// <inheritdoc />
public sealed class ValidPhoneNumberAttribute : ValidationAttribute
{
    // /// <inheritdoc />
    // public override bool IsValid(object value)
    // {
    //     var phoneNumber = (string)value;
    //     
    //     return Utils.IsValid(phoneNumber, "VN");
    // }
    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var instance = (GenerateOtpCodeRequest)validationContext.ObjectInstance; // this is the full object instance
        var phoneNumber = (string)value;

        if (Utils.IsValid(phoneNumber, instance.CountryCode))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
    }
    
    public override string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.CurrentCulture,
            ErrorMessageString, name);
    }
}