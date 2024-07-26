using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Hubs.Lobby;

public class ConnectionsPool(string gameId)
{
    public string GameId { get; private set; } = gameId;

    public string GroupIdAdmin => GetAdminGroupId(GameId);
    public string GroupIdPlayers => GetPlayerGroupId(GameId);

    public Dictionary<Guid, ViewPlayer> Players = new();
    public string? AdminConnectionId { get; set; }

    public List<string> ConnectionIdsCache { get; set; } = new();

    public bool ContainsPlayer(Guid playerId)
    {
        return Players.ContainsKey(playerId);
    }

    public List<ViewPlayer> PlayersList => Players.Values.ToList();


    private static string GetAdminGroupId(string gameId)
    {
        return $"{gameId}-group-admin";
    }

    private static string GetPlayerGroupId(string gameId)
    {
        return $"{gameId}-group-player";
    }
}
