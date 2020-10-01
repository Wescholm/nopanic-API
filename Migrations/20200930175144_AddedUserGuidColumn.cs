using Microsoft.EntityFrameworkCore.Migrations;

namespace nopanic_API.Migrations
{
    public partial class AddedUserGuidColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserGuid",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "User");
        }
    }
}
