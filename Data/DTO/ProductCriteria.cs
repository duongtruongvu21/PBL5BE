using System.ComponentModel.DataAnnotations;

namespace PBL5BE.API.Data.DTO
{
    public class ProductCriteria
    {
        public int categoryId { get; set; } = 0;
        public string productName { get; set; } = "";
        public byte status { get; set; } = 1;
        public int recordQuantity { get; set; } = 99999;
    }
}