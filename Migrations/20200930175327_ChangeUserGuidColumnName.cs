using Microsoft.EntityFrameworkCore.Migrations;

namespace nopanic_API.Migrations
{
    public partial class ChangeUserGuidColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserGuid",
                table: "User",
                newName: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "User",
                newName: "UserGuid");
        }
    }
}
