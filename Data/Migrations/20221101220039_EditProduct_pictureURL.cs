using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBL5BE.API.Data.Migrations
{
    public partial class EditProduct_pictureURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureURL",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfImgs",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfImgs",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "PictureURL",
                table: "Products",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
