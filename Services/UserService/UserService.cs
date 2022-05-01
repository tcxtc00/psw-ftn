using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using psw_ftn.Data;
using psw_ftn.Dtos;
using psw_ftn.Models;

namespace psw_ftn.Services.UserService
{
    public class UserService : IUserService
    {   
        private readonly IMapper mapper;

        private readonly DataContext context;

        public UserService(IMapper mapper, DataContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }
        public async Task<ServiceResponse<List<GetUserDto>>> addUser(AddUserDto newUser)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            User user = mapper.Map<User>(newUser); 
            context.Users.Add(user);
            await context.SaveChangesAsync();
            serviceResponse.Data = await context.Users.Select(u => mapper.Map<GetUserDto>(u)).ToListAsync();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var dbUsers = await context.Users.ToListAsync();
            serviceResponse.Data = dbUsers.Select(u => mapper.Map<GetUserDto>(u)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> getUserById(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var dbUser = await context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            serviceResponse.Data = mapper.Map<GetUserDto>(dbUser);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updateUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            try {
                User user = await context.Users.FirstOrDefaultAsync(u => u.UserId == updateUser.UserId);

                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;
                user.Email = updateUser.Email;
                user.PasswordHash = updateUser.PasswordHash;
                user.PasswordSalt = updateUser.PasswordSalt;
                user.City = updateUser.City;
                user.Street = updateUser.Street;
                user.Phone = updateUser.Phone;
                user.Status = updateUser.Status;

                await context.SaveChangesAsync();

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
                User user = await context.Users.FirstAsync(u => u.UserId == id);
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                serviceResponse.Data = context.Users.Select(u => mapper.Map<GetUserDto>(u)).ToList();
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