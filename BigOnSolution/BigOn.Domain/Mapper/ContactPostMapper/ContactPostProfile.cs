using AutoMapper;
using BigOn.Domain.AppCode.Infracture;
using BigOn.Domain.Models.Dtos.ContactPosts;
using BigOn.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Mapper.ContactPostMapper
{
    public class ContactPostProfile : Profile
    {
        public ContactPostProfile()
        {
            CreateMap<ContactPost, ContactPostDto>();
        }
    }
}
