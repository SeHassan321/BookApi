using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksApi.Migrations
{
    public partial class listOfBooksInFavoritList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritLists_Books_BookId",
                table: "FavoritLists");

            migrationBuilder.DropIndex(
                name: "IX_FavoritLists_BookId",
                table: "FavoritLists");

            migrationBuilder.AddColumn<int>(
                name: "FavoritListId",
                table: "Books",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_FavoritListId",
                table: "Books",
                column: "FavoritListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_FavoritLists_FavoritListId",
                table: "Books",
                column: "FavoritListId",
                principalTable: "FavoritLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_FavoritLists_FavoritListId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_FavoritListId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "FavoritListId",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritLists_BookId",
                table: "FavoritLists",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritLists_Books_BookId",
                table: "FavoritLists",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
