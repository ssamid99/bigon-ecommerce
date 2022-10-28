using BigOn.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Models.DataContents.Configurations
{
    public class BlogPostEntityTypeConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BlogPost> builder)
        {
            builder.HasKey(bp => bp.Id);
            builder.Property(bp => bp.Title)
                   .IsRequired();
            builder.Property(bp => bp.Body)
                   .IsRequired();
            builder.Property(bp => bp.ImagePath)
                   .IsRequired();
            builder.Property(bp => bp.Slug)
                   .IsUnicode(false)
                   .HasMaxLength(900)
                   .IsRequired();
            builder.HasIndex(bp=>bp.Slug)
                .IsUnique();
        }
    }
}
