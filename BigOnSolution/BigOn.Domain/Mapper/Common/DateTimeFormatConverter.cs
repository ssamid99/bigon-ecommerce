using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Mapper.Common
{
    public class DateTimeFormatConverter : IValueConverter<DateTime?, string>
    {
        public string Convert(DateTime? sourceMember, ResolutionContext context)
        {
            if(sourceMember == null)
            {
                return null;
            }

            string format = context.Items["dateFormat"]?.ToString();

            if (string.IsNullOrWhiteSpace(format))
            {
                return sourceMember.ToString();
            }

            return sourceMember?.ToString(format);
        }
    }
}
