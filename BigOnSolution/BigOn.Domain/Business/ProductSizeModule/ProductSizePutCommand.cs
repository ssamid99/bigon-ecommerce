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
    public class ProductSizePutCommand : IRequest<ProductSize>
    {
        public int id { get; set; }
        public string name { get; set; }

        public class ProductSizePutCommandHandler : IRequestHandler<ProductSizePutCommand, ProductSize>
        {
            private readonly BigOnDbContext db;
            
            public ProductSizePutCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<ProductSize> Handle(ProductSizePutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.ProductSizes.FirstOrDefaultAsync(m => m.Id == request.id && m.DeletedDate == null, cancellationToken);

                if (data == null)
                {
                    return null;
                }
                data.Name = request.name;
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
