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

namespace BigOn.Domain.Business.ProductMaterialModule
{
    public class ProductMaterialPostCommand : IRequest<ProductMaterial>
    {
        public string name { get; set; }
        public class ProductMaterialPostCommandHandler : IRequestHandler<ProductMaterialPostCommand, ProductMaterial>
        {
            private readonly BigOnDbContext db;

            public ProductMaterialPostCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<ProductMaterial> Handle(ProductMaterialPostCommand request, CancellationToken cancellationToken)
            {
                var model = new ProductMaterial();
                model.Name = request.name;
                await db.ProductMaterials.AddAsync(model, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return model;            }
        }
    }
}
