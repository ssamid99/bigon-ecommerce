using BigOn.Domain.AppCode.Infracture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.Domain.Models.Entities.Chat
{
    public class Message : BaseEntity
    {
        public int FromId { get; set; }
        public virtual BigOnUser From { get; set; }
        public int? ToId { get; set; }
        public virtual BigOnUser To { get; set; }
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }
        public string Text { get; set; }
    }
}
