using System.Data;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Repository.Contracts;

public interface IResponseRepository
{
    public Task<DataTable> SelectShortAnswersAsync(string gameId, QuestionId questionId);
    public Task<DataRow?> SelectShortAnswerAsync(Guid responseId);
    public Task<DataRow?> SelectShortAnswerAsync(PlayerQuestionResponse responseData);

    public Task<DataRow?> SelectTrueFalseAsync(Guid responseId);
    public Task<DataRow?> SelectTrueFalseAsync(PlayerQuestionResponse responseData);

    public Task<DataRow?> SelectMultipleChoiceAsync(Guid responseId);
    public Task<DataRow?> SelectMultipleChoiceAsync(PlayerQuestionResponse responseData);

    public Task<int> CreateResponseAsync(ResponseShortAnswer response);
    public Task<int> CreateResponseAsync(ResponseTrueFalse response);
    public Task<int> CreateResponseAsync(ResponseMultipleChoice response);


    public Task<DataTable> GetPlayerQuestionResponsesAsync(string gameId, QuestionId questionId);

}



