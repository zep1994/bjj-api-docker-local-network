using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BjjTrainer_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrainingSessionForDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AressOfImprovement",
                table: "TrainingSessions",
                newName: "AreasOfImprovement");

            migrationBuilder.AlterColumn<List<string>>(
                name: "MovesTrained",
                table: "TrainingSessions",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<int>),
                oldType: "integer[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<string>>(
                name: "LessonMoves",
                table: "TrainingSessions",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<int>),
                oldType: "integer[]",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AreasOfImprovement",
                table: "TrainingSessions",
                newName: "AressOfImprovement");

            migrationBuilder.AlterColumn<List<int>>(
                name: "MovesTrained",
                table: "TrainingSessions",
                type: "integer[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<int>>(
                name: "LessonMoves",
                table: "TrainingSessions",
                type: "integer[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);
        }
    }
}
