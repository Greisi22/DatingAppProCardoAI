using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatingAppProCardoAI.Migrations
{
    /// <inheritdoc />
    public partial class addingAnAttributeInImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MemorySize",
                table: "Image",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemorySize",
                table: "Image");
        }
    }
}
