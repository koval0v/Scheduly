using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleProject.WebAPI.Schedules.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomIdForScheduleDiscipline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "ScheduleDisciplines",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "ScheduleDisciplines");
        }
    }
}
