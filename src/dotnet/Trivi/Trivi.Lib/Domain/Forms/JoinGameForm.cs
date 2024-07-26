using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Trivi.Lib.Domain.Forms;

public class JoinGameForm
{
    [BindRequired]
    public required string GameId { get; set; }

    [BindRequired]
    public required string Nickname { get; set; }
}
