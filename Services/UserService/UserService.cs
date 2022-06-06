using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using psw_ftn.Data;
using psw_ftn.Dtos;
using psw_ftn.Dtos.UserDtos;
using psw_ftn.Models;
using psw_ftn.Models.User;

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

        public async Task<ServiceResponse<List<GetUserDto>>> GetMaliciousUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            
            serviceResponse.Data =  await context.Users
            .Where(user => user.Status == Status.Malicious)
            .Select(u => mapper.Map<GetUserDto>(u)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> ChangeUserStatus(StatusDto userStatus, int userId)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            try {
                User user = await context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                user.Status = mapper.Map<Status>(userStatus);

                context.Users.Update(user);
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

        public async Task<ServiceResponse<List<GetUserDto>>> GetBlockedUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            
            var dbBlockedUsers = await context.Users.Where(u => u.Status == Status.Blocked).ToListAsync();

            serviceResponse.Data = dbBlockedUsers.Select(u => mapper.Map<GetUserDto>(u)).ToList();
            return serviceResponse;
        }
    }
}