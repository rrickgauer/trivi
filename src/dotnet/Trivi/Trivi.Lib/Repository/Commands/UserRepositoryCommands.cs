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

    public const string SelectByEmail = @"
        SELECT
            u.*
        FROM
            View_Users u
        WHERE
            u.user_email = @email
        LIMIT
            1;";


    public const string Insert = @"
        INSERT INTO
            Users (id, email, password, created_on)
        VALUES
            (@id, @email, @password, @created_on);";

}
