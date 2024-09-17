using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileManager.Data.Migrations
{
    public partial class modifyProfileRace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MemberId",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Race",
                table: "Profiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Religion",
                table: "Profiles",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Race",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "Profiles");
        }
    }
}
