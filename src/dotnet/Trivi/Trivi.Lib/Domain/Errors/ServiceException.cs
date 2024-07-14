

using Trivi.Lib.Domain.Responses;

namespace Trivi.Lib.Domain.Errors;

public class ServiceException(ServiceResponse serviceResponse) : Exception()
{
    public ServiceException(ErrorCode errorCode) : this(new ServiceResponse(errorCode)) { }

    public ServiceResponse Response { get; } = serviceResponse;
    public List<ErrorCode> Errors => Response.Errors;


    
}


