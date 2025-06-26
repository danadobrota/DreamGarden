namespace DreamGarden.Models.DTOs
{
    public class StockDisplayModel
    {
        public int Id { get; set; } //primary key in the database
        //annotations for the properties
        public int FlowerId { get; set; }
        //annotations for the properties
        public int Quantity { get; set; }
        //annotations for the properties
        public string? FlowerName { get; set; }
    }
}
