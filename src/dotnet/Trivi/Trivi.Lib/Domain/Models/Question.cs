using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.Models;

public abstract class Question
{
    public virtual QuestionId? Id { get; set; }
    public virtual Guid? CollectionId { get; set; }
    public virtual string? Prompt { get; set; }
    public virtual DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public abstract QuestionType QuestionType { get; }
}
