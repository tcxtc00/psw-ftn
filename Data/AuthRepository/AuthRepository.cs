using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using psw_ftn.Dtos;
using psw_ftn.Dtos.UserDtos;
using psw_ftn.Models;
using psw_ftn.Models.User;
using psw_ftn.Models.User.UserTypes;

namespace psw_ftn.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        public AuthRepository(DataContext context, IConfiguration configuration, IMapper mapper)
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.context = context;
        }
        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else if(user.Status == Status.Blocked)
            {
                response.Success = false;
                response.Message = "User is blocked.";
            }
            else
            {
                response.Data = CreateToken(user);
            }

            return response;
        }
        public async Task<ServiceResponse<UserDto>> Register(RegisterUserDto request)
        {
            User user = Utility.UserFromRole(request, mapper);

            ServiceResponse<UserDto> response = new ServiceResponse<UserDto>();
            if (await UserExists(user.Email))
            {
                response.Success = false;
                response.Message = "User already exists";
                return response;
            }

            Utility.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            context.Add(user);
            await context.SaveChangesAsync();

            response.Data = mapper.Map<UserDto>(user);
            response.Data.Role = Utility.RoleFromUser(user);
            return response;
        }
        public async Task<bool> UserExists(string email)
        {
            return await context.Users.AnyAsync(u => u.Email.ToLower().Equals(email.ToLower()));
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName)
            };

            if (user.GetType() == typeof(Doctor))
            {   
                Doctor doctor = (Doctor)user;
                claims.Add(new Claim(ClaimTypes.Role, "Doctor"));

                if(doctor.Expertise == DrExpertise.Generalist.ToString())
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Generalist"));
                }
            }
            else if (user.GetType() == typeof(Patient))
            {
                claims.Add(new Claim(ClaimTypes.Role, "Patient"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value)
            );

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}