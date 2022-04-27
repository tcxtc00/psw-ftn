using System;
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

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updateUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            try {
                User user = users.FirstOrDefault(u => u.UserId == updateUser.UserId);

                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;
                user.Email = updateUser.Email;
                user.Password = updateUser.Password;
                user.PwdSalt = updateUser.PwdSalt;
                user.City = updateUser.City;
                user.Street = updateUser.Street;
                user.Phone = updateUser.Phone;
                user.Status = updateUser.Status;

                serviceResponse.Data = mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
           var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            try {
                User user = users.First(u => u.UserId == id);
                users.Remove(user);
                serviceResponse.Data = users.Select(u => mapper.Map<GetUserDto>(u)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}