using System.ComponentModel.DataAnnotations;

namespace PBL5BE.API.Data.DTO
{
    public class ProductUpdateDTO
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [MaxLength(255)] [Required]
        public string ProductName { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public string HtmlDescription { get; set; }
        public string MarkdownDescription { get; set; }
        [Required]
        public int Count { get; set; } = 1;
        [Required]
        public float PricePerOne { get; set; }
        [Required]
        public byte Status { get; set; } = 1;
        public List<IFormFile> Imgs { get; set; }
    }
    public class ProductCreateDTO
    {
        [Required]
        public int CategoryID { get; set; }
        [MaxLength(255)] [Required]
        public string ProductName { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public string HtmlDescription { get; set; }
        public string MarkdownDescription { get; set; }
        [Required]
        public int Count { get; set; } = 1;
        [Required]
        public float PricePerOne { get; set; }
        public List<IFormFile> Imgs { get; set; }
    }
}