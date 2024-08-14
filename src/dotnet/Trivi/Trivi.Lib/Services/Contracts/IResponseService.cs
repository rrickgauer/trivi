using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IResponseService
{
    public Task<ServiceResponse<List<ViewResponseShortAnswer>>> GetShortAnswersAsync(string gameId, QuestionId questionId);
    
    public Task<ServiceResponse<ViewResponseShortAnswer>> GetShortAnswerAsync(Guid responseId);
    public Task<ServiceResponse<ViewResponseShortAnswer>> GetShortAnswerAsync(PlayerQuestionResponse responseData);

    public Task<ServiceResponse<ViewResponseTrueFalse>> GetTrueFalseAsync(Guid responseId);
    public Task<ServiceResponse<ViewResponseTrueFalse>> GetTrueFalseAsync(PlayerQuestionResponse responseData);

    public Task<ServiceResponse<ViewResponseMultipleChoice>> GetMultipleChoiceAsync(Guid responseId);
    public Task<ServiceResponse<ViewResponseMultipleChoice>> GetMultipleChoiceAsync(PlayerQuestionResponse responseData);

    public Task<ServiceResponse<ViewResponse>> GetResponseAsync(PlayerQuestionResponse responseData);
    
    public Task<ServiceResponse<ViewResponseShortAnswer>> CreateShortAnswerResponseAsync(ResponseShortAnswer response);
    public Task<ServiceResponse<ViewResponseTrueFalse>> CreateTrueFalseResponseAsync(ResponseTrueFalse response);
    public Task<ServiceResponse<ViewResponseMultipleChoice>> CreateMultipleChoiceResponseAsync(ResponseMultipleChoice response);

    public Task<ServiceResponse<List<ViewPlayerQuestionResponse>>> GetPlayerQuestionResponsesAsync(string gameId, QuestionId questionId);

}
