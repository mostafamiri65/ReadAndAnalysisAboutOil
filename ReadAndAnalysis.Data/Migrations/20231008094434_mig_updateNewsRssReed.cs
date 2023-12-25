using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadAndAnalysis.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_updateNewsRssReed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NotOil",
                table: "NewsRssReed",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NotOilUserId",
                table: "NewsRssReed",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotOil",
                table: "NewsRssReed");

            migrationBuilder.DropColumn(
                name: "NotOilUserId",
                table: "NewsRssReed");
        }
    }
}
