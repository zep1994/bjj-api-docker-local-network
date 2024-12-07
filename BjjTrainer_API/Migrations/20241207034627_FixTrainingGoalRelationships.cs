using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BjjTrainer_API.Migrations
{
    /// <inheritdoc />
    public partial class FixTrainingGoalRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "CalendarEvents",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "CalendarEvents",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "TrainingGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GoalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingGoals_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTrainingGoalMoves",
                columns: table => new
                {
                    TrainingGoalId = table.Column<int>(type: "integer", nullable: false),
                    MoveId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrainingGoalMoves", x => new { x.TrainingGoalId, x.MoveId });
                    table.ForeignKey(
                        name: "FK_UserTrainingGoalMoves_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTrainingGoalMoves_TrainingGoals_TrainingGoalId",
                        column: x => x.TrainingGoalId,
                        principalTable: "TrainingGoals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingGoals_ApplicationUserId",
                table: "TrainingGoals",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrainingGoalMoves_MoveId",
                table: "UserTrainingGoalMoves",
                column: "MoveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTrainingGoalMoves");

            migrationBuilder.DropTable(
                name: "TrainingGoals");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "CalendarEvents",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "CalendarEvents",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }
    }
}
