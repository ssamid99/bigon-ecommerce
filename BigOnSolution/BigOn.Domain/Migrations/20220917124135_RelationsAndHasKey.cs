using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BigOn.Domain.Migrations
{
    public partial class RelationsAndHasKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Products_ProductId",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Brands_BrandId",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMaterials_Brands_BrandId",
                table: "ProductMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Brands_BrandId",
                table: "ProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizes_BrandId",
                table: "ProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductMaterials_BrandId",
                table: "ProductMaterials");

            migrationBuilder.DropIndex(
                name: "IX_ProductColors_BrandId",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_Brands_ProductId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "ProductMaterials");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "ProductColorId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ProductMaterialId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ProductSizeId",
                table: "Brands");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductSizes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmallName",
                table: "ProductSizes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hex",
                table: "ProductColors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCatalogItem",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    ProductColorId = table.Column<int>(type: "int", nullable: false),
                    ProductSizeId = table.Column<int>(type: "int", nullable: false),
                    ProductMaterialId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCatalogItem", x => new { x.ProductId, x.ProductSizeId, x.ProductMaterialId, x.ProductColorId, x.ProductTypeId });
                    table.ForeignKey(
                        name: "FK_ProductCatalogItem_ProductColors_ProductColorId",
                        column: x => x.ProductColorId,
                        principalTable: "ProductColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCatalogItem_ProductMaterials_ProductMaterialId",
                        column: x => x.ProductMaterialId,
                        principalTable: "ProductMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCatalogItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCatalogItem_ProductSizes_ProductSizeId",
                        column: x => x.ProductSizeId,
                        principalTable: "ProductSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCatalogItem_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalogItem_ProductColorId",
                table: "ProductCatalogItem",
                column: "ProductColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalogItem_ProductMaterialId",
                table: "ProductCatalogItem",
                column: "ProductMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalogItem_ProductSizeId",
                table: "ProductCatalogItem",
                column: "ProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalogItem_ProductTypeId",
                table: "ProductCatalogItem",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "ProductCatalogItem");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SmallName",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "Hex",
                table: "ProductColors");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductSizes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "ProductSizes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "ProductMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "ProductColors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductColorId",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Brands",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductMaterialId",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductSizeId",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_BrandId",
                table: "ProductSizes",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterials_BrandId",
                table: "ProductMaterials",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_BrandId",
                table: "ProductColors",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_ProductId",
                table: "Brands",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Products_ProductId",
                table: "Brands",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Brands_BrandId",
                table: "ProductColors",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMaterials_Brands_BrandId",
                table: "ProductMaterials",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Brands_BrandId",
                table: "ProductSizes",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
