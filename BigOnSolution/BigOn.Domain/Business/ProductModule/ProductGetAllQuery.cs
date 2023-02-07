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

namespace BigOn.Domain.Business.ProductModule
{
    public class ProductGetAllQuery : PaginateModel, IRequest<PagedViewModel<Product>>
    {
        public class ProductGetAllHandler : IRequestHandler<ProductGetAllQuery, PagedViewModel<Product>>
        {
            private readonly BigOnDbContext db;

            public ProductGetAllHandler(BigOnDbContext db)
            {
                this.db = db;
            }

            public async Task<PagedViewModel<Product>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
            {
                var query = db.Products
                    .Include(p=>p.Images)
                    .Include(p=>p.Brand)
                    .Include(p=>p.Category)
                    .Where(bp => bp.DeletedDate == null)
                    .OrderByDescending(bp => bp.Id)
                    .AsQueryable();


                var pagedModel = new PagedViewModel<Product>(query, request);

                return pagedModel;
            }
        }
    }
}
