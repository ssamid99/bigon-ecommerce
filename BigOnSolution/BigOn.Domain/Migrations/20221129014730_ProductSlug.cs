using Microsoft.EntityFrameworkCore.Migrations;

namespace BigOn.Domain.Migrations
{
    public partial class ProductSlug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductImages",
                newName: "ProductsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductsId");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductsId",
                table: "ProductImages",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductsId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "ProductImages",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductsId",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
