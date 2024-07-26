namespace Trivi.Lib.Repository.Commands;

public class PlayerRepositoryCommands
{
    public const string SelectAllInGame = @"
        SELECT
            p.*
        FROM
            View_Players p
        WHERE
            p.player_game_id = @game_id;";


    public const string SelectById = @"
        SELECT
            p.*
        FROM
            View_Players p
        WHERE
            p.player_id = @player_id
        LIMIT
            1;";


    public const string SelectByGameIdAndNickname = @"
        SELECT
            p.*
        FROM
            View_Players p
        WHERE
            p.player_game_id = @game_id
            AND p.player_nickname = @nickname
        LIMIT
            1;";


    public const string Insert = @"
        INSERT INTO
            Players (id, game_id, nickname, created_on)
        VALUES
            (@id, @game_id, @nickname, @created_on);";

}
