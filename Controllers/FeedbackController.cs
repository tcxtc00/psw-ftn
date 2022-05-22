using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using psw_ftn.Dtos.FeedbackDtos;
using psw_ftn.Models;
using psw_ftn.Services.FeedbackService;

namespace psw_ftn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }

        [Authorize(Roles = "Patient")]
        [HttpPost("AddFeedback")]
        public async Task<ActionResult<ServiceResponse<GetFeedbackDto>>> AddFeedback(AddFeedbackDto newFeedback)
        {
            var response = await feedbackService.AddFeedback(newFeedback);

            if(response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetFeedbackDto>>>> GetAllFeedbacks()
        {
            var response = await feedbackService.GetAllFeedbacks();

            if(response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Show")]
        public async Task<ActionResult<ServiceResponse<GetFeedbackDto>>> ShowFeedback(int feedbackId, bool showFeedback)
        {
            var response = await feedbackService.ShowFeedback(feedbackId, showFeedback);

            if(response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}