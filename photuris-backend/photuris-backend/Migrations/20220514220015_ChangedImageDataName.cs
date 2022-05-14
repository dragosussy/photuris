using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace photuris_backend.Migrations
{
    public partial class ChangedImageDataName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BinaryImageData",
                table: "Pictures",
                newName: "ImageDataBase64");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageDataBase64",
                table: "Pictures",
                newName: "BinaryImageData");
        }
    }
}
