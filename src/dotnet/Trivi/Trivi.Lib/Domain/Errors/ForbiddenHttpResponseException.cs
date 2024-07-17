using System.Net;

namespace Trivi.Lib.Domain.Errors;

public class ForbiddenHttpResponseException() : HttpResponseException(HttpStatusCode.Forbidden, null)
{
    public static void ThrowIfNotEqual<T>(T? value1, T? value2)
    {
        if (value1 == null && value2 == null)
        {
            return;
        }


        if (value1 != null && !value1.Equals(value2))
        {
            throw new ForbiddenHttpResponseException();
        }

        if (value2 != null && !value2.Equals(value1))
        {
            throw new ForbiddenHttpResponseException();
        }

        
    }
}

