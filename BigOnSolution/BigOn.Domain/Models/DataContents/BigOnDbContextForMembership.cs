using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.Domain.Models.DataContents
{
    public partial class BigOnDbContext
    {
        public DbSet<BigOnRole> Roles { get; set; }
        public DbSet<BigOnRoleClaim> RoleClaims { get; set; }
        public DbSet<BigOnUser> Users { get; set; }
        public DbSet<BigOnUserClaim> UserClaims { get; set; }
        public DbSet<BigOnUserLogin> UserLogins { get; set; }
        public DbSet<BigOnUserRole> UserRoles { get; set; }
        public DbSet<BigOnUserToken> UserTokens { get; set; }
    }
}
