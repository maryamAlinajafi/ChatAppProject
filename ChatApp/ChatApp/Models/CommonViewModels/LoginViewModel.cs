using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model

{
    public class LoginViewModel
    {
        public System.Guid ID { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool RemindMe { get; set; }
    }
}