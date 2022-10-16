using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.Domain.Models.DataContexts.Configurations
{
    public class BigOnUserTokenEntityTypeConfiguration : IEntityTypeConfiguration<BigOnUserToken>
    {
        public void Configure(EntityTypeBuilder<BigOnUserToken> builder)
        {
            builder.ToTable("UserTokens","Membership");
        }
    }
}
