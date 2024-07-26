using System.Data;
using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Repository.Contracts;

public interface IPlayerRepository
{
    public Task<DataTable> SelectAllPlayersInGameAsync(string gameId);
    public Task<DataRow?> SelectPlayerAsync(Guid playerId);
    public Task<DataRow?> SelectPlayerAsync(string gameId, string nickname);

    public Task<int> InsertPlayerAsync(Player player);
}
