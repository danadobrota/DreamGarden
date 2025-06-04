using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamGarden.Models
{
    [Table("Flower")]
    public class Flower
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FlowerName { get; set; }
        [Required]

        public string LatinName { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public double Price { get; set; }

        public string? Image { get; set; }
        [Required]


        public int GenreId { get; set; } 

        public Genre Genre { get; set; }; //connection with the Genre database; one flower has only one genre

        public List<OrderDetail> OrderDetail { get; set; } //connection with the OrderDetail database

        public List<CartDetail> CartDetail { get; set; } //connection with the CartDetail database
 
    }
}
