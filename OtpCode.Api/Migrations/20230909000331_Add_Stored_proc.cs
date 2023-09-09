using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtpCode.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredproc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("START TRANSACTION;");
            migrationBuilder.Sql(@"DROP procedure IF EXISTS `InsertOtpEntry`");
            migrationBuilder.Sql(@"CREATE PROCEDURE InsertOtpEntry(
                    IN p_Id VARCHAR(36),
                    IN p_PhoneNumber VARCHAR(20),
                    IN p_OtpCode VARCHAR(10),
                    IN p_CreatedDate DATETIME,
                    IN p_ExpiryDate DATETIME,
                    IN p_IsUsed BOOLEAN,
                    IN p_InvalidAttempts INT,
                    IN p_Purpose VARCHAR(50), 
                    IN p_Metadata TEXT
                )
                BEGIN
                    INSERT INTO OtpEntries 
                    (Id, PhoneNumber, OtpCode, CreatedDate, ExpiryDate, IsUsed, InvalidAttempts, Purpose, Metadata) 
                    VALUES 
                    (p_Id, p_PhoneNumber, p_OtpCode, p_CreatedDate, p_ExpiryDate, p_IsUsed, p_InvalidAttempts, p_Purpose, p_Metadata);
                END;");

            migrationBuilder.Sql("COMMIT;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP procedure IF EXISTS `InsertOtpEntry`");
        }
    }
}
