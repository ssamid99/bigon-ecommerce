using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.Domain.Models.DataContexts.Configurations
{
    public class BigOnUserClaimClaimEntityTypeConfiguration : IEntityTypeConfiguration<BigOnUserClaim>
    {
        public void Configure(EntityTypeBuilder<BigOnUserClaim> builder)
        {
            builder.ToTable("UserClaims","Membership");
        }
    }
}
