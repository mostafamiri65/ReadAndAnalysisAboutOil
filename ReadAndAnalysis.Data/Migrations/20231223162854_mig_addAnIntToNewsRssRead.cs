using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadAndAnalysis.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_addAnIntToNewsRssRead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NotOil",
                table: "NewsRssReed",
                type: "int",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "NotOil",
                table: "NewsRssReed",
                type: "bit",
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "0");
        }
    }
}
