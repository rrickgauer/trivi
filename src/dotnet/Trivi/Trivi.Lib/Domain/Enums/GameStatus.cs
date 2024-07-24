using System.Reflection;
using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Attributes;

namespace Trivi.Lib.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GameStatus : ushort
{
    [CanJoinGame]
    Open = 1,

    [CanJoinGame]
    Running = 2,
    
    Completed = 3,
    
    Disconnected = 4,
}


public static class GameStatusExtensions
{
    public static bool CanJoinGame(this GameStatus gameStatus)
    {
        var name = Enum.GetName(gameStatus)!;
        var field = typeof(GameStatus).GetField(name);

        var attr = field?.GetCustomAttribute<CanJoinGameAttribute>();

        if (attr == null)
        {
            return false;
        }

        return true;
    }
}
