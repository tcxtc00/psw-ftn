using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<ServiceResponse<List<User>>>> AddUser(User newUser)
        {
            return Ok(await userService.addUser(newUser));
        }
    }
}