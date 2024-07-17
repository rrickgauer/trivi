using System.Data;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Repository.Contracts;

public interface IQuestionRepository
{
    public Task<DataTable> SelectCollectionQuestionsAsync(Guid collectionId);

    public Task<DataRow?> SelectShortAnswerAsync(QuestionId questionId);
    public Task<DataRow?> SelectMultipleChoiceAsync(QuestionId questionId);
    public Task<DataRow?> SelectTrueFalseAsync(QuestionId questionId);


    public Task<int> UpsertQuestionAsync(Question question);
    public Task<int> UpsertShortAnswerAsync(ShortAnswer question);
    public Task<int> UpsertTrueFalseAsync(TrueFalse question);
    public Task<int> UpsertMultipleChoiceAsync(MultipleChoice question);

    public Task<int> DeleteQuestionAsync(QuestionId questionId);
}

