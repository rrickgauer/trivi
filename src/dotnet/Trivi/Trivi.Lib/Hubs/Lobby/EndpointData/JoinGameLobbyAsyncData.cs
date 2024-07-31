namespace Trivi.Lib.Hubs.Lobby.EndpointData;

public class JoinGameLobbyAsyncData
{
    public required string GameId { get; set; }
}

public class PlayerJoinGameLobbyAsyncData : JoinGameLobbyAsyncData
{
    public required Guid PlayerId { get; set; }
}