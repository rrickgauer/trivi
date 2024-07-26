using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Domain.Models;

public class Player : IUriApi
{
    public Guid? Id { get; set; }
    public string? GameId { get; set; }
    public string? Nickname { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public string UriApi => $"/api/players/{Id}";

    public static Player From(JoinGameForm joinGameForm)
    {
        Player result = new()
        {
            Id = GuidUtility.New(),
            CreatedOn = DateTime.UtcNow,
            GameId = joinGameForm.GameId,
            Nickname = joinGameForm.Nickname,
        };

        return result;
    }
}
