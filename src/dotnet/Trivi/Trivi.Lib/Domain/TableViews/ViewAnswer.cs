using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Contracts;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.TableViews;

public class ViewAnswer : INanoIdPrefix, ITableView<ViewAnswer, Answer>
{
    public static string NanoIdPrefix => Answer.NanoIdPrefix;

    [SqlColumn("answer_id")]
    [CopyToProperty<Answer>(nameof(Answer.Id))]
    public string? Id { get; set; }

    [SqlColumn("answer_question_id")]
    [CopyToProperty<Answer>(nameof(Answer.QuestionId))]
    public QuestionId? QuestionId { get; set; }

    [SqlColumn("answer_answer")]
    [CopyToProperty<Answer>(nameof(Answer.AnswerText))]
    [JsonPropertyName("answer")]
    public string? AnswerText { get; set; }

    [SqlColumn("answer_is_correct")]
    [CopyToProperty<Answer>(nameof(Answer.IsCorrect))]
    public bool IsCorrect { get; set; } = false;


    [SqlColumn("answer_created_on")]
    [CopyToProperty<Answer>(nameof(Answer.CreatedOn))]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;


    [JsonIgnore]
    [SqlColumn("answer_question_collection_id")]
    public Guid? CollectionId { get; set; }

    [JsonIgnore]
    [SqlColumn("answer_question_collection_user_id")]
    public Guid? UserId { get; set; }


    public static explicit operator Answer(ViewAnswer other) => other.CastToModel();
}
