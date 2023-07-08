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
    public class ContactPostPostCommand : IRequest<ContactPost>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
        public class ContactPostPostCommandHandler : IRequestHandler<ContactPostPostCommand, ContactPost>
        {
            private readonly BigOnDbContext db;

            public ContactPostPostCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<ContactPost> Handle(ContactPostPostCommand request, CancellationToken cancellationToken)
            {
                var model = new ContactPost();
                model.Name = request.Name;
                model.Email = request.Email;
                model.Subject = request.Subject;
                model.Message = request.Message;

                await db.ContactPosts.AddAsync(model, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                
                return model;
            }
        }
    }
}
