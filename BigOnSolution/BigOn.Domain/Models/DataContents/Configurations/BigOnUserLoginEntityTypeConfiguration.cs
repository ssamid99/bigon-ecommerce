using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.Domain.Models.DataContexts.Configurations
{
    public class BigOnUserLoginEntityTypeConfiguration : IEntityTypeConfiguration<BigOnUserLogin>
    {
        public void Configure(EntityTypeBuilder<BigOnUserLogin> builder)
        {
            builder.ToTable("UserLogins","Membership");
        }
    }
}
