using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Constants;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<IGameService>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class GameService(IGameRepository gameRepository, ITableMapperService tableMapperService) : IGameService
{
    private readonly IGameRepository _gameRepository = gameRepository;
    private readonly ITableMapperService _tableMapperService = tableMapperService;

    public async Task<ServiceDataResponse<List<ViewGame>>> GetUserGamesAsync(Guid userId)
    {
        try
        {
            var table = await _gameRepository.SelectUserGamesAsync(userId);
            return _tableMapperService.ToModels<ViewGame>(table);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceDataResponse<ViewGame>> GetGameAsync(string gameId)
    {
        try
        {
            ServiceDataResponse<ViewGame> result = new();

            var row = await _gameRepository.SelectGameAsync(gameId);

            if (row != null)
            {
                result.Data = _tableMapperService.ToModel<ViewGame>(row);
            }

            return result;
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }


    public async Task<ServiceDataResponse<ViewGame>> CreateGameAsync(Game game)
    {
        var validateResult = await ValidateNewGameAsync(game);

        if (!validateResult.Successful)
        {
            return new(validateResult.Errors);
        }

        try
        {
            await _gameRepository.InsertGameAsync(game);
        }
        catch(RepositoryException ex)
        {
            return ex;
        }

        return await GetGameAsync(game.Id!);
    }

    private async Task<ServiceResponse> ValidateNewGameAsync(Game game)
    {
        ServiceResponse result = new();

        if (game.QuestionTimeLimit is ushort timeLimit)
        {
            if (timeLimit < GameConstants.MinQuestionTimeLimit || timeLimit > GameConstants.MaxQuestionTimeLimit)
            {
                result.Errors.Add(ErrorCode.GamesInvalidQuestionTimeLimit);
            }
        }

        return result;
    }
}
