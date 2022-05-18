using System.Text.Json.Serialization;

namespace psw_ftn.Dtos.UserDtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public int Status { get; set; }
        
        [JsonIgnore]
        public RoleDto Role { get; set; }
        
        [JsonIgnore]
        public string Expertise { get; set; }
    }
}