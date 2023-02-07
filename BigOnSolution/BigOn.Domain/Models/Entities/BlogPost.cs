using BigOn.Domain.AppCode.Infracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Models.Entities
{
    public class BlogPost : BaseEntity, IPageable
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }
        public string Slug { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int? AuthorId { get; set; }
        public virtual ICollection<BlogPostComment> Comments { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<BlogPostTagItem> TagCloud { get; set; }

    }
}
