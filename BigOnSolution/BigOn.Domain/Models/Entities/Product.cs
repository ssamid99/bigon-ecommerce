using BigOn.Domain.AppCode.Infracture;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigOn.Domain.Models.Entities
{
    public class Product : BaseEntity, IPageable
    {
        public string Name { get; set; }
        public string StockKeepingUnit { get; set; }
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public int BrandId { get; set; }
       // [ForeignKey("BrandId")] Adina gore basha dushr ki,neye baglanmalidir. Kohne versiyalarda ashagidaki kodun uzerine yazilir
        public virtual Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }  
        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<ProductCatalogItem> ProductCatalog { get; set; }


    }
}
