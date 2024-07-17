namespace Trivi.Lib.Repository.Commands;

public class QuestionRepositoryCommands
{

    public const string SelectAllBaseInCollection = @"
        SELECT
            q.*
        FROM
            View_Questions q
        WHERE
            q.question_collection_id = @collection_id
        ORDER BY
            q.question_created_on DESC;";




    public const string SelectShortAnswer = @"
        SELECT
            q.*
        FROM
            View_Questions_SA q
        WHERE
            q.question_id = @question_id
        LIMIT
            1;";

    public const string SelectTrueFalse = @"
        SELECT
            q.*
        FROM
            View_Questions_TF q
        WHERE
            q.question_id = @question_id
        LIMIT
            1;";

    public const string SelectMultipleChoice = @"
        SELECT
            q.*
        FROM
            View_Questions_MC q
        WHERE
            q.question_id = @question_id
        LIMIT
            1;";





    public const string UpsertQuestionBase = @"
        INSERT INTO Questions 
        (
            id,
            collection_id,
            question_type_id,
            prompt,
            created_on
        )
        VALUES
        (
            @id,
            @collection_id,
            @question_type_id,
            @prompt,
            @created_on
        ) 
        AS new_values 
        ON DUPLICATE KEY UPDATE
            prompt = new_values.prompt;";


    public const string UpsertShortAnswer = @"
        INSERT INTO
            Questions_SA (id, correct_answer)
        VALUES
            (@id, @correct_answer) AS new_values 
        ON DUPLICATE KEY UPDATE
            correct_answer = new_values.correct_answer;";


    public const string UpsertTrueFalse = @"
        INSERT INTO
            Questions_TF (id, correct_answer)
        VALUES
            (@id, @correct_answer) AS new_values 
        ON DUPLICATE KEY UPDATE
            correct_answer = new_values.correct_answer;";


    public const string UpsertMultipleChoice = @"
        INSERT INTO
            Questions_MC (id)
        VALUES
            (@id) AS new_values ON DUPLICATE KEY
        UPDATE
            id = new_values.id;";



    public const string Delete = @"
        DELETE FROM
          Questions
        WHERE
          id = @id;";

}

