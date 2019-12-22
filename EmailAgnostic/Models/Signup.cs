using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailAgnostic.Models
{
    public class Signup
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
    
}