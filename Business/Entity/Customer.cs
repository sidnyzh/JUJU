using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
