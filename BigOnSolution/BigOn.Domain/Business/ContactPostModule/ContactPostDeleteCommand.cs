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
        public int id { get; set; }
        public class ContactPostDeleteCommandHandler : IRequestHandler<ContactPostDeleteCommand, ContactPost>
        {
            private readonly BigOnDbContext db;
            
            public ContactPostDeleteCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<ContactPost> Handle(ContactPostDeleteCommand request, CancellationToken cancellationToken)
            {
                var data = db.ContactPosts.FirstOrDefault(m => m.Id == request.id && m.DeletedDate == null);

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
