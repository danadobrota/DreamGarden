namespace DreamGarden.Models.DTOs
{
    public class FlowerDisplayModel
    {
        public IEnumerable<Flower> Flowers { get; set; } //list of flowers to be displayed
        public IEnumerable<Genre> Genres { get; set; } //list of genres to be displayed

        public string STerm { get; set; } = "";

        public int GenreId { get; set; } = 0;
    }
}
