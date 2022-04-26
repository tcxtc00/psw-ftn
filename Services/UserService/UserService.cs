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
        
        public async Task<List<User>> addUser(User newUser)
        {
            users.Add(newUser);
            return users;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return users;
        }

        public async Task<User> getUserById(int id)
        {
            return users.FirstOrDefault(c => c.UserId == id);
        }
    }
}