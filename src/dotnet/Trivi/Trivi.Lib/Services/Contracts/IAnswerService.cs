using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.RequestArgs;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IAnswerService
{
    public Task<ServiceResponse<List<ViewAnswer>>> GetAnswersAsync(QuestionId questionId);
    public Task<ServiceResponse<ViewAnswer>> GetAnswerAsync(string answerId);
    public Task<ServiceResponse<ViewAnswer>> SaveAnswerAsync(Answer answer);
    public Task<ServiceResponse<List<ViewAnswer>>> ReplaceAnswersAsync(PutAnswersRequest answers);
    public Task<ServiceResponse> DeleteAnswerAsync(string answerId); 
}
