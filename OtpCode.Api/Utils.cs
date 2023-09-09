using System.Security.Cryptography;
using System.Text;
using PhoneNumbers;

namespace OtpCode.Api;

public static class Utils
{
    public static string GenerateNumericOtp(int length)
    {
        var randomNumber = new byte[1];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        StringBuilder result = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            rng.GetBytes(randomNumber);
            int digit = (randomNumber[0] % 10);
            result.Append(digit.ToString());
        }

        return result.ToString();
    }
    
    public static bool IsValid(string phoneNumber, string countryCode)
    {
        try
        {
            var number = PhoneNumberUtil.GetInstance().Parse(phoneNumber, countryCode);
            return PhoneNumberUtil.GetInstance().IsValidNumber(number);
        }
        catch (NumberParseException)
        {
            return false;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}