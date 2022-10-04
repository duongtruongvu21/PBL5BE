using System.ComponentModel.DataAnnotations;

namespace PBL5BE.API.Data.DTO
{
    public class UserRegister
    {
        [Required]
        public string Username {get; set;}
        [Required]
        public string Password {get; set;}
        [EmailAddress][Required]
        public string Email {get; set;}
    }


    public class UserLogin
    {
        [Required]
        public string Username {get; set;}
        [Required]
        public string Password {get; set;}
    }
}