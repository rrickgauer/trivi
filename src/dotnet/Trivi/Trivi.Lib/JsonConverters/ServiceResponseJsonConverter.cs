
using Deadit.Lib.Service.Implementations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Responses;

namespace Trivi.Lib.JsonConverters;


public class ServiceResponseJsonConverter : JsonConverter<ServiceResponse>
{
    public override ServiceResponse? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ServiceResponse value, JsonSerializerOptions options)
    {
        ApiResponse<object> apiResponse = new()
        {
            Data = null,
            Errors = ErrorMessageService.ToErrorMessages(value.Errors),
        };

        JsonSerializer.Serialize(writer, apiResponse, options);
    }


}


