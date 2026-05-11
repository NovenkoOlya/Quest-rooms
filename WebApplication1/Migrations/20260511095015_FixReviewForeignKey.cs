using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class FixReviewForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Full_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Registration_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Account_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Room_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Owner_ID = table.Column<int>(type: "int", nullable: false),
                    QR_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QR_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Info = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Availability_Status = table.Column<bool>(type: "bit", nullable: false),
                    QR_Creation_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Room_ID);
                    table.ForeignKey(
                        name: "FK_Room_User_Owner_ID",
                        column: x => x.Owner_ID,
                        principalTable: "User",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quest",
                columns: table => new
                {
                    Quest_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Room_ID = table.Column<int>(type: "int", nullable: false),
                    Q_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Difficulty_Level = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Min_Players = table.Column<int>(type: "int", nullable: false),
                    Max_Players = table.Column<int>(type: "int", nullable: false),
                    Base_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quest", x => x.Quest_ID);
                    table.ForeignKey(
                        name: "FK_Quest_Room_Room_ID",
                        column: x => x.Room_ID,
                        principalTable: "Room",
                        principalColumn: "Room_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Review_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Room_ID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Review_Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R_Creation_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Review_ID);
                    table.ForeignKey(
                        name: "FK_Review_Room_Room_ID",
                        column: x => x.Room_ID,
                        principalTable: "Room",
                        principalColumn: "Room_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_User_User_ID",
                        column: x => x.User_ID,
                        principalTable: "User",
                        principalColumn: "User_ID");
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Session_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quest_ID = table.Column<int>(type: "int", nullable: false),
                    Start_DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    S_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    S_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quest_ID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Session_ID);
                    table.ForeignKey(
                        name: "FK_Session_Quest_Quest_ID1",
                        column: x => x.Quest_ID1,
                        principalTable: "Quest",
                        principalColumn: "Quest_ID");
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Booking_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Session_ID = table.Column<int>(type: "int", nullable: false),
                    Players_Count = table.Column<int>(type: "int", nullable: false),
                    Client_Contacts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_Creation_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientUser_ID = table.Column<int>(type: "int", nullable: true),
                    Session_ID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Booking_ID);
                    table.ForeignKey(
                        name: "FK_Booking_Session_Session_ID1",
                        column: x => x.Session_ID1,
                        principalTable: "Session",
                        principalColumn: "Session_ID");
                    table.ForeignKey(
                        name: "FK_Booking_User_ClientUser_ID",
                        column: x => x.ClientUser_ID,
                        principalTable: "User",
                        principalColumn: "User_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ClientUser_ID",
                table: "Booking",
                column: "ClientUser_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Session_ID1",
                table: "Booking",
                column: "Session_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Quest_Room_ID",
                table: "Quest",
                column: "Room_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ClientUser_ID",
                table: "Review",
                column: "ClientUser_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_Room_ID",
                table: "Review",
                column: "Room_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Room_Owner_ID",
                table: "Room",
                column: "Owner_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Session_Quest_ID1",
                table: "Session",
                column: "Quest_ID1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Quest");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
