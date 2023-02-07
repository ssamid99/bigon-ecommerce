using BigOn.Domain.AppCode.Infracture;
using BigOn.Domain.Models.DataContents;
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;

namespace BigOn.Domain.Models.Entities
{
    public class ProductImage : BaseEntity
    {
        public string Name { get; set; }    
        public bool IsMain { get; set; }
        public int ProductsId { get; set; }
        public virtual Product Products { get; set; }
    }
}
