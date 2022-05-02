using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace psw_ftn.Models.User.UserTypes
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DrExpertise
    {
        [EnumMember(Value = "Generalist")]
        Generalist,
        
        [EnumMember(Value = "Specialist")]
        Specialist
    }
}