using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BjjTrainer_API.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSharedToCalendarEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CalendarEvents_TrainingLogId",
                table: "CalendarEvents");

            migrationBuilder.RenameColumn(
                name: "IsImported",
                table: "TrainingLogs",
                newName: "IsShared");

            migrationBuilder.AddColumn<bool>(
                name: "IsCoachSelected",
                table: "TrainingLogMoves",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_TrainingLogId",
                table: "CalendarEvents",
                column: "TrainingLogId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CalendarEvents_TrainingLogId",
                table: "CalendarEvents");

            migrationBuilder.DropColumn(
                name: "IsCoachSelected",
                table: "TrainingLogMoves");

            migrationBuilder.RenameColumn(
                name: "IsShared",
                table: "TrainingLogs",
                newName: "IsImported");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_TrainingLogId",
                table: "CalendarEvents",
                column: "TrainingLogId");
        }
    }
}
