using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.ProductColorModule
{
    public class ProductColorPostCommand : IRequest<ProductColor>
    {
        public string name { get; set; }
        public class ProductColorPostCommandHandler : IRequestHandler<ProductColorPostCommand, ProductColor>
        {
            private readonly BigOnDbContext db;

            public ProductColorPostCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<ProductColor> Handle(ProductColorPostCommand request, CancellationToken cancellationToken)
            {
                var model = new ProductColor();
                model.Name = request.name;
                await db.ProductColors.AddAsync(model, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return model;
            }
        }
    }
}
