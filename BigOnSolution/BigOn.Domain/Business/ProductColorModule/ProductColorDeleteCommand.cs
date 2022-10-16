using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.ProductColorModule
{
    public class ProductColorDeleteCommand : IRequest<ProductColor>
    {
        public int id { get; set; }
        public class ProductColorDeleteCommandHandler : IRequestHandler<ProductColorDeleteCommand, ProductColor>
        {
            private readonly BigOnDbContext db;

            public ProductColorDeleteCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<ProductColor> Handle(ProductColorDeleteCommand request, CancellationToken cancellationToken)
            {
                var data = db.ProductColors.FirstOrDefault(m => m.Id == request.id && m.DeletedDate == null);

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
