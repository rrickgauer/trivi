namespace Trivi.Lib.Services.Contracts;

public interface IGameLobbyHubService
{
    public Task StartGameAsync(string gameId);
    public Task PlayersNavigateToAsync(string gameId, string destination);
    public Task AdminNavigateToAsync(string gameId, string destination);
}
