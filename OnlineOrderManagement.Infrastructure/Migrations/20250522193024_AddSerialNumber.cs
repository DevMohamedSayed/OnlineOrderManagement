using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineOrderManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSerialNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Products");
        }
    }
}
