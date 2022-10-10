using BigOn.Domain.AppCode.Infracture;
using System.Collections;
using System.Collections.Generic;

namespace BigOn.Domain.Models.Entities
{
    public class ProductMaterial : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<ProductCatalogItem> ProductCatalog { get; set; }
    }
}
