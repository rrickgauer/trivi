using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.TableViews;

public class ViewGameQuestion : ViewQuestion, ITableView<ViewGameQuestion, GameQuestion>, IUriGui
{
    [SqlColumn("question_id")]
    [CopyToProperty<ShortAnswer>(nameof(ShortAnswer.Id))]
    [CopyToProperty<MultipleChoice>(nameof(MultipleChoice.Id))]
    [CopyToProperty<TrueFalse>(nameof(TrueFalse.Id))]
    [CopyToProperty<GameQuestion>(nameof(GameQuestion.QuestionId))]
    public override QuestionId? Id { get; set; }

    [SqlColumn("game_question_game_id")]
    [CopyToProperty<GameQuestion>(nameof(GameQuestion.GameId))]
    public string? GameId { get; set; }

    [SqlColumn("game_question_status_id")]
    [CopyToProperty<GameQuestion>(nameof(GameQuestion.QuestionStatus))]
    public GameQuestionStatus QuestionStatus { get; set; } = GameQuestionStatus.Pending;

    [SqlColumn("game_status_id")]
    public GameStatus GameStatus { get; set; } = GameStatus.Open;

    public string UriGui => $"/games/{GameId}/questions/{Id?.Id}";

    public static explicit operator GameQuestion(ViewGameQuestion other) => other.CastToModel();
}