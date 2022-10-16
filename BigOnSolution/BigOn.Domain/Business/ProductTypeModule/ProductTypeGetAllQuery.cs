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
    public class ProductTypeGetAllQuery : IRequest<List<ProductType>>
    {
        public class ProductTypeGetAllQueryHandler : IRequestHandler<ProductTypeGetAllQuery, List<ProductType>>
        {
            private readonly BigOnDbContext db;

            public ProductTypeGetAllQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<List<ProductType>> Handle(ProductTypeGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ProductTypes.Where(m => m.DeletedDate == null)
                   .ToListAsync(cancellationToken);
                return data;
            }
        }
    }
}
