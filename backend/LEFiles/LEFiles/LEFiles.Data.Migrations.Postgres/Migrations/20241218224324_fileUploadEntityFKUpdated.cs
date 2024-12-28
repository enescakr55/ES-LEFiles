using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LEFiles.Data.Migrations.Postgres.Migrations
{
    public partial class fileUploadEntityFKUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUploadItems_FileItems_Id",
                schema: "lefiles",
                table: "FileUploadItems");

            migrationBuilder.CreateIndex(
                name: "IX_FileItems_FileUploadId",
                schema: "lefiles",
                table: "FileItems",
                column: "FileUploadId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileItems_FileUploadItems_FileUploadId",
                schema: "lefiles",
                table: "FileItems",
                column: "FileUploadId",
                principalSchema: "lefiles",
                principalTable: "FileUploadItems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileItems_FileUploadItems_FileUploadId",
                schema: "lefiles",
                table: "FileItems");

            migrationBuilder.DropIndex(
                name: "IX_FileItems_FileUploadId",
                schema: "lefiles",
                table: "FileItems");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploadItems_FileItems_Id",
                schema: "lefiles",
                table: "FileUploadItems",
                column: "Id",
                principalSchema: "lefiles",
                principalTable: "FileItems",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
