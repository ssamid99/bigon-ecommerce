using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BigOn.WebUI.Migrations
{
    public partial class FullFaqsAndSomeTableNameChages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "ProductCatalogitem",
                newName: "ProductCatalogItem");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "ProductImages");

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

            migrationBuilder.RenameIndex(
                name: "IX_Images_ProductId",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCatalogItem",
                table: "ProductCatalogItem",
                columns: new[] { "ProductId", "ProductSizeId", "ProductMaterialId", "ProductColorId", "ProductTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Faqs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faqs", x => x.Id);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropTable(
                name: "Faqs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCatalogItem",
                table: "ProductCatalogItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.RenameTable(
                name: "ProductCatalogItem",
                newName: "ProductCatalogitem");

            migrationBuilder.RenameTable(
                name: "ProductImages",
                newName: "Images");

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

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductId",
                table: "Images",
                newName: "IX_Images_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCatalogitem",
                table: "ProductCatalogitem",
                columns: new[] { "ProductId", "ProductSizeId", "ProductMaterialId", "ProductColorId", "ProductTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
