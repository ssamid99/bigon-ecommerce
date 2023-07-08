using AutoMapper;
using BigOn.Domain.Models.Dtos.BlogPosts;
using BigOn.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Mapper.BlogPostMapper
{
    public class BlogPostCategoryResolver : IValueResolver<BlogPost, BlogPostDto, InfoHolder>
    {
        public InfoHolder Resolve(BlogPost source, BlogPostDto destination, InfoHolder destMember, ResolutionContext context)
        {
            if(source?.Category == null)
            {
                return null;
            }
            destMember = new InfoHolder();
            destMember.Id = source.Category.Id;
            destMember.Text = source.Category.Name;

            return destMember;
        }
    }
}
