using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.ProductSizeModule
{
    public class ProductSizePostCommand : IRequest<ProductSize>
    {
        public string name { get; set; }
        public class ProductSizePostCommandHandler : IRequestHandler<ProductSizePostCommand, ProductSize>
        {
            private readonly BigOnDbContext db;

            public ProductSizePostCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<ProductSize> Handle(ProductSizePostCommand request, CancellationToken cancellationToken)
            {
                var model = new ProductSize();
                model.Name = request.name;
                await db.ProductSizes.AddAsync(model, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return model;            }
        }
    }
}
