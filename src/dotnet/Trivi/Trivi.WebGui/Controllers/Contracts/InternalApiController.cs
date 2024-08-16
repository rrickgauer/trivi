using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;

namespace Trivi.WebGui.Controllers.Contracts;

public class InternalApiController : ControllerBase
{
    public SessionManager SessionManager => new(HttpContext.Session);
    public Guid ClientId => SessionManager.ClientId!.Value;

    public ActionResult<ServiceResponse<T>> FromServiceDataResponse<T>(ServiceResponse<T> response)
    {
        if (!response.Successful)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    public ActionResult FromServiceResponse(ServiceResponse response)
    {
        if (!response.Successful)
        {
            return BadRequest(response);
        }

        return NoContent();
    }
}
