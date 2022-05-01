using System.ComponentModel.DataAnnotations;

namespace psw_ftn.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        
        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Street { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }
    }
}