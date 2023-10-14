using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheJitu_Commerce_Cart.Migrations
{
    /// <inheritdoc />
    public partial class columnNameChangeHeaderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cartHeaderId",
                table: "CartHeader",
                newName: "CartHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartHeaderId",
                table: "CartHeader",
                newName: "cartHeaderId");
        }
    }
}
