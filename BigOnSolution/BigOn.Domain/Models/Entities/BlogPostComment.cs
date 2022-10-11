using BigOn.Domain.AppCode.Infracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Models.Entities
{
    public class BlogPostComment : BaseEntity
    {
        public string Text { get; set; }
        public int BlogPostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
        public int? ParentId { get; set; }
        public virtual BlogPostComment Parent { get; set; }
        public virtual ICollection<BlogPostComment> Còmments { get; set; }
        public bool Approved { get; set; }
    }
}
