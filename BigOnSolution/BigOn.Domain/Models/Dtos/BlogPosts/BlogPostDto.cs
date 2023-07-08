using BigOn.Domain.AppCode.Infracture;
using BigOn.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Models.Dtos.BlogPosts
{
    public class BlogPostDto : IPageable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public string PublishedDate { get; set; }
        public int? AuthorId { get; set; }
        public InfoHolder Category { get; set; }
        public InfoHolder[] TagCloud { get; set; }
    }

    public class InfoHolder
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
