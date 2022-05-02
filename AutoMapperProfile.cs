using AutoMapper;
using psw_ftn.Dtos;
using psw_ftn.Dtos.UserDtos;
using psw_ftn.Models.User;
using psw_ftn.Models.User.UserTypes;

namespace psw_ftn
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Patient, UserDto>();
            CreateMap<Doctor, UserDto>();
            //Izbrisati, Admin se dodaje kroz bazu
            CreateMap<Doctor, UserDto>();
            CreateMap<AddUserDto, User>();
            CreateMap<DrExpertiseDto, DrExpertise>();
        }
    }
}