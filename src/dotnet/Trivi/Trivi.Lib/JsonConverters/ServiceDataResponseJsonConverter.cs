﻿using System.Text.Json;
using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Services.Implementations;

namespace Trivi.Lib.JsonConverters;

public class ServiceDataResponseJsonConverter<T> : JsonConverter<ServiceResponse<T>>
{
    public override ServiceResponse<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ServiceResponse<T> value, JsonSerializerOptions options)
    {
        ApiResponse<T> apiResponse = new()
        {
            Data = value.Data,
            Errors = ErrorMessageService.ToErrorMessages(value.Errors),
        };

        JsonSerializer.Serialize(writer, apiResponse, options);
    }
}








