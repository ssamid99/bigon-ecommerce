using BigOn.Domain.Models.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace BigOn.Domain.Models.DataContents.Configurations.Chat
{
    public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages", "Chat");
            builder.Property(m => m.Text)
                .IsRequired();
        }
    }
}
