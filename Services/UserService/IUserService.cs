using System.Collections.Generic;
using System.Threading.Tasks;
using psw_ftn.Dtos;
using psw_ftn.Models;

namespace psw_ftn.Services.UserService
{
    public interface IUserService
    {
         Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
         Task<ServiceResponse<GetUserDto>> getUserById(int id);
         Task<ServiceResponse<List<GetUserDto>>> addUser(AddUserDto newUser);
         Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updateUser);
    }
}