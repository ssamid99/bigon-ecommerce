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
      public class ProductMaterialGetSingleQuery : IRequest<ProductMaterial>
    {
        public int Id { get; set; }
        public class ProductMaterialGetSingleQueryHandler : IRequestHandler<ProductMaterialGetSingleQuery, ProductMaterial>
        {
            private readonly BigOnDbContext db;
            
            public ProductMaterialGetSingleQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<ProductMaterial> Handle(ProductMaterialGetSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ProductMaterials.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                return data;
            }
        }
    }
}
