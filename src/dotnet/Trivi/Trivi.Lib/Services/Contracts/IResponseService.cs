using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IResponseService
{
    public Task<ServiceDataResponse<List<ViewResponseShortAnswer>>> GetShortAnswersAsync(string gameId, QuestionId questionId);
    
    public Task<ServiceDataResponse<ViewResponseShortAnswer>> GetShortAnswerAsync(Guid responseId);
    public Task<ServiceDataResponse<ViewResponseShortAnswer>> GetShortAnswerAsync(PlayerQuestionResponse responseData);

    public Task<ServiceDataResponse<ViewResponseTrueFalse>> GetTrueFalseAsync(Guid responseId);
    public Task<ServiceDataResponse<ViewResponseTrueFalse>> GetTrueFalseAsync(PlayerQuestionResponse responseData);

    public Task<ServiceDataResponse<ViewResponseMultipleChoice>> GetMultipleChoiceAsync(Guid responseId);
    public Task<ServiceDataResponse<ViewResponseMultipleChoice>> GetMultipleChoiceAsync(PlayerQuestionResponse responseData);

    public Task<ServiceDataResponse<ViewResponse>> GetResponseAsync(PlayerQuestionResponse responseData);
    
    public Task<ServiceDataResponse<ViewResponseShortAnswer>> CreateShortAnswerResponseAsync(ResponseShortAnswer response);
    public Task<ServiceDataResponse<ViewResponseTrueFalse>> CreateTrueFalseResponseAsync(ResponseTrueFalse response);
    public Task<ServiceDataResponse<ViewResponseMultipleChoice>> CreateMultipleChoiceResponseAsync(ResponseMultipleChoice response);

    public Task<ServiceDataResponse<List<ViewPlayerQuestionResponse>>> GetPlayerQuestionResponsesAsync(string gameId, QuestionId questionId);

}
