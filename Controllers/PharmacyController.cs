using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using psw_ftn.Dtos.PharmacyDtos;
using Microsoft.AspNetCore.Authorization;
using psw_ftn.Models;
using psw_ftn.Services.PharmacyService;

namespace psw_ftn.Controllers
{
    [Authorize(Roles = "Doctor")]
    [ApiController]
    [Route("[controller]")]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService pharmacyService;
        public PharmacyController(IPharmacyService pharmacyService)
        {
            this.pharmacyService = pharmacyService;
        }
        
        [HttpPost("Recipe")]
        public ActionResult<ServiceResponse<RecipeDto>> PostRecipe(RecipeDto recipe)
        {
            var response = pharmacyService.PostRecipe(recipe);

            if(response.Success == false)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPost("GetMedicine/{name}/{quantity}")]
        public async Task<ActionResult<ServiceResponse<MedicineResponseDto>>> GetMedicine(string name, int quantity)
        {
            var response = await pharmacyService.GetMedicine(name, quantity);

            if(response.Success == false)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }
    }
}