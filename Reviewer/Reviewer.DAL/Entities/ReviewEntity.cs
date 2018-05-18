using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.DAL.Entities
{
    public class ReviewEntity
    {
        public Guid Id { get; set; }

        public string Feedback { get; set; }
        
        public bool IsAnonymous { get; set; }

        public UserEntity Reviewer { get; set; }
        
        public ReviewReceiverEntity Receiver { get; set; }
    }
}
