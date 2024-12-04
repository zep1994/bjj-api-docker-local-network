using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BjjTrainer_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrainingSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"TrainingSessions\" " +
                "ALTER COLUMN \"MovesTrained\" TYPE integer[] USING \"MovesTrained\"::integer[];"
            );

            migrationBuilder.Sql(
                "ALTER TABLE \"TrainingSessions\" " +
                "ALTER COLUMN \"LessonMoves\" TYPE integer[] USING \"LessonMoves\"::integer[];"
            );

            migrationBuilder.AddColumn<List<string>>(
                name: "AressOfImprovement",
                table: "TrainingSessions",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Belt",
                table: "TrainingSessions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Prerequisites",
                table: "TrainingSessions",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rounds",
                table: "TrainingSessions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Submissions",
                table: "TrainingSessions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Taps",
                table: "TrainingSessions",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Techniques",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AressOfImprovement",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "Belt",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "Prerequisites",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "Rounds",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "Submissions",
                table: "TrainingSessions");

            migrationBuilder.DropColumn(
                name: "Taps",
                table: "TrainingSessions");

            // Reverse MovesTrained conversion
            migrationBuilder.Sql(
                "ALTER TABLE \"TrainingSessions\" " +
                "ALTER COLUMN \"MovesTrained\" TYPE text[] USING \"MovesTrained\"::text[];"
            );

            // Reverse LessonMoves conversion
            migrationBuilder.Sql(
                "ALTER TABLE \"TrainingSessions\" " +
                "ALTER COLUMN \"LessonMoves\" TYPE text[] USING \"LessonMoves\"::text[];"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Techniques",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
