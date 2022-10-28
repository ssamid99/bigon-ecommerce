using BigOn.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.Domain.Models.DataContents
{
    public partial class BigOnDbContext : IdentityDbContext<BigOnUser, BigOnRole, int, BigOnUserClaim, BigOnUserRole, BigOnUserLogin, BigOnRoleClaim, BigOnUserToken>
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
        public DbSet<ProductCatalogItem> ProductCatalogItem { get; set; } 
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set; }
        public DbSet<BlogPostTagItem> BlogPostTagCloud { get; set; }
        public DbSet<Tag> Tags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var asm = typeof(BigOnDbContext).Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(asm);
        }
    }
}
