using MySql.Data.MySqlClient;
using System.Data;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Repository.Commands;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Repository.Other;

namespace Trivi.Lib.Repository.Implementations;

[AutoInject<IGameRepository>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class GameRepository(DatabaseConnection connection) : IGameRepository
{
    private readonly DatabaseConnection _connection = connection;

    public async Task<DataTable> SelectUserGamesAsync(Guid userId)
    {
        MySqlCommand command = new(GameRepositoryCommands.SelectUserGames);

        command.Parameters.AddWithValue("@user_id", userId);

        return await _connection.FetchAllAsync(command);
    }

    public async Task<DataRow?> SelectGameAsync(string gameId)
    {
        MySqlCommand command = new(GameRepositoryCommands.SelectGame);

        command.Parameters.AddWithValue("@game_id", gameId);

        return await _connection.FetchAsync(command);
    }

    public async Task<int> InsertGameAsync(Game game)
    {
        MySqlCommand command = new(GameRepositoryCommands.Insert);

        command.Parameters.AddWithValue("@id", game.Id);
        command.Parameters.AddWithValue("@collection_id", game.CollectionId);
        command.Parameters.AddWithValue("@game_status_id", game.Status);
        command.Parameters.AddWithValue("@randomize_questions", game.RandomizeQuestions);
        command.Parameters.AddWithValue("@question_time_limit", game.QuestionTimeLimit);
        command.Parameters.AddWithValue("@created_on", game.CreatedOn);
        command.Parameters.AddWithValue("@started_on", game.StartedOn);

        return await _connection.ModifyAsync(command);
    }

    public async Task<int> UpdateGameStatusAsync(string gameId, GameStatus status)
    {
        MySqlCommand command = new(GameRepositoryCommands.UpdateStatus);

        command.Parameters.AddWithValue("@game_id", gameId);
        command.Parameters.AddWithValue("@status_id", (ushort)status);

        return await _connection.ModifyAsync(command);
    }
}


