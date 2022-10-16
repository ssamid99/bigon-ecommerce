using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using MediatR;
using System.Threading;

namespace BigOn.Domain.Business.ProductColorModule
{
    public class ProductColorGetAllQuery : IRequest<List<ProductColor>>
    {
        public class ProductColorGetAllQueryHandler : IRequestHandler<ProductColorGetAllQuery, List<ProductColor>>
        {
            private readonly BigOnDbContext db;

            public ProductColorGetAllQueryHandler(BigOnDbContext db)
            {
                this.db = db;
            }



            public async Task<List<ProductColor>> Handle(ProductColorGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.ProductColors.Where(m => m.DeletedDate == null)
                                   .ToListAsync(cancellationToken);
                return data;
            }
        }
    }
}
