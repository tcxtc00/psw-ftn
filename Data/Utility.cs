using psw_ftn.Dtos;
using psw_ftn.Dtos.UserDtos;
using psw_ftn.Models.User;
using psw_ftn.Models.User.UserTypes;

namespace psw_ftn.Data
{
    public static class Utility
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public static User UserFromRole(RegisterUserDto request)
        {
            User user = null;

            if (request.Role == RoleDto.Doctor)
            {
                user = new Doctor
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    City = request.City,
                    Street = request.Street,
                    Phone = request.Phone,
                    Status = request.Status,
                    Expertise = request.Expertise.ToString()
                };
            }

            if (request.Role == RoleDto.Patient)
            {
                user = new Patient
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    City = request.City,
                    Street = request.Street,
                    Phone = request.Phone,
                    Status = request.Status
                };
            }
            //Ukloniti Admina, jer se on zadaje kroz bazu
            if (request.Role == RoleDto.Admin)
            {
                user = new Admin
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    City = request.City,
                    Street = request.Street,
                    Phone = request.Phone,
                    Status = request.Status
                };
            }
            return user;
        }
    }
}