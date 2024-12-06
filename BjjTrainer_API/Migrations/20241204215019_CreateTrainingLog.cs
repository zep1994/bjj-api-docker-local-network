using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BjjTrainer_API.Migrations
{
    /// <inheritdoc />
    public partial class CreateTrainingLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Moves",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingLogCount",
                table: "Moves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Belt",
                table: "ApplicationUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BeltStripes",
                table: "ApplicationUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalRoundsRolled",
                table: "ApplicationUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSubmissions",
                table: "ApplicationUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalTaps",
                table: "ApplicationUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TotalTrainingTime",
                table: "ApplicationUsers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TrainingStartDate",
                table: "ApplicationUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrainingLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TrainingTime = table.Column<double>(type: "double precision", nullable: false),
                    RoundsRolled = table.Column<int>(type: "integer", nullable: false),
                    Submissions = table.Column<int>(type: "integer", nullable: false),
                    Taps = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    SelfAssessment = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingLogs_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingLogMoves",
                columns: table => new
                {
                    TrainingLogId = table.Column<int>(type: "integer", nullable: false),
                    MoveId = table.Column<int>(type: "integer", nullable: false),
                    SelfAssessment = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingLogMoves", x => new { x.TrainingLogId, x.MoveId });
                    table.ForeignKey(
                        name: "FK_TrainingLogMoves_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingLogMoves_TrainingLogs_TrainingLogId",
                        column: x => x.TrainingLogId,
                        principalTable: "TrainingLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Moves_ApplicationUserId",
                table: "Moves",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingLogMoves_MoveId",
                table: "TrainingLogMoves",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingLogs_ApplicationUserId",
                table: "TrainingLogs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_ApplicationUsers_ApplicationUserId",
                table: "Moves",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moves_ApplicationUsers_ApplicationUserId",
                table: "Moves");

            migrationBuilder.DropTable(
                name: "TrainingLogMoves");

            migrationBuilder.DropTable(
                name: "TrainingLogs");

            migrationBuilder.DropIndex(
                name: "IX_Moves_ApplicationUserId",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "TrainingLogCount",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "Belt",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "BeltStripes",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "TotalRoundsRolled",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "TotalSubmissions",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "TotalTaps",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "TotalTrainingTime",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "TrainingStartDate",
                table: "ApplicationUsers");
        }
    }
}
