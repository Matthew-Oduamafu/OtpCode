using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtpCode.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddGetOtpStoredProc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("START TRANSACTION;");
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
            migrationBuilder.Sql(@"DROP procedure IF EXISTS `GetOtpEntryByPhoneNumber`");
        }
    }
}
