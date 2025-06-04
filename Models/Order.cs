using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamGarden.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow; //default time is now
        [Required]
        public int OrderStatusId { get; set; }

        public bool IsDeleted { get; set; } = false; //by default it is false

        public OrderStatus OrderStatus { get; set; } //connection with the OrderStatus database

        public List<OrderDetail> OrderDetail { get; set; } //connection with the OrderDetail database


    }
}
