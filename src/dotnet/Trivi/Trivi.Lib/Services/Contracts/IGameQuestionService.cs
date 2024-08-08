using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IGameQuestionService
{
    public Task<ServiceDataResponse<List<ViewGameQuestion>>> CreateGameQuestionsAsync(string gameId);
    public Task<ServiceDataResponse<ViewGameQuestion>> GetGameQuestionAsync(GameQuestionLookup gameQuestionLookup);
    public Task<ServiceDataResponse<ViewGameQuestion>> UpdateGameQuestionStatusAsync(GameQuestionLookup gameQuestionLookup, GameQuestionStatus gameQuestionStatus);
}
