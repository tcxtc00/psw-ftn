using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using psw_ftn.Dtos;
using psw_ftn.Models;
using psw_ftn.Services.UserService;

namespace psw_ftn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    { 
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

         [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> Get()
        {
            return Ok(await userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<User>>> GetSingle(int id)
        {
            return Ok(await userService.getUserById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<User>>>> AddUser(AddUserDto newUser)
        {
            return Ok(await userService.addUser(newUser));
        }

         [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateUser(UpdateUserDto updateUser)
        {
            var response = await userService.UpdateUser(updateUser);

            if(response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> Delete(int id)
        {
           var response = await userService.DeleteUser(id);

            if(response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        
    }
}