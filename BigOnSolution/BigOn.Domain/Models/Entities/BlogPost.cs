using BigOn.Domain.AppCode.Infracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Models.Entities
{
    public class BlogPost : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int? AuthorId { get; set; }
        public virtual ICollection<BlogPostComment> Comments { get; set; }
    }
}
