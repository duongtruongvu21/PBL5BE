using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBL5BE.API.Data.Migrations
{
    public partial class editusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureURL",
                table: "UserInfos");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "UserInfos",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserInfos");

            migrationBuilder.AddColumn<string>(
                name: "PictureURL",
                table: "UserInfos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
