using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProject.Models.Entities.Membership;

namespace BigOn.Domain.Models.Entities.Chat
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public virtual BigOnUser User { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
