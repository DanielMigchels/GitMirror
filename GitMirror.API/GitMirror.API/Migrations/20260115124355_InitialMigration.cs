using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitMirror.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    BaseUrl = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceCloneUrl = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    SourceUsername = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    SourcePassword = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    TargetCloneUrl = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    TargetUsername = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    TargetPassword = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mirrors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourcePlatformId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetPlatformId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mirrors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mirrors_Platforms_SourcePlatformId",
                        column: x => x.SourcePlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mirrors_Platforms_TargetPlatformId",
                        column: x => x.TargetPlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MirrorId = table.Column<Guid>(type: "uuid", nullable: true),
                    RepositoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_Mirrors_MirrorId",
                        column: x => x.MirrorId,
                        principalTable: "Mirrors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Histories_Repositories_RepositoryId",
                        column: x => x.RepositoryId,
                        principalTable: "Repositories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Histories_MirrorId",
                table: "Histories",
                column: "MirrorId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_RepositoryId",
                table: "Histories",
                column: "RepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Mirrors_SourcePlatformId",
                table: "Mirrors",
                column: "SourcePlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Mirrors_TargetPlatformId",
                table: "Mirrors",
                column: "TargetPlatformId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "Mirrors");

            migrationBuilder.DropTable(
                name: "Repositories");

            migrationBuilder.DropTable(
                name: "Platforms");
        }
    }
}
