using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.Domain.Models.DataContexts.Configurations
{
    public class BigOnRoleEntityTypeConfiguration : IEntityTypeConfiguration<BigOnRole>
    {
        public void Configure(EntityTypeBuilder<BigOnRole> builder)
        {
            builder.ToTable("Roles","Membership");
        }
    }
}
