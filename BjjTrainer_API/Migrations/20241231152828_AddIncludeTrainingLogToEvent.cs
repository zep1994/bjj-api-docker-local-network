using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BjjTrainer_API.Migrations
{
    /// <inheritdoc />
    public partial class AddIncludeTrainingLogToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovesCovered",
                table: "TrainingLogs");

            migrationBuilder.RenameColumn(
                name: "IsTrainingSession",
                table: "CalendarEvents",
                newName: "IncludeTrainingLog");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IncludeTrainingLog",
                table: "CalendarEvents",
                newName: "IsTrainingSession");

            migrationBuilder.AddColumn<string>(
                name: "MovesCovered",
                table: "TrainingLogs",
                type: "text",
                nullable: false,
                defaultValue: "");

        }
    }
}
