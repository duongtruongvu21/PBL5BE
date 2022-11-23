using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL5BE.API.Data.Entities
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        [StringLength(255)]
        public int NumberOfProducts { get; set; }
        public string Address { get; set; }
        public byte Status { get; set; }
        public DateTime CreateAt { get; set; }
        [ForeignKey("User")]
        public int CreateBy { get; set; }
    }
}