using Microsoft.AspNetCore.SignalR;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Hubs.Question;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<IGameHubService>(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class GameHubService : IGameHubService
{
    private readonly IHubContext<GameHub, IGameHubClientEvents> _gameHub;
    private readonly IResponseService _responseService;
    private readonly IGameHubConnectionService _gameHubConnectionService;

    public GameHubService(IHubContext<GameHub, IGameHubClientEvents> gameHub, IResponseService responseService, IGameHubConnectionService gameHubConnectionService)
    {
        _gameHub = gameHub;
        _responseService = responseService;
        _gameHubConnectionService = gameHubConnectionService;
    }

    public async Task UpdatePlayerResponseStatusAsync(string gameId, QuestionId questionId)
    {
        if (!_gameHubConnectionService.TryGetGameAdminConnectionId(gameId, out var adminConnectionId))
        {
            return;
        }

        var getResponses = await _responseService.GetPlayerQuestionResponsesAsync(gameId, questionId);

        if (!getResponses.Successful)
        {
            return;
        }

        var responses = getResponses.Data ?? new();

        await _gameHub.Clients.Client(adminConnectionId).AdminUpdatePlayerQuestionResponses(new()
        {
            Responses = responses,
        });

    }

    public async Task NavigatePlayersToPageAsync(string gameId, string destination)
    {
        if (!_gameHubConnectionService.TryGetGameConnections(gameId, out GameHubConnectionsCache? connections))
        {
            return;
        }

        var playerConnectionIds = connections?.PlayerConnections ?? new();

        await _gameHub.Clients.Clients(playerConnectionIds).NavigateToPage(new()
        {
            Destination = destination,
        });
    }






}
