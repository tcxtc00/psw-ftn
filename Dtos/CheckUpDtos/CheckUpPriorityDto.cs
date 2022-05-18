using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace psw_ftn.Dtos.CheckUpDtos
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CheckUpPriorityDto
    {
        [EnumMember(Value = "Doctor")]
        Doctor,
        
        [EnumMember(Value = "CheckUpTime")]
        CheckUpTime
    }
}