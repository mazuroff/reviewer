using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.Core.Models
{
    public class RegisteredUserModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public TokenModel Token { get; set; }
    }
}
