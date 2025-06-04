using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamGarden.Models
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(20)]

        public string StatusName { get; set; }

    }
}
