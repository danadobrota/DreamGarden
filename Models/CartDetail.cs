using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamGarden.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; } //primary key in the database

        //annotations for the properties
        [Required]
        public int ShoppingCartId {get;set;} //relationship with ShoppingCart database

        public int FlowerId { get; set; }

        //annotations for the properties
        [Required]
        public int Quantity { get; set; }

        //annotations for the properties
        [Required]
        public double UnitPrice { get; set; } //price of the flower at the time of adding to cart   

        public Flower Flower { get; set;}

        public ShoppingCart ShoppingCart { get; set; } //connection with the ShoppingCart database

    }
}
