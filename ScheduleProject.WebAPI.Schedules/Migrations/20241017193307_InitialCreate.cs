using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleProject.WebAPI.Schedules.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleDisciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplineId = table.Column<int>(type: "int", nullable: false),
                    DisciplineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    TeacherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    Lesson = table.Column<int>(type: "int", nullable: false),
                    IsLecture = table.Column<bool>(type: "bit", nullable: false),
                    isSelecive = table.Column<bool>(type: "bit", nullable: false),
                    CatalogName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDisciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleDisciplines_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDisciplines_ScheduleId",
                table: "ScheduleDisciplines",
                column: "ScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleDisciplines");

            migrationBuilder.DropTable(
                name: "Schedules");
        }
    }
}
