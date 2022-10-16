using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.Domain.Models.DataContexts.Configurations
{
    public class BigOnUserRoleClaimEntityTypeConfiguration : IEntityTypeConfiguration<BigOnUserRole>
    {
        public void Configure(EntityTypeBuilder<BigOnUserRole> builder)
        {
            builder.ToTable("UserRoles","Membership");
        }
    }
}
