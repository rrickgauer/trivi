namespace Trivi.Lib.Domain.Enums;

public enum ResultFilterCacheKey
{
    GameId,
}


public static class ResultFilterCacheKeyExtensions
{
    public static string GetKeyText(this ResultFilterCacheKey key)
    {
        string enumName = Enum.GetName(key) ?? $"{key}";
        return $"{nameof(ResultFilterCacheKey)}-{enumName}";
    }
}
