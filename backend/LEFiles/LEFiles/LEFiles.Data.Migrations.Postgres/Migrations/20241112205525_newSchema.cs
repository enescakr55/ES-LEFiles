using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LEFiles.Data.Migrations.Postgres.Migrations
{
    public partial class newSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientSettings",
                table: "Clients",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientSettings",
                table: "Clients");
        }
    }
}
