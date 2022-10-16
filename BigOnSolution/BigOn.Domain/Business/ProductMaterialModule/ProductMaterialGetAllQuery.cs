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
    public class ProductMaterialGetAllQuery : IRequest<List<ProductMaterial>>
    {
        public class ProductMaterialGetAllQueryHandler : IRequestHandler<ProductMaterialGetAllQuery, List<ProductMaterial>>
        {
            private readonly BigOnDbContext db;

            public ProductMaterialGetAllQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<List<ProductMaterial>> Handle(ProductMaterialGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ProductMaterials.Where(m => m.DeletedDate == null)
                   .ToListAsync(cancellationToken);
                return data;
            }
        }
    }
}
