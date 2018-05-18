using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.DAL.Entities
{
    public class ReviewReceiverEntity
    {
        public Guid Id { get; set; }
        
        public string  Url { get; set; }

        public string ImagePath { get; set; }
        
        public virtual UserEntity Creator { get; set; }

        public virtual ICollection<ReviewEntity> Reviews { get; set; }
    }
}
