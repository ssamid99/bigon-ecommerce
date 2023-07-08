using BigOn.Domain.Models.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace BigOn.Domain.Models.DataContents.Configurations.Chat
{
    public class UserGroupEntityTypeConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasKey(k => new { k.UserId, k.GroupId });
            builder.ToTable("UserGroups","Chat");
        }
    }
}
