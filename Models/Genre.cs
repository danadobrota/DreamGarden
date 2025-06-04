using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamGarden.Models
{
    [Table("Genre")]
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string GenreName { get; set; }

        public List<Genre> Flowers { get; set; } //one Genre can have multiple flowers

        


    }
}
