using System.Net;

namespace Trivi.Lib.Domain.Errors;

public class ForbiddenHttpResponseException() : HttpResponseException(HttpStatusCode.Forbidden, null)
{

}

