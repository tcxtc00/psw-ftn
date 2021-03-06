using System.Collections.Generic;
using System.Threading.Tasks;
using psw_ftn.Dtos;
using psw_ftn.Dtos.UserDtos;
using psw_ftn.Models;

namespace psw_ftn.Services.UserService
{
    public interface IUserService
    {
         Task<ServiceResponse<List<GetUserDto>>> GetMaliciousUsers();
         Task<ServiceResponse<List<GetUserDto>>> GetBlockedUsers();
         Task<ServiceResponse<GetUserDto>> ChangeUserStatus(StatusDto userStatus, int userId);
    }
}