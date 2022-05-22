using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace photuris_backend.Migrations
{
    public partial class AddedAlbums2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumPicture_Album_AlbumsId",
                table: "AlbumPicture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "Album");

            migrationBuilder.RenameTable(
                name: "Album",
                newName: "Albums");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Albums",
                table: "Albums",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumPicture_Albums_AlbumsId",
                table: "AlbumPicture",
                column: "AlbumsId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumPicture_Albums_AlbumsId",
                table: "AlbumPicture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Albums",
                table: "Albums");

            migrationBuilder.RenameTable(
                name: "Albums",
                newName: "Album");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "Album",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumPicture_Album_AlbumsId",
                table: "AlbumPicture",
                column: "AlbumsId",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
