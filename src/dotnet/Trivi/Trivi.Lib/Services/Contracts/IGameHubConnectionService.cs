using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Services.Contracts;

public interface IGameHubConnectionService
{
    public bool TryGetGameAdminConnectionId(string gameId, out string connectionId);
    public void StoreAdminConnectionId(string gameId, string connectionId);
    public void RemoveConnection(string connectionId);
    public GameHubConnectionsCache StorePlayerConnectionId(string gameId, string connectionId);

    public bool TryGetGameConnections(string gameId, out GameHubConnectionsCache? gameHubConnectionsCache);
}
