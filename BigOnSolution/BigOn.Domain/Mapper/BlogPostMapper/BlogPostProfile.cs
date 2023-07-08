using AutoMapper;
using BigOn.Domain.AppCode.Infracture;
using BigOn.Domain.Mapper.Common;
using BigOn.Domain.Models.Dtos.BlogPosts;
using BigOn.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Mapper.BlogPostMapper
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPost, BlogPostDto>()
                .ForMember(dest => dest.Image, src => src.ConvertUsing(new ImageConvertor(), m=>m.ImagePath))
                .ForMember(dest=>dest.PublishedDate, src=>src.ConvertUsing(new DateTimeFormatConverter(), m=>m.PublishedDate))
                .ForMember(dest=>dest.Category, src=>src.MapFrom(new BlogPostCategoryResolver()))
                .ForMember(dest=>dest.TagCloud, src=>src.MapFrom(new BlogPostTagResolver()));
            CreateMap<PagedViewModel<BlogPost>, PagedViewModel<BlogPostDto>>();
        }
    }
}
