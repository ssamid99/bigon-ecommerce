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
    public class FaqPostCommand : IRequest<Faq>
    {
        public string name { get; set; }
        public class FaqPostCommandHandler : IRequestHandler<FaqPostCommand, Faq>
        {
            private readonly BigOnDbContext db;

            public FaqPostCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<Faq> Handle(FaqPostCommand request, CancellationToken cancellationToken)
            {
                var model = new Faq();
                model.Name = request.name;
                await db.Faqs.AddAsync(model, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return model;            }
        }
    }
}
