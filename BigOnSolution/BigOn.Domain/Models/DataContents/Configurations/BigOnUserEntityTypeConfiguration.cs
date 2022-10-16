using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.Domain.Models.DataContexts.Configurations
{
    public class BigOnUserEntityTypeConfiguration : IEntityTypeConfiguration<BigOnUser>
    {
        public void Configure(EntityTypeBuilder<BigOnUser> builder)
        {
            builder.ToTable("Users","Membership");
        }
    }
}
