using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using psw_ftn.Dtos.CheckUpDtos;
using psw_ftn.Dtos.UserDtos;
using psw_ftn.Models;
using psw_ftn.Services.CheckUpService;

namespace psw_ftn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckUpController : ControllerBase
    {
        private readonly ICheckUpService checkUpService;
        public CheckUpController(ICheckUpService checkUpService)
        {
            this.checkUpService = checkUpService;
        }

        [Authorize(Roles = "Doctor,Patient")]
        [HttpPut("Book")]
        public async Task<ActionResult<ServiceResponse<CheckUpDto>>> BookCheckUp(BookCheckUpDto bookCheckUp)
        {
            var response = await checkUpService.BookCheckUp(bookCheckUp);

            if(response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = "Doctor,Patient,Admin")]
        [HttpGet("GetAllDoctors")]
        public async Task<ActionResult<ServiceResponse<List<UserDto>>>> GetAllDoctors()
        {
            return Ok(await checkUpService.GetAllDoctors());
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("GetAllPatients")]
        public async Task<ActionResult<ServiceResponse<List<UserDto>>>> GetAllPatients()
        {
            return Ok(await checkUpService.GetAllPatients());
        }

        [Authorize(Roles = "Doctor,Patient")]
        [HttpGet("GetDoctorsByExpertise")]
        public async Task<ActionResult<ServiceResponse<List<UserDto>>>> GetDoctorsByExpertise(DrExpertiseDto expertise)
        {
            return Ok(await checkUpService.GetDoctorsByExpertise(expertise));
        }

        [Authorize(Roles = "Doctor,Patient")]
        [HttpGet("GetAvailableCheckUps/{doctorId}/{startIntervalTime}/{endIntervalTime}/{priority}")]
        public async Task<ActionResult<ServiceResponse<List<CheckUpDto>>>> GetAvailableCheckUps(int doctorId, DateTime startIntervalTime, DateTime endIntervalTime, CheckUpPriorityDto priority)
        {
            var response = await checkUpService.GetAvailableCheckUps(doctorId, startIntervalTime, endIntervalTime, priority);

            return Ok(await checkUpService.GetAvailableCheckUps(doctorId, startIntervalTime, endIntervalTime, priority));
        }

        [Authorize(Roles = "Patient")]
        [HttpPut("Cancel")]
        public async Task<ActionResult<ServiceResponse<CheckUpDto>>> CancelCheckUp(int checkUpId, string comment)
        {
            var response = await checkUpService.CancelCheckUp(checkUpId, comment);

            return Ok(response);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("GetPatientCheckUps/{filterCheckUp}")]
         public async Task<ActionResult<ServiceResponse<CheckUpDto>>> GetPatientCheckUps(FilterCheckUpDto filterCheckUp){
            
            var response = await checkUpService.GetPatientCheckUps(filterCheckUp);

             if(response.Data == null)
            {
                return NotFound(response);
            }

             return Ok(response);
         }

        [Authorize(Roles = "Patient")]
        [HttpPost("AddFeedback")]
        public async Task<ActionResult<ServiceResponse<CheckUpDto>>> CheckUpFeedback(HistoryCheckUpDto checkUpFeedback){
            
            var response = await checkUpService.CheckUpFeedback(checkUpFeedback);

            if(response.Message == "Check up doesn't exist")
            {
                return NotFound(response);
            }

             return Ok(response);
        }
    }
}