using System.Collections.Generic;

namespace Lab4.Model
{
    public class Customer
    {
        // ef va crea o cheie pe baza conventiilor
        //mai multe detailii https://www.learnentityframeworkcore.com/conventions
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}