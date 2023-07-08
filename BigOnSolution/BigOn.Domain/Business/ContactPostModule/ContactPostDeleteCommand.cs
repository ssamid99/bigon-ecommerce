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
    public class ContactPostDeleteCommand : IRequest<ContactPost>
    {
        public int Id { get; set; }
        public class ContactPostDeleteCommandHandler : IRequestHandler<ContactPostDeleteCommand, ContactPost>
        {
            private readonly BigOnDbContext db;
            
            public ContactPostDeleteCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<ContactPost> Handle(ContactPostDeleteCommand request, CancellationToken cancellationToken)
            {
                var data = await db.ContactPosts.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);

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
