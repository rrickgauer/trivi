using MySql.Data.MySqlClient;
using System.Data;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Repository.Commands;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Repository.Other;

namespace Trivi.Lib.Repository.Implementations;

[AutoInject<IAnswerRepository>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class AnswerRepository(DatabaseConnection connection) : IAnswerRepository
{
    private readonly DatabaseConnection _connection = connection;

    public async Task<DataTable> SelectAnswersAsync(QuestionId questionId)
    {
        MySqlCommand command = new(AnswerRepositoryCommands.SelectAll);

        command.Parameters.AddWithValue("@question_id", questionId.Id);

        return await _connection.FetchAllAsync(command);
    }

    public async Task<DataRow?> SelectAnswerAsync(string answerId)
    {
        MySqlCommand command = new(AnswerRepositoryCommands.SelectById);

        command.Parameters.AddWithValue("@answer_id", answerId);

        return await _connection.FetchAsync(command);
    }

    public async Task<int> UpsertAnswerAsync(Answer answer)
    {
        MySqlCommand command = GetUpsertCommand(answer);

        return await _connection.ModifyAsync(command);
    }


    public async Task<bool> ReplaceQuestionAnswersAsync(QuestionId questionId, IEnumerable<Answer> answers)
    {
        List<MySqlCommand> commands = new();

        var deleteCommand = GetDeleteQuestionAnswersCommand(questionId);
        commands.Add(deleteCommand);

        foreach (Answer answer in answers)
        {
            var command = GetUpsertCommand(answer);
            commands.Add(command);
        }

        return await _connection.ModifyWithTransactionAsync(commands);
    }



    private static MySqlCommand GetDeleteQuestionAnswersCommand(QuestionId questionId)
    {
        MySqlCommand command = new(AnswerRepositoryCommands.DeleteQuestionAnswers);

        command.Parameters.AddWithValue("@question_id", questionId.Id);

        return command;
    }


    private static MySqlCommand GetUpsertCommand(Answer answer)
    {
        MySqlCommand command = new(AnswerRepositoryCommands.Upsert);

        command.Parameters.AddWithValue("@id", answer.Id);
        command.Parameters.AddWithValue("@question_id", answer.QuestionId);
        command.Parameters.AddWithValue("@answer", answer.AnswerText);
        command.Parameters.AddWithValue("@is_correct", answer.IsCorrect);
        command.Parameters.AddWithValue("@created_on", answer.CreatedOn);

        return command;
    }


    public async Task<int> DeleteAnswerAsync(string answerId)
    {
        MySqlCommand command = new(AnswerRepositoryCommands.DeleteAnswer);

        command.Parameters.AddWithValue("@answer_id", answerId);

        return await _connection.ModifyAsync(command);
    }


}
