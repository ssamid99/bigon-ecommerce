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

namespace BigOn.Domain.Business.ContactPostModule
{
      public class ContactPostGetSingleQuery : IRequest<ContactPost>
    {
        public int Id { get; set; }
        public class ContactPostGetSingleQueryHandler : IRequestHandler<ContactPostGetSingleQuery, ContactPost>
        {
            private readonly BigOnDbContext db;
            
            public ContactPostGetSingleQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<ContactPost> Handle(ContactPostGetSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ContactPosts.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                return data;
            }
        }
    }
}
