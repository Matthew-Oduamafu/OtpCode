namespace OtpCode.Api.Options;

public class DatabaseConfig
{
    public string DbConnectionString { get; set; } = null!;
    public bool EnableRetryOnFailure { get; set; }
    public int MaxRetryCount { get; set; }
    public int MaxRetryDelay { get; set; }
    public int[] ErrorNumbersToAdd { get; set; } = null!;
    public int CommandTimeout { get; set; }
}