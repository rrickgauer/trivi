namespace Trivi.Lib.Repository.Commands;

public class ResponseRepositoryCommands
{
    public const string SelectShortAnswers = @"
        SELECT
            r.*
        FROM
            View_Responses_SA r
        WHERE
            r.question_id = @question_id
            AND r.game_id = @game_id;";



    public const string SelectShortAnswerById = @"
        SELECT
            r.*
        FROM
            View_Responses_SA r
        WHERE
            r.response_id = @response_id
        LIMIT 1;";

    public const string SelectShortAnswerByQuestionPlayer = @"
        SELECT
            r.*
        FROM
            View_Responses_SA r
        WHERE
            r.question_id = @question_id
            AND r.player_id = @player_id
        LIMIT
            1;";


    public const string InsertResponseBase = @"
        INSERT INTO
            Responses (id, question_id, player_id, created_on)
        VALUES
            (@id, @question_id, @player_id, @created_on);";


    public const string UpsertResponseShortAnswer = @"
        INSERT INTO
            Responses_SA (id, answer_given)
        VALUES
            (@id, @answer_given) AS new_values 
        ON DUPLICATE KEY UPDATE
            answer_given = new_values.answer_given;";

}



