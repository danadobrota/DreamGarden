using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamGarden.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]

        public int ShoppingCartId {get;set;} //relationship with ShoppingCart database

        public int FlowerId { get; set; }
        [Required]

        public int Quantity { get; set; }

        public Flower Flower { get; set;}

        public ShoppingCart ShoppingCart { get; set; } //connection with the ShoppingCart database

    }
}
