using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_ClientUser_ID",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ClientUser_ID",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ClientUser_ID",
                table: "Booking");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_User_ID",
                table: "Booking",
                column: "User_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_User_ID",
                table: "Booking",
                column: "User_ID",
                principalTable: "User",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_User_ID",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_User_ID",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "ClientUser_ID",
                table: "Booking",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ClientUser_ID",
                table: "Booking",
                column: "ClientUser_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_ClientUser_ID",
                table: "Booking",
                column: "ClientUser_ID",
                principalTable: "User",
                principalColumn: "User_ID");
        }
    }
}
