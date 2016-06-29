using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeZoneManager.API.Contracts
{
    public class UserInfoDTO
    {
        public string Email { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
