using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksApi.Migrations
{
    public partial class vvvvvvvvvv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_FavoritLists_FavoritListId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "FavoritListId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_FavoritLists_FavoritListId",
                table: "Books",
                column: "FavoritListId",
                principalTable: "FavoritLists",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_FavoritLists_FavoritListId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "FavoritListId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_FavoritLists_FavoritListId",
                table: "Books",
                column: "FavoritListId",
                principalTable: "FavoritLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
