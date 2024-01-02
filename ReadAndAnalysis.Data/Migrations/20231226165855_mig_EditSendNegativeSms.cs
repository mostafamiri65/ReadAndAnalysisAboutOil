using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadAndAnalysis.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_EditSendNegativeSms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SendedLevel",
                table: "NegativeOilNewsForSendingSms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UniqueCode",
                table: "NegativeOilNewsForSendingSms",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendedLevel",
                table: "NegativeOilNewsForSendingSms");

            migrationBuilder.DropColumn(
                name: "UniqueCode",
                table: "NegativeOilNewsForSendingSms");
        }
    }
}
