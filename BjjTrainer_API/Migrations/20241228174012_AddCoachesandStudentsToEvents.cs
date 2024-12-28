using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BjjTrainer_API.Migrations
{
    /// <inheritdoc />
    public partial class AddCoachesandStudentsToEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEvents_ApplicationUsers_ApplicationUserId",
                table: "CalendarEvents");

            migrationBuilder.DropIndex(
                name: "IX_CalendarEvents_ApplicationUserId",
                table: "CalendarEvents");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CalendarEvents");

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "CalendarEvents",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CalendarEventCheckIns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CalendarEventId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CheckInTime = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEventCheckIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarEventCheckIns_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarEventCheckIns_CalendarEvents_CalendarEventId",
                        column: x => x.CalendarEventId,
                        principalTable: "CalendarEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEventUsers",
                columns: table => new
                {
                    CalendarEventId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    IsCheckedIn = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEventUsers", x => new { x.CalendarEventId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CalendarEventUsers_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalendarEventUsers_CalendarEvents_CalendarEventId",
                        column: x => x.CalendarEventId,
                        principalTable: "CalendarEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_SchoolId",
                table: "CalendarEvents",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEventCheckIns_CalendarEventId",
                table: "CalendarEventCheckIns",
                column: "CalendarEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEventCheckIns_UserId",
                table: "CalendarEventCheckIns",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEventUsers_UserId",
                table: "CalendarEventUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEvents_Schools_SchoolId",
                table: "CalendarEvents",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEvents_Schools_SchoolId",
                table: "CalendarEvents");

            migrationBuilder.DropTable(
                name: "CalendarEventCheckIns");

            migrationBuilder.DropTable(
                name: "CalendarEventUsers");

            migrationBuilder.DropIndex(
                name: "IX_CalendarEvents_SchoolId",
                table: "CalendarEvents");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "CalendarEvents");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "CalendarEvents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_ApplicationUserId",
                table: "CalendarEvents",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEvents_ApplicationUsers_ApplicationUserId",
                table: "CalendarEvents",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
