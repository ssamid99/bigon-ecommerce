using BigOn.Domain.AppCode.Infracture;
using System.Collections.Generic;

namespace BigOn.Domain.Models.Entities
{
    public class ProductCatalogItem : BaseEntity
    {
        public int ProductId { get; set; }    
        public virtual Product Product { get; set; }
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public int ProductColorId { get; set; }
        public virtual ProductColor ProductColors { get; set; }
        public int ProductSizeId { get; set; }
        public virtual ProductSize ProductSizes { get; set; }
        public int ProductMaterialId { get; set; }
        public virtual ProductMaterial ProductMaterials { get; set; }
    }
}
