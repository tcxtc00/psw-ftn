using System.Text.Json.Serialization;
using psw_ftn.Dtos.UserDtos;

namespace psw_ftn.Dtos
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public RoleDto Role { get; set; }
        [JsonIgnore]
        public StatusDto Status { get; set; }
        public DrExpertiseDto Expertise { get; set; }
    }
}