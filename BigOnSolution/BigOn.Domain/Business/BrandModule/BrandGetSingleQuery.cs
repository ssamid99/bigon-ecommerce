using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.BrandModule
{
    public class BrandGetSingleQuery : IRequest<Brand>
    {
        public int Id { get; set; }
        public class BrandGetSingleQueryHandler : IRequestHandler<BrandGetSingleQuery, Brand>
        {
            private readonly BigOnDbContext db;
            
            public BrandGetSingleQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<Brand> Handle(BrandGetSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Brands.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                return data;
            }
        }
    }
}
