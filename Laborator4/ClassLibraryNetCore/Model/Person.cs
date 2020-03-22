using System.ComponentModel.DataAnnotations;

namespace Lab4.Model
{
    public class Person
    {
        public int PersonId { get; set; }
        [MaxLength(50)] public string FirstName { get; set; }
        [MaxLength(50)] public string MidleName { get; set; }
        [MaxLength(50)] public string LastName { get; set; }
        public string TelephonNumber { get; set; }
    }
}