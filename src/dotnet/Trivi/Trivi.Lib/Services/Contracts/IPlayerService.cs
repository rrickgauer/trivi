using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IPlayerService
{
    public Task<ServiceResponse<List<ViewPlayer>>> GetPlayersInGameAsync(string gameId);
    public Task<ServiceResponse<ViewPlayer>> GetPlayerAsync(Guid playerId);
    public Task<ServiceResponse<ViewPlayer>> GetPlayerAsync(string gameId, string nickname);

    public Task<ServiceResponse<ViewPlayer>> CreatePlayerAsync(Player player);
}
