using Trivi.Lib.Domain.Responses;

namespace Trivi.Lib.Services.Contracts;

public interface IGamePageService
{
    public Task<ServiceDataResponse<string>> GetPlayerPageUrl(string gameId, Guid playerId);
}
