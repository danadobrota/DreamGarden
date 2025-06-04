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

        public int GenreId { get; set; } //connection with the Genre database

        public Genre Genre; //one flower has only one genre
    }
}
