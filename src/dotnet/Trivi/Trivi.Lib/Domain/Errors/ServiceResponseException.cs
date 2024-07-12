

using Trivi.Lib.Domain.Responses;

namespace Trivi.Lib.Domain.Errors;

public class ServiceResponseException(ServiceResponse serviceResponse) : Exception()
{
    public ServiceResponseException(ErrorCode errorCode) : this(new ServiceResponse(errorCode)) { }

    public ServiceResponse Response { get; } = serviceResponse;
    public List<ErrorCode> Errors => Response.Errors;


    
}


