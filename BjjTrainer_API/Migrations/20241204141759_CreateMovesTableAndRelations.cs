using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BjjTrainer_API.Migrations
{
    /// <inheritdoc />
    public partial class CreateMovesTableAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Content = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    SkillLevel = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    StartingPosition = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    History = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Scenarios = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CounterStrategies = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true),
                    LegalInCompetitions = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubLessonMoves",
                columns: table => new
                {
                    SubLessonId = table.Column<int>(type: "integer", nullable: false),
                    MoveId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubLessonMoves", x => new { x.SubLessonId, x.MoveId });
                    table.ForeignKey(
                        name: "FK_SubLessonMoves_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubLessonMoves_SubLessons_SubLessonId",
                        column: x => x.SubLessonId,
                        principalTable: "SubLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubLessonMoves_MoveId",
                table: "SubLessonMoves",
                column: "MoveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubLessonMoves");

            migrationBuilder.DropTable(
                name: "Moves");
        }
    }
}
