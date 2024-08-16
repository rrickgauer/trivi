using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Domain.TableViews;

public class ViewPlayer : ITableView<ViewPlayer, Player>, IUriApi
{
    [SqlColumn("player_id")]
    [CopyToProperty<Player>(nameof(Player.Id))]
    public Guid? Id { get; set; }

    [SqlColumn("player_game_id")]
    [CopyToProperty<Player>(nameof(Player.GameId))]
    public string? GameId { get; set; }

    [SqlColumn("player_nickname")]
    [CopyToProperty<Player>(nameof(Player.Nickname))]
    public string? Nickname { get; set; }

    [SqlColumn("player_created_on")]
    [CopyToProperty<Player>(nameof(Player.CreatedOn))]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    [SqlColumn("game_collection_id")]
    [JsonIgnore]
    public Guid? CollectionId { get; set; }

    [SqlColumn("game_status_id")]
    [JsonIgnore]
    public GameStatus GameStatus { get; set; } = GameStatus.Open;

    [SqlColumn("game_collection_user_id")]
    [JsonIgnore]
    public Guid? CollectionUserId { get; set; }

    public string UriApi => ((Player)this).UriApi;

    public static explicit operator Player(ViewPlayer other) => other.CastToModel<ViewPlayer, Player>();
}

