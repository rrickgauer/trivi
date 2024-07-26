using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IGameQuestionService
{
    public Task<ServiceDataResponse<List<ViewGameQuestion>>> CreateGameQuestionsAsync(string gameId);
}
