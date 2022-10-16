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
      public class ProductTypeGetSingleQuery : IRequest<ProductType>
    {
        public int Id { get; set; }
        public class ProductTypeGetSingleQueryHandler : IRequestHandler<ProductTypeGetSingleQuery, ProductType>
        {
            private readonly BigOnDbContext db;
            
            public ProductTypeGetSingleQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<ProductType> Handle(ProductTypeGetSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ProductTypes.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                return data;
            }
        }
    }
}
