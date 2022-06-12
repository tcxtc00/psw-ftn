using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace psw_ftn.Dtos.CheckUpDtos
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FilterCheckUpDto
    {
        [EnumMember(Value = "HistoryCheckUps")]
        HistoryCheckUps,
        [EnumMember(Value = "FutureCheckUps")]
        FutureCheckUps
    }
}