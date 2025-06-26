using System.ComponentModel.DataAnnotations;
namespace DreamGarden.Models.DTOs
{
    public class GenreDTO
    {
        public int Id { get; set; } //primary key in the database

        //annotations for the properties
        [Required]
        [MaxLength(50)]
        public string GenreName { get; set; }

    }
}
