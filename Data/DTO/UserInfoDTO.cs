using System.ComponentModel.DataAnnotations;

namespace PBL5BE.API.Data.DTO
{
    public class UserInfoDTO
    {
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        public IFormFile Avatar { get; set; }
        [MaxLength(32)]
        public string PhoneNumber { get; set; }
        [MaxLength(32)]
        public string Address { get; set; }
        public bool Sex { get; set; }
        [MaxLength(32)]
        public string CitizenID { get; set; }
    }

    public class UserInfoEditDTO
    {
        [Required]
        public int UserID { get; set; }
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        public string Role { get; set; }
        public IFormFile Avatar { get; set; }
        [MaxLength(32)]
        public string PhoneNumber { get; set; }
        [MaxLength(32)]
        public string Address { get; set; }
        public bool Sex { get; set; }
        [MaxLength(32)]
        public string CitizenID { get; set; }
    }
}