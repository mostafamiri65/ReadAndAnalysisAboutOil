using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadAndAnalysis.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_AddNotOilNewsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "NotOilNewsForSendingSmsId",
                table: "SendSms",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NotOilNewsForSendingSms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsId = table.Column<long>(type: "bigint", nullable: false),
                    IsSelectForSendSms = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotOilNewsForSendingSms", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotOilNewsForSendingSms");

            migrationBuilder.DropColumn(
                name: "NotOilNewsForSendingSmsId",
                table: "SendSms");
        }
    }
}
