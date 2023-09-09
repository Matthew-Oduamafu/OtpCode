using System.Security.Cryptography;
using System.Text;

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
}