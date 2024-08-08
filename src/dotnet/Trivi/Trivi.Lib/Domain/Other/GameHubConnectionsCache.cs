using System.Collections.Concurrent;

namespace Trivi.Lib.Domain.Other;

public class GameHubConnectionsCache(string gameId)
{
    public string GameId { get; } = gameId;
    public string? AdminConnectionId { get; set; }
    public List<string> PlayerConnections { get; set; } = new();

    public void RemoveConnection(string connectionId)
    {
        if (AdminConnectionId == connectionId)
        {
            AdminConnectionId = null;
            return;
        }

        PlayerConnections.Remove(connectionId);
    }
}


public static class GameHubConnectionsCacheExtensions
{
    public static GameHubConnectionsCache SetAdminConnectionId(this ConcurrentDictionary<string, GameHubConnectionsCache> gamesCache, string gameId, string connectionId)
    {
        var gameConnections = gamesCache.AddOrUpdate(gameId, (key) =>
        {
            // add
            return new(gameId)
            {
                AdminConnectionId = connectionId,
            };
        },

        (key, value) =>
        {
            // update
            value.AdminConnectionId = connectionId;
            return value;
        });

        return gameConnections;
    }

    public static GameHubConnectionsCache SetPlayerConnectionId(this ConcurrentDictionary<string, GameHubConnectionsCache> gamesCache, string gameId, string connectionId)
    {
        var gameConnections = gamesCache.AddOrUpdate(gameId, (key) =>
        {
            // add 
            GameHubConnectionsCache newResult = new(gameId);
            newResult.PlayerConnections.Add(connectionId);

            return newResult;
        },

        (key, value) =>
        {
            // update
            value.PlayerConnections.Add(connectionId);
            return value;
        });

        return gameConnections;
    }

}
