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
    public class ProductTypeDeleteCommand : IRequest<ProductType>
    {
        public int id { get; set; }
        public class ProductTypeDeleteCommandHandler : IRequestHandler<ProductTypeDeleteCommand, ProductType>
        {
            private readonly BigOnDbContext db;
            
            public ProductTypeDeleteCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<ProductType> Handle(ProductTypeDeleteCommand request, CancellationToken cancellationToken)
            {
                var data = db.ProductTypes.FirstOrDefault(m => m.Id == request.id && m.DeletedDate == null);

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
