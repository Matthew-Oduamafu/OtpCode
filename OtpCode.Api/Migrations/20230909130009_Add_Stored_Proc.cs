using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtpCode.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("START TRANSACTION;");
    
            migrationBuilder.Sql(@"DROP procedure IF EXISTS `InsertOtpEntry`");
            migrationBuilder.Sql(@"CREATE PROCEDURE InsertOtpEntry(
                IN p_Id VARCHAR(36),
                IN p_PhoneNumber VARCHAR(20),
                IN p_CountryCode VARCHAR(3),
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
                (Id, PhoneNumber, CountryCode, OtpCode, CreatedDate, ExpiryDate, IsUsed, InvalidAttempts, Purpose, Metadata) 
                VALUES 
                (p_Id, p_PhoneNumber, p_CountryCode, p_OtpCode, p_CreatedDate, p_ExpiryDate, p_IsUsed, p_InvalidAttempts, p_Purpose, p_Metadata);
            END;");

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
                            IN p_Id VARCHAR(36),
                            IN p_UpdatedAt DATETIME
                        )
                        BEGIN
                            UPDATE OtpEntries 
                            SET IsUsed = p_IsUsed, 
                                InvalidAttempts = p_InvalidAttempts,
                                UpdatedAt = p_UpdatedAt
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
            migrationBuilder.Sql(@"DROP procedure IF EXISTS `InsertOtpEntry`");
            migrationBuilder.Sql(@"DROP procedure IF EXISTS `GetLatestUnusedOtpForPhoneNumber`");
            migrationBuilder.Sql(@"DROP procedure IF EXISTS `UpdateOtpEntry`");
            migrationBuilder.Sql(@"DROP procedure IF EXISTS `GetOtpEntryByPhoneNumber`");
        }
    }
}
