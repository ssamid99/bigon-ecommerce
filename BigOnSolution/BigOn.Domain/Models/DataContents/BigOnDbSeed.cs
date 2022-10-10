using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BigOn.Domain.Models.DataContents
{
    public static class BigOnDbSeed
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
               var db=scope.ServiceProvider.GetService<BigOnDbContext>();

                db.Database.Migrate();

                InitBrands(db);
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
                }) ;
                
                db.SaveChanges();
            }
        }
    }
}
