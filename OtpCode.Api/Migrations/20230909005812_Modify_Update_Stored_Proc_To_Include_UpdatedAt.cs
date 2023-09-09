using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtpCode.Api.Migrations
{
    /// <inheritdoc />
    public partial class ModifyUpdateStoredProcToIncludeUpdatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "OtpEntries",
                type: "datetime(6)",
                nullable: true);
            
            migrationBuilder.Sql("START TRANSACTION;");

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

            migrationBuilder.Sql("COMMIT;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "OtpEntries");
            
            migrationBuilder.Sql(@"DROP procedure IF EXISTS `UpdateOtpEntry`");
        }
    }
}
