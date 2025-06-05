using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamGarden.Models
{
    [Table("Flower")]
    public class Flower
    {
        public int Id { get; set; } //primary key in the database

        //annotations for the properties
        [Required]
        [MaxLength(50)]
        public string? FlowerName { get; set; }

        //annotations for the properties
        [Required]
        public string LatinName { get; set; }

        //anotations for the properties
        [Required]
        public string Description { get; set; }

        //annotations for the properties
        [Required]
        public double Price { get; set; }

        //annotations for the properties
        public string? Image { get; set; }

        //annotations for the properties
        [Required]
        public int GenreId { get; set; }


        public Genre Genre { get; set; } //connection with the Genre database; one flower has only one genre

        public List<OrderDetail> OrderDetail { get; set; } //connection with the OrderDetail database

        public List<CartDetail> CartDetail { get; set; } //connection with the CartDetail database

    }
}
