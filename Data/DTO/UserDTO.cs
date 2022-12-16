using System.ComponentModel.DataAnnotations;

namespace PBL5BE.API.Data.DTO
{
    public class UserLogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UserRegister
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
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

    public class UserChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}