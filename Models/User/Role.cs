using System.Text.Json.Serialization;

namespace psw_ftn.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Patient,
        Doctor,
        Admin
    }
}