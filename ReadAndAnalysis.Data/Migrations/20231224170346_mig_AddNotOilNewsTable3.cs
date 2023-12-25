using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadAndAnalysis.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_AddNotOilNewsTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelectForSendSms",
                table: "NegativeOilNewsForSendingSms");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "NegativeOilNewsForSendingSms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "NegativeOilNewsForSendingSms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "NegativeOilNewsForSendingSms",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "NegativeOilNewsForSendingSms");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "NegativeOilNewsForSendingSms");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "NegativeOilNewsForSendingSms");

            migrationBuilder.AddColumn<bool>(
                name: "IsSelectForSendSms",
                table: "NegativeOilNewsForSendingSms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
