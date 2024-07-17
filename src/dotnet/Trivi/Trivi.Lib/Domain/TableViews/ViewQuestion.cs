using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.TableViews;

public class ViewQuestion 
{

    [SqlColumn("question_id")]
    [CopyToProperty<ShortAnswer>(nameof(ShortAnswer.Id))]
    [CopyToProperty<MultipleChoice>(nameof(MultipleChoice.Id))]
    [CopyToProperty<TrueFalse>(nameof(TrueFalse.Id))]
    public virtual QuestionId? Id { get; set; }

    [SqlColumn("question_collection_id")]
    [CopyToProperty<ShortAnswer>(nameof(ShortAnswer.CollectionId))]
    [CopyToProperty<MultipleChoice>(nameof(MultipleChoice.CollectionId))]
    [CopyToProperty<TrueFalse>(nameof(TrueFalse.CollectionId))]
    public virtual Guid? CollectionId { get; set; }

    [SqlColumn("question_prompt")]
    [CopyToProperty<ShortAnswer>(nameof(ShortAnswer.Prompt))]
    [CopyToProperty<MultipleChoice>(nameof(MultipleChoice.Prompt))]
    [CopyToProperty<TrueFalse>(nameof(TrueFalse.Prompt))]
    public virtual string? Prompt { get; set; }

    [SqlColumn("question_created_on")]
    [CopyToProperty<ShortAnswer>(nameof(ShortAnswer.CreatedOn))]
    [CopyToProperty<MultipleChoice>(nameof(MultipleChoice.CreatedOn))]
    [CopyToProperty<TrueFalse>(nameof(TrueFalse.CreatedOn))]
    public virtual DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    [SqlColumn("collection_user_id")]
    public virtual Guid? UserId { get; set; }

    [SqlColumn("question_question_type_id")]
    public virtual QuestionType QuestionType { get; set; } = QuestionType.MultipleChoice;
}



