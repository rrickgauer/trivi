using System.Text.Json;
using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Errors;

namespace Trivi.Lib.JsonConverters;

public class QuestionTypeJsonConverter : JsonConverter<QuestionType>
{
    public override QuestionType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var prefix = reader.GetString()!;

        return QuestionTypeExtensions.ParsePrefix(prefix);
    }

    public override void Write(Utf8JsonWriter writer, QuestionType value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.GetPrefix());
    }
}