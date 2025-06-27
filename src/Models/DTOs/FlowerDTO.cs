using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;   

namespace DreamGarden.Models.DTOs
{
    public class FlowerDTO
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

        public IFormFile? ImageFile { get; set; } //for uploading images    

        public IEnumerable<SelectListItem>? GenreList { get; set; } //for the dropdown list of genres

    }
}
