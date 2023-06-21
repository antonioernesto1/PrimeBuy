using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeBuy.Data.Migrations
{
    public partial class AddingSessionIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "session_id",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "session_id",
                table: "Orders");
        }
    }
}
