using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.TableViews;

public class ViewResponse : ITableView<ViewResponse, Response>
{
    [SqlColumn("response_id")]
    [CopyToProperty<Response>(nameof(Response.Id))]
    [CopyToProperty<ResponseShortAnswer>(nameof(ResponseShortAnswer.Id))]
    [JsonIgnore]
    public virtual Guid? Id { get; set; }

    [SqlColumn("question_id")]
    [CopyToProperty<Response>(nameof(Response.QuestionId))]
    [CopyToProperty<ResponseShortAnswer>(nameof(ResponseShortAnswer.QuestionId))]
    public virtual QuestionId? QuestionId { get; set; }

    [SqlColumn("player_id")]
    [CopyToProperty<Response>(nameof(Response.PlayerId))]
    [CopyToProperty<ResponseShortAnswer>(nameof(ResponseShortAnswer.PlayerId))]
    public virtual Guid? PlayerId { get; set; }

    [SqlColumn("response_created_on")]
    [CopyToProperty<Response>(nameof(Response.CreatedOn))]
    [CopyToProperty<ResponseShortAnswer>(nameof(ResponseShortAnswer.CreatedOn))]
    [JsonPropertyName("createdOn")]
    public virtual DateTime ResponseCreatedOn { get; set; } = DateTime.UtcNow;

    [SqlColumn("game_id")]
    public virtual string? GameId { get; set; }

    [SqlColumn("question_type_id")]
    public virtual QuestionType QuestionType { get; set; } = QuestionType.ShortAnswer;

    [SqlColumn("question_prompt")]
    public virtual string? QuestionPrompt { get; set; }

    [SqlColumn("question_points")]
    public virtual ushort QuestionPoints { get; set; } = 1;

    [SqlColumn("player_nickname")]
    public virtual string? PlayerNickname { get; set; }

    [SqlColumn("collection_id")]
    [JsonIgnore]
    public virtual Guid? CollectionId { get; set; }

    [SqlColumn("collection_user_id")]
    [JsonIgnore]
    public virtual Guid? CollectionUserId { get; set; }


    public string GameUrl => $"/games/{GameId}";

    public static explicit operator Response(ViewResponse other) => other.CastToModel();
}
