using BigOn.Domain.AppCode.Extensions;
using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.ProductModule
{
    public class ProductPostCommand : IRequest<Product>
    {
        public string Name { get; set; }
        public string StockKeepingUnit { get; set; }
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public ImageItem[] Images { get; set; }
        public class ProductPostCommandHandler : IRequestHandler<ProductPostCommand, Product>
        {
            private readonly BigOnDbContext db;
            private readonly IHostEnvironment env;

            public ProductPostCommandHandler(BigOnDbContext db, IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<Product> Handle(ProductPostCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    var entity = new Product();
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
                        entity.Images = new List<ProductImage>();

                        foreach (var item in request.Images.Where(i => i.File != null))
                        {
                            var image = new ProductImage();
                            image.IsMain = item.IsMain;
                            string extension = Path.GetExtension(item.File.FileName);//.jpg
                            image.Name = $"product-{Guid.NewGuid().ToString().ToLower()}{extension}";

                            string fullName = env.GetImagePhysicalPath(image.Name);

                            using (var fs = new FileStream(fullName, FileMode.Create, FileAccess.Write))
                            {
                                await item.File.CopyToAsync(fs, cancellationToken);
                            }
                            entity.Images.Add(image);
                        }
                    }

                    await db.Products.AddAsync(entity, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);
                    return entity;
                }
                catch(System.Exception)
                {
                    return null;
                }
            }
        }
    }
}
