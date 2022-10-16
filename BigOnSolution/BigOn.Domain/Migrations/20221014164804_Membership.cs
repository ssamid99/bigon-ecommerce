using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BigOn.Domain.Migrations
{
    public partial class Membership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCatalogItem",
                table: "ProductCatalogItem");

            migrationBuilder.EnsureSchema(
                name: "Membership");

            migrationBuilder.RenameTable(
                name: "ProductCatalogItem",
                newName: "ProductCatalog");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalogItem_ProductTypeId",
                table: "ProductCatalog",
                newName: "IX_ProductCatalog_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalogItem_ProductSizeId",
                table: "ProductCatalog",
                newName: "IX_ProductCatalog_ProductSizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalogItem_ProductMaterialId",
                table: "ProductCatalog",
                newName: "IX_ProductCatalog_ProductMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalogItem_ProductColorId",
                table: "ProductCatalog",
                newName: "IX_ProductCatalog_ProductColorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCatalog",
                table: "ProductCatalog",
                columns: new[] { "ProductId", "ProductSizeId", "ProductTypeId", "ProductMaterialId", "ProductColorId" });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Membership",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Membership",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Membership",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Membership",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Membership",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalog_Id",
                table: "ProductCatalog",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Membership",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Membership",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "Membership",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "Membership",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Membership",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Membership",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Membership",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalog_ProductColors_ProductColorId",
                table: "ProductCatalog",
                column: "ProductColorId",
                principalTable: "ProductColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalog_ProductMaterials_ProductMaterialId",
                table: "ProductCatalog",
                column: "ProductMaterialId",
                principalTable: "ProductMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalog_Products_ProductId",
                table: "ProductCatalog",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalog_ProductSizes_ProductSizeId",
                table: "ProductCatalog",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalog_ProductTypes_ProductTypeId",
                table: "ProductCatalog",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalog_ProductColors_ProductColorId",
                table: "ProductCatalog");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalog_ProductMaterials_ProductMaterialId",
                table: "ProductCatalog");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalog_Products_ProductId",
                table: "ProductCatalog");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalog_ProductSizes_ProductSizeId",
                table: "ProductCatalog");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalog_ProductTypes_ProductTypeId",
                table: "ProductCatalog");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Membership");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCatalog",
                table: "ProductCatalog");

            migrationBuilder.DropIndex(
                name: "IX_ProductCatalog_Id",
                table: "ProductCatalog");

            migrationBuilder.RenameTable(
                name: "ProductCatalog",
                newName: "ProductCatalogItem");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalog_ProductTypeId",
                table: "ProductCatalogItem",
                newName: "IX_ProductCatalogItem_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalog_ProductSizeId",
                table: "ProductCatalogItem",
                newName: "IX_ProductCatalogItem_ProductSizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalog_ProductMaterialId",
                table: "ProductCatalogItem",
                newName: "IX_ProductCatalogItem_ProductMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCatalog_ProductColorId",
                table: "ProductCatalogItem",
                newName: "IX_ProductCatalogItem_ProductColorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCatalogItem",
                table: "ProductCatalogItem",
                columns: new[] { "ProductId", "ProductSizeId", "ProductMaterialId", "ProductColorId", "ProductTypeId" });

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
