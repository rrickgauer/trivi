using Microsoft.AspNetCore.Mvc.Filters;
using Trivi.Lib.Auth.Implementations;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Filters;

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class JoinGameFilter(JoinGameAuth auth) : IAsyncActionFilter
{
    private readonly JoinGameAuth _auth = auth;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var hasPermission = await _auth.HasPermissionAsync(new()
        {
            JoinGameForm = context.GetJoinGameForm(),
        });

        if (!hasPermission.Successful)
        {
            context.ReturnBadServiceResponse(hasPermission);
            return;
        }

        await next();

    }
}
