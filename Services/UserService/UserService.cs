using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using psw_ftn.Models;

namespace psw_ftn.Services.UserService
{
    public class UserService : IUserService
    {
        private static List<User> users = new List<User>{
            new User { UserId = 1, FirstName = "Zoran", LastName = "Protic"},
            new User { UserId = 2, FirstName = "Bora", LastName = "Gajic"}
        };
        
       public async Task<ServiceResponse<List<User>>> addUser(User newUser)
        {
            var serviceResponse = new ServiceResponse<List<User>>();
            users.Add(newUser);
            serviceResponse.Data = users;
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<User>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<User>>();
            serviceResponse.Data = users;
            return serviceResponse;
        }

        public async Task<ServiceResponse<User>> getUserById(int id)
        {
            var serviceResponse = new ServiceResponse<User>();
            serviceResponse.Data = users.FirstOrDefault(c => c.UserId == id);
            return serviceResponse;
        }
    }
}