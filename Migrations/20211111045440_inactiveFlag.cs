using Microsoft.EntityFrameworkCore.Migrations;

namespace CYCMSchool.Migrations
{
    public partial class inactiveFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Student",
                newName: "Inactive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Inactive",
                table: "Student",
                newName: "Active");
        }
    }
}
