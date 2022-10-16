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

namespace BigOn.Domain.Business.FaqModule
{
    public class FaqGetAllQuery : IRequest<List<Faq>>
    {
        public class FaqGetAllQueryHandler : IRequestHandler<FaqGetAllQuery, List<Faq>>
        {
            private readonly BigOnDbContext db;

            public FaqGetAllQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<List<Faq>> Handle(FaqGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Faqs.Where(m => m.DeletedDate == null)
                   .ToListAsync(cancellationToken);
                return data;
            }
        }
    }
}
