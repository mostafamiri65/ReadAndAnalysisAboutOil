using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadAndAnalysis.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_updaterssread : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotOilUserId",
                table: "NewsRssReed");

            migrationBuilder.AlterColumn<bool>(
                name: "NotOil",
                table: "NewsRssReed",
                type: "bit",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "NotOil",
                table: "NewsRssReed",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AddColumn<Guid>(
                name: "NotOilUserId",
                table: "NewsRssReed",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
