using System.ComponentModel.DataAnnotations;

namespace PBL5BE.API.Data.DTO
{
    public class CartAddDTO
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int ProductCount { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
    }
    public class CartEditDTO
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int ProductCount { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
    }   
    public class CartOnPayment
    {
        [Required]
        public int userID { get; set; }
        [Required]
        public List<int> cartItemsID { get; set; }
        [Required] [StringLength(200)]
        public String Address { get; set; }
        [Required]
        public float ShippingFee { get; set; }
    }
    public class CartGetDTO
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantityLeft { get; set; }
        public int ProductCount { get; set; }
        public float PricePerOne { get; set; }
        public string Description { get; set; }
    }
}