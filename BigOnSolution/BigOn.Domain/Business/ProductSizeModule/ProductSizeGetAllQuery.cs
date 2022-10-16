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
    public class ProductSizeGetAllQuery : IRequest<List<ProductSize>>
    {
        public class ProductSizeGetAllQueryHandler : IRequestHandler<ProductSizeGetAllQuery, List<ProductSize>>
        {
            private readonly BigOnDbContext db;

            public ProductSizeGetAllQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<List<ProductSize>> Handle(ProductSizeGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ProductSizes.Where(m => m.DeletedDate == null)
                   .ToListAsync(cancellationToken);
                return data;
            }
        }
    }
}
