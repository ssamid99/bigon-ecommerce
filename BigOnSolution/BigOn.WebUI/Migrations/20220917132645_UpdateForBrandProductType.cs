using Microsoft.EntityFrameworkCore.Migrations;

namespace BigOn.WebUI.Migrations
{
    public partial class UpdateForBrandProductType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_ProductTypes_ProductTypeId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalogItem_ProductColors_ProductColorId",
                table: "ProductCatalogItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalogItem_ProductMaterials_ProductMaterialId",
                table: "ProductCatalogItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalogItem_Products_ProductId",
                table: "ProductCatalogItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalogItem_ProductSizes_ProductSizeId",
                table: "ProductCatalogItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalogItem_ProductTypes_ProductTypeId",
                table: "ProductCatalogItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCatalogItem",
                table: "ProductCatalogItem");

            migrationBuilder.DropIndex(
                name: "IX_Brands_ProductTypeId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Brands");

            migrationBuilder.RenameTable(
                name: "ProductCatalogItem",
                newName: "ProductCatalogitem");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalogItem_ProductTypeId",
                table: "ProductCatalogitem",
                newName: "IX_ProductCatalogitem_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalogItem_ProductSizeId",
                table: "ProductCatalogitem",
                newName: "IX_ProductCatalogitem_ProductSizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalogItem_ProductMaterialId",
                table: "ProductCatalogitem",
                newName: "IX_ProductCatalogitem_ProductMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalogItem_ProductColorId",
                table: "ProductCatalogitem",
                newName: "IX_ProductCatalogitem_ProductColorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCatalogitem",
                table: "ProductCatalogitem",
                columns: new[] { "ProductId", "ProductSizeId", "ProductMaterialId", "ProductColorId", "ProductTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalogitem_ProductColors_ProductColorId",
                table: "ProductCatalogitem",
                column: "ProductColorId",
                principalTable: "ProductColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalogitem_ProductMaterials_ProductMaterialId",
                table: "ProductCatalogitem",
                column: "ProductMaterialId",
                principalTable: "ProductMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalogitem_Products_ProductId",
                table: "ProductCatalogitem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalogitem_ProductSizes_ProductSizeId",
                table: "ProductCatalogitem",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalogitem_ProductTypes_ProductTypeId",
                table: "ProductCatalogitem",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalogitem_ProductColors_ProductColorId",
                table: "ProductCatalogitem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalogitem_ProductMaterials_ProductMaterialId",
                table: "ProductCatalogitem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalogitem_Products_ProductId",
                table: "ProductCatalogitem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalogitem_ProductSizes_ProductSizeId",
                table: "ProductCatalogitem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalogitem_ProductTypes_ProductTypeId",
                table: "ProductCatalogitem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCatalogitem",
                table: "ProductCatalogitem");

            migrationBuilder.RenameTable(
                name: "ProductCatalogitem",
                newName: "ProductCatalogItem");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalogitem_ProductTypeId",
                table: "ProductCatalogItem",
                newName: "IX_ProductCatalogItem_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalogitem_ProductSizeId",
                table: "ProductCatalogItem",
                newName: "IX_ProductCatalogItem_ProductSizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalogitem_ProductMaterialId",
                table: "ProductCatalogItem",
                newName: "IX_ProductCatalogItem_ProductMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalogitem_ProductColorId",
                table: "ProductCatalogItem",
                newName: "IX_ProductCatalogItem_ProductColorId");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "ProductTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "Brands",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCatalogItem",
                table: "ProductCatalogItem",
                columns: new[] { "ProductId", "ProductSizeId", "ProductMaterialId", "ProductColorId", "ProductTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_ProductTypeId",
                table: "Brands",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_ProductTypes_ProductTypeId",
                table: "Brands",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalogItem_ProductColors_ProductColorId",
                table: "ProductCatalogItem",
                column: "ProductColorId",
                principalTable: "ProductColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalogItem_ProductMaterials_ProductMaterialId",
                table: "ProductCatalogItem",
                column: "ProductMaterialId",
                principalTable: "ProductMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalogItem_Products_ProductId",
                table: "ProductCatalogItem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalogItem_ProductSizes_ProductSizeId",
                table: "ProductCatalogItem",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalogItem_ProductTypes_ProductTypeId",
                table: "ProductCatalogItem",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
