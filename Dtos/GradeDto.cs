using System.Text.Json.Serialization;

namespace psw_ftn.Dtos
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GradeDto
    {
        VeryBad = 1,
        Bad,
        Good,
        VeryGood,
        Excellent
    }
}