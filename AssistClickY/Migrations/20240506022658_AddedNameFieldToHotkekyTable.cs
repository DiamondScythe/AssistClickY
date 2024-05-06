using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssistClickY.Migrations
{
    public partial class AddedNameFieldToHotkekyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Hotkeys",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Hotkeys");
        }
    }
}
