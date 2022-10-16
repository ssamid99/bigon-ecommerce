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
    public class FaqDeleteCommand : IRequest<Faq>
    {
        public int id { get; set; }
        public class FaqDeleteCommandHandler : IRequestHandler<FaqDeleteCommand, Faq>
        {
            private readonly BigOnDbContext db;
            
            public FaqDeleteCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<Faq> Handle(FaqDeleteCommand request, CancellationToken cancellationToken)
            {
                var data = db.Faqs.FirstOrDefault(m => m.Id == request.id && m.DeletedDate == null);

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
