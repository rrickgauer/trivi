using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IQuestionService
{
    public Task<ServiceResponse<List<ViewQuestion>>> GetQuestionsInCollectionAsync(Guid collectionId);

    public Task<ServiceResponse<ViewQuestion>> GetQuestionAsync(QuestionId questionId);
    public Task<ServiceResponse<ViewShortAnswer>> GetShortAnswerAsync(QuestionId questionId);
    public Task<ServiceResponse<ViewMultipleChoice>> GetMultipleChoiceAsync(QuestionId questionId);
    public Task<ServiceResponse<ViewTrueFalse>> GetTrueFalseAsync(QuestionId questionId);

    public Task<ServiceResponse<ViewShortAnswer>> SaveShortAnswerAsync(ShortAnswer question);
    public Task<ServiceResponse<ViewTrueFalse>> SaveTrueFalseAsync(TrueFalse question);
    public Task<ServiceResponse<ViewMultipleChoice>> SaveMultipleChoiceAsync(MultipleChoice question);

    public Task<ServiceResponse> DeleteQuestionAsync(QuestionId questionId);
}
