using System.Data;
using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Repository.Contracts;

public interface IGameRepository
{
    public Task<DataTable> SelectUserGamesAsync(Guid userId);
    public Task<DataRow?> SelectGameAsync(string gameId);   
    public Task<int> InsertGameAsync(Game game);
    public Task<int> UpdateGameStatusAsync(string gameId, GameStatus status);

    public Task<int> ActivateNextGameQuestionAsync(string gameId);
}


