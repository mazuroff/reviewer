using Reviewer.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.Core.Models
{
    public class TokenModel
    {
        public string Token { get; set; }

        public DateTimeOffset IssueAt { get; set; }

        public DateTimeOffset ExpiresAt { get; set; }

        public Guid Id { get; set; }

        public UserType UserType { get; set; }
    }
}
