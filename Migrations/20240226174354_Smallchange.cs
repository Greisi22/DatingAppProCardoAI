using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatingAppProCardoAI.Migrations
{
    /// <inheritdoc />
    public partial class Smallchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_User1Id",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_User1Id",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "User1Id",
                table: "Friendships");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "Friendships",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserId1",
                table: "Friendships",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_UserId1",
                table: "Friendships",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_UserId1",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_UserId1",
                table: "Friendships");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "Friendships",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "User1Id",
                table: "Friendships",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_User1Id",
                table: "Friendships",
                column: "User1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_User1Id",
                table: "Friendships",
                column: "User1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
