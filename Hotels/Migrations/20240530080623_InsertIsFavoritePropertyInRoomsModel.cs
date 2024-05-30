using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotels.Migrations
{
    /// <inheritdoc />
    public partial class InsertIsFavoritePropertyInRoomsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "RoomId",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HotelId",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomId1",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId1",
                table: "Bookings",
                column: "RoomId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId1",
                table: "Bookings",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId1",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomId1",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId");
        }
    }
}
