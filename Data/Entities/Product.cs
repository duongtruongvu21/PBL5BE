using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL5BE.API.Data.Entities
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        [MaxLength(255)] [Required]
        public string ProductName { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public int Count { get; set; } // còn bao nhiêu sản phẩm?
        public float PricePerOne { get; set; }
        public byte Status { get; set; }
        public int NumberOfImgs { get; set; }
        public DateTime CreateAt { get; set; }
        [ForeignKey("User")]
        public int CreateBy { get; set; }
        public int SoldQuantity { get; set; }
        public string HtmlDescription { get; set; }
        public string MarkdownDescription { get; set; }
    }
}