using System.ComponentModel.DataAnnotations;

namespace PBL5BE.API.Data.DTO
{
    public class UserLogin
    {
        [Required][EmailAddress]
        public string Email {get; set;}
        [Required]
        public string Password {get; set;}
    }
}