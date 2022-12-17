using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL5BE.API.Data.Entities
{
    public class UserInfo
    {
        [ForeignKey("User")]
        [Key]
        public int UserID { get; set; }
        [Required]
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        [MaxLength(32)]
        public string PhoneNumber { get; set; }
        [MaxLength(32)]
        public string Address { get; set; }
        [MaxLength(32)]
        public string Role { get; set; }
        public bool Sex { get; set; }
        public byte Status { get; set; }
        [MaxLength(32)]
        public string CitizenID { get; set; }
        [MaxLength(32)]
        public DateTime CreateAt { get; set; }
    }
}