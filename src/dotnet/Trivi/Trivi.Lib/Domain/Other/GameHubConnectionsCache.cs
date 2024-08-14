namespace Trivi.Lib.Domain.Other;

public class GameHubConnectionsCache(string gameId)
{
    public string GameId { get; } = gameId;
    public string? AdminConnectionId { get; set; }


    // player id, connection id
    public Dictionary<Guid, string> Players = new();
    public List<string> PlayerConnectionIds => Players.Values.ToList();

    public void RemoveConnection(string connectionId)
    {
        if (AdminConnectionId == connectionId)
        {
            AdminConnectionId = null;
            return;
        }

        if (TryGetPlayerId(connectionId, out var playerId))
        {
            Players.Remove(playerId);
        }
    }

    

    public bool TryGetPlayerId(string connectionId, out Guid playerId)
    {
        playerId = Guid.Empty;
        
        var foundId = Players.Where(p => p.Value == connectionId)?.FirstOrDefault().Key;

        if (foundId is Guid result)
        {
            playerId = result;
            return true;
        }

        return false;
    }


    public GameHubConnectionsCache AddPlayer(Guid playerId, string connectionId)
    {
        if (!Players.TryAdd(playerId, connectionId))
        {
            Players[playerId] = connectionId;
        }

        return this;
    }
}
