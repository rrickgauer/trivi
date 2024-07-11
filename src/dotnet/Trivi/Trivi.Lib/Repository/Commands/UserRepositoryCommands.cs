namespace Trivi.Lib.Repository.Commands;

public sealed class UserRepositoryCommands
{
    public const string SelectAll = @"
        SELECT
            u.*
        FROM
            View_Users u;";


    public const string SelectByEmailPassword = @"
        SELECT
            u.*
        FROM
            View_Users u
        WHERE
            u.user_email = @email
            AND u.user_password = @password
        LIMIT
            1;";

}
