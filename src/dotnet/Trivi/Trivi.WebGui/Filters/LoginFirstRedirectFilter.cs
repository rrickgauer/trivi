/***

Auth filter that checks if the client is logged in.

If yes: continue on normally.
If no: redirect them to the login page (which will then take them to their destination after they log in)

*/ 

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Services.Contracts;
using Trivi.WebGui.Controllers.Gui;

namespace Trivi.WebGui.Filters;

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class LoginFirstRedirectFilter(IAuthService authService) : IAsyncActionFilter
{
    private readonly IAuthService _authService = authService;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var checkSession = _authService.IsClientLoggedIn();

        if (!checkSession.Data)
        {
            context.Result = new RedirectToActionResult(nameof(AuthController.LoginPage), AuthController.ControllerRedirectName, new
            {
                destination = context.HttpContext.Request.GetEncodedUrl(),
            });

            return;
        }

        await next();
    }
}
