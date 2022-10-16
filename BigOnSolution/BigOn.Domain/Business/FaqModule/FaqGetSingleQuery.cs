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
      public class FaqGetSingleQuery : IRequest<Faq>
    {
        public int Id { get; set; }
        public class FaqGetSingleQueryHandler : IRequestHandler<FaqGetSingleQuery, Faq>
        {
            private readonly BigOnDbContext db;
            
            public FaqGetSingleQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<Faq> Handle(FaqGetSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Faqs.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                return data;
            }
        }
    }
}
