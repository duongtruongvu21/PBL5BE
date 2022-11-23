using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBL5BE.API.Data.Migrations
{
    public partial class UpdateProduct_Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isReviewed",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "HtmlDescription",
                table: "Products",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MarkdownDescription",
                table: "Products",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "SoldQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "imgUrl",
                table: "Categories",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HtmlDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MarkdownDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SoldQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "imgUrl",
                table: "Categories");

            migrationBuilder.AddColumn<bool>(
                name: "isReviewed",
                table: "Products",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
