using psw_ftn.Dtos.UserDtos;

namespace psw_ftn.Dtos
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public StatusDto Status { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}