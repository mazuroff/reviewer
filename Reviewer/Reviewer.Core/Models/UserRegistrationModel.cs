using Reviewer.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.Core.Models
{
    public class UserRegistrationModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public UserType UserType { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }
    }
}
