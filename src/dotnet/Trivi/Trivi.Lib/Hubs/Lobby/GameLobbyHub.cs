using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Hubs.Lobby.EndpointData;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Hubs.Lobby;

public interface IGameHubClientEvents
{
    public Task AdminLobbyUpdated(ServiceDataResponse<AdminLobbyUpdatedData> data);
    public Task NavigateTo(ServiceDataResponse<NavigateToData> data);
}

public class GameLobbyHub(IGamePoolService gamePoolService) : Hub<IGameHubClientEvents>
{
    private static readonly ConcurrentDictionary<string, PlayerConnection> _playerConnectionsCache = new();
    private static readonly ConcurrentDictionary<string, AdminConnection> _adminConnections = new();

    private readonly IGamePoolService _gamePoolService = gamePoolService;

    public async Task PlayerJoinGameLobbyAsync(PlayerJoinGameLobbyAsyncData data)
    {
        _playerConnectionsCache[Context.ConnectionId] = new()
        {
            ConnectionId = Context.ConnectionId,
            GameId = data.GameId,
            PlayerId = data.PlayerId,
        };

        var gamePool = await AddPlayerToGroupAsync(data);
        await SendLobbyUpdatedAsyc(gamePool);
    }

    public async Task AdminJoinGameLobbyAsync(JoinGameLobbyAsyncData data)
    {
        var gamePool = _gamePoolService.GetConnectionPool(data.GameId);

        await Groups.AddToGroupAsync(Context.ConnectionId, gamePool.GroupIdAdmin);

        gamePool.AdminConnectionId = Context.ConnectionId;

        await SendLobbyUpdatedAsyc(gamePool);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (_playerConnectionsCache.Remove(Context.ConnectionId, out var playerConnection))
        {
            var gamePool = _gamePoolService.GetConnectionPool(playerConnection.GameId);

            gamePool.Players.Remove(playerConnection.PlayerId);

            await SendLobbyUpdatedAsyc(gamePool);
        }

        if (_adminConnections.Remove(Context.ConnectionId, out var adminConnection))
        {

        }

        await base.OnDisconnectedAsync(exception);
    }

    private async Task<ConnectionsPool> AddPlayerToGroupAsync(PlayerJoinGameLobbyAsyncData data)
    {
        await _gamePoolService.AddPlayerToGameAsync(data.GameId, data.PlayerId);

        var gamePool = _gamePoolService.GetConnectionPool(data.GameId);

        await Groups.AddToGroupAsync(Context.ConnectionId, gamePool.GroupIdPlayers);

        return gamePool;
    }


    private async Task SendLobbyUpdatedAsyc(ConnectionsPool gamePool)
    {
        await Clients.Group(gamePool.GroupIdAdmin).AdminLobbyUpdated(new AdminLobbyUpdatedData()
        {
            Players = gamePool.PlayersList,
        });
    }

}
