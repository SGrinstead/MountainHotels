using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MountainHotels.Migrations
{
    public partial class AddYearBuiltColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "year_built",
                table: "hotels",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "year_built",
                table: "hotels");
        }
    }
}
