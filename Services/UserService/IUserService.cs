using System.Collections.Generic;
using System.Threading.Tasks;
using psw_ftn.Models;

namespace psw_ftn.Services.UserService
{
    public interface IUserService
    {
         Task<List<User>> GetAllUsers();
         Task<User> getUserById(int id);
         Task<List<User>> addUser(User newUser);
    }
}