using AutoMapper;
using psw_ftn.Dtos;
using psw_ftn.Models;

namespace psw_ftn
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<AddUserDto, User>();
        }
    }
}