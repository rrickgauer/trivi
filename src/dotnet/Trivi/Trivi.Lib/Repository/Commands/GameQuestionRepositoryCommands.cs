namespace Trivi.Lib.Repository.Commands;

public class GameQuestionRepositoryCommands
{
    public const string CopyGameQuestionsProcName = "Copy_Game_Questions";

    public const string SelectByGameIdQuestionId = @"
        SELECT
            v.*
        FROM
            View_Game_Questions v
        WHERE
            v.question_id = @question_id
            AND v.game_question_game_id = @game_id
        LIMIT
            1;";


    public const string UpdateStatus = @"
        UPDATE
            Game_Questions
        SET
            game_question_status_id = @game_question_status_id
        WHERE
            question_id = @question_id
            AND game_id = @game_id;";


}
