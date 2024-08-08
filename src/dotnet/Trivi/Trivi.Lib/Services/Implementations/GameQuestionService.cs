using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<IGameQuestionService>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class GameQuestionService(IGameQuestionRepository repo, ITableMapperService tableMapperService) : IGameQuestionService
{
    private readonly IGameQuestionRepository _repo = repo;
    private readonly ITableMapperService _tableMapperService = tableMapperService;

    public async Task<ServiceDataResponse<List<ViewGameQuestion>>> CreateGameQuestionsAsync(string gameId)
    {
        try
        {

            var table = await _repo.CopyOverGameQuestionsAsync(gameId);

            var models = _tableMapperService.ToModels<ViewGameQuestion>(table);

            return models;
        }
        catch(RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceDataResponse<ViewGameQuestion>> GetGameQuestionAsync(GameQuestionLookup gameQuestionLookup)
    {
        try
        {
            ServiceDataResponse<ViewGameQuestion> result = new();

            var row = await _repo.SelectGameQuestionAsync(gameQuestionLookup);

            if (row != null)
            {
                result.Data = _tableMapperService.ToModel<ViewGameQuestion>(row);
            }

            return result;
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceDataResponse<ViewGameQuestion>> UpdateGameQuestionStatusAsync(GameQuestionLookup gameQuestionLookup, GameQuestionStatus gameQuestionStatus)
    {
        try
        {
            await _repo.UpdateGameQuestionStatusAsync(gameQuestionLookup, gameQuestionStatus);
        }
        catch(RepositoryException ex)
        {
            return ex;
        }

        return await GetGameQuestionAsync(gameQuestionLookup);
    }
}
