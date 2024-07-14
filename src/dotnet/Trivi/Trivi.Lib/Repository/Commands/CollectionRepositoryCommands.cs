namespace Trivi.Lib.Repository.Commands;

public sealed class CollectionRepositoryCommands
{
    public const string SelectAllUserCollections = @"

        SELECT
          c.*
        FROM
          View_Collections c
        WHERE
          c.collection_user_id = @user_id
        ORDER BY
          c.collection_created_on desc;";

    public const string SelectById = @"
        SELECT
          c.*
        FROM
          View_Collections c
        WHERE
          c.collection_id = @collection_id
        LIMIT
          1;";

    public const string Save = @"
        INSERT INTO
            Collections (id, name, user_id, created_on)
        VALUES
            (@id, @name, @user_id, @created_on) AS new_values 
        ON DUPLICATE KEY UPDATE
            name = new_values.name;";

    public const string Delete = @"
        DELETE FROM
            Collections
        WHERE
            id = @collection_id
        LIMIT
            1;";

}
