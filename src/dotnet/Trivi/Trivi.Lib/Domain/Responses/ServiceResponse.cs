using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Errors;

namespace Trivi.Lib.Domain.Responses;


public class ServiceResponse
{
    public List<ErrorCode> Errors { get; set; } = new();

    [JsonIgnore]
    public bool Successful => Errors.Count == 0;

    public ServiceResponse() { }

    public ServiceResponse(ErrorCode errorCode)
    {
        Errors.Add(errorCode);
    }

    public ServiceResponse(IEnumerable<ErrorCode> errors)
    {
        Errors = errors.ToList();
    }

    public ServiceResponse(ServiceResponse other)
    {
        Errors = other.Errors;
    }


    public ServiceResponse(RepositoryException ex)
    {
        AddError(ex.ErrorCode);
    }

    public void AddError(ErrorCode errorCode)
    {
        Errors.Add(errorCode);
    }


    public void AddError(IEnumerable<ErrorCode> errors)
    {
        Errors.AddRange(errors);
    }


    public bool Exists(ErrorCode errorCode)
    {
        int matchingRecords = Errors.Where(e => e == errorCode).Count();

        return matchingRecords > 0;
    }

    public void ThrowIfError()
    {
        if (!Successful)
        {
            throw new ServiceException(this);
        }
    }





    public static implicit operator ServiceResponse(RepositoryException ex)
    {
        return new ServiceResponse(ex);
    }

    public static implicit operator ServiceResponse(ServiceException ex)
    {
        return new ServiceResponse(ex.Response);
    }

}
