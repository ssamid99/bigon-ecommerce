using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Models.FormData
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            if(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
