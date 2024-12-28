using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LEFiles.Data.Migrations.Postgres.Migrations
{
    public partial class fileUploadItemsDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUploadItem_FileItems_Id",
                schema: "lefiles",
                table: "FileUploadItem");

            migrationBuilder.DropForeignKey(
                name: "FK_FileUploadItem_Users_UserId",
                schema: "lefiles",
                table: "FileUploadItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileUploadItem",
                schema: "lefiles",
                table: "FileUploadItem");

            migrationBuilder.RenameTable(
                name: "FileUploadItem",
                schema: "lefiles",
                newName: "FileUploadItems",
                newSchema: "lefiles");

            migrationBuilder.RenameIndex(
                name: "IX_FileUploadItem_UserId",
                schema: "lefiles",
                table: "FileUploadItems",
                newName: "IX_FileUploadItems_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileUploadItems",
                schema: "lefiles",
                table: "FileUploadItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploadItems_FileItems_Id",
                schema: "lefiles",
                table: "FileUploadItems",
                column: "Id",
                principalSchema: "lefiles",
                principalTable: "FileItems",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploadItems_Users_UserId",
                schema: "lefiles",
                table: "FileUploadItems",
                column: "UserId",
                principalSchema: "lefiles",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUploadItems_FileItems_Id",
                schema: "lefiles",
                table: "FileUploadItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FileUploadItems_Users_UserId",
                schema: "lefiles",
                table: "FileUploadItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileUploadItems",
                schema: "lefiles",
                table: "FileUploadItems");

            migrationBuilder.RenameTable(
                name: "FileUploadItems",
                schema: "lefiles",
                newName: "FileUploadItem",
                newSchema: "lefiles");

            migrationBuilder.RenameIndex(
                name: "IX_FileUploadItems_UserId",
                schema: "lefiles",
                table: "FileUploadItem",
                newName: "IX_FileUploadItem_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileUploadItem",
                schema: "lefiles",
                table: "FileUploadItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploadItem_FileItems_Id",
                schema: "lefiles",
                table: "FileUploadItem",
                column: "Id",
                principalSchema: "lefiles",
                principalTable: "FileItems",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploadItem_Users_UserId",
                schema: "lefiles",
                table: "FileUploadItem",
                column: "UserId",
                principalSchema: "lefiles",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
