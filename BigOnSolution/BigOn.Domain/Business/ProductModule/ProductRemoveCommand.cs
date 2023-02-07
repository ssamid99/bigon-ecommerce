using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.ProductModule
{
    public class ProductRemoveCommand : IRequest<Product>
    {
        public int Id { get; set; }
        public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
        {
            private readonly BigOnDbContext db;

            public ProductRemoveCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
            {
                var data = db.Products.FirstOrDefault(m => m.Id == request.Id && m.DeletedDate == null);

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
