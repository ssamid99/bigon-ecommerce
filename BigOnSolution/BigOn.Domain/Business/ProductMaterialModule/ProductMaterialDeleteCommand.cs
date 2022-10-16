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
    public class ProductMaterialDeleteCommand : IRequest<ProductMaterial>
    {
        public int id { get; set; }
        public class ProductMaterialDeleteCommandHandler : IRequestHandler<ProductMaterialDeleteCommand, ProductMaterial>
        {
            private readonly BigOnDbContext db;
            
            public ProductMaterialDeleteCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<ProductMaterial> Handle(ProductMaterialDeleteCommand request, CancellationToken cancellationToken)
            {
                var data = db.ProductMaterials.FirstOrDefault(m => m.Id == request.id && m.DeletedDate == null);

                if (data == null)
                {
                    return null;
                }
                data.DeletedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
