using BigOn.Domain.Models.DataContents;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.Domain.AppCode.Extensions
{
    public static partial class Extension
    {
        public static IServiceCollection SetupIdentity(this IServiceCollection services)
        {
            services.AddIdentity<BigOnUser, BigOnRole>()
                .AddEntityFrameworkStores<BigOnDbContext>();

            services.AddScoped<SignInManager<BigOnUser>>();
            services.AddScoped<UserManager<BigOnUser>>();
            services.AddScoped<RoleManager<BigOnRole>>();
            return services; 
        }
    }
}
