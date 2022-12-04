using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksApi.Migrations
{
    public partial class kofr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritLists_AspNetUsers_UserFavoriteId",
                table: "FavoritLists");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_AspNetUsers_ApplicationUsersId",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FavoritLists");

            migrationBuilder.RenameColumn(
                name: "ApplicationUsersId",
                table: "UserBooks",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBooks_ApplicationUsersId",
                table: "UserBooks",
                newName: "IX_UserBooks_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "UserFavoriteId",
                table: "FavoritLists",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritLists_UserFavoriteId",
                table: "FavoritLists",
                newName: "IX_FavoritLists_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritLists_AspNetUsers_ApplicationUserId",
                table: "FavoritLists",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_AspNetUsers_ApplicationUserId",
                table: "UserBooks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritLists_AspNetUsers_ApplicationUserId",
                table: "FavoritLists");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_AspNetUsers_ApplicationUserId",
                table: "UserBooks");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "UserBooks",
                newName: "ApplicationUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBooks_ApplicationUserId",
                table: "UserBooks",
                newName: "IX_UserBooks_ApplicationUsersId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "FavoritLists",
                newName: "UserFavoriteId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritLists_ApplicationUserId",
                table: "FavoritLists",
                newName: "IX_FavoritLists_UserFavoriteId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserBooks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FavoritLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritLists_AspNetUsers_UserFavoriteId",
                table: "FavoritLists",
                column: "UserFavoriteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_AspNetUsers_ApplicationUsersId",
                table: "UserBooks",
                column: "ApplicationUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
