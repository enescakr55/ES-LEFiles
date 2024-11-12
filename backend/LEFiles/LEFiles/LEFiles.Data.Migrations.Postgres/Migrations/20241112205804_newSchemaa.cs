using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LEFiles.Data.Migrations.Postgres.Migrations
{
    public partial class newSchemaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lefiles");

            migrationBuilder.RenameTable(
                name: "WaitableRequests",
                newName: "WaitableRequests",
                newSchema: "lefiles");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "lefiles");

            migrationBuilder.RenameTable(
                name: "FolderItems",
                newName: "FolderItems",
                newSchema: "lefiles");

            migrationBuilder.RenameTable(
                name: "FileItems",
                newName: "FileItems",
                newSchema: "lefiles");

            migrationBuilder.RenameTable(
                name: "ClientSessions",
                newName: "ClientSessions",
                newSchema: "lefiles");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Clients",
                newSchema: "lefiles");

            migrationBuilder.RenameTable(
                name: "ClientRegistrationTokens",
                newName: "ClientRegistrationTokens",
                newSchema: "lefiles");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "WaitableRequests",
                schema: "lefiles",
                newName: "WaitableRequests");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "lefiles",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "FolderItems",
                schema: "lefiles",
                newName: "FolderItems");

            migrationBuilder.RenameTable(
                name: "FileItems",
                schema: "lefiles",
                newName: "FileItems");

            migrationBuilder.RenameTable(
                name: "ClientSessions",
                schema: "lefiles",
                newName: "ClientSessions");

            migrationBuilder.RenameTable(
                name: "Clients",
                schema: "lefiles",
                newName: "Clients");

            migrationBuilder.RenameTable(
                name: "ClientRegistrationTokens",
                schema: "lefiles",
                newName: "ClientRegistrationTokens");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");
        }
    }
}
