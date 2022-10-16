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

namespace BigOn.Domain.Business.ProductColorModule
{
    public class ProductColorPutCommand : IRequest<ProductColor>
    {
        public int id { get; set; }
        public string name { get; set; }

        public class ProductColorPutCommandHandler : IRequestHandler<ProductColorPutCommand, ProductColor>
        {
            private readonly BigOnDbContext db;
            
            public ProductColorPutCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<ProductColor> Handle(ProductColorPutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.ProductColors.FirstOrDefaultAsync(m => m.Id == request.id && m.DeletedDate == null, cancellationToken);

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
