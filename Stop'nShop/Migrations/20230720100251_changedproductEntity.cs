using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stop_nShop.Migrations
{
    /// <inheritdoc />
    public partial class changedproductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "orderId",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "orderId",
                table: "Products",
                type: "int",
                nullable: true);
        }
    }
}
