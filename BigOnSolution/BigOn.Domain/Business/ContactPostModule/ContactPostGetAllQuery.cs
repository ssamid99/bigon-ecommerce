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
    public class ContactPostGetAllQuery : IRequest<List<ContactPost>>
    {
        public class ContactPostGetAllQueryHandler : IRequestHandler<ContactPostGetAllQuery, List<ContactPost>>
        {
            private readonly BigOnDbContext db;

            public ContactPostGetAllQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<List<ContactPost>> Handle(ContactPostGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ContactPosts.Where(m => m.DeletedDate == null)
                   .ToListAsync(cancellationToken);
                return data;
            }
        }
    }
}

