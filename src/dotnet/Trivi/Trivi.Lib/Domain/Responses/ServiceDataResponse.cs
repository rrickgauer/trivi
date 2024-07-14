﻿
using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Errors;

namespace Trivi.Lib.Domain.Responses;


public class ServiceDataResponse<T> : ServiceResponse
{
    public T? Data { get; set; }

    [JsonIgnore]
    public bool HasData => Data != null;


    public ServiceDataResponse() : base() { }
    public ServiceDataResponse(IEnumerable<ErrorCode> errors) : base(errors) { }
    public ServiceDataResponse(ErrorCode errorCode) : base(errorCode) { }
    public ServiceDataResponse(ServiceResponse other) : base(other) { }
    public ServiceDataResponse(RepositoryException ex) : base(ex) { }


    public ServiceDataResponse(T? data) : base()
    {
        Data = data;
    }


    public ServiceDataResponse(ServiceDataResponse<T> other) : base(other)
    {
        Data = other.Data;
    }

    public static implicit operator ServiceDataResponse<T>(RepositoryException ex)
    {
        return new(ex);
    }

    public static implicit operator ServiceDataResponse<T>(ServiceResponseException ex)
    {
        return new ServiceDataResponse<T>(ex.Response.Errors);
    }

    public static implicit operator ServiceDataResponse<T>(T other)
    {
        return new(other);
    }

}
