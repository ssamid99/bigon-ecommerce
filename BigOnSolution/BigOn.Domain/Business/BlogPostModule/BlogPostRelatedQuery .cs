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
    public class BlogPostRelatedQuery  : IRequest<List<BlogPost>>
    {
        public int Size { get; set; }
        public int PostId { get; set; }
        public class BlogPostRelatedQueryHandler : IRequestHandler<BlogPostRelatedQuery , List<BlogPost>>
        {
            private readonly BigOnDbContext db;

            public BlogPostRelatedQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<List<BlogPost>> Handle(BlogPostRelatedQuery  request, CancellationToken cancellationToken)
            {
                // SELECT TagId FROM[dbo].[BlogPostTagCloud] where BlogPostId = 10

                //SELECT* FROM[dbo].[BlogPosts] where Id = 10
                //SELECT distinct bp.* FROM[dbo].[BlogPostTagCloud] bptc
                //join[dbo].[BlogPosts] bp on bptc.BlogPostId = bp.Id
                //where TagId in (SELECT TagId FROM[dbo].[BlogPostTagCloud] where BlogPostId = 10)
                //and BlogPostId != 10

                var tagIds = await db.BlogPostTagCloud.Where(bptc => bptc.BlogPostId == request.PostId).Select(bptc => bptc.TagId).ToArrayAsync(cancellationToken);

                var data = await (from bp in db.BlogPosts
                                  join bptc in db.BlogPostTagCloud on bp.Id equals bptc.BlogPostId
                                  where tagIds.Contains(bptc.TagId) && bp.Id != request.PostId
                                  select bp)
                .Distinct()
                .Take(request.Size < 2 ? 2 : request.Size)
                .ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
