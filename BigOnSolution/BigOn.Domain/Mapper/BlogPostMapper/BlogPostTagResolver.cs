using AutoMapper;
using AutoMapper.Execution;
using BigOn.Domain.Models.Dtos.BlogPosts;
using BigOn.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Mapper.BlogPostMapper
{
    public class BlogPostTagResolver : IValueResolver<BlogPost, BlogPostDto, InfoHolder[]>
    {
        public InfoHolder[] Resolve(BlogPost source, BlogPostDto destination, InfoHolder[] destMember, ResolutionContext context)
        {
            if(source?.TagCloud == null)
            {
                return null;
            }

            return source?.TagCloud.Select(tc => new InfoHolder
            {
                Id = tc.Tag.Id,
                Text = tc.Tag.Text
            }).ToArray();
        }
    }
}
