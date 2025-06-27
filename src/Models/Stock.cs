using System.ComponentModel.DataAnnotations.Schema;

namespace DreamGarden.Models
{
    [Table("Stock")]
    public class Stock
    {
        public int Id { get; set; }
        public int FlowerId { get; set; }

        public int Quantity { get; set; }

        //foreign key
        public Flower? Flower { get; set; }

    }
}
