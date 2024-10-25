using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteConstructor.Migrations
{
    /// <inheritdoc />
    public partial class addUsersSiteDataToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "LocalUsers",
                newName: "Username");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "LocalUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "LocalUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "BackgroundColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Main = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Additional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Translucent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Navigation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackgroundColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TextColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Main = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Additional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Translucent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersSiteData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ColorSchemeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundColorsId = table.Column<int>(type: "int", nullable: true),
                    TextColorsId = table.Column<int>(type: "int", nullable: true),
                    HeadersFont = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainTextFont = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoBackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemoveLogoBackground = table.Column<bool>(type: "bit", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VkLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelegramLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YoutubeLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersSiteData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersSiteData_BackgroundColors_BackgroundColorsId",
                        column: x => x.BackgroundColorsId,
                        principalTable: "BackgroundColors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersSiteData_TextColors_TextColorsId",
                        column: x => x.TextColorsId,
                        principalTable: "TextColors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserSiteDataId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageFileBase64String = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_UsersSiteData_UserSiteDataId",
                        column: x => x.UserSiteDataId,
                        principalTable: "UsersSiteData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserSiteDataId",
                table: "Images",
                column: "UserSiteDataId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersSiteData_BackgroundColorsId",
                table: "UsersSiteData",
                column: "BackgroundColorsId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersSiteData_TextColorsId",
                table: "UsersSiteData",
                column: "TextColorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "UsersSiteData");

            migrationBuilder.DropTable(
                name: "BackgroundColors");

            migrationBuilder.DropTable(
                name: "TextColors");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "LocalUsers",
                newName: "UserName");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "LocalUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "LocalUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
