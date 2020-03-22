using System;

namespace Lab4.Model
{
    public class Order
    {
        // ef va crea o cheie pe baza conventiilor
        //mai multe detailii https://www.learnentityframeworkcore.com/conventions
        public int OrderId { get; set; }
        public decimal Totalvalue { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}