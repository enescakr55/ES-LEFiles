using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LEFiles.Data.Migrations.Postgres.Migrations
{
    public partial class parentFolderCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileItems_FolderItems_ParentFolderId",
                schema: "lefiles",
                table: "FileItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FolderItems_FolderItems_ParentFolderId",
                schema: "lefiles",
                table: "FolderItems");

            migrationBuilder.AddForeignKey(
                name: "FK_FileItems_FolderItems_ParentFolderId",
                schema: "lefiles",
                table: "FileItems",
                column: "ParentFolderId",
                principalSchema: "lefiles",
                principalTable: "FolderItems",
                principalColumn: "FolderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FolderItems_FolderItems_ParentFolderId",
                schema: "lefiles",
                table: "FolderItems",
                column: "ParentFolderId",
                principalSchema: "lefiles",
                principalTable: "FolderItems",
                principalColumn: "FolderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileItems_FolderItems_ParentFolderId",
                schema: "lefiles",
                table: "FileItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FolderItems_FolderItems_ParentFolderId",
                schema: "lefiles",
                table: "FolderItems");

            migrationBuilder.AddForeignKey(
                name: "FK_FileItems_FolderItems_ParentFolderId",
                schema: "lefiles",
                table: "FileItems",
                column: "ParentFolderId",
                principalSchema: "lefiles",
                principalTable: "FolderItems",
                principalColumn: "FolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FolderItems_FolderItems_ParentFolderId",
                schema: "lefiles",
                table: "FolderItems",
                column: "ParentFolderId",
                principalSchema: "lefiles",
                principalTable: "FolderItems",
                principalColumn: "FolderId");
        }
    }
}
