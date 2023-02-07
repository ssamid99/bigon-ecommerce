using BigOn.Domain.AppCode.Infracture;
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
    public class BlogPostGetAllQuery : PaginateModel, IRequest<PagedViewModel<BlogPost>>
    {
        public class BlogPostGetAllHandler : IRequestHandler<BlogPostGetAllQuery, PagedViewModel<BlogPost>>
        {
            private readonly BigOnDbContext db;

            public BlogPostGetAllHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<PagedViewModel<BlogPost>> Handle(BlogPostGetAllQuery request, CancellationToken cancellationToken)
            {
                if(request.PageSize <= 6)
                {
                    request.PageSize = 6;
                }


                int skipSize = (request.PageIndex - 1) * request.PageSize;
                var query = db.BlogPosts.Where(bp => bp.DeletedDate == null && bp.PublishedDate != null)
                                             .OrderByDescending(bp => bp.PublishedDate)
                                             .AsQueryable();
                                             

                var pagedModel = new PagedViewModel<BlogPost>(query, request);

                return pagedModel;
            }
        }
    }
}
