using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BjjTrainer_API.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldsToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "TrainingStartDate",
                table: "ApplicationUsers",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "LastLoginDate",
                table: "ApplicationUsers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredTrainingStyle",
                table: "ApplicationUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "ApplicationUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TrainingHoursThisWeek",
                table: "ApplicationUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "PreferredTrainingStyle",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "TrainingHoursThisWeek",
                table: "ApplicationUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TrainingStartDate",
                table: "ApplicationUsers",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
