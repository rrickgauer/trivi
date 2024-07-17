using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;

namespace Trivi.WebGui.Controllers.Contracts;

public class InternalApiController : ControllerBase
{
    public SessionManager SessionManager => new(HttpContext.Session);
    public Guid ClientId => SessionManager.ClientId!.Value;

    public IActionResult FromServiceDataResponse<T>(ServiceDataResponse<T> response)
    {
        if (!response.Successful)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    public IActionResult FromServiceResponse(ServiceResponse response)
    {
        if (!response.Successful)
        {
            return BadRequest(response);
        }

        return NoContent();
    }
}
