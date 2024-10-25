using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteConstructor.Migrations
{
    /// <inheritdoc />
    public partial class fixColorsUserSiteDataLoadsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersSiteData_BackgroundColors_BackgroundColorsId",
                table: "UsersSiteData");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersSiteData_TextColors_TextColorsId",
                table: "UsersSiteData");

            migrationBuilder.DropIndex(
                name: "IX_UsersSiteData_BackgroundColorsId",
                table: "UsersSiteData");

            migrationBuilder.DropIndex(
                name: "IX_UsersSiteData_TextColorsId",
                table: "UsersSiteData");

            migrationBuilder.DropColumn(
                name: "BackgroundColorsId",
                table: "UsersSiteData");

            migrationBuilder.DropColumn(
                name: "TextColorsId",
                table: "UsersSiteData");

            migrationBuilder.AddColumn<int>(
                name: "UserSiteDataId",
                table: "TextColors",
                type: "int",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "UserSiteDataId",
                table: "BackgroundColors",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_TextColors_UserSiteDataId",
                table: "TextColors",
                column: "UserSiteDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BackgroundColors_UserSiteDataId",
                table: "BackgroundColors",
                column: "UserSiteDataId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BackgroundColors_UsersSiteData_UserSiteDataId",
                table: "BackgroundColors",
                column: "UserSiteDataId",
                principalTable: "UsersSiteData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TextColors_UsersSiteData_UserSiteDataId",
                table: "TextColors",
                column: "UserSiteDataId",
                principalTable: "UsersSiteData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackgroundColors_UsersSiteData_UserSiteDataId",
                table: "BackgroundColors");

            migrationBuilder.DropForeignKey(
                name: "FK_TextColors_UsersSiteData_UserSiteDataId",
                table: "TextColors");

            migrationBuilder.DropIndex(
                name: "IX_TextColors_UserSiteDataId",
                table: "TextColors");

            migrationBuilder.DropIndex(
                name: "IX_BackgroundColors_UserSiteDataId",
                table: "BackgroundColors");

            migrationBuilder.DropColumn(
                name: "UserSiteDataId",
                table: "TextColors");

            migrationBuilder.DropColumn(
                name: "UserSiteDataId",
                table: "BackgroundColors");

            migrationBuilder.AddColumn<int>(
                name: "BackgroundColorsId",
                table: "UsersSiteData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TextColorsId",
                table: "UsersSiteData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersSiteData_BackgroundColorsId",
                table: "UsersSiteData",
                column: "BackgroundColorsId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersSiteData_TextColorsId",
                table: "UsersSiteData",
                column: "TextColorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSiteData_BackgroundColors_BackgroundColorsId",
                table: "UsersSiteData",
                column: "BackgroundColorsId",
                principalTable: "BackgroundColors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSiteData_TextColors_TextColorsId",
                table: "UsersSiteData",
                column: "TextColorsId",
                principalTable: "TextColors",
                principalColumn: "Id");
        }
    }
}
