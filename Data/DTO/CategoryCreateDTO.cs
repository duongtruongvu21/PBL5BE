using System.ComponentModel.DataAnnotations;

namespace PBL5BE.API.Data.DTO
{
    public class CategoryCreateDTO
    {
        [Required] [StringLength(255)]
        public string CategoryName { get; set; }
        [Required] [StringLength(1000)]
        public string imgUrl { get; set; }
    }
        
}