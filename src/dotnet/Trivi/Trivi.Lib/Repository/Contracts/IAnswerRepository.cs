using System.Data;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Repository.Contracts;

public interface IAnswerRepository
{
    public Task<DataTable> SelectAnswersAsync(QuestionId questionId);
    public Task<DataRow?> SelectAnswerAsync(string answerId);
    
    public Task<int> UpsertAnswerAsync(Answer answer);

    public Task<int> DeleteAnswerAsync(string answerId);

    public Task<bool> ReplaceQuestionAnswersAsync(QuestionId questionId, IEnumerable<Answer> answers);
}
