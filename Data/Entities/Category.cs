using System.ComponentModel.DataAnnotations;

namespace PBL5BE.API.Data.Entities
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Required] [StringLength(255)]
        public string CategoryName { get; set; }
        [StringLength(1000)]
        public string imgUrl { get; set; }
        public byte Status { get; set; }
    }
}