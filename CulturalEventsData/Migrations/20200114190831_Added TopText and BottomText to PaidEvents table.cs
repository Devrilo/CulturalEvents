using Microsoft.EntityFrameworkCore.Migrations;

namespace CulturalEventsData.Migrations
{
    public partial class AddedTopTextandBottomTexttoPaidEventstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BottomText",
                table: "PaidEvents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopText",
                table: "PaidEvents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BottomText",
                table: "PaidEvents");

            migrationBuilder.DropColumn(
                name: "TopText",
                table: "PaidEvents");
        }
    }
}
