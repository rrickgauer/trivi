namespace Trivi.Lib.Repository.Commands;

public class ErrorMessageRepositoryCommands
{
    public const string SelectAll = @"
        SELECT
            e.id AS id,
            e.message AS message
        FROM
            Error_Messages e
        ORDER BY
            id ASC;";
}
