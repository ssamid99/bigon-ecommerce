using BigOn.Domain.AppCode.Providers;
using BigOn.Domain.Models.DataContents;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.Domain.AppCode.Extensions
{
    public static partial class Extension
    {
        public static string[] policies = null;
        public static IServiceCollection SetupIdentity(this IServiceCollection services)
        {
            services.AddIdentity<BigOnUser, BigOnRole>()
                .AddEntityFrameworkStores<BigOnDbContext>();

            services.AddScoped<SignInManager<BigOnUser>>();
            services.AddScoped<UserManager<BigOnUser>>();
            services.AddScoped<RoleManager<BigOnRole>>();

            services.AddScoped<IClaimsTransformation, AppClaimProvider>();

            services.Configure<IdentityOptions>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                //cfg.User.AllowedUserNameCharacters = "";

                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequiredLength = 3;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 1;
                
                cfg.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 1, 0);
                cfg.Lockout.AllowedForNewUsers = false;

                cfg.SignIn.RequireConfirmedPhoneNumber = false;
                cfg.SignIn.RequireConfirmedEmail = true;
                cfg.SignIn.RequireConfirmedAccount = false;
            });

            services.ConfigureApplicationCookie(cfg => {
                cfg.LoginPath = "/signin.html";
                cfg.AccessDeniedPath = "/accessdenied.html";
                cfg.Cookie.Name = "bigon";
                cfg.Cookie.HttpOnly = true;
                cfg.ExpireTimeSpan = new TimeSpan(0, 15, 0);
            
            });

            return services; 
        }

        public static bool HasAccess(this ClaimsPrincipal principal, string policyName)
        {
            if (principal.IsInRole("sa"))
            {
                return true;
            }
            return principal.Claims.Any(c => c.Type.Equals(policyName) && c.Value.Equals("1"));
        }

        public static int GetCurrentUserId(this ClaimsIdentity identity)
        {
            return Convert.ToInt32(
                    identity.Claims.FirstOrDefault(c =>
                    c.Type.Equals(ClaimTypes.NameIdentifier)).Value
                    );
        }
    }
}
