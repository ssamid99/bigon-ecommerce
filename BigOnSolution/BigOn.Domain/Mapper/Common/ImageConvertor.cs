using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.AppCode.Infracture
{
    public class ImageConvertor : IValueConverter<string, string>
    {
        public string Convert(string sourceMember, ResolutionContext context)
        {
            if(string.IsNullOrWhiteSpace(sourceMember))
            {
                return null;
            }

            

            return $"{context.Items["uploadsFolder"]}{sourceMember}";
        }
    }
}
