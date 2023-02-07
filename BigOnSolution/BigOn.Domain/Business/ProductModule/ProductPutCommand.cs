using BigOn.Domain.AppCode.Extensions;
using BigOn.Domain.AppCode.Infracture;
using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BigOn.Domain.Business.ProductModule
{
    public class ProductPutCommand : IRequest<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StockKeepingUnit { get; set; }
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public ImageItem[] Images { get; set; }

        public class ProductPutCommandHandler : IRequestHandler<ProductPutCommand, Product>
        {
            private readonly BigOnDbContext db;
            private readonly IHostEnvironment env;
            private readonly IActionContextAccessor ctx;

            public ProductPutCommandHandler(BigOnDbContext db, IHostEnvironment env, IActionContextAccessor ctx)
            {
                this.db = db;
                this.env = env;
                this.ctx = ctx;
            }
            public async Task<Product> Handle(ProductPutCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = db.Products
                        .Include(p => p.Images)
                         .Include(p => p.Brand)
                         .Include(p => p.Category)
                         .FirstOrDefault(p => p.Id == request.Id && p.DeletedDate == null);

                    if (entity == null)
                    {
                        return null;
                    }

                    entity.Name = request.Name;
                    entity.StockKeepingUnit = request.StockKeepingUnit;
                    entity.Rate = request.Rate;
                    entity.Price = request.Price;
                    entity.ShortDescription = request.ShortDescription;
                    entity.Description = request.Description;
                    entity.BrandId = request.BrandId;
                    entity.CategoryId = request.CategoryId;

                    if (request.Images != null && request.Images.Where(i => i.File != null).Count() > 0)
                    {
                        #region Elave edilen Files
                        foreach (var imageItem in request.Images.Where(i => i.File != null && i.Id == null))
                        {
                            var image = new ProductImage();
                            image.IsMain = imageItem.IsMain;
                            image.ProductsId = entity.Id;

                            string extension = Path.GetExtension(imageItem.File.FileName);//.jpg
                            string name = $"product-{Guid.NewGuid().ToString().ToLower()}{extension}";

                            string fullName = env.GetImagePhysicalPath(name);

                            using (var fs = new FileStream(fullName, FileMode.Create, FileAccess.Write))
                            {
                                await imageItem.File.CopyToAsync(fs, cancellationToken);
                            }
                            entity.Images.Add(image);
                        }
                        #endregion

                        #region Movcud shekilerden silinibse
                        foreach (var imageItem in request.Images.Where(i => i.Id > 0 && string.IsNullOrWhiteSpace(i.TempPath)))
                        {
                            var data = await db.ProductImages.FirstOrDefaultAsync(pi => pi.Id == imageItem.Id && pi.ProductsId == entity.Id);
                            if (data != null)
                            {
                                data.IsMain = false;
                                data.DeletedDate = DateTime.UtcNow.AddHours(4);
                                data.DeletedByUserId = ctx.GetCurrentUserId();
                            }
                        }
                        #endregion

                        #region Deyishiklik edilmeyibse
                        foreach (var imageItem in entity.Images)
                        {
                            var formForm = request.Images.FirstOrDefault(i => i.Id == imageItem.Id);
                            if (formForm != null)
                            {

                                imageItem.IsMain = formForm.IsMain;
                            }
                        }
                        #endregion
                    }

                    if (string.IsNullOrWhiteSpace(entity.Slug))
                    {
                        entity.Slug = request.Name.ToSlug();
                    }


                    await db.SaveChangesAsync(cancellationToken);
                    return entity;
                }
                catch (System.Exception)
                {
                    return null;
                }
            }
        }
    }
}
