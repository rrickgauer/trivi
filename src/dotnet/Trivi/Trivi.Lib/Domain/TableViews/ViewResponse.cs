using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.TableViews;

public class ViewResponse : ITableView<ViewResponse, Response>
{

    [JsonIgnore]
    [SqlColumn("response_id")]
    [CopyToProperty<Response>(nameof(Response.Id))]
    [CopyToProperty<ResponseShortAnswer>(nameof(ResponseShortAnswer.Id))]
    [CopyToProperty<ResponseTrueFalse>(nameof(ResponseTrueFalse.Id))]
    [CopyToProperty<ResponseMultipleChoice>(nameof(ResponseMultipleChoice.Id))]
    public virtual Guid? Id { get; set; }


    [SqlColumn("response_is_correct")]
    [CopyToProperty<Response>(nameof(Response.IsCorrect))]
    [CopyToProperty<ResponseShortAnswer>(nameof(ResponseShortAnswer.IsCorrect))]
    [CopyToProperty<ResponseTrueFalse>(nameof(ResponseTrueFalse.IsCorrect))]
    [CopyToProperty<ResponseMultipleChoice>(nameof(ResponseMultipleChoice.IsCorrect))]
    public virtual bool IsCorrect { get; set; } = false;


    [SqlColumn("response_points_awarded")]
    [CopyToProperty<Response>(nameof(Response.PointsAwarded))]
    [CopyToProperty<ResponseShortAnswer>(nameof(ResponseShortAnswer.PointsAwarded))]
    [CopyToProperty<ResponseTrueFalse>(nameof(ResponseTrueFalse.PointsAwarded))]
    [CopyToProperty<ResponseMultipleChoice>(nameof(ResponseMultipleChoice.PointsAwarded))]
    public virtual ushort PointsAwarded { get; set; } = 0;


    [SqlColumn("question_id")]
    [CopyToProperty<Response>(nameof(Response.QuestionId))]
    [CopyToProperty<ResponseShortAnswer>(nameof(ResponseShortAnswer.QuestionId))]
    [CopyToProperty<ResponseTrueFalse>(nameof(ResponseTrueFalse.QuestionId))]
    [CopyToProperty<ResponseMultipleChoice>(nameof(ResponseMultipleChoice.QuestionId))]
    public virtual QuestionId? QuestionId { get; set; }

    [SqlColumn("player_id")]
    [CopyToProperty<Response>(nameof(Response.PlayerId))]
    [CopyToProperty<ResponseShortAnswer>(nameof(ResponseShortAnswer.PlayerId))]
    [CopyToProperty<ResponseTrueFalse>(nameof(ResponseTrueFalse.PlayerId))]
    [CopyToProperty<ResponseMultipleChoice>(nameof(ResponseMultipleChoice.PlayerId))]
    public virtual Guid? PlayerId { get; set; }

    [SqlColumn("response_created_on")]
    [CopyToProperty<Response>(nameof(Response.CreatedOn))]
    [CopyToProperty<ResponseShortAnswer>(nameof(ResponseShortAnswer.CreatedOn))]
    [CopyToProperty<ResponseTrueFalse>(nameof(ResponseTrueFalse.CreatedOn))]
    [CopyToProperty<ResponseMultipleChoice>(nameof(ResponseMultipleChoice.CreatedOn))]
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

    [JsonIgnore]
    [SqlColumn("collection_id")]
    public virtual Guid? CollectionId { get; set; }

    [JsonIgnore]
    [SqlColumn("collection_user_id")]
    public virtual Guid? CollectionUserId { get; set; }


    public string GameUrl => $"/games/{GameId}";

    public static explicit operator Response(ViewResponse other) => other.CastToModel();
}
