using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IGameQuestionService
{
    public Task<ServiceResponse<List<ViewGameQuestion>>> CreateGameQuestionsAsync(string gameId);
    public Task<ServiceResponse<ViewGameQuestion>> GetGameQuestionAsync(GameQuestionLookup gameQuestionLookup);
    public Task<ServiceResponse<ViewGameQuestion>> UpdateGameQuestionStatusAsync(GameQuestionLookup gameQuestionLookup, GameQuestionStatus gameQuestionStatus);
}
