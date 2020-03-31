using Microsoft.EntityFrameworkCore.Migrations;

namespace PeoplePortalDataAccessLayer.Migrations
{
    public partial class EmployeePrimaryandSecondayType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeType",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "EmployeePrimaryType",
                table: "Employees",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeSecondaryType",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeePrimaryType",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeSecondaryType",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeType",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
