using Microsoft.EntityFrameworkCore.Migrations;

namespace PeoplePortalDataAccessLayer.Migrations
{
    public partial class EmployeeEducationDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HscBranchOfStudy",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HscInstituteName",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HscYearOfPassout",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PGBranchOfStudy",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PGInstituteName",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PGYearOfPassout",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SscBranchOfStudy",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SscInstituteName",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SscYearOfPassout",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UGBranchOfStudy",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UGInstituteName",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UGYearOfPassout",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HscBranchOfStudy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "HscInstituteName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "HscYearOfPassout",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PGBranchOfStudy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PGInstituteName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PGYearOfPassout",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SscBranchOfStudy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SscInstituteName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SscYearOfPassout",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UGBranchOfStudy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UGInstituteName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UGYearOfPassout",
                table: "Employees");
        }
    }
}
