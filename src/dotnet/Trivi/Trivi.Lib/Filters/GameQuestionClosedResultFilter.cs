using Microsoft.AspNetCore.Mvc.Filters;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Filters;

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class GameQuestionClosedResultFilter(ResultFilterCache filterCache, IGameHubService gameHubService) : IAsyncResultFilter
{
    private readonly ResultFilterCache _filterCache = filterCache;
    private readonly IGameHubService _gameHubService = gameHubService;

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_filterCache.GameId is string gameId)
        {
            var destination = $"/games/{gameId}";
            await _gameHubService.NavigatePlayersToPageAsync(gameId, destination);
        }

        await next();
    }
}
