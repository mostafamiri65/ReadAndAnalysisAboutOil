using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadAndAnalysis.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_EditSendNegativeSms2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SendDate",
                table: "NegativeOilNewsForSendingSms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserSendId",
                table: "NegativeOilNewsForSendingSms",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendDate",
                table: "NegativeOilNewsForSendingSms");

            migrationBuilder.DropColumn(
                name: "UserSendId",
                table: "NegativeOilNewsForSendingSms");
        }
    }
}
