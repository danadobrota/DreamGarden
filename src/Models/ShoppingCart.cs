using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamGarden.Models
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        public int Id { get; set; } //primary key in the database

        //annotations for the properties
        [Required]
        public string UserId { get; set; }

        public bool IsDeleted { get; set; } = false; //by default it will false.

        public ICollection<CartDetail> CartDetails { get; set; }

    }
}
