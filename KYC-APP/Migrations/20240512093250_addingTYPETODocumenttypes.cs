using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KYC_APP.Migrations
{
    /// <inheritdoc />
    public partial class addingTYPETODocumenttypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "DocumentTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "DocumentTypes");
        }
    }
}
