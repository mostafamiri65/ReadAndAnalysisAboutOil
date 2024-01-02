using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadAndAnalysis.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_AddNewsLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsLikes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsId = table.Column<long>(type: "bigint", nullable: false),
                    NewsLinkeId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserModifiedId = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedIp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsLikes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsLikes");
        }
    }
}
