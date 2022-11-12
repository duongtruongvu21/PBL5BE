using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PBL5BE.API.Data.DTO
{
    public class OrderDetailCreateDTO
    {
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int ProductCount { get; set; }
        [Required]
        public float Total { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
    }
    public class OrderDetailUpdateDTO
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int ProductCount { get; set; }
        [Required]
        public float Total { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
    }
}