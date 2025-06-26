using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace DreamGarden.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; } //primary key in the database
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow; //default time is now

        //annotations for the properties
        [Required]
        public int OrderStatusId { get; set; }

        public bool IsDeleted { get; set; } = false; //by default it is false

        [Required]
        [MaxLength(30)]
        public string? Name { get; set;}

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? MobileNumber { get; set; }


        [Required]
        [MaxLength(200)]
        public string? Address { get; set; }

        [Required]
        [MaxLength(30)]
        public string? PaymentMethod { get; set; }

        public bool IsPaid { get; set; }

        public OrderStatus OrderStatus { get; set; } //connection with the OrderStatus database

        public List<OrderDetail> OrderDetail { get; set; } //connection with the OrderDetail database


    }
}
