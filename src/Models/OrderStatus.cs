﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamGarden.Models
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        public int Id { get; set; } //primary key in the database

         //annotations for the properties
        [Required]
        public int StatusId { get; set; }

        //annotations for the properties
        [Required]
        [MaxLength(20)]
        public string StatusName { get; set; }






    }
}
