using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL5BE.API.Data.Entities
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        [Required]
        public int ProductCount { get; set; }
        [Required]
        public float PricePerOne { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
    }
}