﻿using Microsoft.AspNetCore.Mvc.Filters;
using Trivi.Lib.Auth.Implementations;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Filters;

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class CloseGameQuestionFilter(CloseGameQuestionAuth auth) : IAsyncActionFilter
{
    private readonly CloseGameQuestionAuth _auth = auth;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var hasPermission = await _auth.HasPermissionAsync(new()
        {
            ClientId = context.GetSessionClientId(),
            GameId = context.GetGameIdFromUrl(),
            QuestionId = context.GetQuestionIdFromUrl(),
        });

        if (!hasPermission.Successful)
        {
            context.ReturnBadServiceResponse(hasPermission);
            return;
        }

        await next();

    }
}
