using MySql.Data.MySqlClient;
using System.Data;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Repository.Other;

namespace Trivi.Lib.Repository.Implementations;


public class GameQuestionRepositoryCommands
{
    public const string CopyGameQuestionsProcName = "Copy_Game_Questions";
}



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
}
