using BigOn.Domain.Migrations;
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

namespace BigOn.Domain.Business.BlogPostModule
{
    public class BlogPostRemoveCommand : IRequest<BlogPost>
    {
        public int Id { get; set; }
        public class BlogPostRemoveCommandHandler : IRequestHandler<BlogPostRemoveCommand, BlogPost>
        {
            private readonly BigOnDbContext db;

            public BlogPostRemoveCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<BlogPost> Handle(BlogPostRemoveCommand request, CancellationToken cancellationToken)
            {

                var data = await db.BlogPosts.FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedDate == null, cancellationToken);

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
