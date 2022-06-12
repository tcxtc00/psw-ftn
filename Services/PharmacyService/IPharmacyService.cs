using System.Threading.Tasks;
using psw_ftn.Dtos.PharmacyDtos;
using psw_ftn.Models;

namespace psw_ftn.Services.PharmacyService
{
    public interface IPharmacyService
    {
        ServiceResponse<RecipeDto> PostRecipe(RecipeDto recipe);
        Task<ServiceResponse<MedicineResponseDto>> GetMedicine (string name, int quantity);
    }
}