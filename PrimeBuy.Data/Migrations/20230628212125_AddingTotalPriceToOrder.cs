using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeBuy.Data.Migrations
{
    public partial class AddingTotalPriceToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "total_price",
                table: "Orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_price",
                table: "Orders");
        }
    }
}
