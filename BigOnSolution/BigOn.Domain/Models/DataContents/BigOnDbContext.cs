using BigOn.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BigOn.Domain.Models.DataContents
{
    public class BigOnDbContext : DbContext
    {
        public BigOnDbContext(DbContextOptions options)
            :base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactPost> ContactPosts { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductCatalogItem> ProductCatalogItem { get; set; } //ad sehvi
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductCatalogItem>(cfg =>
            {
                cfg.HasKey(k => new   //HasKey 1-de cox PrimaryKey yazmaq uchundur.
                {
                    k.ProductId,
                    k.ProductSizeId,
                    k.ProductMaterialId,
                    k.ProductColorId,
                    k.ProductTypeId
                });
                cfg.Property(p => p.Id).UseIdentityColumn(1, 1);
            });
        }
    }
}
