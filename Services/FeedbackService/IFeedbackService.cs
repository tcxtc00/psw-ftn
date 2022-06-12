using System.Collections.Generic;
using System.Threading.Tasks;
using psw_ftn.Dtos;
using psw_ftn.Dtos.FeedbackDtos;
using psw_ftn.Models;

namespace psw_ftn.Services.FeedbackService
{
    public interface IFeedbackService
    {
        Task<ServiceResponse<GetFeedbackDto>> AddFeedback(AddFeedbackDto newFeedback);
        Task<ServiceResponse<List<GetFeedbackDto>>> GetAllFeedbacks();
        Task<ServiceResponse<GetFeedbackDto>> ShowFeedback(int feedbackId, bool showFeedback);
    }
}