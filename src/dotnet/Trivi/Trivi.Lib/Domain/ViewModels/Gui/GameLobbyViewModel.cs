using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Domain.ViewModels.Gui;

public class GameLobbyViewModel
{
    public required ViewGame Game { get; set; }
    public required ViewPlayer Player { get; set; }
}
