using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadAndAnalysis.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_SendingType3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "NegativeOilNewsForSendingSms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "NegativeOilNewsForSendingSms");
        }
    }
}
