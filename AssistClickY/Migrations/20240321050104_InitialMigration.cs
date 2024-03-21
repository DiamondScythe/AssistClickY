using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssistClickY.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotkeys",
                columns: table => new
                {
                    HotkeyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotkeyCombination = table.Column<string>(type: "TEXT", nullable: true),
                    ModifierKeys = table.Column<int>(type: "INTEGER", nullable: false),
                    Key = table.Column<int>(type: "INTEGER", nullable: false),
                    HotkeyJob = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotkeys", x => x.HotkeyId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hotkeys");
        }
    }
}
