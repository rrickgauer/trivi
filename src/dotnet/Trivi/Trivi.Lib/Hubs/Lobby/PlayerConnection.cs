namespace Trivi.Lib.Hubs.Lobby;

public class PlayerConnection
{
    public required string ConnectionId { get; set; }
    public required Guid PlayerId { get; set; }
    public required string GameId { get; set; }
}
