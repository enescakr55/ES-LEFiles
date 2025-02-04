using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LEFiles.Data.Migrations.Postgres.Migrations
{
    public partial class entryAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntryAccessItems",
                schema: "lefiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    EntryType = table.Column<int>(type: "integer", nullable: false),
                    Target = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SubTarget = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryAccessItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryAccessItems_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "lefiles",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntryAccessItems_Code",
                schema: "lefiles",
                table: "EntryAccessItems",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntryAccessItems_Id",
                schema: "lefiles",
                table: "EntryAccessItems",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntryAccessItems_UserId",
                schema: "lefiles",
                table: "EntryAccessItems",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntryAccessItems",
                schema: "lefiles");
        }
    }
}
