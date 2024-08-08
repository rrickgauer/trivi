using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IGameService
{
    public Task<ServiceDataResponse<List<ViewGame>>> GetUserGamesAsync(Guid userId);
    public Task<ServiceDataResponse<ViewGame>> GetGameAsync(string gameId);
    public Task<ServiceDataResponse<ViewGame>> CreateGameAsync(Game game);
    public Task<ServiceDataResponse<ViewGame>> StartGameAsync(string gameId);
    public Task<ServiceDataResponse<ViewGame>> ActivateNextGameQuestionAsync(string gameId);
}
