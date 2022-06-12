using System.Text.Json.Serialization;

namespace psw_ftn.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Grade
    {
        VeryBad = 1,
        Bad,
        Good,
        VeryGood,
        Excellent
    }
}