namespace Trivi.Lib.Repository.Commands;

public class GameRepositoryCommands
{
    public const string SelectUserGames = @"
        SELECT
            g.*
        FROM
            View_Games g
        WHERE
            g.game_collection_user_id = @user_id;";

    public const string SelectGame = @"
        SELECT
            g.*
        FROM
            View_Games g
        WHERE
            g.game_id = @game_id
        LIMIT
            1;";



    public const string Insert = @"
        INSERT INTO Games 
        (
            id,
            collection_id,
            game_status_id,
            randomize_questions,
            question_time_limit,
            created_on,
            started_on
        )
        VALUES
        (
            @id,
            @collection_id,
            @game_status_id,
            @randomize_questions,
            @question_time_limit,
            @created_on,
            @started_on
        );";



    public const string UpdateStatus = @"
        UPDATE
            Games
        SET
            game_status_id = @status_id
        WHERE
            id = @game_id;";

}


