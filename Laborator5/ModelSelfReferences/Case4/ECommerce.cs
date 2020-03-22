using System.ComponentModel.DataAnnotations.Schema;

namespace ModelSelfReferences.Case4
{
    [Table("eCommerce", Schema = "BazaDeDate")]
    public class ECommerce : Business
    {
        public string URL { get; set; }
    }
}