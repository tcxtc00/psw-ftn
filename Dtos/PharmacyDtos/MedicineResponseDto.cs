using Newtonsoft.Json;

namespace psw_ftn.Dtos.PharmacyDtos
{
    public class MedicineResponseDto
    {
        public string name { get; set; }
        public int quantity { get; set; }
        public int supplies { get; set; }
        [JsonIgnore]
        public string errorMsg { get; set; }
    }
}