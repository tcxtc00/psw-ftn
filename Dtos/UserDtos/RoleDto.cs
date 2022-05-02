using System.Text.Json.Serialization;

namespace psw_ftn.Dtos.UserDtos
{
     [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoleDto
    {
        Patient,
        Doctor,
        Admin
    }
}