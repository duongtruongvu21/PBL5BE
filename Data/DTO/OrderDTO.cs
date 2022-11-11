using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL5BE.API.Data.DTO
{
    public class OrderPaidDTO
    {
        [Required]
        public int ID { get; set; }
        [Required] [StringLength(255)]
        public string Address { get; set; }
    }
    public class OrderStatusUpdateDTO
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public byte Status { get; set; }
    }
}