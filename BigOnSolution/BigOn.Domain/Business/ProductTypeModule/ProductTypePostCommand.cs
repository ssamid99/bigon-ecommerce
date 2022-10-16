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

namespace BigOn.Domain.Business.ProductTypeModule
{
    public class ProductTypePostCommand : IRequest<ProductType>
    {
        public string name { get; set; }
        public class ProductTypePostCommandHandler : IRequestHandler<ProductTypePostCommand, ProductType>
        {
            private readonly BigOnDbContext db;

            public ProductTypePostCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<ProductType> Handle(ProductTypePostCommand request, CancellationToken cancellationToken)
            {
                var model = new ProductType();
                model.Name = request.name;
                await db.ProductTypes.AddAsync(model, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return model;            }
        }
    }
}
