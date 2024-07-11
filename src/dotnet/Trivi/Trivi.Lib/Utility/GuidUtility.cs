using UUIDNext;

namespace Trivi.Lib.Utility;

public class GuidUtility
{
    public static Guid New()
    {
        return Uuid.NewDatabaseFriendly(Database.Other);
    }
}
