using Microsoft.EntityFrameworkCore.Migrations;

namespace BigOn.Domain.Migrations
{
    public partial class AddDeletedUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Subscribes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ProductTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ProductSizes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ProductMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ProductImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ProductColors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ProductCatalog",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Faqs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ContactPosts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Brands",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "BlogPostTagCloud",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "BlogPosts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "BlogPostComments",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Subscribes");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ProductMaterials");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ProductCatalog");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Faqs");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ContactPosts");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BlogPostTagCloud");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BlogPostComments");
        }
    }
}
