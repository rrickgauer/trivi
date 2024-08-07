using MySql.Data.MySqlClient;
using System.Data;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Repository.Commands;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Repository.Other;

namespace Trivi.Lib.Repository.Implementations;

[AutoInject<IResponseRepository>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class ResponseRepository(DatabaseConnection connection, TransactionConnection transactionConnection) : IResponseRepository
{
    private readonly DatabaseConnection _connection = connection;
    private readonly TransactionConnection _transactionConnection = transactionConnection;

    #region - Select short answers -

    public async Task<DataTable> SelectShortAnswersAsync(string gameId, QuestionId questionId)
    {
        MySqlCommand command = new(ResponseRepositoryCommands.SelectShortAnswers);

        command.Parameters.AddWithValue("@question_id", questionId.ToString());
        command.Parameters.AddWithValue("@game_id", gameId);

        return await _connection.FetchAllAsync(command);
    }

    public async Task<DataRow?> SelectShortAnswerAsync(Guid responseId)
    {
        MySqlCommand command = new(ResponseRepositoryCommands.SelectShortAnswerById);

        command.Parameters.AddWithValue("@response_id", responseId);

        return await _connection.FetchAsync(command);
    }

    public async Task<DataRow?> SelectShortAnswerAsync(PlayerQuestionResponse responseData)
    {
        MySqlCommand command = new(ResponseRepositoryCommands.SelectShortAnswerByQuestionPlayer);

        command.Parameters.AddWithValue("@question_id", responseData.QuestionId.ToString());
        command.Parameters.AddWithValue("@player_id", responseData.PlayerId);

        return await _connection.FetchAsync(command);
    }

    #endregion


    #region - Select true false -

    public async Task<DataRow?> SelectTrueFalseAsync(Guid responseId)
    {
        MySqlCommand command = new(ResponseRepositoryCommands.SelectTrueFalseById);

        command.Parameters.AddWithValue("@response_id", responseId);

        return await _connection.FetchAsync(command);
    }

    public async Task<DataRow?> SelectTrueFalseAsync(PlayerQuestionResponse responseData)
    {
        MySqlCommand command = new(ResponseRepositoryCommands.SelectTrueFalseByQuestionPlayer);

        command.Parameters.AddWithValue("@question_id", responseData.QuestionId.ToString());
        command.Parameters.AddWithValue("@player_id", responseData.PlayerId);

        return await _connection.FetchAsync(command);
    }

    #endregion

    #region - Create response -

    public async Task<int> CreateResponseAsync(ResponseShortAnswer response)
    {
        // start the transaction
        await _transactionConnection.StartTransactionAsync();

        // insert the base response record
        var baseCommand = GetCreateBaseResponseCommand(response);
        await _transactionConnection.ExecuteInTransactionAsync(baseCommand);


        // insert the short answer response record
        MySqlCommand command = new(ResponseRepositoryCommands.UpsertShortAnswer);
        
        command.Parameters.AddWithValue("@id", response.Id);
        command.Parameters.AddWithValue("@answer_given", response.AnswerGiven);
        
        await _transactionConnection.ExecuteInTransactionAsync(command);

        // commit the changes
        await _transactionConnection.CommitAsync();

        return 1;
    }

    public async Task<int> CreateResponseAsync(ResponseTrueFalse response)
    {
        // start the transaction
        await _transactionConnection.StartTransactionAsync();

        // insert the base response record
        var baseCommand = GetCreateBaseResponseCommand(response);
        await _transactionConnection.ExecuteInTransactionAsync(baseCommand);


        // insert the true false response record
        MySqlCommand command = new(ResponseRepositoryCommands.UpsertTrueFalse);

        command.Parameters.AddWithValue("@id", response.Id);
        command.Parameters.AddWithValue("@answer_given", response.AnswerGiven);

        await _transactionConnection.ExecuteInTransactionAsync(command);

        // commit the changes
        await _transactionConnection.CommitAsync();

        return 1;
    }

    private static MySqlCommand GetCreateBaseResponseCommand(Response response)
    {
        MySqlCommand command = new(ResponseRepositoryCommands.InsertResponseBase);

        command.Parameters.AddWithValue("@id", response.Id);
        command.Parameters.AddWithValue("@question_id", response.QuestionId?.ToString());
        command.Parameters.AddWithValue("@player_id", response.PlayerId);
        command.Parameters.AddWithValue("@created_on", response.CreatedOn);

        return command;
    }

    #endregion

}



