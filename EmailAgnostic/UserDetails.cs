using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailAgnostic
{
    public class UserDetails
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public string UserPhnNo { get; set; }
        public string UserPassword { get; set; }
        public DateTime UserCreationTime { get; set; }
    }
}