using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BjjTrainer_API.Migrations
{
    /// <inheritdoc />
    public partial class AddLessonApplicationUserJoinManyToManyRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "ApplicationUserLessonJoin",
            //    columns: table => new
            //    {
            //        ApplicationUserId = table.Column<string>(type: "text", nullable: false),
            //        LessonId = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ApplicationUserLessonJoin", x => new { x.ApplicationUserId, x.LessonId });
            //        table.ForeignKey(
            //            name: "FK_ApplicationUserLessonJoin_ApplicationUsers_ApplicationUserId",
            //            column: x => x.ApplicationUserId,
            //            principalTable: "ApplicationUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ApplicationUserLessonJoin_Lessons_LessonId",
            //            column: x => x.LessonId,
            //            principalTable: "Lessons",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ApplicationUserLessonJoin_LessonId",
            //    table: "ApplicationUserLessonJoin",
            //    column: "LessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserLessonJoin");

            migrationBuilder.CreateTable(
                name: "LessonApplicationUser",
                columns: table => new
                {
                    ApplicationUsersId = table.Column<string>(type: "text", nullable: false),
                    LessonsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonApplicationUser", x => new { x.ApplicationUsersId, x.LessonsId });
                    table.ForeignKey(
                        name: "FK_LessonApplicationUser_ApplicationUsers_ApplicationUsersId",
                        column: x => x.ApplicationUsersId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonApplicationUser_Lessons_LessonsId",
                        column: x => x.LessonsId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonApplicationUser_LessonsId",
                table: "LessonApplicationUser",
                column: "LessonsId");
        }
    }
}
