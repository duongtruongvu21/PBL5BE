using System.ComponentModel.DataAnnotations;

namespace PBL5BE.API.Data.DTO
{
    public class UserInfoDTO
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public string Email { get; set; }
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }
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
        public IFormFile Avatar { get; set; }
        [MaxLength(32)]
        public string PhoneNumber { get; set; }
        [MaxLength(32)]
        public string Address { get; set; }
        public bool Sex { get; set; }
        [MaxLength(32)]
        public string CitizenID { get; set; }
    }

    public class ChangeRoleDTO
    {
        [Required]
        public int UserID { get; set; }
        [MaxLength(5)]
        public string Role { get; set; }
    }

    public class ChangeSTTDTO
    {
        [Required]
        public int UserID { get; set; }
        public byte Status { get; set; }
    }
}