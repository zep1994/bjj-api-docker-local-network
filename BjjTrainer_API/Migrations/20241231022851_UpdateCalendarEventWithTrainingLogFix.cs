using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BjjTrainer_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCalendarEventWithTrainingLogFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalendarEventId",
                table: "TrainingLogs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImportedFromLogId",
                table: "TrainingLogs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCoachLog",
                table: "TrainingLogs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsImported",
                table: "TrainingLogs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MovesCovered",
                table: "TrainingLogs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsTrainingSession",
                table: "CalendarEvents",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TrainingLogId",
                table: "CalendarEvents",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingLogs_ImportedFromLogId",
                table: "TrainingLogs",
                column: "ImportedFromLogId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_TrainingLogId",
                table: "CalendarEvents",
                column: "TrainingLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEvents_TrainingLogs_TrainingLogId",
                table: "CalendarEvents",
                column: "TrainingLogId",
                principalTable: "TrainingLogs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingLogs_TrainingLogs_ImportedFromLogId",
                table: "TrainingLogs",
                column: "ImportedFromLogId",
                principalTable: "TrainingLogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEvents_TrainingLogs_TrainingLogId",
                table: "CalendarEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingLogs_TrainingLogs_ImportedFromLogId",
                table: "TrainingLogs");

            migrationBuilder.DropIndex(
                name: "IX_TrainingLogs_ImportedFromLogId",
                table: "TrainingLogs");

            migrationBuilder.DropIndex(
                name: "IX_CalendarEvents_TrainingLogId",
                table: "CalendarEvents");

            migrationBuilder.DropColumn(
                name: "CalendarEventId",
                table: "TrainingLogs");

            migrationBuilder.DropColumn(
                name: "ImportedFromLogId",
                table: "TrainingLogs");

            migrationBuilder.DropColumn(
                name: "IsCoachLog",
                table: "TrainingLogs");

            migrationBuilder.DropColumn(
                name: "IsImported",
                table: "TrainingLogs");

            migrationBuilder.DropColumn(
                name: "MovesCovered",
                table: "TrainingLogs");

            migrationBuilder.DropColumn(
                name: "IsTrainingSession",
                table: "CalendarEvents");

            migrationBuilder.DropColumn(
                name: "TrainingLogId",
                table: "CalendarEvents");
        }
    }
}
