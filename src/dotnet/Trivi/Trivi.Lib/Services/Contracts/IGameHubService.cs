namespace Trivi.Lib.Services.Contracts;

public interface IGameHubService
{
    public Task StartGameAsync(string gameId);
    public Task NavigateToAsync(string gameId, string destination);
}
