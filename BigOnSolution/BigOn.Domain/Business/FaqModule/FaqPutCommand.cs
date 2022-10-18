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
    public class FaqPutCommand : IRequest<Faq>
    {
        public int id { get; set; }
        public string question { get; set; }
        public string answer { get; set; }


        public class FaqPutCommandHandler : IRequestHandler<FaqPutCommand, Faq>
        {
            private readonly BigOnDbContext db;
            
            public FaqPutCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<Faq> Handle(FaqPutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Faqs.FirstOrDefaultAsync(m => m.Id == request.id && m.DeletedDate == null, cancellationToken);

                if (data == null)
                {
                    return null;
                }
                data.Question = request.question;
                data.Answer = request.answer;
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
