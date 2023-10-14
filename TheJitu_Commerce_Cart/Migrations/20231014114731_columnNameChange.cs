using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheJitu_Commerce_Cart.Migrations
{
    /// <inheritdoc />
    public partial class columnNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cartHeader",
                table: "CartHeader",
                newName: "cartHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cartHeaderId",
                table: "CartHeader",
                newName: "cartHeader");
        }
    }
}
