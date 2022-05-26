using System.Text.Json.Serialization;

namespace psw_ftn.Dtos.UserDtos
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusDto
    {
        Blocked,
        Active,
        Malicious,
        NotMalicious
    }
}