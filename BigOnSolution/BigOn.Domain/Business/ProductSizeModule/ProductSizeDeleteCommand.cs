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
    public class ProductSizeDeleteCommand : IRequest<ProductSize>
    {
        public int id { get; set; }
        public class ProductSizeDeleteCommandHandler : IRequestHandler<ProductSizeDeleteCommand, ProductSize>
        {
            private readonly BigOnDbContext db;
            
            public ProductSizeDeleteCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<ProductSize> Handle(ProductSizeDeleteCommand request, CancellationToken cancellationToken)
            {
                var data = db.ProductSizes.FirstOrDefault(m => m.Id == request.id && m.DeletedDate == null);

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
