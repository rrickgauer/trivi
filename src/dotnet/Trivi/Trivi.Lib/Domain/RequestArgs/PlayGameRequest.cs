using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Trivi.Lib.Domain.RequestArgs;

public class PlayGameRequest
{
    [BindRequired]
    [FromQuery(Name = "player")]
    [JsonPropertyName("player")]
    public required Guid PlayerId { get; set; }

    [BindRequired]
    [FromRoute]
    public required string GameId { get; set; }


    public object GetRedirectRouteValues()
    {
        return new
        {
            player = PlayerId,
            GameId = GameId,
        };
    }
}
