using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.BrandModule
{
public class BrandDeleteCommand : IRequest<Brand>
    {
        public int id { get; set; }
        public class BrandDeleteCommandHandler : IRequestHandler<BrandDeleteCommand, Brand>
        {
            private readonly BigOnDbContext db;
            
            public BrandDeleteCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<Brand> Handle(BrandDeleteCommand request, CancellationToken cancellationToken)
            {
                var data = db.Brands.FirstOrDefault(m => m.Id == request.id && m.DeletedDate == null);

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

