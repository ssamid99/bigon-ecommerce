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

namespace BigOn.Domain.Business.BrandModule
{
      public class BrandPutCommand : IRequest<Brand>
    {
        public int id { get; set; }
        public string name { get; set; }

        public class BrandPutCommandHandler : IRequestHandler<BrandPutCommand, Brand>
        {
            private readonly BigOnDbContext db;
            
            public BrandPutCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<Brand> Handle(BrandPutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Brands.FirstOrDefaultAsync(m => m.Id == request.id && m.DeletedDate == null, cancellationToken);

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
