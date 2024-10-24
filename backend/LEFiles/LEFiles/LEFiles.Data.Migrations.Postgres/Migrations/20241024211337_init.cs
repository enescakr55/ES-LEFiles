using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LEFiles.Data.Migrations.Postgres.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Firstname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ClientRegistrationTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    UserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Secret = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRegistrationTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientRegistrationTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ClientName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ClientSecret = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OperatingSystem = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    HarddiskSerialNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FolderItems",
                columns: table => new
                {
                    FolderId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FolderName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Shared = table.Column<bool>(type: "boolean", nullable: false),
                    ParentFolderId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderItems", x => x.FolderId);
                    table.ForeignKey(
                        name: "FK_FolderItems_FolderItems_ParentFolderId",
                        column: x => x.ParentFolderId,
                        principalTable: "FolderItems",
                        principalColumn: "FolderId");
                    table.ForeignKey(
                        name: "FK_FolderItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSessions",
                columns: table => new
                {
                    ClientSessionId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ClientId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SessionCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSessions", x => x.ClientSessionId);
                    table.ForeignKey(
                        name: "FK_ClientSessions_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaitableRequests",
                columns: table => new
                {
                    RequestId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ClientId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RequestType = table.Column<int>(type: "integer", nullable: false),
                    RequestContent = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    Response = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaitableRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_WaitableRequests_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WaitableRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileItems",
                columns: table => new
                {
                    FileId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Extension = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ContentType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FilePath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FileAttribute = table.Column<int>(type: "integer", nullable: false),
                    Shared = table.Column<bool>(type: "boolean", nullable: false),
                    ParentFolderId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileItems", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_FileItems_FolderItems_ParentFolderId",
                        column: x => x.ParentFolderId,
                        principalTable: "FolderItems",
                        principalColumn: "FolderId");
                    table.ForeignKey(
                        name: "FK_FileItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientRegistrationTokens_Id",
                table: "ClientRegistrationTokens",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientRegistrationTokens_UserId",
                table: "ClientRegistrationTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientId",
                table: "Clients",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSessions_ClientId",
                table: "ClientSessions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSessions_ClientSessionId",
                table: "ClientSessions",
                column: "ClientSessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileItems_FileId",
                table: "FileItems",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileItems_ParentFolderId",
                table: "FileItems",
                column: "ParentFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_FileItems_UserId",
                table: "FileItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderItems_FolderId",
                table: "FolderItems",
                column: "FolderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FolderItems_ParentFolderId",
                table: "FolderItems",
                column: "ParentFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderItems_UserId",
                table: "FolderItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                table: "Users",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaitableRequests_ClientId",
                table: "WaitableRequests",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WaitableRequests_RequestId",
                table: "WaitableRequests",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaitableRequests_UserId",
                table: "WaitableRequests",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientRegistrationTokens");

            migrationBuilder.DropTable(
                name: "ClientSessions");

            migrationBuilder.DropTable(
                name: "FileItems");

            migrationBuilder.DropTable(
                name: "WaitableRequests");

            migrationBuilder.DropTable(
                name: "FolderItems");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
