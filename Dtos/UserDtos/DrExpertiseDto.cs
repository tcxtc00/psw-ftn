using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace psw_ftn.Dtos.UserDtos
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DrExpertiseDto
    {
        [EnumMember(Value = "Generalist")]
        Generalist,
        
        [EnumMember(Value = "Specialist")]
        Specialist
    }
}