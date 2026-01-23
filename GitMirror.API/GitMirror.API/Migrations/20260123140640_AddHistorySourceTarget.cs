using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitMirror.API.Migrations
{
    /// <inheritdoc />
    public partial class AddHistorySourceTarget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceUrl",
                table: "Histories",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TargetUrl",
                table: "Histories",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceUrl",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "TargetUrl",
                table: "Histories");
        }
    }
}
