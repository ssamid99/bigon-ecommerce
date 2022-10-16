using BigOn.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigOn.Domain.Models.DataContexts.Configurations
{
    internal class SubscribeEntityTypeConfiguration
        : IEntityTypeConfiguration<Subscribe>
    {
        public void Configure(EntityTypeBuilder<Subscribe> builder)
        {
            builder.HasIndex(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(c => c.Email)
                .IsRequired();

        }
    }
}
