using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IPlayerService
{
    public Task<ServiceDataResponse<List<ViewPlayer>>> GetPlayersInGameAsync(string gameId);
    public Task<ServiceDataResponse<ViewPlayer>> GetPlayerAsync(Guid playerId);
    public Task<ServiceDataResponse<ViewPlayer>> GetPlayerAsync(string gameId, string nickname);

    public Task<ServiceDataResponse<ViewPlayer>> CreatePlayerAsync(Player player);
}
