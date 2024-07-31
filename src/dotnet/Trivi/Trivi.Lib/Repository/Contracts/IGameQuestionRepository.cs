using System.Data;

namespace Trivi.Lib.Repository.Contracts;


public interface IGameQuestionRepository
{
    public Task<DataTable> CopyOverGameQuestionsAsync(string gameId);
}
