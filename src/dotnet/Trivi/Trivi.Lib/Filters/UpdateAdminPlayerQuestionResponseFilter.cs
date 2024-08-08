using Microsoft.AspNetCore.Mvc.Filters;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Filters;

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class UpdateAdminPlayerQuestionResponseFilter(RequestItems requestItems, IGameHubService gameHubService) : IAsyncResultFilter
{
    private readonly RequestItems _requestItems = requestItems;
    private readonly IGameHubService _gameHubService = gameHubService;

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var response = _requestItems.ResponseResult;

        if (response.GameId is string gameId && response.QuestionId is QuestionId questionId)
        {
            await _gameHubService.UpdatePlayerResponseStatusAsync(gameId, questionId);
        }

        await next();       
    }
}
