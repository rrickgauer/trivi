using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Services.Contracts;

namespace Trivi.WebGui.Filters;

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class InternalApiAuthFilter(IAuthService authService) : IActionFilter
{
    private readonly IAuthService _authService = authService;

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var getIsLoggedIn = _authService.IsClientLoggedIn();

        if (!getIsLoggedIn.Successful)
        {
            context.Result = new BadRequestObjectResult(getIsLoggedIn);
            return;
        }

        if (!getIsLoggedIn.Data)
        {
            context.Result = new ObjectResult(null)
            {
                StatusCode = (int)HttpStatusCode.Forbidden,
            };

            return;
        }
    }
}
