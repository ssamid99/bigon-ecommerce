using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.ProductColorModule
{
      public class ProductColorGetSingleQuery : IRequest<ProductColor>
    {
        public int Id { get; set; }
        public class ProductColorGetSingleQueryHandler : IRequestHandler<ProductColorGetSingleQuery, ProductColor>
        {
            private readonly BigOnDbContext db;
            
            public ProductColorGetSingleQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<ProductColor> Handle(ProductColorGetSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ProductColors.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                return data;
            }
        }
    }
}
