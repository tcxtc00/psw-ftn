using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using psw_ftn.Dtos.CheckUpDtos;
using psw_ftn.Dtos.UserDtos;
using psw_ftn.Models;
using psw_ftn.Models.User.UserTypes;

namespace psw_ftn.Services.CheckUpService
{
    public interface ICheckUpService
    {
        Task<ServiceResponse<CheckUpDto>> BookCheckUp(BookCheckUpDto newCheckUp);
        Task<ServiceResponse<List<CheckUpDto>>> GetAvailableCheckUps(int doctorId, DateTime startIntervalTime, DateTime endIntervalTime, CheckUpPriorityDto priority);
        Task<ServiceResponse<List<UserDto>>> GetDoctorsByExpertise(DrExpertiseDto expertise);
        Task<ServiceResponse<CheckUpDto>> CancleCheckUp(int checkUpId);
        Task<ServiceResponse<List<CheckUpDto>>> GetPatientCheckUps(FilterCheckUpDto filterCheckUp);
        Task<ServiceResponse<CheckUpDto>> CheckUpFeedback(HistoryCheckUpDto checkUpFeedback);
    }
}