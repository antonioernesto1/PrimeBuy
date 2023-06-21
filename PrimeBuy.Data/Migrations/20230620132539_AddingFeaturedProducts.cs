using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeBuy.Data.Migrations
{
    public partial class AddingFeaturedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_featured",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_featured",
                table: "Products");
        }
    }
}
