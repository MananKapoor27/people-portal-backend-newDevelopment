using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeoplePortalDataAccessLayer.Migrations
{
    public partial class addingRequirementStartDate_EndDate_ProjectBillingType_inProjectRequirementsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectBillingType",
                table: "ProjectRequirements",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequirementEndDate",
                table: "ProjectRequirements",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RequirementStartDate",
                table: "ProjectRequirements",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectBillingType",
                table: "ProjectRequirements");

            migrationBuilder.DropColumn(
                name: "RequirementEndDate",
                table: "ProjectRequirements");

            migrationBuilder.DropColumn(
                name: "RequirementStartDate",
                table: "ProjectRequirements");
        }
    }
}
