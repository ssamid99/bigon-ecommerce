using BigOn.Domain.Migrations;
using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.BlogPostModule
{
    public class BlogPostGetSingleQuery : IRequest<BlogPost>
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public class BlogPostGetSingleQueryHandler : IRequestHandler<BlogPostGetSingleQuery, BlogPost>
        {
            private readonly BigOnDbContext db;
            
            public BlogPostGetSingleQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<BlogPost> Handle(BlogPostGetSingleQuery request, CancellationToken cancellationToken)
            {
                var query = db.BlogPosts
                    .Include(bp => bp.Category)
                    .Include(bp => bp.Comments)

                    .Include(bp => bp.TagCloud)
                    .ThenInclude(bp => bp.Tag)
                    .AsQueryable();
                if (string.IsNullOrWhiteSpace(request.Slug))
                {
                    return await query.FirstOrDefaultAsync(bp => bp.Id == request.Id && bp.DeletedDate == null, cancellationToken);

                }
                return await query.FirstOrDefaultAsync(bp => bp.Slug.Equals(request.Slug) && bp.DeletedDate == null, cancellationToken);
            }
        }
    }
}
