using System.ComponentModel.DataAnnotations;

namespace PBL5BE.API.Data.Entities
{
    public class User
    {


        public int ID { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(64)]
        public string Email { get; set; }

        public byte[] PasswordHashed { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}