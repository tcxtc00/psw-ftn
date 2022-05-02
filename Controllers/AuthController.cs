using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using psw_ftn.Models;
using psw_ftn.Models.User;
using psw_ftn.Data;
using psw_ftn.Dtos;
using psw_ftn.Models.User.UserTypes;
using psw_ftn.Dtos.UserDtos;
using AutoMapper;

namespace psw_ftn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            this.authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(RegisterUserDto request)
        {
            var response = await authRepo.Register(request);

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response); 
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginUserDto request)
        {
            var response = await authRepo.Login(request.Email, request.Password);   

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response); 
        }
    }
}