using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatingAppProCardoAI.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_UserId1",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_UserId2",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_UserId1",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_UserId2",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "Friendships");

            migrationBuilder.CreateTable(
                name: "UserFriendship",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FriendshipsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFriendship", x => new { x.UserId, x.FriendshipsId });
                    table.ForeignKey(
                        name: "FK_UserFriendship_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFriendship_Friendships_FriendshipsId",
                        column: x => x.FriendshipsId,
                        principalTable: "Friendships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFriendship_FriendshipsId",
                table: "UserFriendship",
                column: "FriendshipsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFriendship");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Friendships",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId2",
                table: "Friendships",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserId1",
                table: "Friendships",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserId2",
                table: "Friendships",
                column: "UserId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_UserId1",
                table: "Friendships",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_UserId2",
                table: "Friendships",
                column: "UserId2",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
