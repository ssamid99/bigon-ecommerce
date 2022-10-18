using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.Domain.Models.DataContents
{
    public static class BigOnDbSeed
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<BigOnDbContext>();

                db.Database.Migrate();

                InitBrands(db);
            }

            return app;
        }

        public static IApplicationBuilder SeedMembership(this IApplicationBuilder app)
        {
            
            using (var scope = app.ApplicationServices.CreateScope())
            {
                //var signInManager = scope.ServiceProvider.GetService<SignInManager<BigOnUser>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<BigOnUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<BigOnRole>>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                string superAdminRoleName = configuration["defaultAccount:superAdmin"];
                string superAdminUserName = configuration["defaultAccount:userName"];
                string superAdminEmail = configuration["defaultAccount:email"];
                string superAdminPassword = configuration["defaultAccount:password"];

                var superAdminRole = roleManager.FindByNameAsync(superAdminRoleName).Result;

                if(superAdminRole == null)
                {
                    superAdminRole = new BigOnRole
                    {
                        Name = superAdminRoleName
                    };
                    var roleResult = roleManager.CreateAsync(superAdminRole).Result;
                    if (!roleResult.Succeeded)
                    {
                        throw new Exception("Has a problem in RoleCreating process...");
                    }
                }

                var superAdminUser = userManager.FindByEmailAsync(superAdminEmail).Result;

                if(superAdminUser == null)
                {
                    superAdminUser = new BigOnUser
                    {
                        Email = superAdminEmail,
                        UserName = superAdminUserName
                    };

                    var userResult = userManager.CreateAsync(superAdminUser, superAdminPassword).Result;

                    if (!userResult.Succeeded)
                    {
                        throw new Exception("Has a problem in UserCreating process...");
                    }
                }

                var isInRole = userManager.IsInRoleAsync(superAdminUser, superAdminRole.Name).Result;

                if(isInRole != true)
                {
                    userManager.AddToRoleAsync(superAdminUser, superAdminRole.Name).Wait();
                }
            }
            return app;

        }



        private static void InitBrands(BigOnDbContext db)
        {
            if (!db.Brands.Any())
            {
                db.Brands.Add(new Entities.Brand
                {
                    Name = "Nike"
                });

                db.SaveChanges();
            }
        }
    }
}

