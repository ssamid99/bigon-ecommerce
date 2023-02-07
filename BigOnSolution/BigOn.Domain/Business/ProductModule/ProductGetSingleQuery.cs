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

namespace BigOn.Domain.Business.ProductModule
{
    public class ProductGetSingleQuery : IRequest<Product>
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public class ProductGetSingleQueryHandler : IRequestHandler<ProductGetSingleQuery, Product>
        {
            private readonly BigOnDbContext db;

            public ProductGetSingleQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<Product> Handle(ProductGetSingleQuery request, CancellationToken cancellationToken)
            {
                var query = db.Products
                    .Include(p=>p.Images)
                    .Include(p=>p.Brand)
                    .Include(p=>p.Category)
                    .AsQueryable();
                if (string.IsNullOrWhiteSpace(request.Slug))
                {
                    return await query.FirstOrDefaultAsync(bp => bp.Id == request.Id && bp.DeletedDate == null, cancellationToken);

                }
                return await query.FirstOrDefaultAsync(bp => bp.Slug.Equals(request.Slug) && bp.DeletedDate == null, cancellationToken);
            }
        }
    }
}
