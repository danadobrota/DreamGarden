using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamGarden.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {

        public int Id { get; set; } //primary key in the database
       
        //annotations for the properties
        [Required]
         public int OrderId { get; set; }

        //annotations for the properties
        [Required]
        public int FlowerId { get; set; }

        //annotations for the properties
        [Required]
         public int Quantity { get; set; }

        //anotations for the properties
        [Required]
         public double UnitPrice { get; set; }
        public Order Order { get; set;} //linked to the Order Table

        public Flower Flower { get; set; } //link to the Flower Table
    }
}
