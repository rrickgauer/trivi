using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IGameService
{
    public Task<ServiceResponse<List<ViewGame>>> GetUserGamesAsync(Guid userId);
    public Task<ServiceResponse<ViewGame>> GetGameAsync(string gameId);
    public Task<ServiceResponse<ViewGame>> CreateGameAsync(Game game);
    public Task<ServiceResponse<ViewGame>> StartGameAsync(string gameId);
    public Task<ServiceResponse<ViewGame>> ActivateNextGameQuestionAsync(string gameId);
}
