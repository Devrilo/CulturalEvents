using Microsoft.EntityFrameworkCore.Migrations;

namespace CulturalEventsData.Migrations
{
    public partial class AddedCreatorIdtoEventstableDeletedIsCreatorfromEventAccounttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCreator",
                table: "EventsAccounts");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Events",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Events");

            migrationBuilder.AddColumn<bool>(
                name: "IsCreator",
                table: "EventsAccounts",
                nullable: false,
                defaultValue: false);
        }
    }
}
