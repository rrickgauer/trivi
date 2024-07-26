using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Hubs.Lobby.EndpointData;

public class AdminLobbyUpdatedData
{
    public required List<ViewPlayer> Players { get; set; }
}


public class NavigateToData
{
    public required string Destination { get; set; }
}