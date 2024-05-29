using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotels.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHotelTypeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deluxe",
                table: "HotelTypes");

            migrationBuilder.DropColumn(
                name: "Double",
                table: "HotelTypes");

            migrationBuilder.RenameColumn(
                name: "Single",
                table: "HotelTypes",
                newName: "TypeName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "HotelTypes",
                newName: "Single");

            migrationBuilder.AddColumn<string>(
                name: "Deluxe",
                table: "HotelTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Double",
                table: "HotelTypes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
