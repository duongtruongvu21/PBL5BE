using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL5BE.API.Data.Entities
{
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public int ProductCount { get; set; }
        public float PricePerOne { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
    }
}