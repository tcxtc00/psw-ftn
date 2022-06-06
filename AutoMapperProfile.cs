using AutoMapper;
using psw_ftn.Dtos;
using psw_ftn.Dtos.CheckUpDtos;
using psw_ftn.Dtos.FeedbackDtos;
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
            CreateMap<Status, StatusDto>();
            CreateMap<StatusDto, Status>();
            CreateMap<Patient, UserDto>();
            CreateMap<Doctor, UserDto>();
            CreateMap<Admin, UserDto>();
            CreateMap<AddUserDto, User>();
            CreateMap<DrExpertiseDto, DrExpertise>();
            
            CreateMap<BookCheckUpDto, CheckUp>();
            CreateMap<CheckUp, CheckUpDto>();
            CreateMap<CheckUp, HistoryCheckUp>();
            CreateMap<HistoryCheckUp, HistoryCheckUpDto>();
            CreateMap<HistoryCheckUpDto, HistoryCheckUp>();
            CreateMap<CheckUp, HistoryCheckUpDto>();
            CreateMap<CancelledCheckUp, CheckUp>();
            CreateMap<CheckUp, CancelledCheckUp>();
            CreateMap<CancelledCheckUp, CancelledCheckUpDto>();

            CreateMap<AddFeedbackDto, Feedback>();
            CreateMap<Feedback, GetFeedbackDto>();
        }
    }
}