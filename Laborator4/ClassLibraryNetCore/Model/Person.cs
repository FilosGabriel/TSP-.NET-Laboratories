using System.ComponentModel.DataAnnotations;

namespace Lab4.Model
{
    public class Person
    {
        // ef va crea o cheie pe baza conventiilor
        //mai multe detailii https://www.learnentityframeworkcore.com/conventions
        public int PersonId { get; set; }
        [MaxLength(50)] public string FirstName { get; set; }
        [MaxLength(50)] public string MidleName { get; set; }
        [MaxLength(50)] public string LastName { get; set; }
        public string TelephonNumber { get; set; }
    }
}