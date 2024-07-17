using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.RequestArgs;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IAnswerService
{
    public Task<ServiceDataResponse<List<ViewAnswer>>> GetAnswersAsync(QuestionId questionId);
    public Task<ServiceDataResponse<ViewAnswer>> GetAnswerAsync(string answerId);
    public Task<ServiceDataResponse<ViewAnswer>> SaveAnswerAsync(Answer answer);
    public Task<ServiceDataResponse<List<ViewAnswer>>> ReplaceAnswersAsync(PutAnswersRequest answers);
    public Task<ServiceResponse> DeleteAnswerAsync(string answerId); 
}
