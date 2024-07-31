using System.Data;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Repository.Contracts;

public interface IResponseRepository
{
    public Task<DataTable> SelectShortAnswerResponsesAsync(string gameId, QuestionId questionId);

    public Task<DataRow?> SelectShortAnswerResponseAsync(Guid responseId);
    public Task<DataRow?> SelectShortAnswerResponseAsync(PlayerQuestionResponse responseData);

    public Task<int> CreateResponseAsync(ResponseShortAnswer response);

    
}



