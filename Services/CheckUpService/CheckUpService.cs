using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using psw_ftn.Data;
using psw_ftn.Dtos.CheckUpDtos;
using psw_ftn.Dtos.UserDtos;
using psw_ftn.Models;
using psw_ftn.Models.User.UserTypes;

namespace psw_ftn.Services.CheckUpService
{
    public class CheckUpService : ICheckUpService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CheckUpService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
            this.mapper = mapper;
        }
        
        private int GetUserId() => int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string GetUserName() => httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        ClaimsPrincipal GetUserClaims() => httpContextAccessor.HttpContext.User;

        public async Task<ServiceResponse<CheckUpDto>> BookCheckUp(BookCheckUpDto bookCheckUp)
        {
            string drExpertiseQuery = null;

            var serviceResponse = new ServiceResponse<CheckUpDto>();

            if (GetUserClaims().IsInRole("Generalist"))
            {
                drExpertiseQuery = DrExpertise.Specialist.ToString();   
            }

            if (GetUserClaims().IsInRole("Patient"))
            {
                drExpertiseQuery = DrExpertise.Generalist.ToString();
            }
           
            CheckUp checkUpUpdate = null;

            checkUpUpdate = await context.CheckUps
            .Include(c => c.Doctor)
            .Where(c => c.Doctor.Expertise == drExpertiseQuery
            && c.Patient == null)
            .FirstOrDefaultAsync(c => c.CheckUpId == bookCheckUp.ChechUpId);

            if(checkUpUpdate == null)
            {       
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Can't book checkup for given parameters.";
                    serviceResponse.Success = false;
                    return serviceResponse;
            }

            try
            {
                Patient patientUpdate = await context.Patients.FirstOrDefaultAsync(p => p.UserId == bookCheckUp.PatientId);
                
                checkUpUpdate.Patient = patientUpdate;

                context.CheckUps.Update(checkUpUpdate);
                await context.SaveChangesAsync();

                //izdavanje uputa ako doktor opste prakse zakazuje
                if(drExpertiseQuery == DrExpertise.Specialist.ToString())
                {
                    string patientName = patientUpdate.FirstName + " " + patientUpdate.LastName;
                    string specialistName = checkUpUpdate.Doctor.FirstName + " " + checkUpUpdate.Doctor.LastName;
                    string checkUpDate = checkUpUpdate.StartTime.ToString("yyyy-MM-dd HH:mm");

                    createDoctorReferral(GetUserName(), patientName, specialistName, checkUpDate); 
                }

                serviceResponse.Data = mapper.Map<CheckUpDto>(checkUpUpdate);
                
                serviceResponse.Data.Doctor.Role = Utility.RoleFromUser(checkUpUpdate.Doctor);
                serviceResponse.Data.Patient.Role = Utility.RoleFromUser(checkUpUpdate.Patient);
                return serviceResponse;
            }

            catch (System.Exception e)
            {   
                serviceResponse.Success = false;
                serviceResponse.Message = e.ToString();
                return serviceResponse; 
            }
        }

        public async Task<ServiceResponse<CheckUpDto>> CancleCheckUp(int checkUpId)
        {
            var serviceResponse = new ServiceResponse<CheckUpDto>();
            CheckUp checkUpUpdate = null;

            checkUpUpdate = await context.CheckUps
            .Include(c => c.Doctor)
            .Include(c => c.Patient)
            .Where(c => c.Patient.UserId == GetUserId() && DateTime.Now.AddDays(2) <= c.CancellationTime)
            .FirstOrDefaultAsync(c => c.CheckUpId == checkUpId);
            
            if(checkUpUpdate == null)
            {
                    serviceResponse.Message = "Can't cancle checkup for given check up id.";
                    serviceResponse.Success = false;
                    return serviceResponse;
            }
            
            checkUpUpdate.Patient = null;
            context.CheckUps.Update(checkUpUpdate);
            await context.SaveChangesAsync();

            serviceResponse.Data = mapper.Map<CheckUpDto>(checkUpUpdate);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CheckUpDto>>> GetAvailableCheckUps(int doctorId, DateTime startIntervalTime, DateTime endIntervalTime, CheckUpPriorityDto priority)
        {
            var serviceResponse = new ServiceResponse<List<CheckUpDto>>();
            
            List<CheckUpDto> dbCheckUps = null;

            string drExpertiseQuery = null;

            if (GetUserClaims().IsInRole("Generalist"))
            {
                drExpertiseQuery = DrExpertise.Specialist.ToString();
            }

            if (GetUserClaims().IsInRole("Patient"))
            {
                drExpertiseQuery = DrExpertise.Generalist.ToString();
            }

            if (priority == CheckUpPriorityDto.Doctor)
            {
                dbCheckUps = await context.CheckUps
                .Include(c => c.Doctor)
                .Where(c => c.Doctor.UserId == doctorId
                 && c.Doctor.Expertise == drExpertiseQuery   
                 && c.StartTime >= startIntervalTime.AddDays(-10)
                 && c.EndTime <= endIntervalTime.AddDays(10)
                 && c.Patient == null)
                .Select(u => mapper.Map<CheckUpDto>(u)).ToListAsync();
            }

            if (priority == CheckUpPriorityDto.CheckUpTime)
            {
                dbCheckUps = await context.CheckUps
                .Include(d => d.Doctor)
                .Where(c => c.StartTime >= startIntervalTime
                 && c.Doctor.Expertise == drExpertiseQuery
                 && c.EndTime <= endIntervalTime
                 && c.Patient == null)
                .Select(u => mapper.Map<CheckUpDto>(u)).ToListAsync();
            }

            serviceResponse.Data = dbCheckUps;
            
            if(dbCheckUps.Count() == 0)
            {
                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = "No checkups available for given parameters.";
                return serviceResponse;
            }
        
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<UserDto>>> GetDoctorsByExpertise(DrExpertiseDto expertise)
        {
            var serviceResponse = new ServiceResponse<List<UserDto>>();
            List<UserDto> dbDoctors = await context.Doctors
            .Where(d => d.Expertise.Equals(expertise.ToString()))
            .Select(u => mapper.Map<UserDto>(u)).ToListAsync();
            serviceResponse.Data = dbDoctors;

            return serviceResponse;
        }

        private void createDoctorReferral(string generalistName, string patientName, string specialistName, string checkUpDate)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string documentName = "Refferal-" + generalistName + "-" + patientName + "-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            
            string fileName = currentDirectory + @"\Referalls\" + documentName;    
            try    
            {    
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))    
                {    
                    File.Delete(fileName);    
                }    
                // Create a new file     
                using (FileStream fs = File.Create(fileName))     
                {    
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true)
                    .GetBytes("Greencare Official Document" + System.Environment.NewLine + "Refferal from " + generalistName  
                    + "(Doc. Generalist) for " + patientName + "(Patient) to go for an check up by " 
                    + specialistName + "(Doc. Specialist). Check up date: " + checkUpDate + System.Environment.NewLine + "Date of Refferal: " 
                    + DateTime.Now.ToString("yyyy-MM-dd"));    

                    fs.Write(title, 0, title.Length);  
                }    
            
                // Open the stream and read it back.    
                using (StreamReader sr = File.OpenText(fileName))    
                {    
                    string s = "";    
                    while ((s = sr.ReadLine()) != null)    
                    {    
                        Console.WriteLine(s);    
                    }    
                }    
            }    
            catch (Exception Ex)    
            {    
                Console.WriteLine(Ex.ToString());    
            }
        }

        public async Task<ServiceResponse<List<CheckUpDto>>> GetPatientCheckUps(FilterCheckUpDto filterCheckUp)
        {
            var serviceResponse = new ServiceResponse<List<CheckUpDto>>();
            List<CheckUpDto> dbCheckUps = null;

            if(filterCheckUp == FilterCheckUpDto.HistoryCheckUps)
            {
                dbCheckUps = await context.CheckUps
                .Include(c => c.Doctor)
                .Include(c => c.Patient)
                .Include(c => c.HistoryCheckUp)
                .Where(c => c.Patient.UserId == GetUserId()
                && c.EndTime < DateTime.Now).
                Select(c => mapper.Map<CheckUpDto>(c)).ToListAsync();
            }

            if(filterCheckUp == FilterCheckUpDto.FutureCheckUps)
            {
                dbCheckUps = await context.CheckUps
                .Include(c => c.Doctor)
                .Include(c => c.Patient)
                .Where(c => c.Patient.UserId == GetUserId()
                && c.StartTime >= DateTime.Now).
                Select(c => mapper.Map<CheckUpDto>(c)).ToListAsync();
            }

            if(dbCheckUps == null)
            {       
                    serviceResponse.Data = null;
                    serviceResponse.Message = "You don't have any checkups.";
                    serviceResponse.Success = false;
                    return serviceResponse;
            }
            
            serviceResponse.Data = dbCheckUps;
            return serviceResponse;
        }

        public async Task<ServiceResponse<CheckUpDto>> CheckUpFeedback(HistoryCheckUpDto checkUpFeedback)
        {
            var serviceResponse = new ServiceResponse<CheckUpDto>();
            bool doesCheckUpExist = await context.CheckUps.AnyAsync(c => c.CheckUpId == checkUpFeedback.CheckUpId);
            
            CheckUp historyCheckUpResponse = null;

            if(!doesCheckUpExist)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Check up doesn't exist";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            try
            {
                context.HistoryCheckUps.Add(mapper.Map<HistoryCheckUp>(checkUpFeedback));

                await context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = e.Message;
                serviceResponse.Success = false;
                return serviceResponse;
            }

             historyCheckUpResponse = await context.CheckUps
            .Include(c => c.Doctor)
            .Include(c => c.Patient)
            .Include(c => c.HistoryCheckUp)
            .FirstOrDefaultAsync(c => c.CheckUpId == checkUpFeedback.CheckUpId);
            
            serviceResponse.Data = mapper.Map<CheckUpDto>(historyCheckUpResponse);
            return serviceResponse;
        }
    }
}