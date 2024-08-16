using System.Text.Json.Serialization;

namespace Trivi.Lib.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GameQuestionStatus : ushort
{
    Pending = 1,
    Active = 2,
    Closed = 3,
}
