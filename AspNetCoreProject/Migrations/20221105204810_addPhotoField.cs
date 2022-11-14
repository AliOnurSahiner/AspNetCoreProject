using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreProject.Migrations
{
    public partial class addPhotoField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resim",
                table: "Uruns",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resim",
                table: "Uruns");
        }
    }
}
