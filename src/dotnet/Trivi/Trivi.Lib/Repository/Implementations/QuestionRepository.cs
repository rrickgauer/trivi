using MySql.Data.MySqlClient;
using System.Data;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Repository.Commands;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Repository.Other;

namespace Trivi.Lib.Repository.Implementations;

[AutoInject<IQuestionRepository>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class QuestionRepository(DatabaseConnection connection) : IQuestionRepository
{
    private readonly DatabaseConnection _connection = connection;

    #region - Select Mulitple -

    public async Task<DataTable> SelectCollectionQuestionsAsync(Guid collectionId)
    {
        MySqlCommand command = new(QuestionRepositoryCommands.SelectAllBaseInCollection);

        command.Parameters.AddWithValue("@collection_id", collectionId);

        return await _connection.FetchAllAsync(command);
    }

    #endregion

    #region - Select Single -

    public async Task<DataRow?> SelectShortAnswerAsync(QuestionId questionId)
    {
        MySqlCommand command = new(QuestionRepositoryCommands.SelectShortAnswer);

        command.Parameters.AddWithValue("@question_id", questionId.Id);

        return await _connection.FetchAsync(command);
    }

    public async Task<DataRow?> SelectMultipleChoiceAsync(QuestionId questionId)
    {
        MySqlCommand command = new(QuestionRepositoryCommands.SelectMultipleChoice);

        command.Parameters.AddWithValue("@question_id", questionId.Id);

        return await _connection.FetchAsync(command);
    }
    
    public async Task<DataRow?> SelectTrueFalseAsync(QuestionId questionId)
    {
        MySqlCommand command = new(QuestionRepositoryCommands.SelectTrueFalse);

        command.Parameters.AddWithValue("@question_id", questionId.Id);

        return await _connection.FetchAsync(command);
    }

    #endregion

    #region - Save -

    public async Task<int> UpsertQuestionAsync(Question question)
    {
        MySqlCommand command = new(QuestionRepositoryCommands.UpsertQuestionBase);

        command.Parameters.AddWithValue("@id", question.Id?.Id);
        command.Parameters.AddWithValue("@collection_id", question.CollectionId);
        command.Parameters.AddWithValue("@question_type_id", question.QuestionType);
        command.Parameters.AddWithValue("@prompt", question.Prompt);
        command.Parameters.AddWithValue("@created_on", question.CreatedOn);

        return await _connection.ModifyAsync(command);
    }
    
    
    public async Task<int> UpsertShortAnswerAsync(ShortAnswer question)
    {
        MySqlCommand command = new(QuestionRepositoryCommands.UpsertShortAnswer);

        command.Parameters.AddWithValue("@id", question.Id?.Id);
        command.Parameters.AddWithValue("@correct_answer", question.CorrectAnswer);

        return await _connection.ModifyAsync(command);
    }
    
    public async Task<int> UpsertTrueFalseAsync(TrueFalse question)
    {
        MySqlCommand command = new(QuestionRepositoryCommands.UpsertTrueFalse);

        command.Parameters.AddWithValue("@id", question.Id?.Id);
        command.Parameters.AddWithValue("@correct_answer", question.CorrectAnswer);

        return await _connection.ModifyAsync(command);
    }
    
    
    public async Task<int> UpsertMultipleChoiceAsync(MultipleChoice question)
    {
        MySqlCommand command = new(QuestionRepositoryCommands.UpsertMultipleChoice);

        command.Parameters.AddWithValue("@id", question.Id?.Id);

        return await _connection.ModifyAsync(command);
    }

    #endregion

}

