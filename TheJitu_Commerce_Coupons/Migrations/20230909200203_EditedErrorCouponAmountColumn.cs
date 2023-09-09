using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheJitu_Commerce_Coupons.Migrations
{
    /// <inheritdoc />
    public partial class EditedErrorCouponAmountColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoupomAmount",
                table: "Coupons",
                newName: "CouponAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CouponAmount",
                table: "Coupons",
                newName: "CoupomAmount");
        }
    }
}
