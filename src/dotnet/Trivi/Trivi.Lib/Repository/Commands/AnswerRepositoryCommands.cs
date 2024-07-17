namespace Trivi.Lib.Repository.Commands;

public class AnswerRepositoryCommands
{
    public const string SelectAll = @"
        SELECT
            a.*
        FROM
            View_Answers_MC a
        WHERE
            a.answer_question_id = @question_id;";

    public const string SelectById = @"
        SELECT
            a.*
        FROM
            View_Answers_MC a
        WHERE
            a.answer_id = @answer_id
        LIMIT
            1;";


    public const string Upsert = @"
        INSERT INTO
            Answers_MC (id, question_id, answer, is_correct, created_on)
        VALUES
            (@id, @question_id, @answer, @is_correct, @created_on) AS new_values 
        ON DUPLICATE KEY UPDATE
            answer = new_values.answer,
            is_correct = new_values.is_correct;";


    public const string DeleteQuestionAnswers = @"
        DELETE FROM
            Answers_MC
        WHERE
            question_id = question_id;";


    public const string DeleteAnswer = @"
        DELETE FROM
            Answers_MC
        WHERE
            id = @answer_id;";

}
