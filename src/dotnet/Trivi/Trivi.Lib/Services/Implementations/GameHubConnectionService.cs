using System.Collections.Concurrent;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<IGameHubConnectionService>(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class GameHubConnectionService : IGameHubConnectionService
{
    private static readonly ConcurrentDictionary<string, GameHubConnectionsCache> _gamesCache = new();
    private static readonly ConcurrentDictionary<string, string> _connectionIdsCache = new();     // ConnectionId, GameId

    public bool TryGetGameAdminConnectionId(string gameId, out string connectionId)
    {
        connectionId = string.Empty;

        if (!_gamesCache.TryGetValue(gameId, out var game))
        {
            return false;
        }

        if (game.AdminConnectionId is not string foundConnectionId)
        {
            return false;
        }

        connectionId = foundConnectionId;
        return true;
    }

    public void StoreAdminConnectionId(string gameId, string connectionId)
    {
        _gamesCache.SetAdminConnectionId(gameId, connectionId);
        _connectionIdsCache[connectionId] = gameId;
    }

    public void RemoveConnection(string connectionId)
    {
        if (!_connectionIdsCache.Remove(connectionId, out var gameId))
        {
            return;
        }

        if (!_gamesCache.TryGetValue(gameId, out var game))
        {
            return;
        }

        game.RemoveConnection(connectionId);
    }

    public GameHubConnectionsCache StorePlayerConnectionId(string gameId, string connectionId)
    {
        _connectionIdsCache[connectionId] = gameId;
        return _gamesCache.SetPlayerConnectionId(gameId, connectionId);
    }

    public bool TryGetGameConnections(string gameId, out GameHubConnectionsCache? gameHubConnectionsCache)
    {
        gameHubConnectionsCache = null;

        if (_gamesCache.TryGetValue(gameId, out var result))
        {
            gameHubConnectionsCache = result;
            return true;
        }

        return false;
    }
}
