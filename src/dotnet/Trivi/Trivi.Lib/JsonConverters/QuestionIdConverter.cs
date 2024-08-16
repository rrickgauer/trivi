using System.Text.Json;
using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.JsonConverters;

public class QuestionIdConverter : JsonConverter<QuestionId>
{
    public override QuestionId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, QuestionId value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Id);
    }
}
