using System.Collections.Concurrent;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Hubs.Question.EndpointParms;

namespace Trivi.Lib.Hubs.Question;

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

    public static GameHubConnectionsCache SetPlayerConnectionId(this ConcurrentDictionary<string, GameHubConnectionsCache> gamesCache, PlayerConnectParms playerData, string connectionId)
    {
        var gameConnections = gamesCache.AddOrUpdate(playerData.GameId, (key) =>
        {
            // add 
            GameHubConnectionsCache newResult = new(playerData.GameId);
            return newResult.AddPlayer(playerData.PlayerId, connectionId);
        },

        (key, value) =>
        {
            // update
            return value.AddPlayer(playerData.PlayerId, connectionId);
        });

        return gameConnections;
    }

}
