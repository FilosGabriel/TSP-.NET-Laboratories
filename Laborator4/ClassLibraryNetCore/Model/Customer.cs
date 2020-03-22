using System.Collections.Generic;

namespace Lab4.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}