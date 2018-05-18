using Reviewer.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.DAL.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Name {get;set;}
        
        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public UserType Type { get; set; }

        public string ImagePath { get; set; }

        public ICollection<ReviewReceiverEntity> Receivers { get; set; }
    }
}
