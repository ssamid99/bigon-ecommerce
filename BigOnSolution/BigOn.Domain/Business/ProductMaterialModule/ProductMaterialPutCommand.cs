﻿using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace BigOn.Domain.Business.ProductMaterialModule
{
    public class ProductMaterialPutCommand : IRequest<ProductMaterial>
    {
        public int id { get; set; }
        public string name { get; set; }

        public class ProductMaterialPutCommandHandler : IRequestHandler<ProductMaterialPutCommand, ProductMaterial>
        {
            private readonly BigOnDbContext db;
            
            public ProductMaterialPutCommandHandler(BigOnDbContext db)
            {
                this.db = db;
            }
            public async Task<ProductMaterial> Handle(ProductMaterialPutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.ProductMaterials.FirstOrDefaultAsync(m => m.Id == request.id && m.DeletedDate == null, cancellationToken);

                if (data == null)
                {
                    return null;
                }
                data.Name = request.name;
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}