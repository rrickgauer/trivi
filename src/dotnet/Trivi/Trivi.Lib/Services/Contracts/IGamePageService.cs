using Trivi.Lib.Domain.Responses;

namespace Trivi.Lib.Services.Contracts;

public interface IGamePageService
{
    public Task<ServiceResponse<string>> GetPlayerPageUrl(string gameId, Guid playerId);
}
