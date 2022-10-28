using BigOn.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigOn.Domain.Models.DataContexts.Configurations
{
    internal class ProductCatalogItemEntityTypeConfiguration
        : IEntityTypeConfiguration<ProductCatalogItem>
    {
        public void Configure(EntityTypeBuilder<ProductCatalogItem> builder)
        {
            builder.HasKey(k => new
            {
                k.ProductId,
                k.ProductSizeId,
                k.ProductTypeId,
                k.ProductMaterialId,
                k.ProductColorId
            });

            builder.Property(t => t.Id).UseIdentityColumn();

            builder.HasIndex(t => t.Id).IsUnique();

            builder.ToTable("ProductCatalog");
        }
    }
}
//      Gelenden sonra sqlde deyishiklikleri etmeli, BlogPOstcontrollerde tagleri cagrmali bide detallida tagleri gormeycun FE yazmaliyq  