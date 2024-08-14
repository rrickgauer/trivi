using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<IGamePageService>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class GamePageService : IGamePageService
{
    public async Task<ServiceResponse<string>> GetPlayerPageUrl(string gameId, Guid playerId)
    {
        throw new NotImplementedException();
    }
}
