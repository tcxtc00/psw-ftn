using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using psw_ftn.Data;
using psw_ftn.Dtos.FeedbackDtos;
using psw_ftn.Models;

namespace psw_ftn.Services.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IMapper mapper;

        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public FeedbackService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.context = context;
        }
        
        private int GetUserId() => int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<GetFeedbackDto>> AddFeedback(AddFeedbackDto newFeedback)
        {
            newFeedback.isForDisplay = false;
            var serviceResponse = new ServiceResponse<GetFeedbackDto>();
            Feedback feedback = mapper.Map<Feedback>(newFeedback);
            feedback.PatientId = GetUserId();

            try
            {
                context.Feedbacks.Add(feedback);
                await context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
                return serviceResponse;
            }
            
            serviceResponse.Data = mapper.Map<GetFeedbackDto>(await context.Feedbacks
            .Include(f => f.Patient).FirstOrDefaultAsync(f => f.FeedbackId == feedback.FeedbackId));
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFeedbackDto>>> GetAllFeedbacks()
        {
            var serviceResponse = new ServiceResponse<List<GetFeedbackDto>>();
            var dbFeedbacks = await context.Feedbacks.Include(f => f.Patient).ToListAsync();

            serviceResponse.Data = dbFeedbacks.Select(f => mapper.Map<GetFeedbackDto>(f)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFeedbackDto>> ShowFeedback(int feedbackId, bool showFeedback)
        {
            var serviceResponse = new ServiceResponse<GetFeedbackDto>();
            
            Feedback dbFeedback = await context.Feedbacks
            .Include(f => f.Patient).FirstOrDefaultAsync(f => f.FeedbackId == feedbackId);

            if(dbFeedback == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Feedback with id: " + feedbackId.ToString() + " doesn't exist.";
                return serviceResponse;
            }

            dbFeedback.IsForDisplay = showFeedback;

            try
            {
                context.Feedbacks.Update(dbFeedback);
                await context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
                return serviceResponse;
            }

            serviceResponse.Data = mapper.Map<GetFeedbackDto>(dbFeedback);
            return serviceResponse;
        }
    }
}