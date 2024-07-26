using Microsoft.AspNetCore.Mvc.Filters;
using Trivi.Lib.Auth.Implementations;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Filters;

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class PlayGameFilter(PlayGameAuth auth) : IAsyncActionFilter
{
    private readonly PlayGameAuth _auth = auth;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var playGameRequest = context.GetPlayGameRequest();

        var hasPermission = await _auth.HasPermissionAsync(new()
        {
            GameId = playGameRequest.GameId,
            PlayerId = playGameRequest.PlayerId,
        });


        if (!hasPermission.Successful)
        {
            context.ReturnBadServiceResponse(hasPermission);
            return;
        }

        await next();


    }
}
