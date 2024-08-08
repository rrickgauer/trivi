using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Services.Contracts;

public interface IGameHubService
{
    public Task UpdatePlayerResponseStatusAsync(string gameId, QuestionId questionId);
    public Task NavigatePlayersToPageAsync(string gameId, string destination);
}
