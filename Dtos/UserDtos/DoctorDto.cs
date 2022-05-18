using System.Collections.Generic;
using psw_ftn.Dtos.CheckUpDtos;

namespace psw_ftn.Dtos.UserDtos
{
    public class DoctorDto : UserDto
    {
        public string Expertise { get; set; }
       // public List<CheckUpDto> CheckUps { get; set; }
    }
}