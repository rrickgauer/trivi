using Microsoft.AspNetCore.SignalR;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Hubs.Lobby;
using Trivi.Lib.Hubs.Lobby.EndpointData;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<IGameLobbyHubService>(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class GameLobbyHubService(IHubContext<GameLobbyHub, IGameHubClientEvents> hubContext, IGamePoolService gamePoolService) : IGameLobbyHubService
{
    private readonly IHubContext<GameLobbyHub, IGameHubClientEvents> _hubContext = hubContext;
    private readonly IGamePoolService _gamePoolService = gamePoolService;

    public async Task StartGameAsync(string gameId)
    {
        var pool = _gamePoolService.GetConnectionPool(gameId);

        await _hubContext.Clients.Group(pool.GroupIdAdmin).NavigateTo(new NavigateToData()
        {
            Destination = $"/games/{gameId}/questions/{244}",
        });
    }

    public async Task PlayersNavigateToAsync(string gameId, string destination)
    {
        var pool = _gamePoolService.GetConnectionPool(gameId);

        await _hubContext.Clients.Group(pool.GroupIdPlayers).NavigateTo(new NavigateToData()
        {
            Destination = destination,
        });
    }

    public async Task AdminNavigateToAsync(string gameId, string destination)
    {
        var pool = _gamePoolService.GetConnectionPool(gameId);

        await _hubContext.Clients.Group(pool.GroupIdAdmin).NavigateTo(new NavigateToData()
        {
            Destination = destination,
        });
    }
}
