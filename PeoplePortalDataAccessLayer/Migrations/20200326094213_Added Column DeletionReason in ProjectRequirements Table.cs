using Microsoft.EntityFrameworkCore.Migrations;

namespace PeoplePortalDataAccessLayer.Migrations
{
    public partial class AddedColumnDeletionReasoninProjectRequirementsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletionReason",
                table: "ProjectRequirements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletionReason",
                table: "ProjectRequirements");
        }
    }
}
