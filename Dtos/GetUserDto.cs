namespace psw_ftn.Dtos
{
    public class GetUserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public int Status { get; set; }
        public string PwdSalt { get; set; }
    }
}