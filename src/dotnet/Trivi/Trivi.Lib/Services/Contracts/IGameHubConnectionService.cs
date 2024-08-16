using Trivi.Lib.Domain.Other;
using Trivi.Lib.Hubs.Question.EndpointParms;

namespace Trivi.Lib.Services.Contracts;

public interface IGameHubConnectionService
{

    public bool TryGetGameFromConnectionId(string connectionId, out GameHubConnectionsCache? gameHubConnectionsCache);

    public bool TryGetGameAdminConnectionId(string gameId, out string connectionId);
    public void StoreAdminConnectionId(string gameId, string connectionId);
    public void RemoveConnection(string connectionId);
    public GameHubConnectionsCache StorePlayerConnectionId(PlayerConnectParms playerData, string connectionId);
    public bool TryGetGameConnections(string gameId, out GameHubConnectionsCache? gameHubConnectionsCache);
    public bool TryGetGameIdFromConnection(string connectionId, out string gameId);
}
