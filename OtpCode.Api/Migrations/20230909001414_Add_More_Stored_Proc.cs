using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtpCode.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreStoredProc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("START TRANSACTION;");

            migrationBuilder.Sql(@"DROP procedure IF EXISTS `GetLatestUnusedOtpForPhoneNumber`");
            migrationBuilder.Sql(@"CREATE PROCEDURE GetLatestUnusedOtpForPhoneNumber(IN p_PhoneNumber VARCHAR(20))
                    BEGIN
                        SELECT * FROM OtpEntries 
                        WHERE PhoneNumber = p_PhoneNumber AND IsUsed = 0 
                        ORDER BY CreatedDate DESC 
                        LIMIT 1;
                    END;");

            migrationBuilder.Sql(@"DROP procedure IF EXISTS `UpdateOtpEntry`");
            migrationBuilder.Sql(@"CREATE PROCEDURE UpdateOtpEntry(
                            IN p_IsUsed BOOLEAN, 
                            IN p_InvalidAttempts INT, 
                            IN p_Id VARCHAR(36)
                        )
                        BEGIN
                            UPDATE OtpEntries 
                            SET IsUsed = p_IsUsed, InvalidAttempts = p_InvalidAttempts 
                            WHERE Id = p_Id;
                        END;");
            
            migrationBuilder.Sql(@"DROP procedure IF EXISTS `GetOtpEntryByPhoneNumber`");
            migrationBuilder.Sql(@"CREATE PROCEDURE GetOtpEntryByPhoneNumber(IN p_PhoneNumber VARCHAR(20))
                        BEGIN
                            SELECT * FROM OtpEntries 
                            WHERE PhoneNumber = p_PhoneNumber;
                        END;");
            
            migrationBuilder.Sql("COMMIT;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP procedure IF EXISTS `GetLatestUnusedOtpForPhoneNumber`");
            migrationBuilder.Sql(@"DROP procedure IF EXISTS `UpdateOtpEntry`");
            migrationBuilder.Sql(@"DROP procedure IF EXISTS `GetOtpEntryByPhoneNumber`");
        }

    }
}
