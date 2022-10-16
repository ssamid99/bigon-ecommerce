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
      public class ProductSizeGetSingleQuery : IRequest<ProductSize>
    {
        public int Id { get; set; }
        public class ProductSizeGetSingleQueryHandler : IRequestHandler<ProductSizeGetSingleQuery, ProductSize>
        {
            private readonly BigOnDbContext db;
            
            public ProductSizeGetSingleQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<ProductSize> Handle(ProductSizeGetSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ProductSizes.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                return data;
            }
        }
    }
}
