
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Errors;

namespace Trivi.Lib.Domain.Responses;


public class ServiceResponse<T> : ServiceResponse
{
    public T? Data { get; set; }

    [JsonIgnore]
    public bool HasData => Data != null;


    public ServiceResponse() : base() { }
    public ServiceResponse(IEnumerable<ErrorCode> errors) : base(errors) { }
    public ServiceResponse(ErrorCode errorCode) : base(errorCode) { }
    public ServiceResponse(ServiceResponse other) : base(other) { }
    public ServiceResponse(RepositoryException ex) : base(ex) { }


    public ServiceResponse(T? data) : base()
    {
        Data = data;
    }


    public ServiceResponse(ServiceResponse<T> other) : base(other)
    {
        Data = other.Data;
    }

    /// <summary>
    /// Throws ServiceException if there are errors in response.
    /// Also throws NotFoundHttpResponseException if data is null.
    /// If none of that, it returns the data.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotFoundHttpResponseException"></exception>
    public T GetData()
    {
        ThrowIfError();

        if (Data == null)
        {
            throw new NotFoundHttpResponseException();
        }

        return Data;
    }


    public new ActionResult<ServiceResponse<T>> ToAction()
    {
        if (!Successful)
        {
            return new BadRequestObjectResult(this);
        }

        return new OkObjectResult(this);
    }

    public ActionResult<ServiceResponse<T>> ToActionCreated(string? uri)
    {
        if (!Successful)
        {
            return new BadRequestObjectResult(this);
        }

        return new CreatedResult(uri, this);
    }




    public static implicit operator ServiceResponse<T>(RepositoryException ex)
    {
        return new(ex);
    }

    public static implicit operator ServiceResponse<T>(ServiceException ex)
    {
        return new ServiceResponse<T>(ex.Response.Errors);
    }

    public static implicit operator ServiceResponse<T>(T other)
    {
        return new(other);
    }

}
