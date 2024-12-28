using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LEFiles.Data.Migrations.Postgres.Migrations
{
    public partial class fileUploadItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileUploadId",
                schema: "lefiles",
                table: "FileItems",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FileUploadItem",
                schema: "lefiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Extension = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: true),
                    FilePath = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Provider = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUploadItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileUploadItem_FileItems_Id",
                        column: x => x.Id,
                        principalSchema: "lefiles",
                        principalTable: "FileItems",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileUploadItem_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "lefiles",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileUploadItem_UserId",
                schema: "lefiles",
                table: "FileUploadItem",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileUploadItem",
                schema: "lefiles");

            migrationBuilder.DropColumn(
                name: "FileUploadId",
                schema: "lefiles",
                table: "FileItems");
        }
    }
}
