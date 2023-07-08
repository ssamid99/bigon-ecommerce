using BigOn.Domain.Models.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace BigOn.Domain.Models.DataContents.Configurations.Chat
{
    public class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups", "Chat");
            builder.Property(g => g.Name)
                .IsRequired();
        }
    }
}
