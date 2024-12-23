using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleProject.WebAPI.Faculties.Migrations
{
    /// <inheritdoc />
    public partial class AddBuildingToFaculty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "Faculties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "Faculties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
