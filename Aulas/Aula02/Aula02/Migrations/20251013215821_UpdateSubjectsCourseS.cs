using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aula02.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubjectsCourseS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseSubject");

            migrationBuilder.AddColumn<int>(
                name: "SubjectID",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_SubjectID",
                table: "Course",
                column: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Subjects_SubjectID",
                table: "Course",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Subjects_SubjectID",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_SubjectID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "SubjectID",
                table: "Course");

            migrationBuilder.CreateTable(
                name: "CourseSubject",
                columns: table => new
                {
                    CoursesID = table.Column<int>(type: "int", nullable: false),
                    SubjectsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSubject", x => new { x.CoursesID, x.SubjectsID });
                    table.ForeignKey(
                        name: "FK_CourseSubject_Course_CoursesID",
                        column: x => x.CoursesID,
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSubject_Subjects_SubjectsID",
                        column: x => x.SubjectsID,
                        principalTable: "Subjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubject_SubjectsID",
                table: "CourseSubject",
                column: "SubjectsID");
        }
    }
}
