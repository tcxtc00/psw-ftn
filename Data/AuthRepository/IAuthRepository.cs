using System.Threading.Tasks;
using psw_ftn.Models.User;
using psw_ftn.Models;
using psw_ftn.Dtos;
using psw_ftn.Dtos.UserDtos;

namespace psw_ftn.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<UserDto>> Register(RegisterUserDto request);
        Task<ServiceResponse<UserDto>> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}