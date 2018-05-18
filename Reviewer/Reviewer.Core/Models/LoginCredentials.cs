using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.Core.Models
{
    public class LoginCredentials
    {
        public string GrantType { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
