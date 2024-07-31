using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IResponseService
{
    public Task<ServiceDataResponse<List<ViewResponseShortAnswer>>> GetShortAnswerResponsesAync(string gameId, QuestionId questionId);
    public Task<ServiceDataResponse<ViewResponseShortAnswer>> GetShortAnswerResponseAsync(Guid responseId);
    public Task<ServiceDataResponse<ViewResponseShortAnswer>> GetShortAnswerResponseAsync(PlayerQuestionResponse responseData);

    public Task<ServiceDataResponse<ViewResponse>> GetResponseAsync(PlayerQuestionResponse responseData);

    
    public Task<ServiceDataResponse<ViewResponseShortAnswer>> CreateShortAnswerResponseAsync(ResponseShortAnswer response);
}
