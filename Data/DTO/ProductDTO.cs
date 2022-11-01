using System.ComponentModel.DataAnnotations;

namespace PBL5BE.API.Data.DTO
{
    public class ProductDTO
    {
        public int ID { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [MaxLength(255)] [Required]
        public string ProductName { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        public int Count { get; set; } = 1;
        [Required]
        public float PricePerOne { get; set; }
        public byte Status { get; set; } = 1;
        public bool isReviewed { get; set; } = false;
        public string PictureURL { get; set; }
    }
}