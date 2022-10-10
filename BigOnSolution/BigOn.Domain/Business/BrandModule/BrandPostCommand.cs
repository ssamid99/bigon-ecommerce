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
    public class BrandPostCommand : IRequest<Brand>
    {
        public string name { get; set; }
        public class BrandPostCommandHandler : IRequestHandler<BrandPostCommand, Brand>
        {
            private readonly BigOnDbContext db;

            public BrandPostCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<Brand> Handle(BrandPostCommand request, CancellationToken cancellationToken)
            {
                var model = new Brand();
                model.Name = request.name;
                await db.Brands.AddAsync(model, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return model;            }
        }
    }
}
