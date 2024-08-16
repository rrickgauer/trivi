using System.Data;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Repository.Contracts;


public interface IGameQuestionRepository
{
    public Task<DataTable> CopyOverGameQuestionsAsync(string gameId);
    public Task<DataRow?> SelectGameQuestionAsync(GameQuestionLookup gameQuestionLookup);
    public Task<int> UpdateGameQuestionStatusAsync(GameQuestionLookup gameQuestionLookup, GameQuestionStatus gameQuestionStatus);
}
