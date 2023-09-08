using System.ComponentModel.DataAnnotations.Schema;

namespace OtpCode.Api.Data.Entities;

public class OtpEntry
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(TypeName = "varchar(36)")]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    [Column(TypeName = "varchar(20)")]
    public string PhoneNumber { get; set; }
    [Column(TypeName = "varchar(10)")]
    public string OtpCode { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsUsed { get; set; } = false;
    public int InvalidAttempts { get; set; }
    public string? Purpose { get; set; } // Optional - why the OTP was generated.
    public string? Metadata { get; set; } // Optional - any additional data.
}
