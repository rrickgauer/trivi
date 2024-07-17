using Microsoft.AspNetCore.Mvc.Filters;
using Trivi.Lib.Auth.Implementations;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Filters;

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class PutAnswerFilter(PutAnswerAuth auth) : IAsyncActionFilter
{
    private readonly PutAnswerAuth _auth = auth;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var hasPermission = await _auth.HasPermissionAsync(new()
        {
            AnswerRequest = context.GetPutAnswerRequest(),
            ClientId = context.GetSessionClientId(),
        });

        if (context.NotAuthorized(hasPermission))
        {
            return;
        }
        
        await next();
    }
}
