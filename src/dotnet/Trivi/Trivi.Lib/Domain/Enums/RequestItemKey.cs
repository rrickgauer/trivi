namespace Trivi.Lib.Domain.Enums;

public enum RequestItemKey
{
    Collection,
    Question,
    Answer,
    Game,
    Player,
    ResponseResult,
    GameId,
}


public static class RequestItemKeyExtensions
{
    public static string GetKeyText(this RequestItemKey key)
    {
        var keyName = Enum.GetName(key);
        return $"{nameof(RequestItemKey)}-{keyName}";
    }
}
