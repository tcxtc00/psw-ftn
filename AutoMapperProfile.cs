using AutoMapper;
using psw_ftn.Dtos;
using psw_ftn.Dtos.CheckUpDtos;
using psw_ftn.Dtos.UserDtos;
using psw_ftn.Models;
using psw_ftn.Models.User;
using psw_ftn.Models.User.UserTypes;
using psw_ftn.Services.CheckUpService;

namespace psw_ftn
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<Doctor, GetUserDto>();
            CreateMap<Doctor, DoctorDto>();
            CreateMap<Patient, PatientDto>();
            CreateMap<UserDto, User>();
            CreateMap<User,UserDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<Patient, UserDto>();
            CreateMap<Doctor, UserDto>();
            //Izbrisati, Admin se dodaje kroz bazu
            CreateMap<Admin, UserDto>();
            CreateMap<AddUserDto, User>();
            CreateMap<DrExpertiseDto, DrExpertise>();
            
            CreateMap<BookCheckUpDto, CheckUp>();
            //CreateMap<CheckUp, GetCheckUpDto>();
            CreateMap<CheckUp, CheckUpDto>();
        }
    }
}