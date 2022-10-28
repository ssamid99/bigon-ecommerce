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
    public class BlogPostRecentQuery : IRequest<List<BlogPost>>
    {
        public int Size { get; set; }
        public class BlogPostRecentQueryHandler : IRequestHandler<BlogPostRecentQuery, List<BlogPost>>
        {
            private readonly BigOnDbContext db;

            public BlogPostRecentQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<List<BlogPost>> Handle(BlogPostRecentQuery request, CancellationToken cancellationToken)
            {
                var data = await db.BlogPosts.Where(bp => bp.DeletedDate == null && bp.PublishedDate !=null)
                    .OrderByDescending(bp=>bp.PublishedDate)
                    .Take(request.Size < 2?2:request.Size)
                   .ToListAsync(cancellationToken);
                return data;
            }
        }
    }
}
