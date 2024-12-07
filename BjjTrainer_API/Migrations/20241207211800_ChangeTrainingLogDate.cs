using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BjjTrainer_API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTrainingLogDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "GoalDate",
                table: "TrainingGoals",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "GoalDate",
                table: "TrainingGoals",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
