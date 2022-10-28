using BigOn.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigOn.Domain.Models.DataContexts.Configurations
{
    internal class ContactPostEntityTypeConfiguration
        : IEntityTypeConfiguration<ContactPost>
    {
        public void Configure(EntityTypeBuilder<ContactPost> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.Email)
                .IsRequired();

            builder.HasIndex(e => e.Email)
                .IsUnique();

            builder.Property(c => c.Subject)
                .IsRequired();

            builder.Property(c => c.Message)
                .IsRequired();
        }
    }
}
