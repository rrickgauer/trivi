using MySql.Data.MySqlClient;
using System.Data;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Repository.Commands;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Repository.Other;

namespace Trivi.Lib.Repository.Implementations;

[AutoInject<IGameQuestionRepository>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class GameQuestionRepository(DatabaseConnection connection) : IGameQuestionRepository
{
    private readonly DatabaseConnection _connection = connection;

    public async Task<DataTable> CopyOverGameQuestionsAsync(string gameId)
    {
        MySqlCommand command = new(GameQuestionRepositoryCommands.CopyGameQuestionsProcName)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("in_game_id", gameId);

        return await _connection.FetchAllAsync(command);
    }

    public async Task<DataRow?> SelectGameQuestionAsync(GameQuestionLookup gameQuestionLookup)
    {
        MySqlCommand command = new(GameQuestionRepositoryCommands.SelectByGameIdQuestionId);

        command.Parameters.AddWithValue("@question_id", gameQuestionLookup.QuestionId.Id);
        command.Parameters.AddWithValue("@game_id", gameQuestionLookup.GameId);

        return await _connection.FetchAsync(command);
    }

    public async Task<int> UpdateGameQuestionStatusAsync(GameQuestionLookup gameQuestionLookup, GameQuestionStatus gameQuestionStatus)
    {
        MySqlCommand command = new(GameQuestionRepositoryCommands.UpdateStatus);

        command.Parameters.AddWithValue("@game_question_status_id", (ushort)gameQuestionStatus);
        command.Parameters.AddWithValue("@question_id", gameQuestionLookup.QuestionId.Id);
        command.Parameters.AddWithValue("@game_id", gameQuestionLookup.GameId);

        return await _connection.ModifyAsync(command);
    }
}
