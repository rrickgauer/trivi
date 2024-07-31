using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Hubs.Lobby;

namespace Trivi.Lib.Services.Contracts;

public interface IGamePoolService
{
    public ConnectionsPool GetConnectionPool(string gameId);

    public Task<ServiceDataResponse<ConnectionsPool>> AddPlayerToGameAsync(string gameId, Guid playerId);
}

