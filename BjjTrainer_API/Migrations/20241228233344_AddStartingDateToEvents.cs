using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BjjTrainer_API.Migrations
{
    /// <inheritdoc />
    public partial class AddStartingDateToEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "TrainingLogs",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "TrainingLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "CalendarEvents",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "CalendarEvents",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TrainingLogs");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "TrainingLogs");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "CalendarEvents");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "CalendarEvents");
        }
    }
}
