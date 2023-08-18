using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stop_nShop.Migrations
{
    /// <inheritdoc />
    public partial class productupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sellers_sellerId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_sellerId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "size",
                table: "Products",
                newName: "productSize");

            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "productSize",
                table: "Products",
                newName: "size");

            migrationBuilder.CreateIndex(
                name: "IX_Products_sellerId",
                table: "Products",
                column: "sellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sellers_sellerId",
                table: "Products",
                column: "sellerId",
                principalTable: "Sellers",
                principalColumn: "sellerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
