using System.ComponentModel.DataAnnotations;

namespace PBL5BE.API.Data.DTO
{
    public class UserInfoDTO
    {
        public int UserID { get; set; }
        [Required]
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        public string PictureURL { get; set; }
        [MaxLength(32)]
        public string PhoneNumber { get; set; }
        [MaxLength(32)]
        public string Address { get; set; }
        public bool Sex { get; set; }
        public byte Status { get; set; }
        [MaxLength(32)]
        public string CitizenID { get; set; }
    }
}