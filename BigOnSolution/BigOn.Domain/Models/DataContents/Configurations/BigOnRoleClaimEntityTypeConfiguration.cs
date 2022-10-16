using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.Domain.Models.DataContexts.Configurations
{
    public class BigOnRoleClaimEntityTypeConfiguration : IEntityTypeConfiguration<BigOnRoleClaim>
    {
        public void Configure(EntityTypeBuilder<BigOnRoleClaim> builder)
        {
            builder.ToTable("RoleClaims","Membership");
        }
    }
}
