using MySql.Data.MySqlClient;
using System.Data;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Repository.Commands;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Repository.Other;

namespace Trivi.Lib.Repository.Implementations;

[AutoInject<IPlayerRepository>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class PlayerRepository(DatabaseConnection connection) : IPlayerRepository
{
    private readonly DatabaseConnection _connection = connection;

    public async Task<DataTable> SelectAllPlayersInGameAsync(string gameId)
    {
        MySqlCommand command = new(PlayerRepositoryCommands.SelectAllInGame);

        command.Parameters.AddWithValue("@game_id", gameId);

        return await _connection.FetchAllAsync(command);
    }

    public async Task<DataRow?> SelectPlayerAsync(Guid playerId)
    {
        MySqlCommand command = new(PlayerRepositoryCommands.SelectById);

        command.Parameters.AddWithValue("@player_id", playerId);

        return await _connection.FetchAsync(command);
    }

    public async Task<DataRow?> SelectPlayerAsync(string gameId, string nickname)
    {
        MySqlCommand command = new(PlayerRepositoryCommands.SelectByGameIdAndNickname);

        command.Parameters.AddWithValue("@game_id", gameId);
        command.Parameters.AddWithValue("@nickname", nickname);

        return await _connection.FetchAsync(command);
    }

    public async Task<int> InsertPlayerAsync(Player player)
    {
        MySqlCommand command = new(PlayerRepositoryCommands.Insert);

        command.Parameters.AddWithValue("@id", player.Id);
        command.Parameters.AddWithValue("@game_id", player.GameId);
        command.Parameters.AddWithValue("@nickname", player.Nickname);
        command.Parameters.AddWithValue("@created_on", player.CreatedOn);

        return await _connection.ModifyAsync(command);
    }
}
