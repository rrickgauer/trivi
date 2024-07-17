using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IQuestionService
{
    public Task<ServiceDataResponse<List<ViewQuestion>>> GetQuestionsInCollectionAsync(Guid collectionId);

    public Task<ServiceDataResponse<ViewQuestion>> GetQuestionAsync(QuestionId questionId);
    public Task<ServiceDataResponse<ViewShortAnswer>> GetShortAnswerAsync(QuestionId questionId);
    public Task<ServiceDataResponse<ViewMultipleChoice>> GetMultipleChoiceAsync(QuestionId questionId);
    public Task<ServiceDataResponse<ViewTrueFalse>> GetTrueFalseAsync(QuestionId questionId);

    public Task<ServiceDataResponse<ViewShortAnswer>> SaveShortAnswerAsync(ShortAnswer question);
    public Task<ServiceDataResponse<ViewTrueFalse>> SaveTrueFalseAsync(TrueFalse question);
    public Task<ServiceDataResponse<ViewMultipleChoice>> SaveMultipleChoiceAsync(MultipleChoice question);

    public Task<ServiceResponse> DeleteQuestionAsync(QuestionId questionId);
}
