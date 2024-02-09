using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatingAppProCardoAI.Migrations
{
    /// <inheritdoc />
    public partial class change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsProfile",
                table: "Image",
                newName: "IsProfilePicture");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsProfilePicture",
                table: "Image",
                newName: "IsProfile");
        }
    }
}
