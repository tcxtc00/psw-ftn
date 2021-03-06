namespace psw_ftn.Dtos
{
    public class AddUserDto
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public int Status { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}