using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using psw_ftn.Dtos;
using psw_ftn.Models;

namespace psw_ftn.Services.UserService
{
    public class UserService : IUserService
    {
        private static List<User> users = new List<User>{
            new User { UserId = 1, FirstName = "Zoran", LastName = "Protic"},
            new User { UserId = 2, FirstName = "Bora", LastName = "Gajic"}
        };
        
        private readonly IMapper mapper;

        public UserService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetUserDto>>> addUser(AddUserDto newUser)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            User user = mapper.Map<User>(newUser);
            user.UserId = users.Max(u => u.UserId) + 1;
            users.Add(user);
            serviceResponse.Data = users.Select(u => mapper.Map<GetUserDto>(u)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            serviceResponse.Data = users.Select(u => mapper.Map<GetUserDto>(u)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> getUserById(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            serviceResponse.Data = mapper.Map<GetUserDto>(users.FirstOrDefault(u => u.UserId == id));
            return serviceResponse;
        }
    }
}