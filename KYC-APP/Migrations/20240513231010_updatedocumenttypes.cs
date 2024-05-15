using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KYC_APP.Migrations
{
    /// <inheritdoc />
    public partial class updatedocumenttypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "DocumentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "generic",
                table: "DocumentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ownerid",
                table: "DocumentTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "KYCRequirements",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serviceproviderid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Documentid = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYCRequirements", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KYCRequirements");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "generic",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "ownerid",
                table: "DocumentTypes");
        }
    }
}
