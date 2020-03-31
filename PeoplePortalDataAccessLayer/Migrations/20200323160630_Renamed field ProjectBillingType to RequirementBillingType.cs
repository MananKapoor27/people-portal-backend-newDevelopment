using Microsoft.EntityFrameworkCore.Migrations;

namespace PeoplePortalDataAccessLayer.Migrations
{
    public partial class RenamedfieldProjectBillingTypetoRequirementBillingType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectBillingType",
                table: "ProjectRequirements");

            migrationBuilder.AddColumn<string>(
                name: "RequirementBillingType",
                table: "ProjectRequirements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequirementBillingType",
                table: "ProjectRequirements");

            migrationBuilder.AddColumn<string>(
                name: "ProjectBillingType",
                table: "ProjectRequirements",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
