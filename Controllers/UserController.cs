using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using psw_ftn.Dtos;
using psw_ftn.Dtos.UserDtos;
using psw_ftn.Models;
using psw_ftn.Models.User;
using psw_ftn.Services.UserService;

namespace psw_ftn.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    { 
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("GetMallicious")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetMallicious()
        {
            return Ok(await userService.GetMaliciousUsers());
        }

        [HttpGet("GetBlocked")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetBlocked()
        {
            return Ok(await userService.GetBlockedUsers());
        }        

        [HttpPut("ChangeStatus")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> ChangeUserStatus(StatusDto userStatus, int userId)
        {
            var response = await userService.ChangeUserStatus(userStatus, userId);

            if(response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}