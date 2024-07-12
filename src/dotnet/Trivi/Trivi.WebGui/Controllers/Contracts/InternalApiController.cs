using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Other;

namespace Trivi.WebGui.Controllers.Contracts;

public class InternalApiController : ControllerBase
{
    public SessionManager SessionManager => new(HttpContext.Session);
    public Guid ClientId => SessionManager.ClientId!.Value;
}
